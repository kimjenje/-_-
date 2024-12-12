using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HudleController : MonoBehaviour
{
    [SerializeField] private GameObject[] hudleprefabs;
    [SerializeField] private float spawnRateMin;
    [SerializeField] private float spawnRateMax;
    [SerializeField] private float speedIncrementInterval = 10f; // 속도 증가 간격
    [SerializeField] private float speedIncrementAmount = 1f;    // 속도 증가량
    [SerializeField] private float maxSpeed = 15f;               // 최대 속도

    private float nextSpawnTime;
    private bool canSpawn = true;
    private ObstacleManager obstacleManager;
    private float currentSpeed = 5f; // 초기 속도

    private List<ObstacleMovement> spawnedObstacles = new List<ObstacleMovement>(); // 생성된 장애물 리스트

    private void Start()
    {
        // 첫 번째 장애물 생성 시간 설정
        SetNextSpawnTime();

        // ObstacleManager 찾아서 할당
        obstacleManager = FindObjectOfType<ObstacleManager>();
        if (obstacleManager == null)
        {
            Debug.LogError("No ObstacleManager found in the scene.");
        }

        // 속도를 증가시키는 코루틴 시작
        StartCoroutine(IncreaseSpeedOverTime());
    }

    private void Update()
    {
        // 현재 시간이 다음 장애물 생성 시간을 넘었으면 장애물 생성
        if (Time.time >= nextSpawnTime && canSpawn)
        {
            SpawnObstacle();
            SetNextSpawnTime(); // 다음 장애물 생성 시간 설정
        }
    }

    private void SetNextSpawnTime()
    {
        // 랜덤한 생성 간격으로 다음 장애물 생성 시간 설정
        nextSpawnTime = Time.time + UnityEngine.Random.Range(spawnRateMin, spawnRateMax);
    }

    private void SpawnObstacle()
    {
        // 랜덤한 장애물 선택
        int rand = UnityEngine.Random.Range(0, hudleprefabs.Length);
        GameObject obstacleToSpawn = hudleprefabs[rand];

        // 현재 오브젝트의 위치에 장애물 생성
        GameObject newObstacle = Instantiate(obstacleToSpawn, transform.position, Quaternion.identity);

        // 장애물 리스트에 추가
        if (obstacleManager != null)
        {
            obstacleManager.AddObstacle(newObstacle);
        }

        // 장애물이 제거될 때 리스트에서 제거하기 위해 Obstacle 컴포넌트를 추가하고 설정
        Obstacle obstacleComponent = newObstacle.AddComponent<Obstacle>();
        obstacleComponent.SetObstacleManager(obstacleManager);

        // ObstacleMovement 컴포넌트를 추가하고 속도를 현재 속도로 설정
        ObstacleMovement obstacleMovement = newObstacle.AddComponent<ObstacleMovement>();
        obstacleMovement.speed = currentSpeed;

        // 생성된 장애물 리스트에 추가
        spawnedObstacles.Add(obstacleMovement);
    }

    private IEnumerator IncreaseSpeedOverTime()
    {
        while (canSpawn)
        {
            yield return new WaitForSeconds(speedIncrementInterval);

            // 속도가 maxSpeed를 초과하지 않도록 설정
            if (currentSpeed < maxSpeed)
            {
                currentSpeed += speedIncrementAmount;

                // currentSpeed가 maxSpeed를 초과하지 않도록 한 번 더 제한
                if (currentSpeed > maxSpeed)
                {
                    currentSpeed = maxSpeed;
                }

                // 이미 생성된 장애물들의 속도를 업데이트
                foreach (ObstacleMovement obstacle in spawnedObstacles)
                {
                    if (obstacle != null)
                    {
                        obstacle.speed = currentSpeed;
                    }
                }
            }
        }
    }

    // 게임 종료 시 호출하여 장애물 생성 중지
    public void StopSpawning()
    {
        canSpawn = false;
        StopCoroutine(IncreaseSpeedOverTime());
    }
}
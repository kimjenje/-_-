using UnityEngine;

public class Bossstart : MonoBehaviour
{

    private Animator animator;
    public Transform map; // map 오브젝트의 Transform을 참조할 변수
    public GameObject boss; // boss 오브젝트를 참조할 변수
    public GameObject bossSkillController; // Bossskillcontroller 오브젝트를 참조할 변수
    
    public AudioSource Music;

    // 서서히 이동할 시작 위치 및 목표 위치
    private Vector3 startPosition = new Vector3(12, -5, 0);
    private Vector3 targetPosition = new Vector3(4, -5, 0);
    public int Bossstartpostion= -422;

    // 이동 속도 (0과 1 사이의 값으로 설정)
    public float moveSpeed = 0.02f;

    // 보스의 현재 위치를 저장할 변수
    private Vector3 currentBossPosition;

    // 보스가 이동 중인지 여부를 나타내는 변수
    private bool isBossMoving = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        animator.applyRootMotion = true;
        // 초기화 시 보스의 시작 위치를 설정
        boss.transform.position = startPosition;
        
    }


    void hammer()
    {
        animator.applyRootMotion = false;
    }


    // Update는 프레임마다 한 번씩 실행됩니다.
    void Update()
    {
        // map 오브젝트의 x 좌표를 확인합니다.
        if (map.position.x <Bossstartpostion && !isBossMoving)
        {   
            animator.SetBool("HAMMER", true);
            

            // boss 오브젝트를 활성화합니다.
            boss.SetActive(true);
            Music.enabled = false;

            // 보스 이동을 시작하도록 설정
            isBossMoving = true;
            
        }


        // 보스가 이동 중인 경우 목표 위치로 부드럽게 이동
        if (isBossMoving)
        {
            currentBossPosition = boss.transform.position;
            currentBossPosition = Vector3.Lerp(currentBossPosition, targetPosition, moveSpeed);

            // 보스의 위치 업데이트
            boss.transform.position = currentBossPosition;

            // 목표 위치에 가까이 도달했을 때 이동을 멈춤
            if (Vector3.Distance(currentBossPosition, targetPosition) < 0.01f)
            {
                // 이동 완료 시 보스 이동 중 플래그를 false로 설정
                isBossMoving = false;

                // Bossskillcontroller 오브젝트를 활성화
                bossSkillController.SetActive(true);
            }
        }
    }
}

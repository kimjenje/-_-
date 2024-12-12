using UnityEngine;

public class healItm : MonoBehaviour
{
    public int healthAmount = 1; // 증가할 체력 양
    public GameManager gameManager; // 게임 매니저 참조
    public Life lifeController; // 이미지를 교체하는 Life 컨트롤러 참조
    public AudioSource healSound;
    private void Start()
    {
        // 게임 매니저 참조 설정
        if (gameManager == null)
        {
            gameManager = FindObjectOfType<GameManager>();
            if (gameManager == null)
            {
                Debug.LogError("No GameManager found in the scene.");
            }
        }

        // Life 컨트롤러 참조 설정
        if (lifeController == null)
        {
            lifeController = FindObjectOfType<Life>();
            if (lifeController == null)
            {
                Debug.LogError("No Life controller found in the scene.");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // 플레이어와 충돌했을 때
        {
            if (gameManager._life < 5)
            {

                if (healSound != null)
                {
                    healSound.Play();
                }
                gameManager._life++;
                lifeController.RecoverLife(); // Life 컨트롤러의 이미지 교체 함수 호출
            }
            else
            {
                if (healSound != null)
                {
                    healSound.Play();
                }
                GameManager.Instance.AddScore(5000);
                Debug.Log("추가점수 획득!");
            }
            
        }
        Destroy(gameObject);
    }

}

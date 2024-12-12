using UnityEngine;
using System.Collections;

public class speeditem : MonoBehaviour
{
    //private bool speedChanged;
    private SpriteRenderer _spriteRenderer;
    public ObstacleMovement[] obstacleMovement;
    public BackgroundScroll[] backgroundScrolls;
    public AudioSource speedSound;
    private void Start()
    {
        //speedChanged = false;
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (speedSound != null)
            {
                speedSound.Play();
            }
            PlayerController playerController = other.gameObject.GetComponent<PlayerController>();
            StartCoroutine(playerController.PlayerBegine());

            foreach (ObstacleMovement obstacle in obstacleMovement)
            {
                obstacle.speed = 10; // 장애물의 speed 변경
            }
            _spriteRenderer.color = new Color(1, 1, 1, 0f);
            //speedChanged = true;

            // 배경화면 속도 변경
            foreach (BackgroundScroll backgroundScroll in backgroundScrolls)
            {
                backgroundScroll.SetSpeed(backgroundScroll.speed * 1.5f); // 배경 속도를 1.7배로 설정
            }

            // 애니메이션 오브젝트의 스피드 변경
            Animator[] animators = FindObjectsOfType<Animator>();
            foreach (Animator animator in animators)
            {
                animator.speed = 1.3f; // 애니메이션 속도를 1.3배로 설정
            }

            StartCoroutine(RevertSpeedCoroutine());
        }
    }

    private IEnumerator RevertSpeedCoroutine()
    {
        yield return new WaitForSeconds(4.5f);

        // 장애물 속도 원래 값으로 되돌림
        foreach (ObstacleMovement obstacle in obstacleMovement)
        {
            obstacle.speed = 5.5f; // 장애물의 speed 원래 값으로 변경
        }

        // 배경화면 속도 원래 값으로 되돌림
        foreach (BackgroundScroll backgroundScroll in backgroundScrolls)
        {
            backgroundScroll.SetSpeed(backgroundScroll.speed / 2.0f); // 배경 속도를 원래 값으로 변경
        }

        // 애니메이션 오브젝트의 스피드 원래 값으로 되돌림
        Animator[] animators = FindObjectsOfType<Animator>();
        foreach (Animator animator in animators)
        {
            animator.speed = 1.0f; // 애니메이션 속도 원래 값으로 설정
        }

        //speedChanged = false;
    }
}

using System.Collections;
using UnityEngine;

public class PetController : MonoBehaviour
{
    private Rigidbody2D rigid2D;
    private float jumpForce = 15.0f; //점프 높이
    private float jumpForce2 = 12.5f;//2단 점프 높이
    private int jumpCount = 0;// 1단,2단 표시

    private bool isSliding = false;
    //private bool isAttacking = false; // 추가: 공격 중인지 여부를 나타내는 변수
    public bool isBegine = false;

    public GameManager GameManager;
    private Animator animator;

    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        // Animator 컴포넌트를 가져옵니다.
        animator = GetComponent<Animator>();
    }
    private void Awake()
    {
        // Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // 슬라이드 입력을 감지하고 처리
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isSliding = true;  // 슬라이드 상태를 true로 설정
            animator.SetInteger("PetJump", -1); // 점프 파라미터를 -1로 설정
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isSliding = false;  // 슬라이드 상태를 false로 설정
            animator.SetInteger("PetJump", 0); // 점프 파라미터를 0으로 설정
        }
        else
        {
            // 일반 업데이트 로직
            if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
            {
                if (jumpCount == 0)
                {
                    rigid2D.velocity = new Vector2(0, jumpForce);
                    animator.SetInteger("PetJump", 1); // 점프 파라미터를 1로 설정
                }
                else if (jumpCount == 1)
                {
                    rigid2D.velocity = new Vector2(0, jumpForce2);
                    animator.SetInteger("PetJump", 2); // 점프 파라미터를 2로 설정
                }
                jumpCount++;
            }
        }

        if(GameManager._life <= 0)
        {
            animator.SetBool("petdeath", true);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            animator.SetInteger("PetJump", 0); // 점프 파라미터를 0으로 초기화
            if (isSliding == true)
            {
                animator.SetInteger("PetJump", -1); // 점프 파라미터를 -1로 설정
            }
        }
    }
}

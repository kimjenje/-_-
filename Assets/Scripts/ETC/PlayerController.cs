using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigid2D;
    private float jumpForce = 15.0f;
    private float jumpForce2 = 12.5f;
    private int jumpCount = 0;
    private bool isSliding = false;
    public bool attack = false;
    public bool isBegine = false;
    public bool jumpstop = false;
    private Animator animator;
    public GameManager GameManager;
    public GameObject menugameover;
    public Text atkCount;

    public AudioSource Jump_SFX;
    public AudioSource BossBGM;
    public AudioSource GameOverSound;

    private bool isAttacking = false; // 공격 중인지 여부를 나타내는 변수
    public bool isAttackInProgress = false; // 공격 진행 중인지를 나타내는 플래그
    Color[] _color;
    private SpriteRenderer[] spriteRenderers;

    private bool ItemOn = false;

    public int Damage;
    public int AttackUp;
    public int AttackCount;

    void Start()
    {
        this.rigid2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
    }

    private void Awake()
    {
        _color = new Color[2];
        _color[0] = new Color(1, 1, 1, 0.5f);
        _color[1] = new Color(1, 1, 1, 1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            isSliding = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isSliding = false;
            animator.SetBool("SlideBool", false);
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && jumpCount < 2)
            {
                if (!jumpstop)
                {
                    if (jumpCount == 0)
                    {
                        rigid2D.velocity = new Vector2(0, jumpForce);
                        animator.SetInteger("Jump", 1);
                        if (Jump_SFX != null)
                        {
                            Jump_SFX.Play();
                        }
                    }
                    else if (jumpCount == 1)
                    {
                        rigid2D.velocity = new Vector2(0, jumpForce2);
                        animator.SetInteger("Jump", 2);
                        if (Jump_SFX != null)
                        {
                            Jump_SFX.Play();
                        }
                    }
                    jumpCount++;
                }
            }

            if (Input.GetKeyDown(KeyCode.C) && !isAttackInProgress)
            {
                if (AttackCount > 0)
                {
                    StartCoroutine(PerformAttack());
                }
            }

            if (transform.position.x >= 2 && !isAttacking)
            {
                isAttacking = true;
                animator.SetBool("AtkHammer", true);
            }
            else if (transform.position.x < 2 && isAttacking)
            {
                isAttacking = false;
                animator.SetBool("AtkHammer", false);
            }
        }
        if (GameManager.Instance._life <= 0)
        {
            animator.SetBool("death", true);
        }
    }

    private IEnumerator PerformAttack()
    {
        isAttackInProgress = true;
        animator.SetBool("AtkHammer", true);

        AttackCount--;
        GameManager.Instance.AddAttackCount(-1);

        yield return new WaitForSeconds(1.5f); // 공격 애니메이션의 지속 시간 (예: 0.5초)

        animator.SetBool("AtkHammer", false);        


        yield return new WaitForSeconds(1f); // 공격 스킬 돌아올떄 까지 쿨 (예: 2초)

        isAttackInProgress = false;
    }

    public void DeathMoment()
    {
        Time.timeScale = 0;
        menugameover.SetActive(true);

        BossBGM.enabled = false;
        if (GameOverSound != null)
        {
            GameOverSound.Play();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0;
            animator.SetInteger("Jump", 0);
            if (isSliding == true)
            {
                animator.SetBool("SlideBool", true);
            }
        }
    }

    public IEnumerator SpeedBegine()
    {
        if (!isBegine) // 이미 무적 상태가 아니면 실행
        {
            // 스피드 아이템 효과를 처리
            yield return new WaitForSeconds(0f); // 무적 지속 시간
            StartCoroutine(PlayerBegine());
        }
    }

    public IEnumerator AttackBegine()
    {
        if (!isBegine) // 이미 무적 상태가 아니면 실행
        {
            isBegine = true;
            // 공격 무적 상태 처리
            yield return new WaitForSeconds(2f);
            isBegine = false;
        }
    }

    public IEnumerator PlayerBegine()
    {
        if (!isBegine) // 이미 무적 상태가 아니면 실행
        {
            isBegine = true;
            // 플레이어 무적 처리
            for (int i = 0; i < 20; i++)
            {
                foreach (var renderer in spriteRenderers)
                {
                    renderer.color = _color[0]; // 반투명
                }
                yield return new WaitForSeconds(0.1f);
                foreach (var renderer in spriteRenderers)
                {
                    renderer.color = _color[1]; // 원래 색상
                }
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(0.3f);
            isBegine = false; // 무적 상태 종료
        }
    }


    public IEnumerator jumpsstop()
    {
        jumpstop = true;
        yield return new WaitForSeconds(4.5f);
        jumpstop = false;
    }

    public IEnumerator hammerjumpstop()
    {
        jumpstop = true;
        yield return new WaitForSeconds(1.7f);
        jumpstop = false;
    }

    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.onAtkCountChange += OnAtkCountChange;
        }
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.onAtkCountChange -= OnAtkCountChange;
        }
    }

    IEnumerator ActivateSpeedItem()
    {
        yield return new WaitForSeconds(4.5f);
        animator.SetBool("SpeedOn", false);
        ItemOn = false;
    }

    void OnAtkCountChange(int num)
    {
        atkCount.text = num.ToString();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Item":
                ItemController item = collision.GetComponent<ItemController>();
                if (item)
                {
                    switch (item._itemtype)
                    {
                        case ItemType.Coin:
                            Destroy(collision.gameObject);
                            GameManager.Instance.AddScore(100);
                            break;
                        case ItemType.SpeedItem:
                            ItemOn = true;
                            if (ItemOn == true)
                            {
                                animator.SetBool("SpeedOn", true);
                                StartCoroutine(ActivateSpeedItem());
                                GameManager.Instance.AddScore(500);
                                ItemOn = false;
                            }
                            AudioSource SpeedItemSound = GetComponent<AudioSource>();
                            if (SpeedItemSound != null)
                            {
                                SpeedItemSound.Play();
                            }
                            break;
                        case ItemType.HealthItem:
                            // HealthItem 처리
                            break;
                        case ItemType.AttackItem:
                            AttackCount++;
                            GameManager.Instance.AddAttackCount(1);
                            Destroy(collision.gameObject);
                            break;
                        case ItemType.AttackUp:
                            Destroy(collision.gameObject);
                            GameManager.Instance.AddAttackup(1);
                            AudioSource CoinSound = GetComponent<AudioSource>();
                            if (CoinSound != null)
                            {
                                CoinSound.Play();
                            }
                            GameManager.Instance.AddScore(2000);
                            break;
                    }
                }
                break;
        }
    }
}

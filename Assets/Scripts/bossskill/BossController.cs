using UnityEngine;
using System.Collections;

public class BossController : MonoBehaviour
{
    public AnimationClip[] animations; // 애니메이션 클립 배열

    float initialDelay = 7f; // 보스가 나오기까지의 초기 딜레이
    float bossStartTime; // 보스 시작 시간
    float cooldown = 8f; // 스킬 쿨타임
    float nextSkillTime = 0f; // 다음 스킬 사용 시간
    bool bossActivated = false; // 보스가 활성화되었는지 여부
    public Transform attackSpawn;
    public bool isBossAtking;
    public GameObject spawnZone;
    public GameObject attackItem;
    public GameObject gameClear;
    public AudioSource gameClearSound;
    public AudioSource BossBGM;
    private Animator animator;
    public BossHealth BossHealth;

    void Start()
    {
        animator = GetComponent<Animator>(); // 이 스크립트가 붙은 객체의 애니메이터 컴포넌트를 가져옴
        
        // 보스 시작 시간 설정
        bossStartTime = Time.time + initialDelay;
        if (BossBGM != null)
        {
            BossBGM.Play();
        }
    }

    void Update()
    {
        if (!bossActivated && Time.time >= bossStartTime)
        {
            ActivateBoss(); // 보스 시작
        }

        if (bossActivated && Time.time >= nextSkillTime) // 다음 스킬 사용 시간이 지났는지 확인
        {
            PlayRandomAnimation();
        }
        if (BossHealth != null)
        {
            if (BossHealth.currentHealth <= 0)
            {
                StartCoroutine(BossDeathSequence());

            }
        }
        
    }


    
    public IEnumerator Bossattack()
    {
        isBossAtking = true;
        yield return new WaitForSeconds(4.8f);
        isBossAtking = false;
        
    }

    

        void ActivateBoss()
    {
        bossActivated = true;
        Debug.Log("Boss activated!");
        // 보스가 활성화되었을 때 추가로 실행할 코드가 있다면 이 곳에 추가하세요.
        StartCoroutine(SpawnAttackItemRepeatedly(5f, 7f));
        spawnZone.SetActive(true);
    }

    void PlayRandomAnimation()
    {
        nextSkillTime = Time.time + cooldown; // 쿨타임 후 다음 스킬 사용 가능
        int randomValue = Random.Range(1, animations.Length + 1);

        switch (randomValue)
        {
            case 1:
                animator.SetInteger("Attack_Num", 1);
                break;
            case 2:
                animator.SetInteger("Attack_Num", 2);
                break;
            case 3:
                animator.SetInteger("Attack_Num", 3);
                break;
            case 4:
                animator.SetInteger("Attack_Num", 4);
                break;
            default:
                Debug.LogError("Unexpected random value: " + randomValue);
                break;
        }
        
        // 2초 후에 Attack_Num을 0으로 되돌리는 코루틴 시작
        StartCoroutine(ResetAttackNumAfterDelay(2f));
    }

    IEnumerator SpawnAttackItemRepeatedly(float minDelay, float maxDelay)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            Instantiate(attackItem, attackSpawn.position, attackSpawn.rotation);
        }
        
    }
    
    public void OnBossDeathAnimationEnd()
    {
        // 애니메이션이 끝난 후 실행할 로직
        Time.timeScale = 0;
        Debug.Log("Boss death animation ended. Time scale set to 0.");
    }


    IEnumerator BossDeathSequence()
    {
        // 보스가 죽는 애니메이션 실행
        animator.SetBool("bossdeath", true);
        // 보스가 사라지기 전에 일정 시간 대기
        yield return new WaitForSeconds(4f);

        
        StartCoroutine(ActivateClearImageAfterDialogue(1f));
    }


    IEnumerator ActivateClearImageAfterDialogue(float delay)
    {
        yield return new WaitForSeconds(delay);
        Time.timeScale = 0;
        gameClear.SetActive(true); // 클리어 창을 활성화
        BossBGM.enabled = false;
        if (gameClearSound != null)
        {
            gameClearSound.Play();
        }
    }

    IEnumerator ResetAttackNumAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetInteger("Attack_Num", 0);
    }
}

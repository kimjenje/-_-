using UnityEngine;
using System.Collections;

public class BossTutoSkill : MonoBehaviour
{
    public AnimationClip[] animations; // �ִϸ��̼� Ŭ�� �迭
    public GameObject attackItem;
    public GameObject spawnZone;
    public GameObject tutoBoss;
    public GameObject clearimage;

    float initialDelay = 5f; // ������ ����������� �ʱ� ������
    float bossStartTime; // ���� ���� �ð�
    float cooldown = 15f; // ��ų ��Ÿ��
    float nextSkillTime = 0f; // ���� ��ų ��� �ð�
    bool bossActivated = false;
    bool isFirstAction = true;
    public bool isBossAtking;
    Animator animator;

    public string[] firstDialogue; // ó�� ��ų �� ��� �迭
    public string[] secondSkillDialogue; // �� ��° ��ų �� ��� �迭
    public string[] thirdDialogue;
    public string[] lastDialogue;
    public Sprite[] firstImages;
    public Sprite[] secondImages;
    public Sprite[] thirdImages;
    public Sprite[] lastImages;
    public DialogueManager dialogueManager; // ���̾�α� �Ŵ��� ����
    public Transform attackSpawn;
    public TutobossHealth BossHealth;
    public AudioSource Spike_SFX;
    public AudioSource Boast_SFX;
    public AudioSource Dead_SFX;
    public AudioSource Fire_SFX;

    bool isDialoguePlaying = false; // ��ȭ�� ���� ������ ����
    bool isFinalDialogueStarted = false;


    int currentAnimationIndex = 0;

    void Start()
    {
        animator = GetComponent<Animator>();

        // ���� ���� �ð� ����
        bossStartTime = Time.time + initialDelay;
    }

    void Update()
    {
        if (!bossActivated && Time.time >= bossStartTime)
        {
            ActivateBoss(); 
        }

        else if (bossActivated) 
        {

            if (!isDialoguePlaying)
            {
                PlayNextAnimation();
            }
        }
        if (BossHealth != null)
        {
            if (BossHealth.currentHealth <= 0)
            {
                StartCoroutine(BossDeathSequence());
            }
        }


    }

    IEnumerator BossDeathSequence()
    {
        // 보스가 죽는 애니메이션 실행
        animator.SetBool("bossdeath", true);
        // 보스가 사라지기 전에 일정 시간 대기
        yield return new WaitForSeconds(5f); // 예시로 5초 대기, 애니메이션 길이에 맞게 조절 필요
                                             // 다이얼로그 실행
        if (!isFinalDialogueStarted) // 마지막 대화가 이미 시작되지 않았을 때만 실행
        {
            StartCoroutine(StartFinalDialogueAfterDelay(1f));
            isFinalDialogueStarted = true; // 마지막 대화가 시작됨을 나타냄

        }
    }
    IEnumerator StartFinalDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        // 마지막 대화 실행
        dialogueManager.StartDialogue(lastDialogue, lastImages);

        // 대화가 끝난 후 클리어 창 활성화
        StartCoroutine(ActivateClearImageAfterDialogue(2.5f)); // 대화의 길이를 기준으로 대략적인 대기 시간 계산
    }

    IEnumerator ActivateClearImageAfterDialogue(float delay)
    {
        yield return new WaitForSeconds(delay);
        Time.timeScale = 0;
        clearimage.SetActive(true); // 클리어 창을 활성화
    }

    IEnumerator PlayFireWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Fire_SFX.Play();
    }

    void ActivateBoss()
    {
        bossActivated = true;
        Debug.Log("Boss activated!");
        isDialoguePlaying = true;
        // ���� ���� �� ��ȭ ����
        StartCoroutine(StartFirstDialogueAfterDelay());
    }

    IEnumerator StartFirstDialogueAfterDelay()
    {

        yield return new WaitForSeconds(5f);
        isDialoguePlaying = false;
    }

    void AnimationEnd()
    {
        // 애니메이션이 끝난 후 실행할 로직
        Time.timeScale = 0;
    }

    void PlayNextAnimation()
    {
        nextSkillTime = Time.time + cooldown;
        //if (!isDialoguePlaying)
        //{
        Debug.Log($"currentAnimationIndex : {currentAnimationIndex}");

        dialogueManager.StartDialogue(firstDialogue, firstImages);
        animator.SetInteger("Attack_Num", 1);
        isDialoguePlaying = true;
        StartCoroutine(WaitForSkillEndAndSetNextSkillTime(2f));
        StartCoroutine(ResetAttackNumAfterDelay(13));

    }

    IEnumerator WaitForSkillEndAndSetNextSkillTime(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetInteger("Attack_Num", 0);
    }

    IEnumerator ResetAttackNumAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        animator.SetInteger("Attack_Num", 2);
        dialogueManager.StartDialogue(secondSkillDialogue, secondImages);
        isDialoguePlaying = true;
        StartCoroutine(WaitForSkillEndAndSetNextSkillTime(2f));
        StartCoroutine(ResetAttackNumRepeatedly(10f));
        StartCoroutine(SpawnAttackItemRepeatedly(7f, 10f));
        spawnZone.SetActive(true);
    }
    IEnumerator ResetAttackNumAfterAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        if (isFirstAction)
            dialogueManager.StartDialogue(thirdDialogue, thirdImages);
        int randomValue = Random.Range(1, 3);
        switch (randomValue)
        {
            case 1:
                animator.SetInteger("Attack_Num", 1);
                break;
            case 2:
                animator.SetInteger("Attack_Num", 2);
                break;
            default:
                Debug.LogError("Unexpected random value: " + randomValue);
                break;
        }
        StartCoroutine(WaitForSkillEndAndSetNextSkillTime(2f));
    }
    IEnumerator ResetAttackNumRepeatedly(float delay)
    {
        isFirstAction = true;
        while (true)
        {
            yield return ResetAttackNumAfterAfterDelay(delay);
            isFirstAction = false;
        }
    }


    IEnumerator SpawnAttackItemRepeatedly(float minDelay, float maxDelay)
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minDelay, maxDelay));
            Instantiate(attackItem, attackSpawn.position, attackSpawn.rotation);
        }
    }

    /*IEnumerator PlaySpikeSoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Spike_SFX.Play();
    }

    IEnumerator PlayBoastSoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Boast_SFX.Play();
    }


    IEnumerator PlayBossDeathWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Dead_SFX.Play();
    }*/
    public IEnumerator Bossattack()
    {
        isBossAtking = true;
        yield return new WaitForSeconds(4.8f);
        isBossAtking = false;
    }
}
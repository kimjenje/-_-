using UnityEngine;
using UnityEngine.UI;

public class TutobossHealth : MonoBehaviour
{
    public int maxHealth = 100; // �ִ� ü��
    public int currentHealth; // ���� ü��
    public BossTutoSkill bossSkillController;
    public DialogueManager dialogueManager;
    public GameObject tutoBoss;
    Animator animator;

    public Slider healthSlider; // �� ü���� ǥ���� Slider
    public string[] lastDialogue;
    public Sprite[] lastImages;


    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth; // ������ �� �ִ� ü������ ����
        healthSlider.maxValue = maxHealth; // Slider�� �ִ� �� ����
        healthSlider.value = currentHealth; // Slider�� ���� ���� ü������ ����
    }

    public void TakeDamage(int damageAmount)
    {
        if (bossSkillController != null && !bossSkillController.isBossAtking)
        {
            currentHealth -= damageAmount; // ���ظ� ����
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // ü���� ������ ���� �ʵ��� Ŭ����

            healthSlider.value = currentHealth; // Slider�� ���� ���� ü������ ����

            if (currentHealth <= 0)
            {
                Die(); // ���� �׾��� ���� ó��
            }
        }
    }

    void Die()
    {
        // ���⿡ ���� �׾��� ���� ������ �߰��մϴ�.
        Debug.Log("Enemy died!");
        gameObject.SetActive(false); // ���� ��Ȱ��ȭ�Ͽ� �� �̻� ǥ������ ����
    }
}

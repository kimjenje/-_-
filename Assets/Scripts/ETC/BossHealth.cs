using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{
    public int maxHealth = 100; // 최대 체력
    public int currentHealth; // 현재 체력
    public BossController bossController;

    public Slider healthSlider; // 적 체력을 표시할 Slider

    private void Start()
    {
        currentHealth = maxHealth; // 시작할 때 최대 체력으로 설정
        healthSlider.maxValue = maxHealth; // Slider의 최대 값 설정
        healthSlider.value = currentHealth; // Slider의 값을 현재 체력으로 설정
    }

    public void TakeDamage(int damageAmount)
    {
        if(bossController.isBossAtking == false)
        {
            currentHealth -= damageAmount; // 피해를 입음
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // 체력이 음수가 되지 않도록 클램핑

            healthSlider.value = currentHealth; // Slider의 값을 현재 체력으로 설정
        }
        if (currentHealth <= 0)
        {
            Die(); // 적이 죽었을 때의 처리
        }
    }

    void Die()
    {
        // 여기에 적이 죽었을 때의 동작을 추가합니다.
        Debug.Log("Enemy died!");
        gameObject.SetActive(false); // 적을 비활성화하여 더 이상 표시하지 않음
    }
}

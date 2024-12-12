using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CounDown : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent OnDeath;

    [SerializeField] public float playerHealth;
    [SerializeField] public float maxHealth;
    [SerializeField] public Image healthImage;

    [SerializeField] public int damage;

    [SerializeField] public int damagetaken;

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);
            playerHealth += damagetaken;
            UpdateHealth();

        }
    }

    public void ButtonClick()
    {
        playerHealth -= damage;
        UpdateHealth();
        if(playerHealth < 0)
            OnDeath.Invoke();
    }



    private void UpdateHealth()
    {
        healthImage.fillAmount = playerHealth / maxHealth;
    }

   public void Heal(float amount)
{
    playerHealth += amount;
    if (playerHealth > maxHealth)
    {
        playerHealth = maxHealth; // 최대 체력을 초과하지 않도록 보정
    }
    UpdateHealth();
}

}
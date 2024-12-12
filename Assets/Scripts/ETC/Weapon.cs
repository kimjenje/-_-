using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public int baseDamage = 10; // 기본 피해량
    public float invincibilityDuration = 1.0f; // 무적 지속 시간
    public GameObject damageTextPrefab; // 데미지 텍스트 프리팹
    public PlayerController playerController; // PlayerController 참조 추가

    private bool isInvincible = false; // 무적 상태인지 여부를 나타내는 변수

    private void Start()
    {
        // PlayerController를 찾습니다. (PlayerController가 같은 오브젝트에 있거나 다른 오브젝트에 있다면 해당 오브젝트를 찾아 참조합니다.)
        if (playerController == null)
        {
            playerController = FindObjectOfType<PlayerController>();
        }
    }

    private void Update()
    {
        // 공격력 업 개수에 따라 피해량을 계산합니다.
        int attackUp = GameManager.Instance._attackup;
        float additionalDamage = attackUp * 0.5f;
        int totalDamage = baseDamage + Mathf.FloorToInt(additionalDamage);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // 피해를 받을 수 있는 상태인지 확인하고, 피해를 입힙니다.
        if (!isInvincible)
        {
            // 적의 체력을 감소시키는 BossHealth 스크립트를 찾아서 피해를 입힘
            BossHealth enemyHealth = other.GetComponent<BossHealth>();
            if (enemyHealth != null)
            {
                int attackUp = GameManager.Instance._attackup;
                float additionalDamage = attackUp * 0.5f;
                int totalDamage = baseDamage + Mathf.FloorToInt(additionalDamage);

                enemyHealth.TakeDamage(totalDamage);

                // PlayerController의 isAttackInProgress가 true일 때만 데미지 텍스트를 생성합니다.
                if (playerController != null && playerController.isAttackInProgress)
                {
                    ShowDamageText(totalDamage);
                }

                // 피해를 입은 후 무적 상태로 전환합니다.
                SetInvincible(true);

                // 일정 시간 후에 무적 상태를 해제합니다.
                Invoke("SetInvincibleFalse", invincibilityDuration);
            }
        }
    }

    private void ShowDamageText(int damage)
    {
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            GameObject damageTextInstance = Instantiate(damageTextPrefab, canvas.transform);
            Text damageText = damageTextInstance.GetComponent<Text>();
            damageText.text = damage.ToString();

            StartCoroutine(AnimateDamageText(damageTextInstance));
        }
    }

    private IEnumerator AnimateDamageText(GameObject damageTextInstance)
    {
        Text damageText = damageTextInstance.GetComponent<Text>();
        Vector3 startPosition = damageTextInstance.transform.position;
        Vector3 endPosition = startPosition + new Vector3(0, 50, 0);
        float duration = 1.0f;
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            damageTextInstance.transform.position = Vector3.Lerp(startPosition, endPosition, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        Destroy(damageTextInstance);
    }

    private void SetInvincible(bool value)
    {
        isInvincible = value;
    }

    private void SetInvincibleFalse()
    {
        SetInvincible(false); // 무적 상태를 해제합니다.
    }
}

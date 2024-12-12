using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    public int damageAmount = 10; // 무기의 피해량
    public float invincibilityDuration = 1.0f; // 무적 지속 시간

    private bool isInvincible = false; // 무적 상태인지 여부를 나타내는 변수

    private void OnTriggerExit2D(Collider2D other)
    {
        // 피해를 받을 수 있는 상태인지 확인하고, 피해를 입힙니다.
        if (!isInvincible)
        {
            // 적의 체력을 감소시키는 BossHealth 스크립트를 찾아서 피해를 입힘
            TutobossHealth enemyHealth = other.GetComponent<TutobossHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
                // 피해를 입은 후 무적 상태로 전환합니다.
                SetInvincible(true);
                // 일정 시간 후에 무적 상태를 해제합니다.
                Invoke("SetInvincibleFalse", invincibilityDuration);
            }
        }
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

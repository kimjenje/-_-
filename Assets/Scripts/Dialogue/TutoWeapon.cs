using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    public int damageAmount = 10; // ������ ���ط�
    public float invincibilityDuration = 1.0f; // ���� ���� �ð�

    private bool isInvincible = false; // ���� �������� ���θ� ��Ÿ���� ����

    private void OnTriggerExit2D(Collider2D other)
    {
        // ���ظ� ���� �� �ִ� �������� Ȯ���ϰ�, ���ظ� �����ϴ�.
        if (!isInvincible)
        {
            // ���� ü���� ���ҽ�Ű�� BossHealth ��ũ��Ʈ�� ã�Ƽ� ���ظ� ����
            TutobossHealth enemyHealth = other.GetComponent<TutobossHealth>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(damageAmount);
                // ���ظ� ���� �� ���� ���·� ��ȯ�մϴ�.
                SetInvincible(true);
                // ���� �ð� �Ŀ� ���� ���¸� �����մϴ�.
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
        SetInvincible(false); // ���� ���¸� �����մϴ�.
    }
}

using UnityEngine;

public class Damage : MonoBehaviour
{
    public int damageAmount = 1;
    public Life lifeScript;
    private PlayerController playerController;
    GameManager gameManager;

    private void Start()
    {
        lifeScript = FindObjectOfType<Life>();
        if (lifeScript == null)
        {
            Debug.LogError("Life script not found in the scene.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // �浹�� ��ü�� �÷��̾����� Ȯ��
        if (collision.CompareTag("Player"))
        {
            // �浹�� ��ü�κ��� PlayerController ������Ʈ ��������
            playerController = collision.gameObject.GetComponent<PlayerController>();

            // playerController�� null�� �ƴϰ�, isBegine�� false�� ��쿡�� ������ ó��
            if (playerController != null && !playerController.isBegine)
            {
                Debug.Log("Damage!");
                gameManager = FindObjectOfType<GameManager>();
                gameManager._life--;

                // lifeScript�� null�� �ƴϸ� �÷��̾�� ������ ������
                if (lifeScript != null)
                {
                    lifeScript.TakeDamage();

                    StartCoroutine(playerController.PlayerBegine());
                }
                else
                {
                    Debug.LogError("Life script is not assigned.");
                }
            }
        }
    }
}






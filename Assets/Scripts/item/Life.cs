using UnityEngine;
using UnityEngine.UI;

public class Life : MonoBehaviour
{
    public GameObject[] lifeObjects; // 이미지를 가지고 있는 GameObject 배열
    public Sprite breakHeart; // 체력이 줄었을 때 표시할 이미지
    public Sprite _heart; // 아이템을 획득했을 때 복원할 초기 이미지

    private int currentLifeIndex = 0; // 현재 활성화된 이미지의 인덱스

    public void TakeDamage()
    {
        if (currentLifeIndex < lifeObjects.Length)
        {
            if (breakHeart != null)
            {
                Image imageComponent = lifeObjects[currentLifeIndex].GetComponent<Image>();
                if (imageComponent != null)
                {
                    imageComponent.sprite = breakHeart;
                }
                 if (currentLifeIndex >= 4)
                {
                    // Life 스크립트의 GameManager에 접근하여 GameOver 호출
                    //GameManager.Instance.GameOver();
                }
                currentLifeIndex++; // 다음 이미지의 인덱스로 이동
            }
        }
    }

    public void RecoverLife()
    {
        if (currentLifeIndex > 0)
        {
            currentLifeIndex--; // 이전 이미지의 인덱스로 되돌아감
            if (_heart != null)
            {
                Image imageComponent = lifeObjects[currentLifeIndex].GetComponent<Image>();
                if (imageComponent != null)
                {
                    imageComponent.sprite = _heart;
                }

            }
        }
    }
}
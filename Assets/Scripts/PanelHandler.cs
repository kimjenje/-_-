using UnityEngine;

public class PanelHandler : MonoBehaviour
{
    public GameObject panel;

    void Start()
    {
        // 상태를 확인하여 패널 비활성화
        if (PlayerPrefs.GetInt("ButtonClicked", 0) == 1)
        {
            panel.SetActive(false);
        }
    }
}
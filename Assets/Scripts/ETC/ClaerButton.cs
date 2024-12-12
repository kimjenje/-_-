using UnityEngine;
using UnityEngine.UI;

public class ClaerButton : MonoBehaviour
{
    public Button EastClearButton;

    void Start()
    {
        EastClearButton.onClick.AddListener(OnEastClear);
    }

    void OnEastClear()
    {
        // ���¸� false���� true�� ����
        PlayerPrefs.SetInt("EastButtonClicked", 1);
        PlayerPrefs.Save();
    }
}

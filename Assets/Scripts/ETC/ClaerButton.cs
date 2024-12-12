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
        // 상태를 false에서 true로 변경
        PlayerPrefs.SetInt("EastButtonClicked", 1);
        PlayerPrefs.Save();
    }
}

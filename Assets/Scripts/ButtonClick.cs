using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{
    public Button myButton;

    void Start()
    {
        myButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        // 상태를 false에서 true로 변경
        PlayerPrefs.SetInt("ButtonClicked", 1);
        PlayerPrefs.Save();
    }
}
using UnityEngine;
using UnityEngine.UI;

public class RunningProgress : MonoBehaviour
{
    public Slider slider;
    private float progress = 0f;
    public static bool isFast;


    
    public void FixedUpdate()
    {
        if (isFast)
        {
            progress += Time.deltaTime * 4f;
            Debug.Log("값 변경 성공 속도가 빨라집니다.");
        }
        else
        {
            progress += Time.deltaTime;
            Debug.Log("기본 속도");
        }
        slider.value = progress;
    }

    public void OnToggleValueChanged(bool value)
    {
        isFast = value;
        FixedUpdate();
    }
}
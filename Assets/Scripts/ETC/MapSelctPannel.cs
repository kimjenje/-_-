using UnityEngine;

public class MapSelctPannel : MonoBehaviour
{
    public GameObject NorthNaga;
    public GameObject NorthButton;
    public GameObject NorthLock;
    public GameObject NorthUnlock;

    void Start()
    {
        // 상태를 확인하여 패널 비활성화
        if (PlayerPrefs.GetInt("EastButtonClicked", 0) == 1)
        {
            NorthNaga.SetActive(false);
            NorthLock.SetActive(false);
            NorthUnlock.SetActive(true);
            NorthButton.SetActive(true);
        }
    }
}

using UnityEngine;

public class MapSelctPannel : MonoBehaviour
{
    public GameObject NorthNaga;
    public GameObject NorthButton;
    public GameObject NorthLock;
    public GameObject NorthUnlock;

    void Start()
    {
        // ���¸� Ȯ���Ͽ� �г� ��Ȱ��ȭ
        if (PlayerPrefs.GetInt("EastButtonClicked", 0) == 1)
        {
            NorthNaga.SetActive(false);
            NorthLock.SetActive(false);
            NorthUnlock.SetActive(true);
            NorthButton.SetActive(true);
        }
    }
}

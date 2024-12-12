using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class att_up_text : MonoBehaviour
{
    [SerializeField] Text att_text;  // UI Text ������Ʈ�� ����
    private GameManager gameManager;

    private void Start()
    {
        // GameManager �ν��Ͻ��� ������
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        // GameManager�� _attackup ���� UI Text�� ������Ʈ
        att_text.text = string.Format("X {0}", gameManager._attackup);
    }
}
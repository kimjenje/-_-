using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class att_up_text : MonoBehaviour
{
    [SerializeField] Text att_text;  // UI Text 컴포넌트를 참조
    private GameManager gameManager;

    private void Start()
    {
        // GameManager 인스턴스를 가져옴
        gameManager = GameManager.Instance;
    }

    private void Update()
    {
        // GameManager의 _attackup 값을 UI Text에 업데이트
        att_text.text = string.Format("X {0}", gameManager._attackup);
    }
}
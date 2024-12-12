using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class item : MonoBehaviour
{
    public float speed = 5f; // 아이템의 이동 속도

    // Update is called once per frame
    void Update()
    {
        // 아이템을 오른쪽에서 왼쪽으로 이동시킴
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

}

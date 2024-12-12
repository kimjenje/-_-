using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class swordpower : MonoBehaviour
{
   public float speed = 5f; // 이동 속도

    void Update()
    {
        // 왼쪽으로 이동
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}

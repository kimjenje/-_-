using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parrying : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("DarkBall"))
        {
            //if (패링)
            //else 데미지
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transformcontroller : MonoBehaviour
{
    float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;
        MovePosition(new Vector2(Mathf.Sin(timer), 0f));
    }

    public void MoveTranslate(Vector2 moveVector)
    {
        transform.Translate(moveVector);
    }
    public void MovePosition(Vector3 newPos)
    {
        transform.position = newPos;
    }
}

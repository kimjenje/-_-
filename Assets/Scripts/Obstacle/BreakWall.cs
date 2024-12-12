using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWall : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        switch (collider2D.tag)
        {
            case "Item":
                Destroy(collider2D.gameObject);
                break; 
            case "Obstacle":
                Destroy(collider2D.gameObject);
                break;
            case "Object":
                Destroy(collider2D.gameObject);
                
                break;

        }
    }
}

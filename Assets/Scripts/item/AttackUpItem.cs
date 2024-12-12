using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackUpItem : MonoBehaviour
{
    public AudioSource attupSound;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) 
        {
            if (attupSound != null)
            {
                attupSound.Play();
            }
        }
    }

}

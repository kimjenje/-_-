using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string[] sentences;
    public Sprite[] images; // 각 문장에 해당하는 이미지를 저장하는 배열
    public DialogueManager dialogueManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueManager.StartDialogue(sentences, images); // 이미지 배열을 전달
            gameObject.SetActive(false);
        }
    }
}

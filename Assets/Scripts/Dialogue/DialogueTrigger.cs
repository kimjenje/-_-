using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public string[] sentences;
    public Sprite[] images; // �� ���忡 �ش��ϴ� �̹����� �����ϴ� �迭
    public DialogueManager dialogueManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            dialogueManager.StartDialogue(sentences, images); // �̹��� �迭�� ����
            gameObject.SetActive(false);
        }
    }
}

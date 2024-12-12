using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI; // 이미지를 사용하기 위해 추가
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI textDisplay; // 대화 텍스트를 표시할 UI Text 요소
    public Image imageDisplay; // 해당 이미지를 표시할 UI Image 요소
    private string[] sentences; // 대화의 문장 배열
    private Sprite[] images; // 각 문장에 해당하는 이미지 배열
    private int index; // 현재 문장 인덱스를 추적
    public GameObject continueButton; // 다음 문장으로 넘어가기 위한 버튼
    public GameObject dialogueSystem; // 전체 대화 시스템을 담는 컨테이너
    private GameManager gameManager; // GameManager에 대한 참조
    public KeyCode nextKey = KeyCode.Return; // 다음 문장으로 넘어가기 위한 키 (Enter 키로 설정)

    private bool isDialogueActive = false; // 대화가 활성화되어 있는지 체크하는 변수

    void Start()
    {
        dialogueSystem.SetActive(false); // 대화 시스템을 초기에는 비활성화
        imageDisplay.gameObject.SetActive(false); // 이미지 표시를 비활성화
        gameManager = FindObjectOfType<GameManager>(); // GameManager 참조 초기화
    }

    void Update()
    {
        
        if (Input.GetKeyDown(nextKey))
        {

            NextSentence(); // 다음 문장으로 넘어감
        }

    }

    public void StartDialogue(string[] newSentences, Sprite[] newImages)
    {
        // 대화 시작 시 문장과 이미지를 초기화
        sentences = newSentences;
        images = newImages; // 이미지 배열 초기화
        dialogueSystem.SetActive(true); // 대화 시스템 활성화
        imageDisplay.gameObject.SetActive(true); // 이미지 표시 활성화
        index = 0; // 문장 인덱스를 0으로 초기화
        DisplaySentence(); // 첫 문장 표시
        Time.timeScale = 0f; // 게임 시간을 멈춤
        isDialogueActive = true; // 대화가 활성화됨
    }

    void DisplaySentence()
    {
        // 현재 문장과 해당 이미지를 표시
        textDisplay.text = sentences[index];
        imageDisplay.sprite = images[index]; // 해당하는 이미지 표시
        continueButton.SetActive(true); // 다음 문장으로 넘어가는 버튼 활성화
    }

    public void NextSentence()
    {
        // 다음 문장이 있을 경우
        if (index < sentences.Length - 1)
        {   
            index++; // 인덱스 증가
            EventSystem.current.SetSelectedGameObject(null);
            DisplaySentence(); // 다음 문장 표시
        }
        else
        {
            EndDialogue(); // 대화 종료
        }
    }

    void EndDialogue()
    {
        // 대화 종료 시 처리
        dialogueSystem.SetActive(false); // 대화 시스템 비활성화
        continueButton.SetActive(false); // 다음 문장 버튼 비활성화
        imageDisplay.gameObject.SetActive(false); // 이미지 표시 비활성화
        if (isDialogueActive) // 대화가 활성화되어 있을 때만 시간 재개
        {
            Time.timeScale = 1f; // 게임 시간 재개
            isDialogueActive = false; // 대화 비활성화
        }
    }

    public bool IsDialogueActive()
    {
        return isDialogueActive; // 대화 활성화 상태 반환
    }
}
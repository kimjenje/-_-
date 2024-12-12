using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class BTNType : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public BtnType currentType;
    public Transform buttonScale;
    Vector3 defaultScale;
    public CanvasGroup mainGroup;
    public CanvasGroup optionGroup;
    public CanvasGroup GameModeMenu;
    public CanvasGroup StoryModeStage;
    public CanvasGroup StoryMap;
    public CanvasGroup ArcadeModeReady;
    public CanvasGroup IllustratedGuide;//도감
    public CanvasGroup PetPanel;
    public CanvasGroup Pet_1;//그슨새 도감
    public CanvasGroup Pet_2;//그슨대 도감 
    public CanvasGroup NorthInfo;
    public CanvasGroup WestInfo;
    public CanvasGroup SouthInfo;
    public CanvasGroup CenterInfo;


    public CanvasGroup CopyRightGroup;
    public CanvasGroup ArcadeInfo;
    private void Start()
    {
        defaultScale = buttonScale.localScale;
    }

    bool isSound;
    public void OnBtnClick()
    {
        switch (currentType)
        {
            case BtnType.Start:
                Debug.Log("게임 시작");
                CanvasGroupOn(GameModeMenu);
                CanvasGroupoff(mainGroup);
                break;

            case BtnType.Option:
                CanvasGroupOn(optionGroup);
                CanvasGroupoff(mainGroup);
                Debug.Log("설정");
                break;

            case BtnType.KeepGoing:
                Debug.Log("계속하기 (게임중간)");
                CanvasGroupOn(mainGroup);
                GameManager.Instance.menuSet.SetActive(false);

                // DialogueManager를 찾고 비활성화된 경우 시간 스케일을 1로 설정
                DialogueManager dialogueManager = FindObjectOfType<DialogueManager>();
                if (dialogueManager == null || !dialogueManager.IsDialogueActive())
                {
                    Time.timeScale = 1f;
                }
                break;

            case BtnType.Replay:
                Debug.Log("다시하기 (게임중간)");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                Time.timeScale = 1;
                break;


            case BtnType.Pause:
                GameManager.Instance.TogglePause();
                break;

            case BtnType.Sound:
                if (isSound)
                {
                    isSound = !isSound;
                    Debug.Log("사운드 off");
                }
                else
                {
                    Debug.Log("사운드 on");
                }
                break;
            case BtnType.Back:
                CanvasGroupOn(mainGroup);
                CanvasGroupoff(optionGroup);
                CanvasGroupoff(GameModeMenu);
                //CanvasGroupoff(StoryMap);
                CanvasGroupoff(StoryModeStage);
                CanvasGroupoff(CopyRightGroup);
                CanvasGroupoff(ArcadeModeReady);
                CanvasGroupoff(ArcadeInfo);
                CanvasGroupoff(IllustratedGuide);
                CanvasGroupoff(PetPanel);
                Debug.Log("뒤로 가기");
                break;

            case BtnType.BackSelect:
                CanvasGroupOn(StoryModeStage);
                CanvasGroupoff(StoryMap);
                break;


            case BtnType.Quit:
                Application.Quit();
                Debug.Log("게임 종료");
                break;

            case BtnType.StoryMode:
                CanvasGroupOn(StoryModeStage);
                CanvasGroupoff(GameModeMenu);
                Debug.Log("스토리 모드");
                break;

            case BtnType.EastStage:
                CanvasGroupOn(StoryMap);
                CanvasGroupoff(StoryModeStage);
                Debug.Log("동쪽 씬 전환");
                break;

            case BtnType.NorthStage:
                CanvasGroupOn(NorthInfo);
                CanvasGroupOn(StoryModeStage);
                Debug.Log("북 씬 전환");
                break;

            case BtnType.WestStage:
                CanvasGroupOn(WestInfo);
                CanvasGroupOn(StoryModeStage);
                Debug.Log("서 씬 전환");
                break;

            case BtnType.SouthStage:
                CanvasGroupOn(SouthInfo);
                CanvasGroupOn(StoryModeStage);
                Debug.Log("남 씬 전환");
                break;

            case BtnType.CenterStage:
                CanvasGroupOn(CenterInfo);
                CanvasGroupOn(StoryModeStage);
                Debug.Log("중 씬 전환");
                break;

            case BtnType.StoryMapBack:
                CanvasGroupOn(StoryModeStage);
                CanvasGroupoff(StoryMap);
                CanvasGroupoff(NorthInfo);
                CanvasGroupoff(CenterInfo);
                CanvasGroupoff(SouthInfo);
                CanvasGroupoff(WestInfo);
                break;//스토리 맵 선택으로

            case BtnType.StoryStartGame:
                SceneManager.LoadScene(3); // 스토리 모드 게임 시작 가제
                Debug.Log("게임 씬 전환");
                Time.timeScale = 1;
                break;

            case BtnType.NorthStageStart:
                SceneManager.LoadScene(6); // 여름 스토리 모드 게임 시작 가제
                Debug.Log("여름 씬 전환");
                Time.timeScale = 1;
                break;

            case BtnType.ArcadeMode:
                CanvasGroupOn(ArcadeModeReady);
                CanvasGroupoff(ArcadeInfo);
                CanvasGroupoff(GameModeMenu);
                Debug.Log("아케이드 모드");
                break;

            case BtnType.TutorialMode:
                SceneManager.LoadScene(2); //튜토리얼 가제
                Debug.Log("튜토리얼 모드");
                break;
            case BtnType.GoBack:
                CanvasGroupOn(GameModeMenu);
                CanvasGroupoff(StoryModeStage);
                CanvasGroupoff(ArcadeModeReady);
                Debug.Log("뒤로가기");
                break;

            case BtnType.Info:
                CanvasGroupOn(ArcadeInfo);
                Debug.Log("설명창");
                break;



            case BtnType.CopyRight:
                CanvasGroupOn(CopyRightGroup);
                CanvasGroupoff(mainGroup);
                Debug.Log("저작권");
                break;


            case BtnType.Illustrated_Guide:
                CanvasGroupOn(IllustratedGuide);
                CanvasGroupOn(PetPanel);
                CanvasGroupoff(mainGroup);
                Debug.Log("도감");
                break;

            case BtnType.Pet1:
                CanvasGroupOn(Pet_1);
                CanvasGroupoff(PetPanel);
                CanvasGroupoff(mainGroup);
                Debug.Log("그슨새도감");
                break;

            case BtnType.Pet2:
                CanvasGroupOn(Pet_2);
                CanvasGroupoff(PetPanel);
                CanvasGroupoff(mainGroup);
                Debug.Log("그슨대도감");
                break;


            case BtnType.DogamBack:
                CanvasGroupOn(IllustratedGuide);
                CanvasGroupOn(PetPanel);
                CanvasGroupoff(Pet_1);
                CanvasGroupoff(Pet_2);
                Debug.Log("도감돌아가기");
                break;

            case BtnType.ArcadeStart:
                SceneManager.LoadScene(4);
                break;

        }
    }

    public void CanvasGroupOn(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;

    }

    public void CanvasGroupoff(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale * 1.2f;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        buttonScale.localScale = defaultScale;
    }
}
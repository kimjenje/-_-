using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story_Main : MonoBehaviour
{
    void SceneLoadBtn(int sceneIndex)
    {
        // 원하는 로드할 씬의 인덱스를 사용하여 씬을 로드합니다.
        SceneManager.LoadScene(0);
    }
}
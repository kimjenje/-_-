using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Story_Main : MonoBehaviour
{
    void SceneLoadBtn(int sceneIndex)
    {
        // ���ϴ� �ε��� ���� �ε����� ����Ͽ� ���� �ε��մϴ�.
        SceneManager.LoadScene(0);
    }
}
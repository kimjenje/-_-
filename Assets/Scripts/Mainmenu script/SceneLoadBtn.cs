using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadBtn : MonoBehaviour
{
    public void StartClick(string sceneName)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);

    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text _score;
    public GameObject[] _life;
    public GameObject _gameOver;

    private void OnEnable()
    {
       //GameManager.Instance.onScoreChange += OnScoreChange;
        //GameManager.Instance.onLifeChange += OnLifeChange;
    }

    private void OnDisable()
    {
        //GameManager.Instance.onScoreChange -= OnScoreChange;
        //GameManager.Instance.onLifeChange -= OnLifeChange;
    }
    void OnScoreChange(int num)
    {
        _score.text = num.ToString();
    }

    private void OnLifeChange(int num)
    {
        for (int i = 0; i < _life.Length; i++)
        {
            if (i < num)
            {
                _life[i].SetActive(true);
            }
            else
            {
                _life[i].SetActive(false);
            }
        }
    }
}

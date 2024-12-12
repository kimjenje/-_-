using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text _score; // 점수를 표시할 UI Text 요소

    private void OnEnable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreChange += OnScoreChange;
        }
        else
        {
            Debug.LogError("GameManager.Instance is null in OnEnable.");
        }
    }

    private void OnDisable()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.OnScoreChange -= OnScoreChange;
        }
        
    }

    void OnScoreChange(int num)
    {
        if (_score != null)
        {
            _score.text = num.ToString();
        }
        
    }

    private void Start()
    {
        if (_score == null)
        {
            Debug.LogError("_score is not assigned in the inspector.");
        }
    }
}

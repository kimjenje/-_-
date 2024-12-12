using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int scorePerSecond = 1; // 1초당 획득할 점수
    public static GameManager Instance;
    public GameObject menuSet;
    public GameObject Option;
    private bool isPaused = false;
    public event Action<int> OnScoreChange;
    public event Action<int> onLifeChange;
    public event Action<int> onAtkCountChange;
    public int _damage;
    public int _attackup;
    public UserData _userData;
    public int _score;
    public int _life;
    public int atkCount;

    public GameObject _menuGameover;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        _menuGameover.SetActive(false);
        _score = 0;
        _life = 5;
        _attackup = 0;
        Instance = this;
        Debug.Log("GameManager instance set in Awake.");
    }

    private void Start()
    {
        Time.timeScale = 1;
        string data = PlayerPrefs.GetString("userData");
        if (string.IsNullOrEmpty(data))
        {
            _userData = new UserData();
        }
        else
        {
            _userData = SaveData.Deserialize<UserData>(data);
        }
        onAtkCountChange?.Invoke(atkCount);
        Debug.Log("GameManager initialized in Start.");
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            TogglePause();
        }
    }

    void FixedUpdate()
    {
        _score += scorePerSecond;
        OnScoreChange?.Invoke(_score);
    }

    public bool RespawnPlayer()
    {
        _life--;
        onLifeChange?.Invoke(_life);

        if (_life <= 0)
        {
            GameOver();
            return false;
        }
        else
        {
            return true;
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            menuSet.SetActive(true);
            Time.timeScale = 0;
            isPaused = !isPaused;
        }
        else
        {
            if (Option != null)
            {
                Option.SetActive(false);
            }
            isPaused = !isPaused;
            menuSet.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void AddScore(int num)
    {
        _score += num;
        OnScoreChange?.Invoke(_score);
        Debug.Log("점수 업데이트 : " + _score);
    }

    public void GameOver()
    {
        SceneManager.LoadScene(2);
    }

    public void AddAttackup(int num)
    {
        _attackup += num;
    }

    public void AddAttackCount(int num)
    {
        atkCount += num;
        onAtkCountChange?.Invoke(atkCount);
    }
}

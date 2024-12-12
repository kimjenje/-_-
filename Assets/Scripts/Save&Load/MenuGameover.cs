using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuGameover : MonoBehaviour
{
    public InputField _editText;
    public GameObject _menuRank;

    public void ButtonAct_Save() 
    {
       
        //GameManager.Instance._userData._list.Add(new RankData(1, _editText.text, GameManager.Instance._score));
        UserData ud = GameManager.Instance._userData;
        bool isLike = false;
        for(int i =0; i< ud._list.Count; i++)
        {
            if (ud._list[i]._id.Equals(_editText.text))
            {
                if (ud._list[i]._score < GameManager.Instance._score)
                {
                    ud._list[i] = new RankData(1, _editText.text, GameManager.Instance._score);
                }
                isLike = true;
                break;
            }
        }
        if (isLike == false)
        {
            GameManager.Instance._userData._list.Add(
            new RankData(1, _editText.text, GameManager.Instance._score));
        }
        string data = SaveData.ObjectToStringSerialize(GameManager.Instance._userData);
        PlayerPrefs.SetString("userData", data);
        gameObject.SetActive(false);
        _menuRank.SetActive(true);
    }
}

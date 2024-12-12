using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MenuRanking : MonoBehaviour
{
    public UIRank[] _rankDataList;
    UserData _plyerData;

    private void OnEnable()
    {
        /*UserData user = new UserData();
        user._list = new List<RankData>();
        user._list.Add(new RankData(1, "fff", 1000));
        string strData = SaveData.ObjectToStringSerialize(user);
        PlayerPrefs.SetString("userData", strData);*/ //테스트 부분
        

        string data = PlayerPrefs.GetString("userData");
        if(string.IsNullOrEmpty(data)== false)
        {
            _plyerData = SaveData.Deserialize<UserData>(data);
            Debug.Log($"{_plyerData._list[0]._id}");
            _plyerData.SortList();
            
            UpdateUI();
        }

    }

    void UpdateUI()
    {
        for (int i = 0; i < _rankDataList.Length; i++)
        {
            if (_plyerData._list.Count > i)
            {
                RankData data = _plyerData._list[i];
                data._rank = i + 1;
                _plyerData._list[i] = data;
                _rankDataList[i].Init(_plyerData._list[i]);
            }
            else
            {
                _rankDataList[i].HideUI();
            }
        }
    }
}
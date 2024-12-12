using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRank : MonoBehaviour
{
    public Text _tRank;
    public Text _tID;
    public Text _tScore;

    RankData _rank;
    public void Init(RankData rank)
    {
        _rank = rank;
        UpdateUI();
    }

    public void HideUI()
    {
        gameObject.SetActive(false);
    }

    private void UpdateUI()
    {
        _tRank.text = _rank._rank.ToString();
        _tID.text = _rank._id;
        _tScore.text = _rank._score.ToString();
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //Private VAR
    [SerializeField]
    private float score, scorePower = 1;

    //UI Reference
    public TextMeshProUGUI showScore;

    public float GetScore()
    {
        return score;
    }

    public float GetScorePower()
    {
        return scorePower;
    }

    public void UpdateScore(float ammout)
    {
        score += ammout;
        showScore.text = Math.Floor(score).ToString("000000");
    }
}

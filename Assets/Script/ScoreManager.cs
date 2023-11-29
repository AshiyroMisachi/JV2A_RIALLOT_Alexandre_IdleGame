using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    //Var
    private int score;
    public int clickPower;
    public TextMeshProUGUI uiScore;


    void Start()
    {
    }

    void Update()
    {

    }

    //Click Object
    public void updateScore(int amount)
    {
        //Update Score
        uiScore.text = score.ToString("0000");
        score += amount;
    }

    public int GetScore()
    {
        return score;
    }
}

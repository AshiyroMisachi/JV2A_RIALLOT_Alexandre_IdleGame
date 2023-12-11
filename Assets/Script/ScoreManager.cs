using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    //Var
    private int score;
    public int clickPower;
    public TextMeshProUGUI uiScore;

    public int costUpgrade;
    public TextMeshProUGUI uiCostUpgrade;
    public TextMeshProUGUI uiClickPower;

    //Click Object
    public void updateScore(int amount)
    {
        //Update Score
        uiScore.text = score.ToString("0000");
        score += amount;
    }

    public void ClickButton()
    {
        updateScore(clickPower);
    }

    public void upgradeClickPower()
    {
        if (score >= costUpgrade)
        {
            updateScore(-costUpgrade);
            costUpgrade += costUpgrade + (costUpgrade/2);
            clickPower += 1;

            uiCostUpgrade.text = costUpgrade.ToString("0000");
            uiClickPower.text = clickPower.ToString("0000");
        }
    }

    public int GetScore()
    {
        return score;
    }
}

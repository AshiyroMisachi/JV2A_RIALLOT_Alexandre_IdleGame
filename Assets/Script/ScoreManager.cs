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
    private int ballNumber = 5;

    //Level VAR
    [SerializeField]
    public TextMeshProUGUI showCommon, showUncommon, showRare, showEpic, showLegendary;
    private int currentLevel = 1;
    public int[] level1;
    public int[] level2;
    public int[] level3;
    public int[] level4;
    public int[] level5;
    public int[] level6;
    public int[] level7;
    public int[] level8;
    public int[] level9;
    public int[] level10;
    public int[] level11;
    public Array[] allLevel;

    //Shop Cost
    private float upgradeCostLevel = 50;
    public TextMeshProUGUI upgradeCostLevelText;

    //UI Reference
    public TextMeshProUGUI showScore;
    public GameObject shopHolder;

    private void Start()
    {
        //Setup Array
        allLevel = new Array[11];
        allLevel[0] = level1;
        allLevel[1] = level2;
        allLevel[2] = level3;
        allLevel[3] = level4;
        allLevel[4] = level5;
        allLevel[5] = level6;
        allLevel[6] = level7;
        allLevel[7] = level8;
        allLevel[8] = level9;
        allLevel[9] = level10;
        allLevel[10] = level11;
    }

    public float GetScore()
    {
        return score;
    }

    public float GetScorePower()
    {
        return scorePower;
    }

    public int GetBallNumber()
    {
        return ballNumber;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void UpdateScore(float ammout)
    {
        score += ammout;
        showScore.text = Math.Floor(score).ToString("000000");
    }

    //SHOP
    public void OpenShop()
    {
        if (shopHolder.activeSelf)
        {
            shopHolder.SetActive(false);
        }
        else
        {
            shopHolder.SetActive(true);
        }
    }

    public void UpgradeLevel()
    {
        if (currentLevel < 11 && score >= upgradeCostLevel)
        {
            currentLevel++;
            UpdateScore(-upgradeCostLevel);
            upgradeCostLevel *= 2;
            upgradeCostLevelText.text = "Cost: " + upgradeCostLevel;
            int[] currentChance = (int[])allLevel[currentLevel - 1];
            showCommon.text = (0 + currentChance[0]) + "%";
            if (currentChance[1] > 0)
                showUncommon.text = (currentChance[1] - currentChance[0]) + "%";
            if (currentChance[2] > 0)
                showRare.text = (currentChance[2] - currentChance[1]) + "%";
            if (currentChance[3] > 0)
                showEpic.text = (currentChance[3] - currentChance[2]) + "%";
            if (currentChance[3] < 100 && currentChance[3] > 0)
                showLegendary.text = (100 - currentChance[3]) + "%";
        }
    }
}

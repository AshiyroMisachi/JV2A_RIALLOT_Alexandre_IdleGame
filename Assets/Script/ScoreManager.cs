using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    //Level VAR
    public TextMeshProUGUI showCommon, showUncommon, showRare, showEpic, showLegendary;
    [SerializeField]
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

    //Camera
    public Camera cameraPool, cameraMeal;

    //Swimming Pool
    [Header("Swimming Pool")]
    [SerializeField]
    private float scoreBall;
    [SerializeField]
    private int ballNumber = 1;
    private float powerClick = 1, upgradePowerClick = 560, upgradeCostLevel = 50;
    public TextMeshProUGUI powerCostText, upgradeCostLevelText;
    public GameObject canvasSwiming, shopHolder, buttonGoTo;
    public TextMeshProUGUI showScore;

    //Meal
    [Header("Meal")]
    [SerializeField]
    private float scoreMeal, damage = 1;
    public TextMeshProUGUI mealCount, ballMunition;

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

        buttonGoTo.SetActive(false);
        cameraMeal.enabled = false;
    }

    public float GetScore()
    {
        return scoreBall;
    }

    public float GetScorePower()
    {
        return powerClick;
    }

    public int GetBallNumber()
    {
        return ballNumber;
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public float GetDamage()
    {
        return damage;
    }

    public void UpdateScorePool(float ammout)
    {
        scoreBall += ammout;
        showScore.text = Math.Floor(scoreBall).ToString("000000");
        ballMunition.text = Math.Floor(scoreBall).ToString("000000");
    }
    
    public void UpdateScoreMeal(float ammout)
    {
        scoreMeal += ammout;
        mealCount.text = Math.Floor(scoreMeal).ToString("000000");
    }

    //SHOP POOL
    public void OpenShopPool()
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

    public void UpgradeLevelPool()
    {
        if (currentLevel < 11 && scoreBall >= upgradeCostLevel)
        {
            currentLevel++;
            UpdateScorePool(-upgradeCostLevel);
            upgradeCostLevel *= 1.6f;
            if (currentLevel == 11)
            {
                upgradeCostLevelText.text = "Maxed";
            }
            else
            {
                upgradeCostLevelText.text = Math.Floor(upgradeCostLevel).ToString();
            }
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

    public void UpgradePowerPool()
    {
        if (GetScore() >= upgradePowerClick)
        {
            UpdateScorePool(-upgradePowerClick);
            upgradePowerClick *= 2.5f;
            powerCostText.text = Math.Floor(upgradePowerClick).ToString();
            ballNumber++;
        }
    }

    public void ChangeMode()
    {
        cameraPool.enabled = !cameraPool.enabled;
        canvasSwiming.SetActive(cameraPool.enabled);
        cameraMeal.enabled = !cameraMeal.enabled;
    }
}
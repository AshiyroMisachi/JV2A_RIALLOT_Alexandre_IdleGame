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

    //Object Reference
    public Camera cameraPool, cameraMeal;
    public GameObject audioListener;

    //Swimming Pool
    [Header("Swimming Pool")]
    [SerializeField]
    private float scoreBall;
    [SerializeField]
    private int ballNumber = 1;
    private float powerClick = 1, upgradePowerClick = 560, upgradeCostLevel = 50;
    public TextMeshProUGUI powerCostText, upgradeCostLevelText;
    public GameObject canvasSwiming, shopHolderPool, buttonGoTo;
    public TextMeshProUGUI showScore;

    //Meal
    [Header("Meal")]
    [SerializeField]
    private float scoreMeal;
    private float ballWeight = 1, upgradeBallWeight = 10;
    public TextMeshProUGUI mealCount, ballMunition, ballWeightCostText;
    public GameObject canvasMeal, shopHolderMeal, childImage;

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

        //Setup Scene
        buttonGoTo.SetActive(false);
        cameraMeal.enabled = false;
        canvasMeal.SetActive(false);
        shopHolderMeal.SetActive(false);    
        shopHolderPool.SetActive(false);
    }

    public float GetScorePool()
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

    public float GetScoreMeal()
    {
        return scoreMeal;
    }

    public float GetBallWeight()
    {
        return ballWeight;
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
        shopHolderPool.SetActive(!shopHolderPool.activeSelf);
    }

    public void UpgradeLevelPool()
    {
        //Check if the level is not maxed already
        if (currentLevel < 11 && scoreBall >= upgradeCostLevel)
        {
            currentLevel++;
            UpdateScorePool(-upgradeCostLevel);

            //If the level is maxed don't show cost anymore
            if (currentLevel == 11)
            {
                upgradeCostLevelText.text = "Maxed";
                return;
            }
            upgradeCostLevel *= 1.6f;
            upgradeCostLevelText.text = Math.Floor(upgradeCostLevel).ToString();

            //Update value of % rarity
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
        if (GetScorePool() >= upgradePowerClick)
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
        canvasMeal.SetActive(cameraMeal.enabled);

        //Move audio to change the music
        audioListener.transform.position = Camera.main.transform.position;
    }

    //Shop Meal
    public void OpenShopMeal()
    {
        shopHolderMeal.SetActive(!shopHolderMeal.activeSelf);
    }

    public void UpgradeBallWeight()
    {
        if (scoreMeal >= upgradeBallWeight)
        {
            UpdateScoreMeal(-upgradeBallWeight);
            ballWeight++;
            upgradeBallWeight *= 1.4f;
            ballWeightCostText.text = Math.Floor(upgradeBallWeight).ToString();
        }
    }
}
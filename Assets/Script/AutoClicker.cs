using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutoClicker : MonoBehaviour
{
    //Reference
    public ScoreManager scoreManager;

    //VAR Pool
    [SerializeField]
    private float timerBetweenClick = 2, upgradePowerClick = 20;
    private bool autoClick = false;
    public SwimmingBall pool;

    //Shop UI Meal
    public TextMeshProUGUI powerCostText;

    //VAR Meal
    private float timerBetweenThrow = 2, upgradeAutoThrow = 15;
    private bool autoThrow = false;
    public Child childImage;
    public TextMeshProUGUI autoThrowCostText;

    //Component
    public ParticleSystem myParticle;
    void Start()
    {
        //Find
        scoreManager = FindObjectOfType<ScoreManager>();
        myParticle = GetComponent<ParticleSystem>();


        StartCoroutine(PasiveClick());
        StartCoroutine(PassiveThrow());
    }

    //POOL
    public IEnumerator PasiveClick()
    {
        while (true)
        {
            if (autoClick)
            {
                pool.GenerateBall();
            }
            yield return new WaitForSeconds(timerBetweenClick);
        }
    }

    public void UpgradePower()
    {
        if (scoreManager.GetScorePool() >= upgradePowerClick && timerBetweenClick > 0.1f)
        {
            scoreManager.UpdateScorePool(-upgradePowerClick);
            upgradePowerClick *= 1.2f;
            powerCostText.text = Math.Floor(upgradePowerClick).ToString();
            if (!autoClick)
            {
                autoClick =  true;
                return;
            }
            timerBetweenClick -= 0.1f;
            UpdateEmission();
            if (timerBetweenClick == 0.1f)
            {
                powerCostText.text = "MAX";
            }
        }
    }

    public void UpdateEmission()
    {
        var myParticleEmission = myParticle.emission;
        myParticleEmission.enabled = true;
        myParticleEmission.rateOverTime = scoreManager.GetBallNumber() / timerBetweenClick;
    }

    //MEAL

    public IEnumerator PassiveThrow()
    {
        while (true)
        {
            if (autoThrow && childImage.gameObject.activeSelf)
            {
                childImage.ThrowBall();
                scoreManager.UpdateScorePool(-1);
            }
            yield return new WaitForSeconds(timerBetweenThrow);
        }
    }

    public void UpgradeAutoThrow()
    {
        if (scoreManager.GetScoreMeal() >= upgradeAutoThrow && timerBetweenThrow > 0.1f)
        {
            scoreManager.UpdateScoreMeal(-upgradeAutoThrow);
            upgradeAutoThrow *= 1.6f;
            autoThrowCostText.text = Math.Floor(upgradeAutoThrow).ToString();
            if (!autoThrow)
            {
                autoThrow = true;
                return;
            }
            timerBetweenThrow -= 0.1f;
            if (timerBetweenClick == 0.1f)
            {
                autoThrowCostText.text = "MAX";
            }
        }
    }
}
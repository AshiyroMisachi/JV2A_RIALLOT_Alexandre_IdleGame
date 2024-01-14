using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutoClicker : MonoBehaviour
{
    //Reference
    public ScoreManager scoreManager;

    //Private Var
    [SerializeField]
    private float timerBetweenClick = 1, powerClick = 0, upgradePowerClick = 20;

    //Shop UI
    public TextMeshProUGUI powerCostText;

    //Component
    public ParticleSystem myParticle;
    void Start()
    {
        //Find
        scoreManager = FindObjectOfType<ScoreManager>();
        myParticle = GetComponent<ParticleSystem>();


        StartCoroutine(PasiveClick());
    }

    public IEnumerator PasiveClick()
    {
        while (true)
        {
            scoreManager.UpdateScorePool(powerClick);
            yield return new WaitForSeconds(timerBetweenClick);
        }
    }

    public void UpgradePower()
    {
        if (scoreManager.GetScore() >= upgradePowerClick)
        {
            scoreManager.UpdateScorePool(-upgradePowerClick);
            upgradePowerClick *= 1.2f;
            powerCostText.text = Math.Floor(upgradePowerClick).ToString();
            powerClick++;
            UpdateEmission();
        }
    }

    public void UpdateEmission()
    {
        var myParticleEmission = myParticle.emission;
        myParticleEmission.enabled = true;
        myParticleEmission.rateOverTime = powerClick / timerBetweenClick;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AutoClick : MonoBehaviour
{
    //Var
    public int clickPower;
    public TextMeshProUGUI clickPower_Text;

    public int costUpgrade;
    public TextMeshProUGUI costUpgrade_Text;

    public ScoreManager scoreManager;
    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        StartCoroutine(autoClick());
    }

    public void upgradeClickPower()
    {
        if (scoreManager.GetScore() >= costUpgrade)
        {
            scoreManager.updateScore(-costUpgrade);
            costUpgrade += 10;
            clickPower += 1;

            costUpgrade_Text.text = costUpgrade.ToString("0000");
            clickPower_Text.text = clickPower.ToString("0000");
        }
    }

    public IEnumerator autoClick()
    {
        while (true)
        {
            scoreManager.updateScore(clickPower);
            yield return new WaitForSeconds(1);
        }
    }
}

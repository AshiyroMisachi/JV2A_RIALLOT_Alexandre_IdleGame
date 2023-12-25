using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingBall : MonoBehaviour
{
    //GameObject Reference
    public ScoreManager scoreManager;
    public GameObject ballShelf, ballPrefab, allBall;
    public Animator myAnimator;

    private void Start()
    {
        //Find Reference
        scoreManager = FindObjectOfType<ScoreManager>();
    }

    public void OnMouseDown()
    {
        myAnimator.SetTrigger("OnClick");
        for (int i = 0; i < scoreManager.GetBallNumber(); i++)
        {
            GameObject newBall = Instantiate(ballPrefab, ballShelf.transform.position, Quaternion.identity);
            newBall.transform.SetParent(allBall.transform, true);
            int X = Random.Range(1, 101);
            int[] currentRarity = (int[])scoreManager.allLevel[scoreManager.GetCurrentLevel()-1];
            if (X <= currentRarity[0])
            {
                newBall.gameObject.GetComponent<Ball>().myRarity = 0;
                scoreManager.UpdateScore(scoreManager.GetScorePower());
            }
            else if (X <= currentRarity[1])
            {
                newBall.gameObject.GetComponent<Ball>().myRarity = (BallRarity)1;
                scoreManager.UpdateScore(scoreManager.GetScorePower() * 2);
            }
            else if (X <= currentRarity[2])
            {
                newBall.gameObject.GetComponent<Ball>().myRarity = (BallRarity)2;
                scoreManager.UpdateScore(scoreManager.GetScorePower() * 3);
            }
            else if (X <= currentRarity[3])
            {
                newBall.gameObject.GetComponent<Ball>().myRarity = (BallRarity)3;
                scoreManager.UpdateScore(scoreManager.GetScorePower() * 4);
            }
            else
            {
                newBall.gameObject.GetComponent<Ball>().myRarity = (BallRarity)4;
                scoreManager.UpdateScore(scoreManager.GetScorePower() * 5);
            }
        }
    }
}

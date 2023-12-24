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
        scoreManager.UpdateScore(scoreManager.GetScorePower());
        GameObject newBall = Instantiate(ballPrefab, ballShelf.transform.position, Quaternion.identity);
        newBall.transform.SetParent(allBall.transform, true);
        newBall.gameObject.GetComponent<Ball>().myRarity = 0;
    }
}

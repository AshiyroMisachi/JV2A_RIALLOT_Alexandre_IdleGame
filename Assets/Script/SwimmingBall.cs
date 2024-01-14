using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SwimmingBall : MonoBehaviour
{
    //GameObject Reference
    public ScoreManager scoreManager;
    public GameObject ballShelf, ballPrefab, allBall;
    public Animator myAnimator;

    //Var
    public SciptablePool[] sciptablePools;
    [SerializeField]
    private int ballNeeded, ballNumber, scoreDrop;
    private float[] ballSpawn;
    public TextMeshProUGUI ballCount;

    [Header("Debug")]
    public bool autoSpawn;


    private void Start()
    {
        //Find Reference
        scoreManager = FindObjectOfType<ScoreManager>();
        AttributeNewValue(0);

        //Bebug Code
        StartCoroutine(DebugMode());
    }

    public void AttributeNewValue(int value)
    {
        transform.parent.transform.localScale = sciptablePools[value].myScale;
        transform.localPosition = sciptablePools[value].myPos;
        scoreManager.cameraPool.transform.position = sciptablePools[value].cameraPos;
        ballNeeded = sciptablePools[value].ballNeeded;
        ballSpawn = sciptablePools[value].ballSpawn;
        scoreDrop = sciptablePools[value].scoreDrop;
        gameObject.GetComponent<MeshFilter>().mesh = sciptablePools[value].mesh;
        gameObject.GetComponent<MeshCollider>().sharedMesh = sciptablePools[value].mesh;
        ballCount.text = ballNumber.ToString() + "/" + ballNeeded.ToString();
    }

    public void OnMouseDown()
    {
        GenerateBall();
    }
    public void GenerateBall()
    {
        //myAnimator.SetTrigger("OnClick");
        for (int i = 0; i < scoreManager.GetBallNumber(); i++)
        {
            Vector3 ballPosSpawn = new Vector3(Random.Range(-ballSpawn[0], ballSpawn[0]), ballSpawn[1], Random.Range(-ballSpawn[2], ballSpawn[2]));
            GameObject newBall = Instantiate(ballPrefab, ballPosSpawn, Quaternion.identity);
            newBall.transform.SetParent(allBall.transform, true);
            int X = Random.Range(1, 101);
            int[] currentRarity = (int[])scoreManager.allLevel[scoreManager.GetCurrentLevel() - 1];
            if (X <= currentRarity[0])
            {
                newBall.gameObject.GetComponent<Ball>().myRarity = 0;
                scoreManager.UpdateScorePool(scoreManager.GetScorePower());
            }
            else if (X <= currentRarity[1])
            {
                newBall.gameObject.GetComponent<Ball>().myRarity = (BallRarity)1;
                scoreManager.UpdateScorePool(scoreManager.GetScorePower() * 2);
            }
            else if (X <= currentRarity[2])
            {
                newBall.gameObject.GetComponent<Ball>().myRarity = (BallRarity)2;
                scoreManager.UpdateScorePool(scoreManager.GetScorePower() * 3);
            }
            else if (X <= currentRarity[3])
            {
                newBall.gameObject.GetComponent<Ball>().myRarity = (BallRarity)3;
                scoreManager.UpdateScorePool(scoreManager.GetScorePower() * 4);
            }
            else
            {
                newBall.gameObject.GetComponent<Ball>().myRarity = (BallRarity)4;
                scoreManager.UpdateScorePool(scoreManager.GetScorePower() * 5);
            }
            ballNumber++;
        }
        ballCount.text = ballNumber.ToString() + "/" + ballNeeded.ToString();
        if (ballNumber >= ballNeeded)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<MeshCollider>().enabled = false;
            ballNumber = 0;
            StartCoroutine(GenerateNewPool());
        }
    }

    public IEnumerator GenerateNewPool()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < allBall.transform.childCount; i++)
        {
            Destroy(allBall.transform.GetChild(i).gameObject);
        }
        scoreManager.UpdateScorePool(scoreDrop);
        //Generate New Pool
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<MeshCollider>().enabled = true;
        AttributeNewValue(Random.Range(0, sciptablePools.Length));

        //Open acces to Meal
        scoreManager.buttonGoTo.SetActive(true);
    }

    public IEnumerator DebugMode()
    {
        yield return new WaitForEndOfFrame();
        while (true)
        {
            if (autoSpawn && gameObject.GetComponent<MeshRenderer>().enabled)
            {
                GenerateBall();
            }
            yield return new WaitForSeconds(0.05f);
        }
    }
}

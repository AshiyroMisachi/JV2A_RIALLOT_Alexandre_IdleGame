using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Child : MonoBehaviour
{
    public ScoreManager scoreManager;
    public TextMeshProUGUI childName;
    public GameObject childImage, childLifebar;
    public ScriptableChild[] childs;
    public ScriptableChild childBoss;
    private List<ScriptableChild> childsActiveList;
    public ScriptableChild actualChild;
    public float hp;

    public GameObject ballPrefab;
    public GameObject allBall;

    void Start()
    {
        childsActiveList = childs.ToList<ScriptableChild>();
        scoreManager = FindObjectOfType<ScoreManager>();
        AttributeNewValue();
    }

    public void AttributeNewValue()
    {
        //Clear Ball in air
        KillAllBall();

        //Boss Child
        if (childsActiveList.Count == 0)
        {
            childsActiveList = childs.ToList<ScriptableChild>();
            actualChild = childBoss;
            childImage.transform.localPosition = new Vector3(0, 0, 22);
        }
        //Basic Child
        else
        {
            var randomNumber = Random.Range(0, childsActiveList.Count);
            actualChild = childsActiveList[randomNumber];
            childsActiveList.RemoveAt(randomNumber);
            childImage.transform.localPosition = new Vector3(0, 0, 15);
        }
        //Apply new value
        childImage.GetComponent<SpriteRenderer>().sprite = actualChild.sprite;
        childName.text = actualChild.myName;
        hp = actualChild.hp;
        childLifebar.GetComponent<Image>().fillAmount = 1f;

        //Resize Box Collider
        Destroy(childImage.GetComponent<BoxCollider>());
        childImage.AddComponent<BoxCollider>();
    }

    public void OnMouseDown()
    {
        if (scoreManager.GetScore() >= 1)
        {
            //Throw Ball on Child
            Camera myCamera = Camera.main;
            var spawnPosition = new Vector3(myCamera.transform.position.x, myCamera.transform.position.y - 1, myCamera.transform.position.z + 2);
            GameObject newBall = Instantiate(ballPrefab, allBall.transform);
            newBall.transform.position = spawnPosition;
            newBall.GetComponent<Rigidbody>().velocity = Vector3.forward * 10;
            scoreManager.UpdateScorePool(-1);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            StartCoroutine(loseHp(scoreManager.GetDamage()));
            Destroy(ball.gameObject);
        }
    }

    public IEnumerator loseHp(float damage)
    {
        hp -= damage;
        childLifebar.GetComponent<Image>().fillAmount = hp / actualChild.hp;
        childImage.GetComponent<SpriteRenderer>().color = Color.red;
        if (hp <= 0)
        {
            scoreManager.UpdateScoreMeal(actualChild.mealLoot);
            AttributeNewValue();
        }
        yield return new WaitForSeconds(0.2f);
        childImage.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void KillAllBall()
    {
        if (allBall.transform.childCount > 0)
        {
            for (int i = 0; i < allBall.transform.childCount; i++)
            {
                Destroy(allBall.transform.GetChild(i).gameObject);
                //Refund Ball who got destroy
                scoreManager.UpdateScorePool(1);
            }
        }
    }
}
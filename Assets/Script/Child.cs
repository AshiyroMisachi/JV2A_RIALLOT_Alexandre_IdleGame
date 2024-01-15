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
    [SerializeField]
    private List<ScriptableChild> childsActiveList;
    public ScriptableChild actualChild;
    public float hp;

    public GameObject ballPrefab;

    void Start()
    {
        childsActiveList = childs.ToList<ScriptableChild>();
        scoreManager = FindObjectOfType<ScoreManager>();
        AttributeNewValue();
    }

    public void AttributeNewValue()
    {
        if (childsActiveList.Count == 0)
        {
            childsActiveList = childs.ToList<ScriptableChild>();
            //Spawn Megazord

            return;
        }
        var randomNumber = Random.Range(0, childsActiveList.Count);
        actualChild = childsActiveList[randomNumber];
        childsActiveList.RemoveAt(randomNumber);
        childImage.GetComponent<SpriteRenderer>().sprite = actualChild.sprite;
        childName.text = actualChild.myName;
        hp = actualChild.hp;
        childLifebar.GetComponent<Image>().fillAmount = 1f;
    }

    public void OnMouseDown()
    {
        if (scoreManager.GetScore() >= 1)
        {
            //Throw Ball on Child
            Camera myCamera = Camera.main;
            var spawnPosition = new Vector3(myCamera.transform.position.x, myCamera.transform.position.y - 1, myCamera.transform.position.z + 2 );
            GameObject newBall = Instantiate(ballPrefab, spawnPosition, Quaternion.identity);
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
        childLifebar.GetComponent<Image>().fillAmount = hp/actualChild.hp;
        childImage.GetComponent<SpriteRenderer>().color = Color.red;
        if (hp <= 0)
        {
            scoreManager.UpdateScoreMeal(actualChild.mealLoot);
            AttributeNewValue();
        }
        yield return new WaitForSeconds(0.2f);
        childImage.GetComponent<SpriteRenderer>().color = Color.white;
    }
}

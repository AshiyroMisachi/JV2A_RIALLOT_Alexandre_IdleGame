using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private float timer;
    public MeshRenderer myRenderer;
    public BallRarity myRarity;
    public GameObject[] rarityVFX, destroyVFX;

    private void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
        myRenderer.material.color = new Color(Random.Range(0f,1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        Instantiate(rarityVFX[(int)myRarity], transform);
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 10)
        {
            Destroy(gameObject);
            Instantiate(destroyVFX[(int)myRarity], transform.position, Quaternion.identity);
        }
    }
}

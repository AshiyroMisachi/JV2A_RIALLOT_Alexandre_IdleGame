using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private float timer;
    public MeshRenderer myRenderer;
    public BallRarity myRarity;
    public Material[] rarityMaterial;
    public GameObject[] rarityVFX, destroyVFX;
    private float colorSample = 0.2f;

    private void Start()
    {
        myRenderer = GetComponent<MeshRenderer>();
        if (myRarity != BallRarity.None)
        {
            myRenderer.material = rarityMaterial[(int)myRarity];
            myRenderer.material.color = new Color(myRenderer.material.color.r + Random.Range(-colorSample, colorSample), myRenderer.material.color.g + Random.Range(-colorSample, colorSample), myRenderer.material.color.b + Random.Range(-colorSample, colorSample));
            Instantiate(rarityVFX[(int)myRarity], transform);
        }
        else
        {
            myRenderer.material.color = new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f));
        }
    }

    public void OnDestroy()
    {
        if (myRarity != BallRarity.None)
            Instantiate(destroyVFX[(int)myRarity], transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}

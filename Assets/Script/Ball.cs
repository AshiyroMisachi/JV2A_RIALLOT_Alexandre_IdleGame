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
        myRenderer.material = rarityMaterial[(int)myRarity];
        myRenderer.material.color = new Color(myRenderer.material.color.r + Random.Range(-colorSample, colorSample), myRenderer.material.color.g + Random.Range(-colorSample, colorSample), myRenderer.material.color.b + Random.Range(-colorSample, colorSample));
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

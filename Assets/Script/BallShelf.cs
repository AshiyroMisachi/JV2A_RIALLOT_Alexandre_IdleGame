using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShelf : MonoBehaviour
{
    [SerializeField]
    private float timer = 0;
    private bool lerpFactorOn = true;
    public Vector3 posMin, posMax;
    void Start()
    {
        
    }

    void Update()
    {
        if (lerpFactorOn)
        {
            timer += Time.deltaTime;
        }
        else
        {
            timer -= Time.deltaTime;
        }

        transform.position = Vector3.Lerp(posMin, posMax, timer);

        if (timer > 1 && lerpFactorOn)
        {
            lerpFactorOn = false;
        }
        else if (timer < 0 && !lerpFactorOn)
        {
            lerpFactorOn = true;
        }
    }
}
    
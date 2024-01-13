using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Pool", menuName = "SwimmingBall", order = 0)]
public class SciptablePool : ScriptableObject
{
    public int ballNeeded;
    public Mesh mesh;
    public float[] ballSpawn; 
}

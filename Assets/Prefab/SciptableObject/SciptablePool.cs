using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Pool", menuName = "SwimmingBall", order = 0)]
public class SciptablePool : ScriptableObject
{
    public Vector3 myPos, myScale, cameraPos;
    public int ballNeeded, scoreDrop;
    public Mesh mesh;
    public float[] ballSpawn; 
}

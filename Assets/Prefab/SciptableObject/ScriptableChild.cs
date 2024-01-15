using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New_Child", menuName = "Child", order = 0)]
public class ScriptableChild : ScriptableObject
{
    public Sprite sprite;
    public string myName;
    public int hp, mealLoot;
}

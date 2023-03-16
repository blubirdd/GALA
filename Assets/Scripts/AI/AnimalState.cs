using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New State", menuName = "Animal/State")]
public class AnimalState : ScriptableObject
{
    [Header("Sprites")]

    public Sprite normal;
    public Sprite runningAway;
    public Sprite eating;
    public Sprite drinking;
    public Sprite hunting;

    [Header("Quest Sprite")]
    public Sprite injured;

}

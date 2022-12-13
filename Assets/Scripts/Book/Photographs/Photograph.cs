using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Photograph", menuName = "Picture/Photograph")]
public class Photograph : ScriptableObject
{
    new public string name = "New Picture";
    public string animalGroup;
    public Sprite polaroidPhoto = null;

    [Header("Stats")]
    public string lifeSpan;
    public string weight;
    public string height;
    public string length;

    [Header("Diet")]
    public string diet;
    public string biome;

    [Header("Details")]
    [TextArea(3, 10)]
    public string details;

}

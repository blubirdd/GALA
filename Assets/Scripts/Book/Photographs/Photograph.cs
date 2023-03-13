using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Photograph", menuName = "Picture/Photograph")]
public class Photograph : ScriptableObject
{
    new public string name = "New Picture";
    public string scientificName;
    public string animalGroup;
    public Sprite polaroidPhoto = null;

    [Header("Stats")]
    public string lifeSpan = "Life Span: ";
    public string weight = "Weight: ";
    public string height = "Height: ";
    public string length = "Length: ";

    [Header("Diet")]
    public Sprite biomeImage;
    public string biome = "";
    public string diet = "Diet: ";
    public string food = "Food: ";

    [Header("Details")]
    [TextArea(3, 10)]
    public string appearance;

    [TextArea(3, 10)]
    public string details;

}

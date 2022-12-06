using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Photograph", menuName = "Picture/Photograph")]
public class Photograph : ScriptableObject
{
    new public string name = "New Picture";
    public string animalGroup;
    public Sprite polaroidPhoto = null;
    

    public string lifeSpan;
    public string height;
    public string width;

}

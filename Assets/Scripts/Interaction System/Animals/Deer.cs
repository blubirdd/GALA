using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deer : MonoBehaviour, IAnimal
{

    PictureGoal pictureGoal;
    public string animalName { get; set; }

    void Start()
    {
        animalName = "Deer";
    }

    
    public void Discovered()
    {
        PictureEvents.AnimalDiscovered(this);
       
    }
}

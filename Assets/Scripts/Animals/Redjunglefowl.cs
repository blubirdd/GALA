using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Redjunglefowl : MonoBehaviour, IAnimal
{

    public string animalName { get; set; }
    public string animalGroup { get; set; }

    public Photograph photo;
    void Start()
    {
        animalName = photo.name;
        animalGroup = photo.animalGroup;
    }

    
    public void Discovered()
    {
        PictureEvents.AnimalDiscovered(this);
        Book.instance.AddAnimalPhoto(photo);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Tamaraw : MonoBehaviour, IAnimal
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

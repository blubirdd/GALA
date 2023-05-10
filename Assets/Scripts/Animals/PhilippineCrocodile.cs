using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhilippineCrocodile : MonoBehaviour, IAnimal
{
    public string animalName { get; set; }
    public string animalGroup { get; set; }
    public string scientificName { get; set; }
    public Photograph photo;
    void Start()
    {
        animalName = photo.name;
        animalGroup = photo.animalGroup;
        scientificName = photo.scientificName;
    }


    public void Discovered()
    {
        PictureEvents.AnimalDiscovered(this);
        Book.instance.AddAnimalPhoto(photo);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tarsier : MonoBehaviour, IAnimal
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

        if (!Book.instance.photosInventory.Contains(photo))
        {
            Debug.Log("NEWLY ISCOVERED ADDED TO DATABASE: " + animalName);

            StartCoroutine(WaitForPhoto());
            IEnumerator WaitForPhoto()
            {
                yield return new WaitForEndOfFrame();
                Inventory.instance.itemDiscovery.NewItemDiscovered(photo.polaroidPhoto, photo.name, "New Animal Discovered. Check your journal for more details", false);
            }

        }

        Book.instance.AddAnimalPhoto(photo);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Book : MonoBehaviour
{
    #region Singleton

    public static Book instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of book found");
            return;
        }

        instance = this;
    }
    #endregion

    public delegate void OnPictureAdded();
    public OnPictureAdded OnPictureAddedCallback;

    public List<Photograph> photosInventory = new List<Photograph>();
    public List<Photograph> photosMammal = new List<Photograph>();
    public List<Photograph> photosBird = new List<Photograph>();
    public List<Photograph> photosReptile = new List<Photograph>();


    public string recentAnimalDiscovered;
    public void AddAnimalPhoto(Photograph photo)
    {
        if (!photosInventory.Contains(photo))
        {
            photosInventory.Add(photo);

            if(photo.animalGroup =="Terrestrial Mammal")
            {
                photosMammal.Add(photo);
            }

            if(photo.animalGroup == "Bird")
            {
                photosBird.Add(photo);
            }

            if (photo.animalGroup == "Reptile")
            {
                photosReptile.Add(photo);
            }


            Debug.Log("Took a picture of " + photo.name);

            recentAnimalDiscovered = photo.name;

            if (OnPictureAddedCallback != null)
            {
                OnPictureAddedCallback.Invoke();
                Debug.Log("Invoked");
           
            }

        }
    }
}

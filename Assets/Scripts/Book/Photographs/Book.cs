using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;


public class Book : MonoBehaviour, IDataPersistence
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


    //savable photos
    [NonReorderable] public List<PhotosSave> photosContainer = new List<PhotosSave>();

    public List<Photograph> photosInventory = new List<Photograph>();
    public List<Photograph> photosMammal = new List<Photograph>();
    public List<Photograph> photosBird = new List<Photograph>();
    public List<Photograph> photosReptile = new List<Photograph>();

    public List<Photograph> photosAquatic = new List<Photograph>();

    public List<ThreatScriptable> photosThreat = new List<ThreatScriptable>();

    public string recentAnimalDiscovered;

    public BookUI bookui;
    //database
    [SerializeField] private PhotographDatabase database;
    private void OnEnable()
    {
#if UNITY_EDITOR
        database = (PhotographDatabase)AssetDatabase.LoadAssetAtPath("Assets/Resources/PhotosDatabase.asset", typeof(PhotographDatabase));

#else
        database = Resources.Load<PhotographDatabase>("PhotosDatabase");
#endif
    }

    public void AddThreatPhoto(ThreatScriptable photo)
    {
        if (!photosThreat.Contains(photo))
        {
            photosThreat.Add(photo);

            Debug.Log("Took a picture of " + photo.threatName);

            if (OnPictureAddedCallback != null)
            {
                OnPictureAddedCallback.Invoke();
                Debug.Log("Invoked");

            }
        }
    }

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

            if (photo.animalGroup == "Aquatic")
            {
                photosAquatic.Add(photo);
            }

            //saving
            photosContainer.Add(new PhotosSave(database.getId[photo], photo));

            Debug.Log("Took a picture of " + photo.name);

            recentAnimalDiscovered = photo.name;

            if (OnPictureAddedCallback != null)
            {
                OnPictureAddedCallback.Invoke();
                Debug.Log("Picture Invoked");
           
            }

        }
    }

    [System.Serializable]
    public class PhotosSave
    {
        public int ID;
        public Photograph photo;

        public PhotosSave(int _id, Photograph _photo)
        {
            ID = _id;
            photo = _photo;
        }
    }

    public void LoadData(GameData data)
    {
        StartCoroutine(WaitToInitialize());
        IEnumerator WaitToInitialize()
        {
            yield return new WaitForEndOfFrame();
            foreach (KeyValuePair<int, PhotosSave> keyValuePair in data.photosCollected)
            {
                //this method works by getting the item using the id
                AddAnimalPhoto(database.getPhoto[keyValuePair.Value.ID]);
            }

            bookui.UpdateBookAtStart();
        }

    }

    public void SaveData(GameData data)
    {        //clear current dictionary
        data.photosCollected.Clear();

        //Add inventory to gamedata inventory dictionary
        for (int i = 0; i < photosContainer.Count; i++)
        {
            data.photosCollected.Add(photosContainer[i].ID, photosContainer[i]);
        }
    }

    public void UnlockAllPhotos()
    {
        for (int i = 0; i < database.photos.Length; i++)
        {
            AddAnimalPhoto(database.photos[i]);
        }
        bookui.UpdateBookAtStart();
    }
}

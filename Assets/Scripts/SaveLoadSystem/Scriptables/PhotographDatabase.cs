using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Photograph Database", menuName = "Photo/Photos/Database")]
public class PhotographDatabase : ScriptableObject, ISerializationCallbackReceiver
{
    public Photograph[] photos;
    public Dictionary<Photograph, int> getId = new Dictionary<Photograph, int>();
    public Dictionary<int, Photograph> getPhoto = new Dictionary<int, Photograph>();

    public void OnAfterDeserialize()
    {
        getId = new Dictionary<Photograph, int>();
        getPhoto = new Dictionary<int, Photograph>();

        for (int i = 0; i < photos.Length; i++)
        {
            getId.Add(photos[i], i);
            getPhoto.Add(i, photos[i]);

        }
    }

    public void OnBeforeSerialize()
    {

    }
}

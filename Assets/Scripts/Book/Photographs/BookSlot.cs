using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class BookSlot : MonoBehaviour
{
    public Image polaroidPhoto;

    Photograph photo;
    public void AddPhoto(Photograph newPhoto)
    {
        photo = newPhoto;

        polaroidPhoto.sprite = photo.polaroidPhoto;
        polaroidPhoto.enabled = true;

    }



}

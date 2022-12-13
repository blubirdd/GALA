using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BookSlot : MonoBehaviour
{
    public Image polaroidPhoto;

    
    [SerializeField] private TextMeshProUGUI _animalName;
    [SerializeField] private TextMeshProUGUI _lifeSpan;
    [SerializeField] private TextMeshProUGUI _weight;
    [SerializeField] private TextMeshProUGUI _height;
    [SerializeField] private TextMeshProUGUI _length;

    [SerializeField] private TextMeshProUGUI _diet;
    [SerializeField] private TextMeshProUGUI _biome;

    [SerializeField] private TextMeshProUGUI _details;


    Photograph photo;

    private void Awake()
    {

       
    }
    public void AddPhoto(Photograph newPhoto)
    {

            photo = newPhoto;

            polaroidPhoto.sprite = photo.polaroidPhoto;
            polaroidPhoto.enabled = true;

            //set the book ui
            _animalName.SetText(photo.name);

            _lifeSpan.SetText(photo.lifeSpan);
            _weight.SetText(photo.weight);
            _height.SetText(photo.height);
            _length.SetText(photo.length);

            _diet.SetText(photo.diet);
            _biome.SetText(photo.biome);

            _details.SetText(photo.details);



    }



}

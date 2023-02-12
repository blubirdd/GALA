using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BookSlot : MonoBehaviour
{
    public Image polaroidPhoto;

    [Header("Upper Left")]
    [SerializeField] private TextMeshProUGUI _animalName;
    [SerializeField] private TextMeshProUGUI _lifeSpan;
    [SerializeField] private TextMeshProUGUI _weight;
    [SerializeField] private TextMeshProUGUI _height;
    [SerializeField] private TextMeshProUGUI _length;
    [SerializeField] private Image _biomeImage;

    [Header("Lower Left")]
    public Image dietImage;
    [SerializeField] private TextMeshProUGUI _biome;
    [SerializeField] private TextMeshProUGUI _diet;
    [SerializeField] private TextMeshProUGUI _food;

    [Header("Right Side")]
    [SerializeField] private TextMeshProUGUI _details;
    [SerializeField] private TextMeshProUGUI _appearance;

//    TextMeshProUGUI[] allTexts = new TextMeshProUGUI[10];

    Photograph photo;

    private void Awake()
    {
        //TextMeshProUGUI[] allTexts = { _animalName, _lifeSpan, _weight, _height, _length, _biome, _diet, _food, _details, _appearance };
        //for (int i = 0; i < allTexts.Length; i++)
        //{
        //    allTexts[i].gameObject.SetActive(false);
        //}

    }
    public void AddPhoto(Photograph newPhoto)
    {
        photo = newPhoto;

        //TextMeshProUGUI[] allTexts = { _animalName, _lifeSpan, _weight, _height, _length, _biome, _diet, _food, _details, _appearance };
        //for (int i = 0; i < allTexts.Length; i++)
        //{
        //    allTexts[i].gameObject.SetActive(true);
        //}

        polaroidPhoto.sprite = photo.polaroidPhoto;
        polaroidPhoto.enabled = true;

        dietImage.enabled = true;
        //set the book ui
        _animalName.SetText(photo.name);

        _lifeSpan.SetText(photo.lifeSpan);
        _weight.SetText(photo.weight);
        _height.SetText(photo.height);
        _length.SetText(photo.length);
        _biomeImage.sprite = photo.biomeImage;

        _diet.SetText(photo.diet);
        _biome.SetText(photo.biome);
        _food.SetText(photo.food);

        _appearance.SetText(photo.appearance);
        _details.SetText(photo.details);

    }



}

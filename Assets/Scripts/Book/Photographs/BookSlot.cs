using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class BookSlot : MonoBehaviour
{
    public Image polaroidPhoto;

    [Header("Upper Left")]
    [SerializeField] private TextMeshProUGUI _animalName;
    [SerializeField] private TextMeshProUGUI _scientificName;
    [SerializeField] private TextMeshProUGUI _lifeSpan;
    [SerializeField] private TextMeshProUGUI _weight;
    [SerializeField] private TextMeshProUGUI _height;
    [SerializeField] private TextMeshProUGUI _length;
    [SerializeField] private Image _biomeImage;
    [Header("NEW ADDITIONS")]
    [SerializeField] private GameObject questionMark;

    [Header("Lower Left")]
    public Image dietImage;
    [SerializeField] private TextMeshProUGUI _biome;

    public Image dietBG;
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
        questionMark.SetActive(false);

        dietImage.enabled = true;

        //set the book ui
        //_animalName.SetText(photo.name + " (" + photo.scientificName + ")");
        _animalName.SetText(photo.name);
        _scientificName.SetText(photo.scientificName);
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

        if (_diet.text == "Diet Type: Herbivore")
        {
            dietBG.color = new UnityEngine.Color(0.7f, 1.0f, 0.7f);
        
        }

        if (_diet.text == "Diet Type: Carnivore")
        {
            dietBG.color = new UnityEngine.Color(1.0f, 0.7f, 0.7f);

        }

    }



}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BookUI : MonoBehaviour
{
    Book book;
    
    BookSlot[] slotsMammals;
    BookSlot[] slotsBirds;
    BookSlot[] slotsReptiles;
    BookSlot[] slotsAquatic;


    public Transform mammalsCategory;
    public Transform birdsCategory;
    public Transform reptilesCategory;
    public Transform aquaticCategory;

    private string _animalGroup;


    void Start()
    {
        book = Book.instance;
        book.OnPictureAddedCallback += UpdateBookUI;

        PictureEvents.onAnimalDiscovered += GetAnimalGroup;

        slotsMammals = mammalsCategory.GetComponentsInChildren<BookSlot>(true);
        slotsBirds = birdsCategory.GetComponentsInChildren<BookSlot>(true);
        slotsReptiles = reptilesCategory.GetComponentsInChildren<BookSlot>(true);
        slotsAquatic = aquaticCategory.GetComponentsInChildren<BookSlot>(true);

    }

    public void UpdateBookAtStart()
    {
        UpdateMammals();
        UpdateBirds();
        UpdateReptiles();
        UpdateAquatic();
    }

    void GetAnimalGroup(IAnimal animal)
    {
        _animalGroup = animal.animalGroup;


    }
    void UpdateBookUI()
    {
        if (_animalGroup == "Terrestrial Mammal")
        {
            UpdateMammals();
            IndicatorController.instance.categoriesCircle[0].SetActive(true);
        }

        if(_animalGroup == "Bird")
        {
            UpdateBirds();
            IndicatorController.instance.categoriesCircle[1].SetActive(true);
        }

        if (_animalGroup == "Reptile")
        {
            UpdateReptiles();
            IndicatorController.instance.categoriesCircle[2].SetActive(true);
        }

        if (_animalGroup == "Aquatic")
        {
            UpdateAquatic();
            IndicatorController.instance.categoriesCircle[3].SetActive(true);
        }
        IndicatorController.instance.EnableBookRedCircle();
    }

    void UpdateMammals()
    {
        for (int i = 0; i < slotsMammals.Length; i++)
        {
            if (i < book.photosMammal.Count)
            {
                slotsMammals[i].AddPhoto(book.photosMammal[i]);

            }
        }
    }

    void UpdateBirds()
    {
        for (int i = 0; i < slotsBirds.Length; i++)
        {
            if (i < book.photosBird.Count)
            {
                slotsBirds[i].AddPhoto(book.photosBird[i]);

            }
        }

    }


    void UpdateReptiles()
    {
        for (int i = 0; i < slotsReptiles.Length; i++)
        {
            if (i < book.photosReptile.Count)
            {
                slotsReptiles[i].AddPhoto(book.photosReptile[i]);

            }
        }

    }

    void UpdateAquatic()
    {
        for (int i = 0; i < slotsAquatic.Length; i++)
        {
            if (i < book.photosAquatic.Count)
            {
                slotsAquatic[i].AddPhoto(book.photosAquatic[i]);

            }
        }

    }
}

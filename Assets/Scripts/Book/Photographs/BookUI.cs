using System.Collections;
using System.Collections.Generic;
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


    void GetAnimalGroup(IAnimal animal)
    {
        _animalGroup = animal.animalGroup;


    }
    void UpdateBookUI()
    {
        if (_animalGroup == "Terrestrial Mammal")
        {
            UpdateMammals();

        }

        if(_animalGroup == "Bird")
        {
            UpdateBirds();
        }

        if (_animalGroup == "Reptile")
        {
            UpdateReptiles();
        }

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

}

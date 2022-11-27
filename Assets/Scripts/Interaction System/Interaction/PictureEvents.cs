using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureEvents : MonoBehaviour
{
    public delegate void AnimalEventHandler(IAnimal animal);

    public static event AnimalEventHandler onAnimalDiscovered;

    public static void AnimalDiscovered(IAnimal animal)
    {
        if(onAnimalDiscovered != null)
        {
           onAnimalDiscovered(animal);
        }
        
    }

}

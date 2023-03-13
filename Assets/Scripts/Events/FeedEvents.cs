using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeedEvents : MonoBehaviour
{
    public delegate void FeedEventsHandler(IAnimal animal);

    public static event FeedEventsHandler onAnimalFed;

    public static void AnimalFed(IAnimal animal)
    {
        if(onAnimalFed != null)
        {
            onAnimalFed(animal);
        }
    }
}

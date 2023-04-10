using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureEvents : MonoBehaviour
{
    public delegate void AnimalEventHandler(IAnimal animal);

    public static event AnimalEventHandler onAnimalDiscovered;

    public delegate void ThreatEventHandler(IThreat threat);

    public static event ThreatEventHandler onThreatDiscovered;
    public static void AnimalDiscovered(IAnimal animal)
    {
        if(onAnimalDiscovered != null)
        {
           onAnimalDiscovered(animal);
        }
        
    }

    public static void ThreatDiscovered(IThreat threat)
    {
        if (onThreatDiscovered != null)
        {
            onThreatDiscovered(threat);
        }
    }

}

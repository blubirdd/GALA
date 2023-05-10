using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAnimal
{
     string animalName { get; set; }
     string animalGroup { get; set; }

     string scientificName { get; set; }
    void Discovered();
}
    
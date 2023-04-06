using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IThreat
{
    string threatName { get; set; }
    void Discovered();
}

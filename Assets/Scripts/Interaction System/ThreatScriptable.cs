using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Threat", menuName = "Picture/ThreatPhotograph")]
public class ThreatScriptable : ScriptableObject
{
    public string threatName;
    public Sprite threatPicture;

}

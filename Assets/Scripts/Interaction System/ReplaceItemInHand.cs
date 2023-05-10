using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceItemInHand : MonoBehaviour
{
    public Equipment equipmentToPut;

    public void ReplaceItem()
    {
        EquipmentManager.instance.Equip(equipmentToPut);
        SoundManager.instance.PlaySoundFromClips(12);
    }
}

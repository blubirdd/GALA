using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragParentObject : MonoBehaviour
{
    public List<Transform> childObjects;
    private List<Vector3> initialPositions;

    void Awake()
    {
        // Save the initial positions of the child objects
        initialPositions = new List<Vector3>();
        for (int i = 0; i < childObjects.Count; i++)
        {
            initialPositions.Add(childObjects[i].localPosition);
        }
    }

    void OnEnable()
    {
        Shuffle(childObjects);
        for (int i = 0; i < childObjects.Count; i++)
        {
            // Set the new position to the initial position
            Vector3 newPosition = initialPositions[i];

            // Set the new position for the child object
            childObjects[i].localPosition = newPosition;

            // Set the new sibling index for the child object
            childObjects[i].SetSiblingIndex(i);
        }
    }

    void Shuffle<T>(List<T> list)
    {
        // Fisher-Yates shuffle algorithm
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = Random.Range(0, n + 1);
            T temp = list[k];
            list[k] = list[n];
            list[n] = temp;
        }
    }
}

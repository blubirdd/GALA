using UnityEngine;

public class RotateStand : MonoBehaviour
{
    private float rotSpeed = 10f;
    void Update()
    {
        transform.Rotate(-Vector3.up * (rotSpeed * Time.deltaTime));
    }
}

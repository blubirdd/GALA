using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class WaypointUI : MonoBehaviour
{

    public Image img;

    public Transform target;

    public TextMeshProUGUI meter;

    public Vector3 offset;

    public Transform playerLocation;

    private float _distance;
    private void Start()
    {
        playerLocation = GameObject.FindGameObjectWithTag("Player").transform;

        StartCoroutine(UpdateWaypointWithDelay(0.2f));
    }
    public void SetTarget(Transform location)
    {
        target = location;
    }

    private void LateUpdate()
    {
        float minX = img.GetPixelAdjustedRect().width / 2;

        // Maximum X position: screen width - half of the icon width
        float maxX = Screen.width - minX;

        // Minimum Y position: half of the height
        float minY = img.GetPixelAdjustedRect().height / 2;

        // Maximum Y position: screen height - half of the icon height
        float maxY = Screen.height - minY;

        // Temporary variable to store the converted position from to 2D screen point
        Vector2 pos = Camera.main.WorldToScreenPoint(target.position + offset);


        // Check if the target is behind ,
        if (Vector3.Dot((target.position - Camera.main.transform.position), Camera.main.transform.forward) < 0)
        {
            // Check if the target is on the left side of the screen
            if (pos.x < Screen.width / 2)
            {
                // Place it on the right
                pos.x = maxX;
            }
            else
            {
                // Place it on the left side
                pos.x = minX;
            }
        }

        // Limit the X and Y positions
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        //update position each time
        img.transform.position = pos;
    }

    IEnumerator UpdateWaypointWithDelay(float delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            _distance = Vector3.Distance(target.position, playerLocation.position);

            // Change the meter text to the distance with the meter unit 'm'
            meter.text = _distance.ToString("0") + "m";
            if (_distance >= 5)
            {
                img.gameObject.SetActive(true);
            }

            else
            {
                img.gameObject.SetActive(false);
            }


        }
    }


}

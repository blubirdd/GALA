using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AstarPath
{
    public readonly Vector3[] lookPoints;
    public readonly Line[] turnBoundaries;
    public readonly int finishLineIndex;
    public readonly int slowDownIndex;

    public AstarPath(Vector3[] waypoints, Vector3 startPos, float turnDst, float stoppingDst)
    {
        lookPoints = waypoints;
        turnBoundaries = new Line[lookPoints.Length];
        finishLineIndex = turnBoundaries.Length - 1;

        Vector2 previousPoint = V3ToV2(startPos);
        for (int i = 0; i < lookPoints.Length; i++)
        {
            Vector2 currentPoint = V3ToV2(lookPoints[i]);
            Vector2 dirToCurrentPoint = (currentPoint - previousPoint).normalized;
            Vector2 turnBoundaryPoint = (i == finishLineIndex) ? currentPoint : currentPoint - dirToCurrentPoint * turnDst;
            turnBoundaries[i] = new Line(turnBoundaryPoint, previousPoint - dirToCurrentPoint * turnDst);
            previousPoint = turnBoundaryPoint;
        }

        float dstFromEndPoint = 0;
        for (int i = lookPoints.Length - 1; i > 0; i--)
        {
            dstFromEndPoint += Vector3.Distance(lookPoints[i], lookPoints[i - 1]);
            if (dstFromEndPoint > stoppingDst)
            {
                slowDownIndex = i;
                break;
            }
        }
    }

    Vector2 V3ToV2(Vector3 v3)
    {
        return new Vector2(v3.x, v3.z);
    }

    public void DrawWithGizmos()
    {
        Gizmos.color = Color.yellow;
        for (int i = 0; i < lookPoints.Length; i++)
        {
            Vector3 p = lookPoints[i];
            Gizmos.DrawCube(p + Vector3.up, Vector3.one);
            // Check if there is a next point
            if (i < lookPoints.Length - 1)
            {
                // Draw a line to the next point
                Vector3 nextPoint = lookPoints[i + 1];
                Gizmos.DrawLine(p + Vector3.up, nextPoint + Vector3.up);
            }
        }

        //Gizmos.color = Color.white;
        //foreach (Line l in turnBoundaries)
        //{
        //    l.DrawWithGizmos(10);
        //}

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathRendering : MonoBehaviour
{

    private void OnDrawGizmos()
    {
        for (int i = 0; i < transform.childCount; i ++)
        {
            Transform waypoint = transform.GetChild(i);
            int nextIndex = i + 1;
            if (nextIndex == transform.childCount)
            {
                nextIndex = 0;
            }
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(waypoint.position, 0.5f);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(waypoint.position, transform.GetChild(nextIndex).position);
        }
    }
}

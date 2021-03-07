using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : MonoBehaviour
{
    public Transform waypointPath;
    public float speed = 2f;
    public float rotationSpeed = 90f;
    public float waypointWaitTime = 1;
    IEnumerator currentMoveCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        currentMoveCoroutine = FollowPath();
        StartCoroutine(currentMoveCoroutine);
    }

    IEnumerator FollowPath()
    {
        foreach(Transform waypoint in waypointPath)
        {
            yield return StartCoroutine(RotateToFace(waypoint.position));
            yield return StartCoroutine(Move(waypoint.position));
            yield return new WaitForSeconds(waypointWaitTime);
        }
        currentMoveCoroutine = FollowPath();
        StartCoroutine(currentMoveCoroutine);
    }

    IEnumerator RotateToFace(Vector3 target)
    {

        Vector3 targetDirection = (target - transform.position).normalized;
        float targetAngle = 90 - Mathf.Atan2(targetDirection.z, targetDirection.x) * Mathf.Rad2Deg;
        while (Mathf.Abs(Mathf.DeltaAngle(transform.rotation.eulerAngles.y, targetAngle)) > .05f)
        {
            Debug.DrawRay(transform.position, targetDirection * 10, Color.red);
            float newAngle = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.y, targetAngle, rotationSpeed * Time.deltaTime);
            transform.eulerAngles = Vector3.up * newAngle;
            yield return null;
        }
    }

    IEnumerator Move(Vector3 destination)
    {
        destination.y = 1;
        while (transform.position != destination)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            yield return null;
        }
    }
}

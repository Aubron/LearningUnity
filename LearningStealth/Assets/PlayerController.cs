using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 4f;
    public float turnSpeed = 8;
    Vector3 direction;
    float velocity;
    float targetAngle;
    new Rigidbody rigidbody;
    float smoothInputMagnitude;
    float smoothVelocity;
    float angle;
    public float smoothMovementSpeed = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector3(Input.GetAxisRaw("Horizontal"),0, Input.GetAxisRaw("Vertical")).normalized;
        float inputMagnitude = direction.magnitude;
        smoothInputMagnitude = Mathf.SmoothDamp(smoothInputMagnitude, inputMagnitude, ref smoothVelocity, smoothMovementSpeed);
        if (inputMagnitude > 0)
        {
            targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            angle = Mathf.LerpAngle(transform.rotation.eulerAngles.y, targetAngle, Time.deltaTime * turnSpeed);
        }
        velocity = speed * smoothInputMagnitude;
    }

    private void FixedUpdate()
    {
        rigidbody.MoveRotation(Quaternion.Euler(0,angle,0));
        rigidbody.MovePosition(rigidbody.position + transform.forward * velocity * Time.deltaTime);
    }
}

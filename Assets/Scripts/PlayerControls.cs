using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    [Flags]
    public enum RotationDirection
    {
        None,
        Horizontal = (1 << 0),
        Vertical = (1 << 1)
    }

    [Tooltip("Which directions this object can rotate")]
    [SerializeField] private RotationDirection rotationDirections;
    [Tooltip("The rotation acceleration, in degrees / second")]
    [SerializeField] private Vector2 acceleration;
    [Tooltip("A multiplier to the input. Describes the maximum speed in degrees / second. To flip vertical rotation, set Y to a negative value")]
    [SerializeField] private Vector2 sensitivity;
    [Tooltip("The maximum angle from the horizon the player can rotate, in degrees")]
    [SerializeField] private float maxVerticalAngleFromHorizon;
    [Tooltip("The period to wait until resetting the input value. Set this as low as possible, without encountering stuttering")]
    [SerializeField] private float inputLagPeriod;

    private Vector2 velocity;
    private Vector2 rotation;
    private Vector2 lastInputEvent;
    private float inputLagTimer;
    public Camera cam;
    public GameObject player;
    public float speed = 10.0f;
    public float lookSpeed = 10.0f;

    public bool onlyMouse = false;

    private void Start() { }

    private void OnEnable()
    {
        velocity = Vector2.zero;
        inputLagTimer = 0;
        lastInputEvent = Vector2.zero;

        Vector3 euler = transform.localEulerAngles;

        if (euler.x >= 180)
        {
            euler.x -= 360;
        }
        euler.x = ClampVerticalAngle(euler.x);

        transform.localEulerAngles = euler;

        rotation = new Vector2(euler.y, euler.x);
    }

    private float ClampVerticalAngle(float angle)
    {
        return Mathf.Clamp(angle, -maxVerticalAngleFromHorizon, maxVerticalAngleFromHorizon);
    }

    private Vector2 GetInput()
    {
        inputLagTimer += Time.deltaTime;

        Vector2 input = new Vector2(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y")
        );

        if ((Mathf.Approximately(0, input.x) && Mathf.Approximately(0, input.y)) == false || inputLagTimer >= inputLagPeriod)
        {
            lastInputEvent = input;
            inputLagTimer = 0;
        }

        return lastInputEvent;
    }

    private void Update()
    {
        if (!onlyMouse)
        {
            if (Input.GetKey(KeyCode.W))
            {
                player.transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }

            if (Input.GetKey(KeyCode.S))
            {
                player.transform.Translate(Vector3.back * Time.deltaTime * speed);
            }

            if (Input.GetKey(KeyCode.A))
            {
                player.transform.Translate(Vector3.left * Time.deltaTime * speed);
            }

            if (Input.GetKey(KeyCode.D))
            {
                player.transform.Translate(Vector3.right * Time.deltaTime * speed);
            }

            if (Input.GetKey(KeyCode.Space))
            {
                player.transform.Translate(Vector3.up * Time.deltaTime * speed);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                player.transform.Translate(Vector3.down * Time.deltaTime * speed);
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                Cursor.lockState = CursorLockMode.None;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Vector2 wantedVelocity = GetInput() * sensitivity;

            if ((rotationDirections & RotationDirection.Horizontal) == 0)
            {
                wantedVelocity.x = 0;
            }
            if ((rotationDirections & RotationDirection.Vertical) == 0)
            {
                wantedVelocity.y = 0;
            }

<<<<<<< HEAD
        // detect mouse movement and rotate the player
        if (Input.GetAxis("Mouse X") > 0)
        {
            player.transform.rotation += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * lookSpeed, 0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * lookSpeed);
            cam.transform.rotation += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * lookSpeed, 0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * lookSpeed);
        }

        if (Input.GetAxis("Mouse X") < 0)
        {
            player.transform.rotation += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * lookSpeed, 0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * lookSpeed);
            cam.transform.rotation += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * lookSpeed, 0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * lookSpeed);
=======
            velocity = new Vector2(
                Mathf.MoveTowards(velocity.x, wantedVelocity.x, acceleration.x * Time.deltaTime),
                Mathf.MoveTowards(velocity.y, wantedVelocity.y, acceleration.y * Time.deltaTime));

            rotation += velocity * Time.deltaTime;
            rotation.y = ClampVerticalAngle(rotation.y);

            transform.localEulerAngles = new Vector3(rotation.y, rotation.x, 0);

            if (cam != null && !onlyMouse)
            {
                cam.transform.localPosition = new Vector3(0, 1.5f, 0);
            }
>>>>>>> e87fef62d26c2769c057ab9e6f866bbfdb982424
        }
    }
}

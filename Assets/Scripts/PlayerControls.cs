using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Camera cam;
    public GameObject player;
    public float speed = 10.0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // detect W and move the player
        if (Input.GetKey(KeyCode.W))
        {
            player.transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        // detect S and move the player
        if (Input.GetKey(KeyCode.S))
        {
            player.transform.Translate(Vector3.back * Time.deltaTime * speed);
        }

        // detect A and move the player
        if (Input.GetKey(KeyCode.A))
        {
            player.transform.Translate(Vector3.left * Time.deltaTime * speed);
        }

        // detect D and move the player
        if (Input.GetKey(KeyCode.D))
        {
            player.transform.Translate(Vector3.right * Time.deltaTime * speed);
        }

        // detect mouse movement and rotate the player
        if (Input.GetAxis("Mouse X") > 0)
        {
            player.transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed, 0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed);
            cam.transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed, 0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed);
        }

        if (Input.GetAxis("Mouse X") < 0)
        {
            player.transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed, 0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed);
            cam.transform.position += new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed, 0.0f, Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed);
        }
    }
}

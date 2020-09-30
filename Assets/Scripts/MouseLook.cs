using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public CharacterController character;

    float mouseSensitiviy = 5f;

    private float xRot = 0;
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float x = Input.GetAxis("Mouse X") * mouseSensitiviy;
        float y = Input.GetAxis("Mouse Y") * mouseSensitiviy;

        xRot -= y;
        xRot = Mathf.Clamp(xRot, -90f, 90f); 
        transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);

        character.transform.Rotate(Vector3.up * x);
    }
}

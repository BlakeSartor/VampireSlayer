using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponLook : MonoBehaviour
{
    public GameObject pauseMenu;
    float mouseSensitiviy = 5f;

    private float xRot = 0;
    public void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }


    public void Update()
    {
        PauseMenu pause = pauseMenu.GetComponent<PauseMenu>();
        if (!pause.getIsPaused())
        {
            float y = Input.GetAxis("Mouse Y") * mouseSensitiviy;

            xRot -= y;
            xRot = Mathf.Clamp(xRot, -90f, 90f);
            transform.localRotation = Quaternion.Euler(0f, xRot, 0f);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement2 : Bolt.EntityEventListener<ISlayerState>
{
    public GameObject eyes;
    public Camera cam;
    public CharacterController character;
    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public float speed = 0;
    public float gravity = -9.81f;
    public float walkSpeed = 12f;
    public float runSpeed = 32f;
    public float jumpHeight = 3f;

    Vector3 velocity;
    bool isGrounded;

    public override void Attached()
    {
        state.SetTransforms(state.SlayerTransform, transform);


        if (entity.IsOwner)
        {
            cam.gameObject.SetActive(true);

            eyes.SetActive(false);

            state.SlayerColor = new Color(Random.value, Random.value, Random.value);
        } 

        state.AddCallback("SlayerColor", ColorChanged);
    }

    public void Update()
    {
        
        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public override void SimulateOwner()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = runSpeed;
        }
        else
        {
            speed = walkSpeed;
        }

        if (Input.GetButtonDown("Jump"))
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 moveDir = transform.right * x + transform.forward * z;

        character.Move(moveDir.normalized * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        character.Move(velocity * Time.deltaTime);
    }

    void OnGUI()
    {
        if (entity.IsOwner)
        {
            GUI.color = state.SlayerColor;
            GUILayout.Label("@@@");
            GUI.color = Color.white;
        }
    }

    void ColorChanged()
    {
        GetComponentInChildren<Renderer>().material.color = state.SlayerColor;
    }
}

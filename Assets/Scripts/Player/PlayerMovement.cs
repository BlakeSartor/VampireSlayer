using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMovement : Bolt.EntityEventListener<ISlayerState>
{
    public GameObject pauseMenu;
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

    int team;

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
            state.Team = 1;
        } 

        state.AddCallback("SlayerColor", ColorChanged);
        state.AddCallback("Team", TeamAssigned);
    }

    public void Update()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    public override void SimulateOwner()
    {
        PauseMenu pause = pauseMenu.GetComponent<PauseMenu>();
        if (!pause.getIsPaused())
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

            if (Input.GetButtonDown("Jump") && isGrounded)
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
    }

    void OnGUI()
    {
        if (entity.IsOwner)
        {
            GUI.color = state.SlayerColor;
            if (state.Team == 1)
            {
                GUILayout.Label("TEAM: Vampire Slayer");
            }
            else
            {
                GUILayout.Label("TEAM: Vampire");

            }
            GUI.color = Color.white;

        }
    }

    void TeamAssigned()
    {
        team = state.Team;
    }

    void ColorChanged()
    {
        GetComponentInChildren<Renderer>().material.color = state.SlayerColor;
    }
}

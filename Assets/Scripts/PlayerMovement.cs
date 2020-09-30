using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Bolt.EntityBehaviour<ISlayerState>
{
    public float playerSpeed = 4f;
    public override void Attached()
    {
        state.SetTransforms(state.SlayerTransform, transform);

        if (entity.IsOwner)
        {
            state.SlayerColor = new Color(Random.value, Random.value, Random.value);
        }

        state.AddCallback("SlayerColor", ColorChanged);

    }

    public override void SimulateOwner()
    {
        var movement = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) 
        { 
            movement.z += 1; 
        }
        if (Input.GetKey(KeyCode.S)) 
        {
            movement.z -= 1; 
        }
        if (Input.GetKey(KeyCode.A)) 
        { 
            movement.x -= 1; 
        }
        if (Input.GetKey(KeyCode.D)) 
        { 
            movement.x += 1; 
        }

        if (movement != Vector3.zero)
        {
            transform.position = transform.position + (movement.normalized * playerSpeed * BoltNetwork.FrameDeltaTime);
        }
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
        GetComponent<Renderer>().material.color = state.SlayerColor;
    }
}

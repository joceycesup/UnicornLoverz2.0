using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULMilitianMovement : MonoBehaviour
{

    private ULPlayerController Player;
    public float speed = 1.0f;
    public float magnitudeDist = 1.0f;

    // Use this for initialization
    void Start()
    {
        Player = FindObjectOfType<ULPlayerController>();
    }


    void FixedUpdate()
    {
        Vector3 direction = Player.transform.position - transform.position;
        float distance = direction.magnitude;
        direction = Vector3.Normalize(direction) * speed * Time.fixedDeltaTime;
        if ((this.transform.position - Player.transform.position).magnitude > magnitudeDist)
        {
            if (direction.magnitude > distance)
                direction = Vector3.ClampMagnitude(direction, distance);
            transform.Translate(direction);
        }
        else
        {
            // LE JOLI CODE DE THIBAULT
        }

    }
}

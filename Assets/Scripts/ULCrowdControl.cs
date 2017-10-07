using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULCrowdControl : MonoBehaviour
{

    private ULCharacterController Player;
    private Vector3 lastPosition;
    public float speed = 1.0f;
    private Queue<Vector3> previousPositions = new Queue<Vector3>();
    public float pathfindingGranularity = 0.5f;
    private Vector3 tmpPosition;

    // Use this for initialization
    void Awake()
    {
        Player = FindObjectOfType<ULCharacterController>();
        lastPosition = Player.transform.position;
        previousPositions.Enqueue(lastPosition);
        StartCoroutine("CheckPlayerPosition");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (previousPositions.Count != 1)
            tmpPosition = previousPositions.Dequeue();
            transform.Translate (Vector3.Normalize(tmpPosition - transform.position) * speed * Time.fixedDeltaTime);
    }

    IEnumerator CheckPlayerPosition()
    {   
        yield return new WaitForSeconds(pathfindingGranularity);
        lastPosition = Player.transform.position;
        previousPositions.Enqueue(lastPosition);
        Debug.Log("Yolo");
        StartCoroutine("CheckPlayerPosition");
    }
}

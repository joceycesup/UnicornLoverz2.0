using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULCrowdControl : MonoBehaviour
{

    private ULPlayerController Player;
    private Vector3 lastPosition;
    public float speed = 1.0f;
    private Queue<Vector3> previousPositions = new Queue<Vector3>();
    public float pathfindingGranularity = 0.5f;
    private Vector3 tmpPosition;
    private int cptPos = 0;


    // Use this for initialization
    void Awake()
    {
        Player = FindObjectOfType<ULPlayerController>();
        lastPosition = Player.transform.position;
        previousPositions.Enqueue(lastPosition);
        StartCoroutine("CheckPlayerPosition");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (previousPositions.Count > 1 && cptPos%2==0)
        {
            tmpPosition = previousPositions.Dequeue();
        }
        Vector3 direction = tmpPosition - transform.position;
        float distance = direction.magnitude;
        direction = Vector3.Normalize(direction) * speed * Time.fixedDeltaTime;
        if (direction.magnitude > distance)
           direction = Vector3.ClampMagnitude(direction, distance);
        transform.Translate(direction);
    }

    IEnumerator CheckPlayerPosition()
    {
        yield return new WaitForSeconds(pathfindingGranularity);

        Vector3 temp = (lastPosition - Player.transform.position);

        if ( temp.magnitude > 1 )
        {
            cptPos++;
            lastPosition = Player.transform.position;
            previousPositions.Enqueue(lastPosition);
        }
        StartCoroutine("CheckPlayerPosition");
    }
}

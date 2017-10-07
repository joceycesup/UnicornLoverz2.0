using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULSingleMovement : MonoBehaviour
{

    public float velocidadMax;

    public float xMax;
    public float yMax;
    public float xMin;
    public float yMin;

    private float x;
    private float y;
    private float tiempo;
    private int speedMod = 1;
    public int speedModMax = 25;
    public int magnitudeDist = 1;

    // Use this for initialization
    void Start()
    {

        x = Random.Range(-velocidadMax, velocidadMax);
        y = Random.Range(-velocidadMax, velocidadMax);

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this.transform.parent != null)
            if ((this.transform.position - this.transform.parent.position).magnitude > magnitudeDist)
                speedMod = speedModMax;
            else
                speedMod = 1;
        tiempo += Time.deltaTime;

        if (transform.localPosition.x > xMax)
        {
            x = Random.Range(-velocidadMax * speedMod, 0.0f);
            tiempo = 0.0f;
        }
        if (transform.localPosition.x < xMin)
        {
            x = Random.Range(0.0f, velocidadMax * speedMod);
            tiempo = 0.0f;
        }
        if (transform.localPosition.y > yMax)
        {
            y = Random.Range(-velocidadMax * speedMod, 0.0f);
            tiempo = 0.0f;
        }
        if (transform.localPosition.y < yMin)
        {
            y = Random.Range(0.0f, velocidadMax * speedMod);
            tiempo = 0.0f;
        }


        if (tiempo > 1.0f)
        {
            x = Random.Range(-velocidadMax, velocidadMax);
            y = Random.Range(-velocidadMax, velocidadMax);
            tiempo = 0.0f;
        }

        transform.localPosition = new Vector3(transform.localPosition.x + x, transform.localPosition.y + y, transform.localPosition.y);
    }

}

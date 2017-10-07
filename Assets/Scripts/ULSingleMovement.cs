using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULSingleMovement : MonoBehaviour {

    public float velocidadMax;

    public float xMax;
    public float yMax;
    public float xMin;
    public float yMin;

    private float x;
    private float y;
    private float tiempo;
    private float angulo;

    // Use this for initialization
    void Start () {

        x = Random.Range(-velocidadMax, velocidadMax);
        y = Random.Range(-velocidadMax, velocidadMax);

    }
	
	// Update is called once per frame
	void FixedUpdate() {


        tiempo += Time.deltaTime;

        if (transform.localPosition.x > xMax)
        {
            x = Random.Range(-velocidadMax, 0.0f);
            tiempo = 0.0f;
        }
        if (transform.localPosition.x < xMin)
        {
            x = Random.Range(0.0f, velocidadMax);
            tiempo = 0.0f;
        }
        if (transform.localPosition.y > yMax)
        {
            y = Random.Range(-velocidadMax, 0.0f);
            tiempo = 0.0f;
        }
        if (transform.localPosition.y < yMin)
        {
            y = Random.Range(0.0f, velocidadMax);
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

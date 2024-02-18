using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accel : MonoBehaviour
{
    private Rigidbody rb;
    private float speed = 1;



    void Start()
    {
        //asociamos con el rb de la bola.
        rb=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //se encarga de recoger la aceleración dependiendo de la orientación del móvil.
        Vector3 tilt = Input.acceleration;

        //TODO= tener en cuenta si el móvil está sobre la mesa o no.
        tilt = Quaternion.Euler(90, 0, 0)* tilt;

        rb.AddForce(tilt*speed);
        
    }
}

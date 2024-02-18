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
        //se encarga de recoger la aceleraci�n dependiendo de la orientaci�n del m�vil.
        Vector3 tilt = Input.acceleration;

        //TODO= tener en cuenta si el m�vil est� sobre la mesa o no.
        tilt = Quaternion.Euler(90, 0, 0)* tilt;

        rb.AddForce(tilt*speed);
        
    }
}

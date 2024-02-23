using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class VueltaYVuelta : MonoBehaviour
{


    public float velocidad = 360; //velocidad a la que va a girar la pelota.
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //hacemos la rotación en el eje x.
        transform.Rotate(0, 0, velocidad * Time.deltaTime);
    }

}

   

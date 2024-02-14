using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraControl : MonoBehaviour
{
    public GameObject bolica;//variable que guarda la bola.
    private Vector3 offset;//variable que registra la diferencia entre la posición de la cámara y de la bola.

    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position-bolica.transform.position;
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Actualizamos la posición de la cámara
        transform.position = bolica.transform.position + offset;

    }
}

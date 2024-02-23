using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class BolaMala : MonoBehaviour
{
    public GameObject bola;
    public GameObject bolicaBuena;
    private float minDistance = 5.0f;
    private bool targetCollision=false;
    private float speed = 2.0f;
    private float range;
    private float hanChocado = 1.0f;

    public TMP_Text velocidadTexto;
    private Rigidbody rbBolica;

    public Accel accel;//hacemos una referecnia al script de la bola buena

    Vector3 posicionInicial = new Vector3(-20, 5, -20); //posición a la que vuelve la pelota si hace trigger con un enemigo.


    // Start is called before the first frame update

    private void Start()
    {
        rbBolica = GetComponent<Rigidbody>();
       
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 direction = bolicaBuena.transform.position - bola.transform.position;
        //calcula la distancia entre la bola mala y la bola buena.
        range = Vector3.Distance(bola.transform.position, bolicaBuena.transform.position);

        if (range<=minDistance)
        {
     //       Debug.Log("La bola mala nos persigue");
            //Si no han colisionado todavía, la persigue.
            if (!targetCollision)
            {
                //cogemos la posición de la bola.
                bola.transform.LookAt(bolicaBuena.transform.position);

                bola.transform.Rotate(new Vector3(0, -90, 0), Space.Self);
                bola.transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));

               // Debug.Log("La bola mala está cerca");

                if (range<=hanChocado)
                {
                    targetCollision = true;
                    
                }


            }
            else
            {

                Object.Destroy(bola);
         //       Debug.Log("La bola mala se nos ha pegado");

                targetCollision = false;    

                //reducimos en uno el speed de la bola buena
                rbBolica.velocity = rbBolica.velocity - Vector3.one;
                accel.speed--;


                //actualizamos el texto.
                velocidadTexto.text = "Velocidad:" + accel.speed;

                
                if (accel.speed == 0)
                {
                    //llamamos a la función que interrumpe el juego en caso de que la velocidad sea 0.
                    accel.reinicio();

                }

                

            }

        }

    }

   

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Accel : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;

    Vector3 movimiento;
    Vector3 posicionInicial= new Vector3(-20, 5, -20); //posici�n a la que vuelve la pelota si hace trigger con un enemigo.
    int monedas = 0;
    public TMP_Text textoMonedas;
    public TMP_Text victoria;
   


    bool isOut;

    public float tamRaycast = 5.0f;

    bool canJump = true;//booleano que detecta si ha saltado o no
    public float jumpForce = 5.0f;//el impulso de la bola.
    bool isGrounded = true; //booleano que detecta si la bola est� tocando el suelo o no
    float limitesuelo = -7f;//variable que pondr� un l�mite por si la bola se cae.

    //public Joystick input;//variable que detecta el joystick.



    void Start()
    {
        //asociamos con el rb de la bola.
        rb=GetComponent<Rigidbody>();

        //desactivamos el texto de victoria, para que no sea vea desde el principio.
        victoria.enabled = false;

        isOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        //verificamos si la posici�n en el eje Y de la bola est� por encima del l�mite.
        if (transform.position.y>limitesuelo)
        {
            //se encarga de recoger la aceleraci�n dependiendo de la orientaci�n del m�vil.
            Vector3 tilt = Input.acceleration;

            //TODO= tener en cuenta si el m�vil est� sobre la mesa o no.
            tilt = Quaternion.Euler(90, 0, 0) * tilt;

            rb.AddForce(tilt * speed);


            //TODO: esto tiene que ser un tap

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began && isGrounded)
                {
                    Jump();


                }
            }

        //en caso de que est� por debajo del l�mite, pues 
        } else {

            reinicio();
        }

    }

    private void FixedUpdate()
    {
        //creamos unas variables para capturar los movimientos horizontal y vertical
        //float horizontal = Input.GetAxis("Horizontal");
        //float vertical = Input.GetAxis("Vertical");
        if (!isOut)
        {
            //asignamos el movimiento al joystick.
            //float horizontal = input.Horizontal;
            //float vertical = input.Vertical;

            //ahora capturamos el movimiento de forma completa.
            //movimiento = new Vector3(horizontal, 0.0f, vertical);

            //asignamos el movimiento al rb.
            //rb.AddForce(movimiento * velocidad);

            //comprobamos que el raycast est� tocando el suelo
            Debug.DrawRay(transform.position, Vector3.down * tamRaycast, Color.red);
            Vector3 floor = transform.TransformDirection(Vector3.down);

            if (Physics.Raycast(transform.position, floor, 1.03f))
            {
                isGrounded = true;
                canJump = true;
      //          Debug.Log("Contacto con el suelo");

            }
            else
            {
                canJump = false;
                isGrounded = false;
       //         Debug.Log("No hay contacto con el suelo");
            }

            //si ha salido, llamamos a la la siguiente fase.
        }
        else
        {

        }


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("salida"))
        {
            Debug.Log("Has ganado. Has recogido" + monedas + "monedas");

            //ponemos visible el texto de victoria
            victoria.enabled = true;
            isOut = true;
            reinicio();

            //en caso de que la bola haga trigger con un enemigo
        }
        
        //en caso de que la bola choque con una moneda
        else if (other.CompareTag("Dollars"))
        {
            other.gameObject.SetActive(false);//desactivamos la moneda
            monedas++;

            textoMonedas.text = "Monedas: " + monedas;
        }

    }

    private void Jump()
    {
        if (canJump)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);

            Vector3 floor = transform.TransformDirection(Vector3.down);

            if (Physics.Raycast(transform.position, floor, 1.03f))
            {
                isGrounded = true;
                canJump = true;
                Debug.Log("Contacto con el suelo");

            }
            else
            {
                canJump = false;
                isGrounded = false;
                Debug.Log("No hay contacto con el suelo");
            }


        }
    }

    public void reinicio()
    {
        //recargamos la escena actual.
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        monedas = 0;
        textoMonedas.text = "Monedas: 0";

    }

    public void muertePorBola()
    {
        //volvemos a la posici�n inicial
        rb.MovePosition(posicionInicial);
        rb.velocity = Vector3.zero;//ponemos la velocidad inicial a 0.
        rb.angularVelocity = Vector3.zero;//ponemos la rotaci�n a 0.
        monedas = 0;
        textoMonedas.text = "Monedas: 0";
    }
}

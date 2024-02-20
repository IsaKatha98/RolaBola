using UnityEngine;

public class BolicaControl : MonoBehaviour
{
    private Rigidbody rb;//el rigidbody de la pelota.
    public float velocidad;//variable que modifica la velocidad de la pelota
    Vector3 movimiento;
    Vector3 posicionInicial; //posici�n a la que vuelve la pelota si hace trigger con un enemigo.
    int monedas = 0;

    public float tamRaycast = 5.0f;

    bool canJump=true;//booleano que detecta si ha saltado o no
    public float jumpForce = 5.0f;//el impulso de la bola.
    bool isGrounded = true; //booleano que detecta si la bola est� tocando el suelo o no.

    // Start is called before the first frame update
    void Start()
    {
        //capturamos el rb de la bola.
        rb = GetComponent<Rigidbody>();
        posicionInicial=transform.position;//guardamos la posici�n inicial de la pelota.

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)&&isGrounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        //creamos unas variables para capturar los movimientos horizontal y vertical
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //ahora capturamos el movimiento de forma completa.
        movimiento= new Vector3 (horizontal, 0.0f, vertical);

        //asignamos el movimiento al rb.
        rb.AddForce(movimiento*velocidad);

        //comprobamos que el raycast est� tocando el suelo
        Debug.DrawRay(transform.position, Vector3.down * tamRaycast, Color.red);
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("salida"))
        {
            Debug.Log("Has ganado. Has recogido"+monedas+"monedas");
        
            //en caso de que la bola ha trigger con un enemigo
        } else if (other.CompareTag("Enemy"))
        {
            //volvemos a la posici�n inicial
            rb.MovePosition(posicionInicial);
            rb.velocity = Vector3.zero;//ponemos la velocidad inicial a 0.
            rb.angularVelocity = Vector3.zero;//ponemos la rotaci�n a 0.
            monedas = 0;
        
        }
        //en caso de que la bola choque con una moneda
        else if (other.CompareTag("Dollars"))
        {
            other.gameObject.SetActive(false);//desactivamos la moneda
            monedas=monedas+1;
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
}

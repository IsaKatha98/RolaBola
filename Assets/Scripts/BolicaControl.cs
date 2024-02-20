using UnityEngine;

public class BolicaControl : MonoBehaviour
{
    private Rigidbody rb;//el rigidbody de la pelota.
    public float velocidad;//variable que modifica la velocidad de la pelota
    Vector3 movimiento;
    Vector3 posicionInicial; //posición a la que vuelve la pelota si hace trigger con un enemigo.
    int monedas = 0; 

    bool isJump=false;//booleano que detecta si ha saltado o no
    public float jumpForce = 5.0f;//el impulso de la bola.

    // Start is called before the first frame update
    void Start()
    {
        //capturamos el rb de la bola.
        rb = GetComponent<Rigidbody>();
        posicionInicial=transform.position;//guardamos la posición inicial de la pelota.

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

        if (Input.GetKeyDown(KeyCode.Space)) {

            Jump();
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
            //volvemos a la posición inicial
            rb.MovePosition(posicionInicial);
            rb.velocity = Vector3.zero;//ponemos la velocidad inicial a 0.
            rb.angularVelocity = Vector3.zero;//ponemos la rotación a 0.
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
        isJump = true;
        if (isJump)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isJump=false;
        
        
        }
    }
}

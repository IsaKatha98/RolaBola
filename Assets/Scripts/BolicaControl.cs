using UnityEngine;

public class BolicaControl : MonoBehaviour
{
    private Rigidbody rb;//el rigidbody de la pelota.
    public float velocidad;//variable que modifica la velocidad de la pelota

    // Start is called before the first frame update
    void Start()
    {
        //capturamos el rb de la bola.
        rb = GetComponent<Rigidbody>();

    }

    private void FixedUpdate()
    {
        //creamos unas variables para capturar los movimientos horizontal y vertical
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        //ahora capturamos el movimiento de forma completa.
        Vector3 movimiento= new Vector3 (horizontal, 0.0f, vertical);

        //asignamos el movimiento al rb.
        rb.AddForce(movimiento*velocidad);
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}

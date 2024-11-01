using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float velocidad = 5f;
    public int vida = 5;

    public int kii = 100;

    public float fuerzaSalto = 10f; 
    public float fuerzaRebote = 5f; 
    public float longitudRaycast = 0.1f; 
    public LayerMask groundLayer; 

    private bool enSuelo; 
    private bool recibiendoDano;
    private bool atacando;
    private bool CargandoKii; // Cambia el tipo de CargandoKii a bool
    public bool muerto;

    private Rigidbody2D rb; 

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!muerto)
        {
            if (!atacando)
            {
                Move();

                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRaycast, groundLayer);
                enSuelo = hit.collider != null;

                if (enSuelo && Input.GetKeyDown(KeyCode.Space) && !recibiendoDano)
                {
                    rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
                }
            }

            if (Input.GetKeyDown(KeyCode.Z) && !atacando && enSuelo)
            {
                Atacando();
            }
            if (Input.GetKey(KeyCode.C) && !CargandoKii && enSuelo)
            {
                Cargando();
            }
            if (Input.GetKeyUp(KeyCode.C) && CargandoKii)
            {
                DesactivaCargando();
            }
        }
        
        animator.SetBool("ensuelo", enSuelo);
        animator.SetBool("recibeDano", recibiendoDano);
        animator.SetBool("Atacando", atacando);
        animator.SetBool("muerto", muerto);
    }
       
    public void Move()
    {
        float velocidadX = Input.GetAxis("Horizontal") * Time.deltaTime * velocidad;

        animator.SetFloat("Movement", velocidadX * velocidad);

        if (velocidadX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (velocidadX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        Vector3 posicion = transform.position;

        if (!recibiendoDano)
            transform.position = new Vector3(velocidadX + posicion.x, posicion.y, posicion.z);
    }
    public void RecibeDanio(Vector2 direccion, int cantDanio)
    {
        if(!recibiendoDano)
        {
            recibiendoDano = true;
            vida -= cantDanio;
            if (vida<=0)
            {
                muerto = true;
            }
            if (!muerto)
            {
                Vector2 rebote = new Vector2(transform.position.x - direccion.x, 0.2f).normalized;
                rb.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
            }
        }
    }

    public void DesactivaDanio()
    {
        recibiendoDano = false;
        rb.velocity = Vector2.zero;
    }

    public void Atacando(){
        atacando = true;
    }

    public void Cargando(){
        CargandoKii = true;
        animator.SetBool("CargandoKii", CargandoKii);
    }

    public void DesactivaAtaque()
    {
        atacando = false;
    }

    public void DesactivaCargando()
    {
        CargandoKii = false;
        animator.SetBool("CargandoKii", CargandoKii);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRaycast);
    }
}
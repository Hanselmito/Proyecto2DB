using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 5f;

    [Header("Estadisticas")]
    public int vida = 10;
    public int kii = 0;

    [Header("Salto")]
    public float fuerzaSalto = 10f; 

    [Header("Ataque")]
    public float fuerzaRebote = 5f;
    

    [Header("Raycast")]
    public float longitudRaycast = 0.1f;

    [Header("Layer")] 
    public LayerMask groundLayer; 

    [Header("Estados")]
    private bool enSuelo; 
    private bool recibiendoDano;
    private bool atacando;
    private bool CargandoKii;
    private bool KiBlast;
    public bool muerto;

    public Transform FirePoint;
    public GameObject Kiiblast;

    private Rigidbody2D rb; 

    public Animator animator;

    [Header("recolectar bolas")]
    private int collectedBolas = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!muerto)
        {
            if (!atacando && !CargandoKii && !KiBlast)
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
            if (Input.GetKeyDown(KeyCode.X) && !KiBlast && enSuelo && kii >= 10)
            {
                KiiBlast();
                GameObject kiiblastInstance = Instantiate(Kiiblast, FirePoint.position, Quaternion.identity);
                kiiblastInstance.transform.localScale = transform.localScale;
            }
            if (Input.GetKeyUp(KeyCode.X) && KiBlast)
            {
                desactivaKiiBlast();
            }
        }
        else
        {
            SceneManager.LoadScene(3);
        }

        animator.SetBool("ensuelo", enSuelo);
        animator.SetBool("recibeDano", recibiendoDano);
        animator.SetBool("Atacando", atacando);
        animator.SetBool("muerto", muerto);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("DEAD"))
        {
            SceneManager.LoadScene(3);
        }
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
        animator.SetTrigger("Atacando");
        
    }

    public void Cargando()
    {
        CargandoKii = true;
        animator.SetBool("CargandoKii", CargandoKii);
        StartCoroutine(CargarKii());
    }

    private IEnumerator CargarKii()
    {
        while (CargandoKii && kii < 100)
        {
            kii += 1;
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void KiiBlast()
    {
        if (kii >= 10)
        {
            KiBlast = true;
            animator.SetBool("KiBlast", KiBlast);
            kii -= 10;
        }
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

    public void desactivaKiiBlast(){
        KiBlast = false;
        animator.SetBool("KiBlast", KiBlast);
    }

    public void CollectBola()
    {
        collectedBolas++;
        if (collectedBolas >= 7)
        {
            SceneManager.LoadScene(2);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRaycast);
    }
}
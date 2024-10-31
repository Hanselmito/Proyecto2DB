using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    public Transform player; 
    public float detectionRadius = 5.0f; 
    public float speed = 2.0f;
    public float fuerzaRebote = 6f;
    public int vida = 100;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool enMovimiento;
    private bool MuereEnemy;
    private bool recibiendoDanio;
    private bool playerVivo;

    private Animator animator;

    void Start(){
        playerVivo = true;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update(){
        if (playerVivo && !MuereEnemy){
            Move();
        }

        animator.SetBool("enMovimiento", enMovimiento);
        animator.SetBool("MuereEnemy", MuereEnemy);
    }

    private void Move(){
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius){
            Vector2 direction = (player.position - transform.position).normalized;

            if (direction.x < 0){
                transform.localScale = new Vector3(-1, 1, 1);
            }
            if (direction.x > 0){
                transform.localScale = new Vector3(1, 1, 1);
            }
            movement = new Vector2(direction.x, 0);
            enMovimiento = true;

        }else{
            movement = Vector2.zero;
            enMovimiento = false;
        }
        if (!recibiendoDanio)
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direccionDanio = new Vector2(transform.position.x, 0);
            PlayerMovement playerScript = collision.gameObject.GetComponent<PlayerMovement>();

            playerScript.RecibeDanio(direccionDanio, 1);
            playerVivo = !playerScript.muerto;
            if (!playerVivo)
            {
                enMovimiento = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("AtaquePuno")){
            Vector2 direccionDanio = new Vector2(collision.gameObject.transform.position.x, 0);
            RecibeDanio(direccionDanio, 1);
        }
    }

    public void RecibeDanio(Vector2 direccion, int cantDanio){
        if (!recibiendoDanio){
            vida -= cantDanio;
            recibiendoDanio = true;
            if (vida <= 0){
                MuereEnemy = true;
                enMovimiento = false;
            }else{
                Vector2 rebote = new Vector2(transform.position.x - direccion.x, 0.2f).normalized;
                rb.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
                StartCoroutine(DesactivaDanio());
            }
        }
    }

    IEnumerator DesactivaDanio(){
        yield return new WaitForSeconds(0.4f);
        recibiendoDanio = false;
        rb.velocity = Vector2.zero;
    }

    public void EliminarCuerpo(){
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
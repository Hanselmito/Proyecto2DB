using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kiiblast : MonoBehaviour
{

    [Header("estadisticas")]
    public int damage = 1;
    private Rigidbody2D rb;
    public float speed;
    public Vector2 direction = Vector2.right; // New property to set the direction

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Determine the direction based on the player's facing direction
        if (transform.localScale.x < 0)
        {
            direction = Vector2.left;
        }
        else
        {
            direction = Vector2.right;
        }
    }

    void Update()
    {
        rb.velocity = direction * speed;
        Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            EnemyController enemy = collision.GetComponent<EnemyController>();
            if (enemy != null)
            {
                Vector2 direccionDanio = new Vector2(transform.position.x, 0);
                enemy.RecibeDanio(direccionDanio, damage);
            }
            Destroy(gameObject);
        }
    }
}

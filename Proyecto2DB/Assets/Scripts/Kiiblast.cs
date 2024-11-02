using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kiiblast : MonoBehaviour
{
    public int damage = 1;
    private Rigidbody2D rb;
    public float speed;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.velocity = new Vector2(+speed, 0);
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
            Destroy(gameObject); // Destroy the Kiiblast after collision
        }
    }
}

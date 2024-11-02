using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolas : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().CollectBola(); // Add this line
            Destroy(gameObject);
        }
    }
}

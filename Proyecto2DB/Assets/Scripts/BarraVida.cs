using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour{

    public Image rellenovida;
    public PlayerController playerController;
    public float vidaMaxima;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        rellenovida.fillAmount = playerController.vida / vidaMaxima;
    }
}

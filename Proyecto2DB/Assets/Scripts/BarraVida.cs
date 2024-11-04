using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour{

    [Header("Estadisticas")]
    public Image rellenovida;
    public PlayerController playerController;
    public float vidaMaxima;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        rellenovida.fillAmount = playerController.vida / vidaMaxima;
    }
}

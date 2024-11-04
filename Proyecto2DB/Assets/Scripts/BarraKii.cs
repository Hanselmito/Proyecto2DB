using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraKii : MonoBehaviour{

    [Header("Estadisticas")]
    public Image rellenoKii;
    public PlayerController playerController;
    public float KiiMaxima;

    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    void Update()
    {
        rellenoKii.fillAmount = playerController.kii / KiiMaxima;
    }
}

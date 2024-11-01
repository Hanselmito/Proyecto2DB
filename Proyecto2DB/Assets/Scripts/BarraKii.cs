using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraKii : MonoBehaviour{

    public Image rellenoKii;
    public PlayerController playerController;
    public float KiiMaxima;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        rellenoKii.fillAmount = playerController.vida / KiiMaxima;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{
    

    private void PlayAgain(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Menu(){
        SceneManager.LoadScene(0);
    }

    public void QuitGame() {
        Application.Quit();
    }
    public void jugar(){
        SceneManager.LoadScene(1);
    }
}

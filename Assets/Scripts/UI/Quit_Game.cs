using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quit_Game : MonoBehaviour
{
    public AudioManager audioManager;
    private void Start()
    {
        audioManager.Play("Theme_Test");
    }
    public void QuitGame()
    {
        // You can customize this function to perform any necessary clean-up or save operations before quitting the game
        Debug.Log("Quitting the game...");
        Application.Quit();
    }

}

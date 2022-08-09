using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public AudioSource buttonClickSound;

    public void RestartGame()
    {
        if(GameController.sound == 1)
        {
            buttonClickSound.Play();
        }
        
        SceneManager.LoadScene("Main");
    }
}

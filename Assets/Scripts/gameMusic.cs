using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class gameMusic : MonoBehaviour
{
    public AudioSource gameMusicAS;
    static GameObject instance;
    // Start is called before the first frame update
    void Start()
    {

        
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = gameObject;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(GameController.music == 1)
        {
            gameMusicAS.volume = 1;
        }
        else
        {
            gameMusicAS.volume = 0;
        }
    }
}

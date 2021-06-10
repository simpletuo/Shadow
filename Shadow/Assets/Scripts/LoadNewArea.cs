using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Script for a doorway to load a new scene
public class LoadNewArea : MonoBehaviour
{

    public string levelToLoad;              //The name of the scene to be loaded

    public string exitPoint;                //The name of the start point that the Player will spawn at after loading the new scene

    private PlayerController thePlayer;     //The PlayerController of the Player's sprite

    void Start()
    {
        //Get a component reference to the Player's sprite's PlayerController
        thePlayer = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Check if the Player has entered the doorway (collided with the doorway's collider)
        if (other.gameObject.CompareTag("Player"))
        {
            //If the Player entered the doorway, load the new scene
            UnityEngine.SceneManagement.SceneManager.LoadScene(levelToLoad);

            //Set the Player's start point after loading the new scene
            thePlayer.startPoint = exitPoint;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public bool quit;

    private Collider2D col;
    private bool inRange = false;
    [SerializeField] AudioSource audioSource;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange)
        {
            if(quit == true)
            {
                //quit
                Application.Quit();
            }
            else
            {
                //play
                SceneManager.LoadScene(1);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            col = collision;
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        inRange = false;
    }

}

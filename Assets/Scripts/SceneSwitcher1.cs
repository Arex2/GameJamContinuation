using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher1 : MonoBehaviour
{
    public int sceneToLoad;

    private void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            //Switch scene
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}

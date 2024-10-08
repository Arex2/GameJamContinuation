using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class CookieClicking : MonoBehaviour
{
    PlayerHealth playerHealth;
    GameObject panel;
    GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        panel = GameObject.Find("Panel");
        button = GameObject.Find("Button");
        HideCookieClicker();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Shift"))
        {
            ShowCookieClicker();
        }
        if(Input.GetButtonUp("Shift"))
        {
            HideCookieClicker();
        }
    }

    public void HPClick()
    {
        //add 1 hp to player hp
        //EventController.onHPClick += HPClick;
        //playerHealth.AddHealth();
        EventController.RaiseHPClick();
    }

    private void ShowCookieClicker()
    {
        //panel & button display
        panel.SetActive(true);
        button.SetActive(true);
    }

    private void HideCookieClicker()
    {
        panel.SetActive(false);
        button.SetActive(false);
    }
}

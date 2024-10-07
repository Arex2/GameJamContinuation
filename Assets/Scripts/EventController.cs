using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventController : MonoBehaviour
{
    //MAKE THIS A SINGLETON?!
    public delegate void OnDeath();
    public static event OnDeath onDeath;

    public delegate void OnRespawn();
    public static event OnRespawn onRespawn;

    public static void RaiseOnDeath()
    {
        if (onDeath != null) //Only raise event if things are subbed to the event
        {
            onDeath(); //invoke event
        }
    }

    public static void RaiseOnRespawn()
    {
        if (onRespawn != null) //Only raise event if things are subbed to the event
        {
            onRespawn(); //invoke event
        }
    }
    //        EventController.onDeath += ResetEnemy;//subscribes to the onDeath event
}

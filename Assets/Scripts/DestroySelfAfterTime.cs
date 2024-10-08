using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfAfterTime : MonoBehaviour
{
    void Start()
    {
        Invoke("DestorySelf", 1f);
    }

    void DestorySelf()
    {
        Destroy(this.gameObject);
    }
}

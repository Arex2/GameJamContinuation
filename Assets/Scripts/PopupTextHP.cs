using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Collections.AllocatorManager;
using UnityEngine.UI;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;

public class PopupTextHP : MonoBehaviour
{
    Vector3 offset = new Vector3 (0, 0, 0);

    void Start()
    {
        EventController.onHPClick += SpawnText;
        offset.x = GetComponent<RectTransform>().rect.width;
        offset.y = GetComponent<RectTransform>().rect.height;
    }

    public void SpawnText()
    {
        TMP_Text tempTextBox = Instantiate(Resources.Load<TMP_Text>("PopupText"), Input.mousePosition - (offset/2), transform.rotation) as TMP_Text; //Camera.main.ScreenToWorldPoint(Input.mousePosition)
        //Parent to the panel 
        tempTextBox.transform.SetParent(this.transform, false);
    }
}

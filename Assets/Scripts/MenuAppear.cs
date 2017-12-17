using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAppear : MonoBehaviour
{

    public GameObject menu;
    public string joystickbutton;


    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        menu.SetActive(false);

        if (Input.GetKey(joystickbutton))
        {
            menu.SetActive(true);
        }


    }
}

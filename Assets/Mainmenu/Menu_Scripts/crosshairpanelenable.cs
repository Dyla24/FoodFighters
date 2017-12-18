using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class crosshairpanelenable : MonoBehaviour {
    public GameObject enablebutton;
    public GameObject disablebutton;
    public GameObject crosshair;
    public Slider centergap, dotsize, thickness, crosshairlength;



    public void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(enablebutton);
        enablebutton.GetComponent<Button>().OnSelect(null);
        centergap.value = crosshair.GetComponent<crosshairmanager>().ccentergap;
        dotsize.value = crosshair.GetComponent<crosshairmanager>().cdotsize;
        thickness.value = crosshair.GetComponent<crosshairmanager>().cthickness;
        crosshairlength.value = crosshair.GetComponent<crosshairmanager>().clength;

    }
    public void OnDisable()
    {
        EventSystem.current.SetSelectedGameObject(disablebutton);
        disablebutton.GetComponent<Button>().OnSelect(null);
    }
}

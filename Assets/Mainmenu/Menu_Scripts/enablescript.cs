using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class enablescript : MonoBehaviour {
	public GameObject activebutton;

	public void OnEnable()
	{
        EventSystem.current.SetSelectedGameObject(activebutton);
        activebutton.GetComponent<Button>().OnSelect(null);
    }
}

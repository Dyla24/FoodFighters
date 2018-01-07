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
		if (activebutton.GetComponent<Button> () != null) {
			activebutton.GetComponent<Button> ().OnSelect (null);
		}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class disablescript : MonoBehaviour {
	public GameObject button;

	public void OnDisable()
	{
		EventSystem.current.SetSelectedGameObject (button);
        button.GetComponent<Button>().OnSelect(null);

    }
}

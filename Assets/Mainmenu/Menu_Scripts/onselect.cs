using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class onselect : MonoBehaviour, ISelectHandler{
	public GameObject activepanel;
	public int newi;

	public void OnSelect(BaseEventData eventData)
	{
		if (activepanel.GetComponent<ActivePanel>().i  != newi) {
			activepanel.GetComponent<ActivePanel> ().i = newi;
			activepanel.GetComponent<ActivePanel> ().panelpos ();
		}
	}
}

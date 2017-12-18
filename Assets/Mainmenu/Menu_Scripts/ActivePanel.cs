using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActivePanel : MonoBehaviour {
	public Vector3[] positions = new Vector3[3];
	public GameObject[] activebuttons = new GameObject[3];
	public int i = 0;
    public string menutab;

	void Update () 
	{
		if (Input.GetAxisRaw (menutab) >= 0.1f && Input.GetButtonDown (menutab)) 
		{
			i++;
			if (i >= 3)
				i = 0;
			changeactivepanel ();
		}
	}
	public void OnEnable()
	{
		i = 0;
	}
	public void changeactivepanel()
	{
		this.transform.GetComponent<RectTransform>().anchoredPosition = positions [i];
		EventSystem.current.SetSelectedGameObject (activebuttons [i], null);
	}
	public void panelpos()
	{
		this.transform.GetComponent<RectTransform>().anchoredPosition = positions [i];
	}
}

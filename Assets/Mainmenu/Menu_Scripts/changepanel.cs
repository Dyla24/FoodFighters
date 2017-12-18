using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class changepanel : MonoBehaviour {
	public GameObject[] panels;
	public GameObject targetpanel;
	public GameObject targetbutton;

	// Use this for initialization
	void Start () 
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetAxis ("Cancel") >= 1 && transform.GetChild(0).GetComponent<Text>().text == ("Back"))
			this.GetComponent<changepanel> ().panelchange ();
	}
	public void panelchange()
	{
		targetpanel.gameObject.SetActive (true);
		EventSystem.current.SetSelectedGameObject (targetbutton, null);

	}
}

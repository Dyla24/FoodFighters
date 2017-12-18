using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {
	public GameObject timer;
	float time;

	// Use this for initialization
	void Start () {
		time = 0;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButton("P1A"))
		{
			time += Time.deltaTime/1.5f;

		}
		if (Input.GetButtonUp ("P1A")) 
		{
			time = 0;
		}
		timer.GetComponent<Image> ().fillAmount = time;
		if(time >= 1)
		{
			print (time);
			LS ();
		}
	}
	public void LS()
	{
		SceneManager.LoadScene (0);
	}
}

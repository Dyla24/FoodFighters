using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class creditsbullet : MonoBehaviour {
	public GameObject splat;
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Finish") {
			Destroy (this.gameObject);
			GameObject canvas = GameObject.FindGameObjectWithTag ("Canvas");
			GameObject s = (GameObject)Instantiate (splat, transform.position, Quaternion.identity, canvas.transform.GetChild (0));
			s.GetComponent<Image> ().color = Random.ColorHSV (0f,1f,0f,1f,0.5f,1f,1,1);
		} else {
			Destroy (this.gameObject);
			other.transform.parent.GetComponent<Containerscript> ().explosion (transform.position);
		}
	}
}

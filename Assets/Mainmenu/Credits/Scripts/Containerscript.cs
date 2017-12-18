using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Containerscript : MonoBehaviour {
	public float radius = 100000.0f;
	public float power = 5000.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (transform.position.y < -50 || transform.position.y > 50) 
		{
			Destroy (this.gameObject);
		}
	}
	public void explosion(Vector3 explosionpos)
	{
		print ("BOOM");
		for(int i= 0;i < transform.childCount; i++)
		{
			Rigidbody rb = transform.GetChild (i).GetComponent<Rigidbody> ();
			rb.constraints = RigidbodyConstraints.None;
			rb.AddExplosionForce (power*10,explosionpos,0,Random.Range(-20f,20f));
			//rb.useGravity = true;
			transform.GetChild(i).GetComponent<BoxCollider>().enabled = false;
		}
	}
}

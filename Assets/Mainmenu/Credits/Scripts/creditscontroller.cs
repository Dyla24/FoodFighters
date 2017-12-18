using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditscontroller : MonoBehaviour {
	public float horizontal;
	public float vertical;
	Vector3 movement;
	public float speed;
	Vector3 clampedPosition;
	RectTransform rt;
	GameObject Maincamera;
	public GameObject pewpew;
	public GameObject bulletPrefab;
	float bulletspeed = -70;
	float firerate;
	float lastShot;

	// Use this for initialization
	void Start () {
		rt = GetComponent<RectTransform> ();
		Maincamera = GameObject.FindGameObjectWithTag ("MainCamera");
		firerate = 0.1f;
	}
	
	// Update is called once per frame
	void Update () 
	{
		horizontal = Input.GetAxis ("Horizontal");
		vertical = Input.GetAxis ("Vertical");
		movement = new Vector3 (horizontal, vertical, 0);
		rt.Translate (movement * speed);
		clampedPosition = rt.localPosition;
		clampedPosition = new Vector3 (Mathf.Clamp (rt.localPosition.x, -960, 960), Mathf.Clamp (rt.localPosition.y, -540, 540), -20);
		rt.localPosition = clampedPosition;
		//print (movement);
		if(Input.GetAxisRaw("PrimaryAttack") <= -0.37f)
		{
			shoot ();
		}
	}
	void shoot()
	{
		if (Time.time > firerate + lastShot) {
			pewpew = (GameObject)Instantiate (bulletPrefab, Maincamera.transform.GetChild(0).position, Quaternion.identity);
			print (Maincamera.transform.position + (Maincamera.transform.forward*10 ));
			pewpew.transform.LookAt (this.gameObject.transform);
			//Ray ray = Maincamera.GetComponent<Camera> ().ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0));
			//RaycastHit hit;
			//if (Physics.Raycast (ray, out hit)) {
				//pewpew.transform.LookAt (hit.point);
				pewpew.GetComponent<Rigidbody> ().velocity = pewpew.transform.forward * -bulletspeed;
			//} else {
			//	pewpew.GetComponent<Rigidbody> ().velocity = ray.direction * -bulletspeed;
			//}
			lastShot = Time.time;
		}
	}
}

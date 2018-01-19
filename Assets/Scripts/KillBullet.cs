using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBullet : MonoBehaviour {
	public GameObject firetag; //new
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        Destroy(gameObject, 5);
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameObject.SetActive(false);
        }
		if (collision.gameObject.tag != "Player") 
		{
			Destroy (gameObject);
		}
    }

	public void Setfiretag(GameObject tag) //new
    {
        firetag = tag;

    }

	public GameObject Getfiretag() //new
    {

        return firetag;
    }
}

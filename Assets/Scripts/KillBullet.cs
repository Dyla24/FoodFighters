using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillBullet : MonoBehaviour {
    public string firetag; //new
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
    }

    public void Setfiretag(string tag) //new
    {
        firetag = tag;

    }

    public string Getfiretag() //new
    {

        return firetag;
    }
}

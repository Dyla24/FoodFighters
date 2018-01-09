using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupCart : MonoBehaviour {
    public GameObject[] powerups = new GameObject[4];
    int hits;
    public GameObject confetti;
    GameObject pspawnl;
    bool pspawn;

	// Use this for initialization
	void Start ()
    {
        hits = 3;
        pspawnl = transform.GetChild(0).gameObject;
        pspawn = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Bullet")
        {
            hits--;
            if(hits == 0)
            {
                if (pspawn)
                {
                    pspawn = false;
                    hits = 3;
                    GameObject c = (GameObject)Instantiate(confetti, this.transform.position, Quaternion.identity, this.gameObject.transform);
                    Destroy(c, 2);
                    GameObject p = (GameObject)Instantiate(powerups[Random.Range(0, 3)], pspawnl.transform.position, Quaternion.identity);
                    StartCoroutine(powerupreset());
                }
            }
        }
    }
    IEnumerator powerupreset()
    {
        yield return new WaitForSeconds(10);
        pspawn = true;
        yield return null;
    }
}

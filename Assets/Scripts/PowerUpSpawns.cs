using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawns : MonoBehaviour {

    public List<GameObject> powerUps = new List<GameObject>();
    public List<GameObject> PU_spawnPoints = new List<GameObject>();
    private List<GameObject> PU_active = new List<GameObject>();

    // Use this for initialization
    void Start ()
    {
        spawnObjects();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void spawnObjects()
    {
        for (int i = 0; i < 6; i++)
        {
            PU_active.Add(Instantiate(powerUps[i], PU_spawnPoints[i].transform.position, PU_spawnPoints[i].transform.rotation));
        }
    }
}

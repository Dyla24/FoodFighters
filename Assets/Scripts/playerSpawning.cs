using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSpawning : MonoBehaviour
{
    public static playerSpawning Instance;

    public List<GameObject> players = new List<GameObject>();
    public List<GameObject> spawnPoints = new List<GameObject>();
    private List<GameObject> activePlayers = new List<GameObject>();

    private void Awake()
    {
        Instance = this;
    }

    // Use this for initialization
    void Start ()
    {
        spawnObjects();
	}
	
	// Update is called once per frame
	void Update ()
    {

	}

    void spawnObjects()
    {
        for (int i = 0; i < 4; i++)
        {
            activePlayers.Add(Instantiate(players[i], spawnPoints[i].transform.position, Quaternion.identity));
        }
    }

    public static void spawn(GameObject playerToSpawn)
    {
        for(int i = 0; i < Instance.activePlayers.Count; ++i)
        {
            if(playerToSpawn == Instance.activePlayers[i])
            {
                playerToSpawn.transform.position = Instance.spawnPoints[i].transform.position;
            }
        }
    }

}

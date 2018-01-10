using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class Scoremanager : MonoBehaviour {

    // Tag the score manager object with scoremanager

   // int Player1score;
   // int Player2score;
   // int Player3score;
   // int Player4score;
	public GameObject[] leaderboard = new GameObject[4];
	public GameObject[] players = new GameObject[4];
	int[] playerscore = new int[4]; 
	public GameObject[] kdimages = new GameObject[4];

    void Start ()
	{
		hudref ();
	}
	public void hudref()
	{
		for (int i = 0; i < 4; i++) 
		{
			kdimages [i] = GameObject.FindGameObjectWithTag ("P"+(i+1)+"hud").transform.GetChild(3).GetChild(1).gameObject;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		
		for (int i = 0; i < 4; i++) 
		{
			kdimages [i].transform.GetChild (1).GetChild (0).GetComponent<Text> ().text = (players [0].GetComponent<Charactercontroller> ().Getkills () + "\n" + players [0].GetComponent<Charactercontroller> ().deaths.ToString());
			kdimages [i].transform.GetChild (2).GetChild (0).GetComponent<Text> ().text = (players [1].GetComponent<Charactercontroller> ().Getkills () + "\n" + players [1].GetComponent<Charactercontroller> ().deaths.ToString());
			kdimages [i].transform.GetChild (3).GetChild (0).GetComponent<Text> ().text = (players [2].GetComponent<Charactercontroller> ().Getkills () + "\n" + players [2].GetComponent<Charactercontroller> ().deaths.ToString());
			kdimages [i].transform.GetChild (4).GetChild (0).GetComponent<Text> ().text = (players [3].GetComponent<Charactercontroller> ().Getkills () + "\n" + players [3].GetComponent<Charactercontroller> ().deaths.ToString());
		}
	}

    public class Score : IComparable
    {
        public int score;
        public string playerName;

        public Score(int score, string playerName)
        {
            this.score = score;
            this.playerName = playerName;
        }

        public int CompareTo(object obj)
        {
            Score otherScore = obj as Score;
            if (otherScore != null)
            {
                return this.score.CompareTo(otherScore.score);
            }
            else
            {
                throw new ArgumentException("Object is not a Score");
            }
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}", this.playerName, this.score);
        }
    }
  

public void GetScores ()
    {
		for (int i = 0; i < 4; i++) 
		{
			playerscore [i] = players [i].GetComponent<Charactercontroller> ().Getkills ();
			players [i].GetComponent<Charactercontroller> ().tr = false;
		}

        List<Score> highscores = new List<Score>();
		char t = '_';
		for (int i = 0; i < 4; i++) 
		{
			string[] s = players [i].transform.GetChild(0).GetChild(1).gameObject.name.ToString ().Split(t);
			highscores.Add (new Score (playerscore [i],s[0]));
		}
        highscores.Sort();
		for (int i = 0; i < 4; i++) 
		{
			char breaker = ':';
			string[] s = highscores [i].ToString().Split(breaker);
			leaderboard [i].transform.GetChild (0).GetComponent<Text> ().text = s [0];
			leaderboard [i].transform.GetChild (1).GetComponent<Text> ().text = s [1];
		}
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Scoremanager : MonoBehaviour {
   

    // Tag the score manager object with scoremanager

    
    int Player1score;
    int Player2score;
    int Player3score;
    int Player4score;
  


   

    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update () {
		
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

        Player1score = GameObject.FindGameObjectWithTag("Player1").GetComponent<Charactercontroller>().Getkills();
        Player2score = GameObject.FindGameObjectWithTag("Player2").GetComponent<Charactercontroller>().Getkills();
        Player3score = GameObject.FindGameObjectWithTag("Player3").GetComponent<Charactercontroller>().Getkills();
        Player4score = GameObject.FindGameObjectWithTag("Player4").GetComponent<Charactercontroller>().Getkills();

        List<Score> highscores = new List<Score>();
        highscores.Add(new Score(Player1score, "Player 1"));
        highscores.Add(new Score(Player2score, "Player 2"));
        highscores.Add(new Score(Player3score, "Player 3"));
        highscores.Add(new Score(Player4score, "Player 4"));

        highscores.Sort();

        Debug.Log(highscores[3].ToString());
        Debug.Log(highscores[2].ToString());
        Debug.Log(highscores[1].ToString());
        Debug.Log(highscores[0].ToString());

        //for (int i = 0; i < highscores.Count; i++)
        //{
        //    Debug.Log(highscores[i].ToString());
        //}
    }
}

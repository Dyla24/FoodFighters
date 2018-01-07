using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Timer : MonoBehaviour {
	public Text text;
	public float starttime;
	public float minutes, seconds, miliseconds;
    public bool timer;
	public float ftime = 0;


    private bool scoredisplayed = true;

	// Use this for initialization
	void Start () 
	{
		
	}

	public void starttimer()
	{
		minutes = starttime;
		starttime = 0;
		text.text = string.Format("{0}:{1}:{2}", minutes, seconds, (int)miliseconds);
		timer = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (minutes == 0 && seconds == 0)
        {
            timer = false;
        }
        if (timer)
        {
            if (miliseconds <= 0)
            {
                if (seconds <= 0)
                {
                    minutes--;
                    seconds = 59;
                }
                else if (seconds >= 0)
                {
                    seconds--;
                }
                miliseconds = 100;
            }
            miliseconds -= Time.deltaTime * 100;
            if (seconds >= 10)
            {
                text.text = string.Format("{0}:{1}", minutes, seconds);
            }
            else if (seconds < 10)
            {
                text.text = string.Format("{0}:{1}", minutes, "0" + seconds);
            }
        }
        
		if (!timer && starttime == 0)
        {
			Time.timeScale = Mathf.Lerp(1, 0, ftime);
			ftime += Time.deltaTime/2;
            if (Time.timeScale <= 0.3f)
            {
                Time.timeScale = 1;
                if (scoredisplayed == true)
                {
                    Invoke("LS", 2);
                    scoredisplayed = !scoredisplayed;
                }
            }
        }
	}
	void LS()
	{
        GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<Scoremanager>().GetScores(); // new

        Time.timeScale = 0;
    }

}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crosshairmanager : MonoBehaviour {
	public Gamesettings gamesettings;
	public float ccentergap, cdotsize, cthickness, clength;
	public GameObject dot, ubar, dbar, lbar, rbar;

	// Use this for initialization
	void Awake () 
	{
		//findsetting ();
        setup();
	}
    void setup()
    {
        if(Settingsmanager.settings != null)
        {
            gamesettings = Settingsmanager.settings.gamesettings;
        }
        if (gamesettings == null)
        {
            ccentergap = 10;
            cdotsize = 10;
            cthickness = 10;
            clength = 10;
        }
        else
        {
            ccentergap = gamesettings.ccentergap;
            cdotsize = gamesettings.cdotsize;
            cthickness = gamesettings.cthickness;
            clength = gamesettings.clength;
        }
        centergap();
        dotsize();
        thickness();
        crosshairlength();
    }

	public void centergap()
	{
        if (gamesettings != null)
        {
            ccentergap = gamesettings.ccentergap;
            if (ccentergap == 0)
                ccentergap = 10;
            this.GetComponent<RectTransform>().sizeDelta = new Vector2(ccentergap, ccentergap);
        }
	}
	public void dotsize()
	{
		if (gamesettings != null) {
			cdotsize = gamesettings.cdotsize;
			if (cdotsize == 0)
				cdotsize = 10;
			dot.GetComponent<RectTransform> ().sizeDelta = new Vector2 (cdotsize, 100);
		}
	}
	public void thickness()
	{
		if (gamesettings != null) {
			cthickness = gamesettings.cthickness;
			if (cthickness == 0)
				cthickness = 10;
			ubar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (cthickness, clength);
			dbar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (cthickness, clength);
			lbar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (clength, cthickness);
			rbar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (clength, cthickness);
		}
	}
	public void crosshairlength()
	{
		if (gamesettings != null) {
			clength = gamesettings.clength;
			if (clength == 0)
				clength = 40;
			ubar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (cthickness, clength);
			dbar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (cthickness, clength);
			lbar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (clength, cthickness);
			rbar.GetComponent<RectTransform> ().sizeDelta = new Vector2 (clength, cthickness);
		}
	}
}

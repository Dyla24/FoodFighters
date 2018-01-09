using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEngine.SceneManagement;

public class Settingsmanager : MonoBehaviour {
	public static Settingsmanager settings;
	public Toggle fullscreen, sprint;
	public Dropdown quality, resolutiondropdown, vsync;
	public Slider ccentergap, cdotsize, cthickness, clength, sensitivity;
	public Gamesettings gamesettings;
	public Resolution[] resolutions;
	public GameObject crosshair;
	public int[] character = new int[4];
	public GameObject applysettings;

	void Awake()
	{
		if (settings == null) {
			DontDestroyOnLoad (gameObject);
			settings = this;
		} else if (settings != this)
			Destroy (gameObject);
		gamesettings = new Gamesettings();
		SceneManager.activeSceneChanged += levelloaded;
		setupsettings ();
		loadsettings ();
	}
	public void setupsettings()
	{
		fullscreen.onValueChanged.AddListener(delegate {fullscreenchange();});
		quality.onValueChanged.AddListener(delegate {qualitychange();});
		resolutiondropdown.onValueChanged.AddListener(delegate {resolutionchange();});
		vsync.onValueChanged.AddListener(delegate {vsyncchange();});
		ccentergap.onValueChanged.AddListener(delegate {centergapchange();});
		cdotsize.onValueChanged.AddListener(delegate {dotsizechange();});
		cthickness.onValueChanged.AddListener(delegate {thicknesschange();});
		clength.onValueChanged.AddListener(delegate {crosshairlengthchange();});
        sprint.onValueChanged.AddListener(delegate {schange(); });
        sensitivity.onValueChanged.AddListener(delegate { senschange(); });
		applysettings.GetComponent<Button> ().onClick.AddListener (delegate {savesettings ();});
		resolutions = Screen.resolutions;
		foreach (Resolution resolution in resolutions) 
		{
			resolutiondropdown.options.Add (new Dropdown.OptionData (resolution.ToString ()));
		}
	}
	public void levelloaded(Scene a, Scene b)
	{
		if (b.buildIndex == 0) {
			mainmenureferences sr = GameObject.FindGameObjectWithTag ("Canvas").GetComponent<mainmenureferences> ();
			fullscreen = sr.fullscreen;
			quality = sr.quality;
			resolutiondropdown = sr.resolutiondropdown;
			vsync = sr.vsync;
			ccentergap = sr.ccentergap;
			cdotsize = sr.cdotsize;
			cthickness = sr.cthickness;
			clength = sr.clength;
			crosshair = sr.crosshair;
            sensitivity = sr.sensitivity;
            sprint = sr.sprint;
			applysettings = sr.applysettings;
			setupsettings ();
			loadsettings ();
		}
	}

	public void fullscreenchange()
	{
		Screen.fullScreen = gamesettings.fullscreen = fullscreen.isOn;
	}
	public void qualitychange()
	{
		QualitySettings.masterTextureLimit = gamesettings.quality = quality.value;
	}
	public void resolutionchange()
	{
		Screen.SetResolution (resolutions [resolutiondropdown.value].width, resolutions [resolutiondropdown.value].height, Screen.fullScreen);
	}
	public void vsyncchange()
	{
		QualitySettings.vSyncCount = gamesettings.vsync = vsync.value;
	}
	public void centergapchange()
	{
		gamesettings.ccentergap = ccentergap.value;
		crosshair.GetComponent<crosshairmanager> ().centergap ();
	}
	public void dotsizechange()
	{
		gamesettings.cdotsize = cdotsize.value;
		crosshair.GetComponent<crosshairmanager> ().dotsize ();
	}
	public void thicknesschange()
	{
		gamesettings.cthickness = cthickness.value;
		crosshair.GetComponent<crosshairmanager> ().thickness ();
	}
	public void crosshairlengthchange ()
	{
		gamesettings.clength = clength.value;
		crosshair.GetComponent<crosshairmanager> ().crosshairlength ();
	}
    public void schange()
    {
        gamesettings.sprint = sprint.isOn;
    }
    public void senschange()
    {
        gamesettings.sensitivity = sensitivity.value;
    }

	public void savesettings()
	{
		string filename = "/videosettings.txt";
		StreamWriter gconfig = new StreamWriter (Application.persistentDataPath + filename, false);
		gconfig.WriteLine ("Fullscreen = " + gamesettings.fullscreen);
		gconfig.WriteLine ("Quality = " + gamesettings.quality);
		gconfig.WriteLine ("Resolution = " + gamesettings.resolutionindex);
		gconfig.WriteLine ("Vsync = " + gamesettings.vsync);
		gconfig.WriteLine ("Crosshair Settings");
		gconfig.WriteLine ("Center Gap = " + gamesettings.ccentergap);
		gconfig.WriteLine ("Dot Size = " + gamesettings.cdotsize);
		gconfig.WriteLine ("Thickness = " + gamesettings.cthickness);
		gconfig.WriteLine ("Crosshair Length = " + gamesettings.clength);
        gconfig.WriteLine("Controller Settings");
        gconfig.WriteLine("Shift = " + gamesettings.sprint);
        gconfig.WriteLine("Sensitivity = " + gamesettings.sensitivity);
		gconfig.Close ();
	}
	public void loadsettings()
	{
		string filename = "/videosettings.txt";
		StreamReader reader = new StreamReader (Application.persistentDataPath + filename);
		string fs = reader.ReadLine ();
		fullscreen.isOn = gamesettings.fullscreen = bool.Parse (fs.Substring (fs.Length - 5));
		string q = reader.ReadLine ();
		quality.value = gamesettings.quality = int.Parse (q.Substring (q.Length - 1));
		string r = reader.ReadLine ();
		resolutiondropdown.value = gamesettings.resolutionindex = int.Parse (r.Substring (r.Length - 1));
		string v = reader.ReadLine ();
		vsync.value = gamesettings.vsync = int.Parse (v.Substring (v.Length - 1));
		reader.ReadLine ();
		string cg = reader.ReadLine ();
		ccentergap.value = gamesettings.ccentergap = int.Parse(cg.Substring(cg.Length-2));
		string ds = reader.ReadLine ();
		cdotsize.value = gamesettings.cdotsize = int.Parse(ds.Substring(ds.Length-2));
		string ct = reader.ReadLine ();
		cthickness.value = gamesettings.cthickness = int.Parse(ct.Substring(ct.Length-2));
		string cl = reader.ReadLine ();
		clength.value = gamesettings.clength = int.Parse(cl.Substring(cl.Length-2));
        reader.ReadLine();
        string sp = reader.ReadLine();
        sprint.isOn = gamesettings.sprint = bool.Parse(sp.Substring(sp.Length - 5));
        string se = reader.ReadLine();
        sensitivity.value = gamesettings.sensitivity = int.Parse(se.Substring(se.Length - 1));
		reader.Close ();
	}

}

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
	public Slider ccentergap, cdotsize, cthickness, clength, sensitivity,mastervol, backgroundvol,effectvol;
	public Gamesettings gamesettings;
	public Resolution[] resolutions;
	public GameObject crosshair;
	public int[] character = new int[4];
	public GameObject applysettings;
	GameObject bgm;

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
		mastervol.onValueChanged.AddListener(delegate {mvolchange();});
		backgroundvol.onValueChanged.AddListener (delegate {bvolchange ();});
		effectvol.onValueChanged.AddListener (delegate {evolchange ();});
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
			mastervol = sr.mastervol;
			backgroundvol = sr.backgroundvol;
			effectvol = sr.effectvol;
            sensitivity = sr.sensitivity;
            sprint = sr.sprint;
			applysettings = sr.applysettings;
			setupsettings ();
			loadsettings ();
		}
		if (b.buildIndex == 2) 
		{
			bvolset ();
		}
	}
	void bvolset()
	{
		bgm = GameObject.FindGameObjectWithTag ("BGM");
		bgm.transform.GetChild (0).GetComponent<AudioSource> ().volume = gamesettings.backgroundvolume;
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
	public void mvolchange()
	{
		AudioListener.volume = gamesettings.mastervolume = mastervol.value;
	}
	public void bvolchange()
	{
		if (bgm == null) {
			bgm = GameObject.FindGameObjectWithTag ("BGM");
		}
		bgm.transform.GetChild(0).GetComponent<AudioSource> ().volume = gamesettings.backgroundvolume = backgroundvol.value;
	}
	public void evolchange()
	{
		gamesettings.effectvolume = effectvol.value;
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
		gconfig.WriteLine ("Sound Settings");
		gconfig.WriteLine ("Master Volume = " + gamesettings.mastervolume);
		gconfig.WriteLine ("Background Volume = " + gamesettings.backgroundvolume);
		gconfig.WriteLine ("Effect Volume = " + gamesettings.effectvolume);
        gconfig.WriteLine("Controller Settings");
        gconfig.WriteLine("Shift = " + gamesettings.sprint);
        gconfig.WriteLine("Sensitivity = " + gamesettings.sensitivity);
		gconfig.Close ();
	}
	public void loadsettings()
	{
		string filename = "/videosettings.txt";
		char breaker = '=';
		StreamReader reader = new StreamReader (Application.persistentDataPath + filename);
		string[] fs = reader.ReadLine ().Split (breaker);
		fullscreen.isOn = gamesettings.fullscreen = bool.Parse (fs[1]);
		string[] q = reader.ReadLine ().Split (breaker);
		quality.value = gamesettings.quality = int.Parse (q[1]);
		string[] r = reader.ReadLine ().Split (breaker);
		resolutiondropdown.value = gamesettings.resolutionindex = int.Parse (r[1]);
		string[] v = reader.ReadLine ().Split (breaker);
		vsync.value = gamesettings.vsync = int.Parse (v[1]);
		reader.ReadLine ();
		string[] cg = reader.ReadLine ().Split (breaker);
		ccentergap.value = gamesettings.ccentergap = int.Parse(cg[1]);
		string[] ds = reader.ReadLine ().Split (breaker);
		cdotsize.value = gamesettings.cdotsize = int.Parse(ds[1]);
		string[] ct = reader.ReadLine ().Split (breaker);
		cthickness.value = gamesettings.cthickness = int.Parse(ct[1]);
		string[] cl = reader.ReadLine ().Split (breaker);
		clength.value = gamesettings.clength = int.Parse(cl[1]);
		reader.ReadLine ();
		string[] mv = reader.ReadLine ().Split (breaker);
		mastervol.value = gamesettings.mastervolume = float.Parse (mv [1]);
		string[] bv = reader.ReadLine ().Split (breaker);
		backgroundvol.value = gamesettings.backgroundvolume = float.Parse (bv [1]);
		string[] ev = reader.ReadLine ().Split (breaker);
		effectvol.value = gamesettings.effectvolume = float.Parse (ev [1]);
        reader.ReadLine();
		string[] sp = reader.ReadLine().Split (breaker);
        sprint.isOn = gamesettings.sprint = bool.Parse(sp[1]);
		string[] se = reader.ReadLine().Split (breaker);
        sensitivity.value = gamesettings.sensitivity = int.Parse(se[1]);
		reader.Close ();
	}

}

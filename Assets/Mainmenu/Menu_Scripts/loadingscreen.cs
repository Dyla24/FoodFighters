using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadingscreen : MonoBehaviour {
	public Slider progress;
	//AsyncOperation load;

	// Use this for initialization
	void Start () 
	{
		StartCoroutine (LoadingScreen ());	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	IEnumerator LoadingScreen()
	{
		AsyncOperation load = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
		while (load.progress < 0.9f) 
		{
			progress.value = 0.5f * load.progress/ 0.9f;
		}
		load.allowSceneActivation = true;
		while (!load.isDone) 
		{
			yield return null;
			progress.value = Mathf.Lerp (progress.value, 1, 0.2f);
		}
	}
}

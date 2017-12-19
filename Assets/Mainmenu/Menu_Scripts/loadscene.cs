using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadscene : MonoBehaviour {
	public int scene;
	
	public void sceneload()
	{
		SceneManager.LoadScene (scene);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainmenuanimations : MonoBehaviour {
	public Animator animator;
	public GameObject characterselect, mainmenu, options, credits, mpanel1, mpanel2;




	public void pressstart()
	{
		animator.SetTrigger ("Mainstart");
	}

	public void maintocharselect()
	{
		animator.SetTrigger ("Maintocharselect");
	}

	public void chartomain()
	{
		animator.SetTrigger ("Chartomain");
	}
	public void maintooptions()
	{
		animator.SetTrigger ("Maintooptions");
	}
	public void optionstomain()
	{
		animator.SetTrigger ("optionstomain");
	}

	public void togglechar()
	{
		characterselect.SetActive (!characterselect.activeInHierarchy);
	}
	public void togglemain()
	{
		mainmenu.SetActive (!mainmenu.activeInHierarchy);
	}
	public void toggleoptions()
	{
		options.SetActive (!options.activeInHierarchy);
	}
	public void togglepanels()
	{
		mpanel1.SetActive (!mpanel1.activeInHierarchy);
		mpanel2.SetActive (!mpanel2.activeInHierarchy);
	}
}


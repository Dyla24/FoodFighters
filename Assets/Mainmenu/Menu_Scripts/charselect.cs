using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class charselect : MonoBehaviour {
	public GameObject playbutton;
	private Settingsmanager sm;
	int i;
	public GameObject[] pickedchar = new GameObject[4];
	public Sprite[] charimage = new Sprite[4];
	public GameObject hand;
	public Vector2[] handpos = new Vector2[4];
	public GameObject[] buttons = new GameObject[4];
	public GameObject slight;
	public Vector3[] lightpos = new Vector3[4];
    public GameObject activebutton;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void charselected(int charnumber)
	{
		Settingsmanager.settings.character [i] = charnumber;
		pickedchar [i].GetComponent<Image>().sprite = charimage[charnumber];
		i++;
		if (i == 4) 
		{
			playbutton.SetActive (true);
		}
		for (int j = 0; j < buttons.Length; j++){
			if (buttons [j].GetComponent<Button> ().interactable) 
			{
				EventSystem.current.SetSelectedGameObject (buttons [j]);
				break;
			}
		}
	}
	public void onselect(int bno)
	{
		hand.GetComponent<RectTransform> ().anchoredPosition = handpos [bno];
		slight.GetComponent<RectTransform>().anchoredPosition = lightpos [bno];
	}
    public void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(activebutton);
        activebutton.GetComponent<Button>().OnSelect(null);
    }
    public void OnDisable()
    {
        foreach (GameObject button in buttons)
        {
            button.GetComponent<Button>().interactable = true;
        }
        i = 0;
        playbutton.SetActive(false);
        foreach (GameObject image in pickedchar)
        {
            image.GetComponent<Image>().sprite = null;
        }
    }
}

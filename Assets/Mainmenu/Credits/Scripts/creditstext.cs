using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class creditstext : MonoBehaviour {
	public TextAsset text;
    public GameObject[] Letters = new GameObject[27];
    Dictionary<string, GameObject> L = new Dictionary<string, GameObject>();
	public List<string> credits;
	public int textspeed;
	public int i = 0;
    public GameObject container;
	public GameObject backbut;

	// Use this for initialization
	void Start () {
		credits = TextAssetToList (text);
        for (int i = 0; i < Letters.Length; i++)
        {
            L.Add(Letters[i].transform.name,Letters[i]);
        }
        StartCoroutine(textspawns());
    }
	void Update ()
    {

	}
	private List<string> TextAssetToList(TextAsset ta) {
		return new List<string> (ta.text.Split('\n'));
	}
	public IEnumerator textspawns()
	{
		while (true) {
			string txt = credits [i];
			if(txt.Length >1)
			{
                GameObject creditstext = (GameObject)Instantiate(container, transform.position, Quaternion.identity, this.transform);
                txt = txt.Trim();
                string[] characters = new string[txt.Length];
                for (int j = 0; j < txt.Length; j++)
                {
                    characters[j] = txt[j].ToString();
                    if (characters[j].Contains(" "))
                    {
						Instantiate(L["Space"], transform.position, L["Space"].transform.rotation, creditstext.transform);
                    }
                    else
                    {
						Instantiate(L[characters[j].ToUpper()], transform.position, L[characters[j].ToUpper()].transform.rotation, creditstext.transform);
                    }
                }
				creditstext.GetComponent<Rigidbody> ().velocity = Vector3.up * textspeed;
			}
			i++;
			yield return new WaitForSeconds (1);
			if (i == credits.Count) {
				yield return new WaitForSeconds (12);
				backbut.GetComponent<Exit> ().LS ();
				break;
			}
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour {

    public GameObject gunModel;
    public Animation gunAnim;
    public GameObject bulletPrefab;
    public Rigidbody bulletRB;
    public Shader gunShader;
    public Texture gunText;
    public Color gunColor;
    public float firerate;
    public float bulletspeed;
    private float lastShot = 0.0f;
    public string fireController;
    public string reloadController;
    public float ammo;
    bool reloadcheck;
    float baseammo;
    public float shotspershot = 1;
    public Text textbox;
	GameObject pewpew;
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
    string playertag; // new
=======
    public Animator animator;
=======
>>>>>>> parent of 1b9d7ce... Started to add animations
=======
>>>>>>> parent of 1b9d7ce... Started to add animations

>>>>>>> 349ed7a4731348feb38ce03d1e07ddbcaa87dfab

    // Use this for initialization
    void Start ()
    {
        baseammo = ammo;

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> parent of 1b9d7ce... Started to add animations
=======
>>>>>>> parent of 1b9d7ce... Started to add animations
        Renderer rend = GetComponent<Renderer>();
        rend.material = new Material(gunShader);
        rend.material.mainTexture = gunText;
        rend.material.color = gunColor;
<<<<<<< HEAD
<<<<<<< HEAD

        if (this.transform.parent.parent) { playertag = this.transform.parent.parent.tag; } //new need to assign player tags to p1,2,3,4
=======
>>>>>>> 349ed7a4731348feb38ce03d1e07ddbcaa87dfab
=======
        

>>>>>>> parent of 1b9d7ce... Started to add animations
=======
        

>>>>>>> parent of 1b9d7ce... Started to add animations
    }
	
	// Update is called once per frame
	void Update ()
    {
        //Ammo_UI();
        float primaryAttack = Input.GetAxis(fireController);
        bool reload = Input.GetButtonDown(reloadController);
        if (primaryAttack <= -0.37f)
        {
            if (ammo <= 0)
            {
                reloadcheck = true;
            }
            else
            {
                fire();
<<<<<<< HEAD
<<<<<<< HEAD
                animator.SetBool("IsShooting", true);
                reloadcheck = true;
=======
>>>>>>> parent of 1b9d7ce... Started to add animations
=======
>>>>>>> parent of 1b9d7ce... Started to add animations

            }
        }
        else
        {
            animator.SetBool("IsShooting", false);
        }

        if(reloadcheck == true)
        {
             if (reload == true)
             {
                ammo = baseammo;
                reloadcheck = false;
             }

        }
		
	}

    void fire()
    {
        if (Time.time > firerate + lastShot)
        {
            ammo -= shotspershot;

<<<<<<< HEAD
<<<<<<< HEAD
            pewpew = (GameObject)Instantiate(bulletPrefab, transform.GetChild(0).transform.position, Quaternion.identity);
<<<<<<< HEAD
=======
            Ray ray = transform.parent.GetChild(0).GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                pewpew.transform.LookAt(hit.point);
                pewpew.GetComponent<Rigidbody>().velocity = pewpew.transform.forward * -bulletspeed;
            }
            else
            {
                pewpew.GetComponent<Rigidbody>().velocity = ray.direction * -bulletspeed;
            }
>>>>>>> 349ed7a4731348feb38ce03d1e07ddbcaa87dfab
=======
=======
>>>>>>> parent of 1b9d7ce... Started to add animations
			pewpew = (GameObject)Instantiate (bulletPrefab, transform.GetChild (0).transform.position, Quaternion.identity);
			Ray ray = transform.parent.GetChild (0).GetComponent<Camera> ().ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0));
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit)) {
				pewpew.transform.LookAt (hit.point);
				pewpew.GetComponent<Rigidbody> ().velocity = pewpew.transform.forward * -bulletspeed;
			} else {
				pewpew.GetComponent<Rigidbody> ().velocity = ray.direction * -bulletspeed;
			}
<<<<<<< HEAD
>>>>>>> parent of 1b9d7ce... Started to add animations

            pewpew.GetComponent<KillBullet>().Setfiretag(playertag);
=======
>>>>>>> parent of 1b9d7ce... Started to add animations

            Ray ray = transform.parent.GetChild(0).GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                pewpew.transform.LookAt(hit.point);
                pewpew.GetComponent<Rigidbody>().velocity = pewpew.transform.forward * -bulletspeed;
            }
            else
            {
                pewpew.GetComponent<Rigidbody>().velocity = ray.direction * -bulletspeed;
            }

            lastShot = Time.time;
        }

    }

    void Ammo_UI()
    {
        textbox.text = "Ammo: " + ammo.ToString();
    }

    
}

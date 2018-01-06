using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunScript : MonoBehaviour {

    public GameObject bulletPrefab;
    public Rigidbody bulletRB;
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
    public Animator animator;


    // Use this for initialization
    void Start ()
    {
        baseammo = ammo;

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
                animator.SetBool("IsShooting", false);
            }
            else
            {
                fire();
                reloadcheck = true;

            }
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

            pewpew = (GameObject)Instantiate(bulletPrefab, transform.GetChild(0).transform.position, Quaternion.identity);
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

            //Rigidbody BulletCreate = Instantiate(bulletRB, transform.GetChild(0).transform.position, transform.GetChild(0).transform.rotation);

            //BulletCreate.velocity = transform.TransformDirection(new Vector3(bulletspeed, 0, 0));

            lastShot = Time.time;
            animator.SetBool("IsShooting", true);
        }
        else
        {
            animator.SetBool("IsShooting", false);
        }

    }

    void Ammo_UI()
    {
        textbox.text = "Ammo: " + ammo.ToString();
    }

    
}

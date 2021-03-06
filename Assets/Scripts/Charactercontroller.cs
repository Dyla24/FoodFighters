﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Charactercontroller : MonoBehaviour {
    private GameObject pcamera;
    private GameObject playerhud;
    public string HudTag;
    private GameObject uicrosshair;
    public AnimationClip deathClip;
    //GameObject spawn;
    Vector3 movementh, movementv;
    public Vector3 movement;
    private Animator animator;
    private Vector3 shootdirection;
    private Rigidbody myrigidbody;
    public string controllerHorizontal, controllerVertical, controllerHorizontalRight, controllerVerticalRight, controllerJump, controllerLeftClick, controllerMap;
    public string controllerEscape;
    public float moveh, movev, vrotation;
    public float vrangeu = 40f, vranged = 20f;
    public float nspeed;
    private float sspeed;
    private bool sprint;
    public int curhealth;
    public bool grounded;
    private bool doublejump, jumpkey;
    public AnimationClip jumpclip;
    [Range(1, 10)]
    public float jumpheight;
    private bool jump;
    public int bulletStrength;
    public Text textbox;
    private float speed;
    public bool ammoadder;
    public int ammoHolder;
    int starthealth;
	public GameObject shoulder;
	public float ammopercentage;
	public float startammo;
	public GunScript gun;
    string lasthitby; // new
	public int kills; //new
    public GameObject killer; //new
    bool killconfirm; //new
	public bool tr;
    public GameObject timer;
    float sensitivity = 2;
    Gamesettings gamesettings;
	public int deaths;

    void Start()
    {
		playerhud = GameObject.FindGameObjectWithTag(HudTag);
        killconfirm = true; //new
        kills = 0; //new
        lasthitby = "N/A"; // new
        starthealth = 25;
        nspeed = 5;
        sspeed = nspeed * 1.5f;
        myrigidbody = this.GetComponent<Rigidbody>();
        pcamera = this.gameObject.GetComponentInChildren<Camera>().gameObject;
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        uicrosshair = playerhud.transform.GetChild(0).gameObject;
        curhealth = starthealth;
        gun = pcamera.transform.parent.GetChild(1).gameObject.GetComponent<GunScript>();
        startammo = gun.ammo;
        timer = GameObject.FindGameObjectWithTag("Timer");
        if (Settingsmanager.settings != null)
        {
            gamesettings = Settingsmanager.settings.gamesettings;
        }
		if (gamesettings != null) {
			if (gamesettings.sensitivity != 0) 
			{
				sensitivity = gamesettings.sensitivity;
			}
			sprint = gamesettings.sprint;
		} else {
			sensitivity = 2;
			sprint = false;
		}
	}

    void Update()
    {
		if (!tr)
		{
			tr = timer.GetComponent<Timer>().timer;
			movement = Vector3.zero;
			animator.SetFloat ("SetSpeed", myrigidbody.velocity.magnitude);
			//print (myrigidbody.velocity);
		}
        if (curhealth <= 0)
        {
            //killer = GameObject.FindGameObjectWithTag(lasthitby); // new
            if (killconfirm == true)
            {
                killer.GetComponentInChildren<Charactercontroller>().Addkill(); // new
                killconfirm = false;
				deaths++;
            }
            StartCoroutine(respawn());
        }
        UI_Health();
        if (tr)
        {
            character_movement();
        }
        if (ammoadder == true)
        {
            gun.reloads += ammoHolder;
            ammoadder = false;
            ammoHolder = 0;
		}
        pcamera.transform.parent.position = shoulder.transform.position;
    }

	void FixedUpdate()
	{
		//applies movements through physics update instead of framerate update
		if (grounded) {
			myrigidbody.velocity = movement;
		}
		if (jump) 
		{
            myrigidbody.velocity += jumpheight * Vector3.up;
            animator.SetBool("IsJumping", true);
            jump = false;
		}
        if (myrigidbody.velocity.y < 0)
        {
            myrigidbody.velocity += Vector3.up * Physics.gravity.y * (2f - 1) * Time.deltaTime;
        }

	}

	private void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.tag == "Bullet")
        {
            curhealth = curhealth - bulletStrength;
			killer = collision.gameObject.GetComponent<KillBullet>().Getfiretag(); // new

            collision.gameObject.SetActive(false);
        }
    }

    public void UI_Health()
    {
		if (playerhud != null) {
			Image himage = playerhud.transform.GetChild (1).GetChild(0).GetComponent<Image> ();
			float hpper = curhealth;
			hpper = hpper / starthealth;
			if (hpper < 1)
            {
                float ho = hpper * (100 - 25) % (100 - 25) + 25;
                himage.fillAmount = ho/100;
            }
			else
            {
                himage.fillAmount = 1;
            }
			Image aimage = playerhud.transform.GetChild (2).GetChild(1).GetComponent<Image> ();
			ammopercentage = gun.ammo / startammo;
            if (ammopercentage < 1)
            {
                float ao = ammopercentage * (100 - 25) % (100 - 25) + 25;
                aimage.fillAmount = ao / 100;
            }
            else
            {
                aimage.fillAmount = 1;
            }
            if(ammopercentage == 0)
            {
                aimage.transform.parent.GetChild(0).GetComponent<Text>().text = "X";
                aimage.transform.parent.GetChild(0).GetComponent<Text>().color = Color.red;
                aimage.transform.parent.GetChild(0).localScale = new Vector3(2, 2, 2);
            }
            else
            {
                aimage.transform.parent.GetChild(0).GetComponent<Text>().text = (ammopercentage * 100).ToString();
                aimage.transform.parent.GetChild(0).GetComponent<Text>().color = new Color32(17, 17, 17,255);
                aimage.transform.parent.GetChild(0).localScale = new Vector3(1, 1, 1);
            }
			aimage.transform.parent.GetChild (2).GetComponent<RectTransform> ().sizeDelta = new Vector2 (160 * gun.reloads, 181);
		}
    }



    public int Getkills() //new
    {
        return kills;
    }

    public void Addkill() //new
    {
        kills += 1;
    }

    IEnumerator respawn()
    {
        animator.SetBool("IsDead", true);
        myrigidbody.useGravity = false;
        myrigidbody.detectCollisions = false;
        curhealth = 0;

        yield return new WaitForSeconds(deathClip.length);

        animator.SetBool("IsDead", false);
        myrigidbody.useGravity = true;
        myrigidbody.detectCollisions = true;

		transform.GetChild(0).rotation = transform.rotation;
		transform.GetChild(0).position = transform.position;
        curhealth = starthealth;
        killconfirm = true;
        playerSpawning.spawn(gameObject);
    }

    public void character_movement()
    {
        //sets input direction and jump
        moveh = Input.GetAxisRaw(controllerHorizontal);
        movev = Input.GetAxisRaw(controllerVertical);
        movementh = transform.right * moveh;
        movementv = transform.forward * movev;
        jumpkey = Input.GetButtonDown(controllerJump);

        //applies speed and sets movement if the value is high enough (an extra deadzone basically)
        movement = (movementh + movementv) * speed;
        animator.SetFloat("SetSpeed", myrigidbody.velocity.magnitude);

        //checks for sprint button then applied the toggle and sets speed
        if (Input.GetButtonDown(controllerLeftClick))
            sprint = !sprint;
        if (sprint)
        {
            speed = sspeed;
        }
        else
        {
            speed = nspeed;
        }


        //checks if the player is touching the ground
		if (Physics.CheckSphere(transform.position, 0.06f, 1 << 8))
        {
            grounded = true;
            doublejump = true;
        }
        else
        {
            grounded = false;
        }
        //triggers jump on the next psysics update
        if (jumpkey == true && grounded == true)
        {
            StartCoroutine(Jumping());
            jump = true;
        }
        if (jumpkey == true && grounded == false && doublejump == true)
        {
            jump = true;
            doublejump = false;
        }


        //left/right rotation
        shootdirection = new Vector2(0f, Input.GetAxisRaw(controllerHorizontalRight));
        if (shootdirection.sqrMagnitude >= 0.2f)
        {
			transform.Rotate(shootdirection * (sensitivity + 0.2f));
        }
        //up/down rotation
		vrotation += Input.GetAxisRaw(controllerVerticalRight)* ((sensitivity + 0.2f)/2);
		vrotation = Mathf.Clamp(vrotation, -vrangeu, vranged);
		pcamera.transform.parent.localRotation = Quaternion.Euler (vrotation, 0, 0);
		RaycastHit hit;
		if (Physics.Linecast (pcamera.transform.parent.GetChild(2).position, new Vector3(transform.position.x,pcamera.transform.parent.position.y,transform.position.z), out hit,1 << 8)) {
			pcamera.transform.localPosition = Vector3.Lerp(pcamera.transform.localPosition, new Vector3 (pcamera.transform.localPosition.x, pcamera.transform.localPosition.y, transform.InverseTransformPoint(hit.point).z + 0.1f),Time.deltaTime*2);
		}else {
			pcamera.transform.localPosition = Vector3.Lerp (pcamera.transform.localPosition, pcamera.transform.parent.GetChild (2).localPosition, Time.deltaTime*2);
		}
        if (playerhud != null && uicrosshair != null)
        {
            if (Input.GetButtonDown(controllerMap))
            {
				playerhud.transform.GetChild(3).gameObject.SetActive(true);
                uicrosshair.SetActive(false);
            }
            else if (Input.GetButtonUp(controllerMap))
            {
				playerhud.transform.GetChild(3).gameObject.SetActive(false);
                uicrosshair.SetActive(true);
            }
        }
		if (Input.GetButton(controllerMap) && Input.GetButton(controllerEscape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }

    IEnumerator Jumping()
    {
        animator.SetBool("IsJumping", true);

        yield return new WaitForSeconds(jumpclip.length);

        animator.SetBool("IsJumping", false);
    }
}

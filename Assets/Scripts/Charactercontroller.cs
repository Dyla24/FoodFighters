using System.Collections;
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
    private float nspeed, sspeed;
    private bool sprint;
    private int curhealth;
    public bool grounded;
    private bool doublejump, jumpkey;
    [Range(1, 10)]
    public float jumpheight;
    private bool jump;
    public int bulletStrength;
    public Text textbox;
    private float speed;
    int starthealth;
	public GameObject shoulder;
	public float ammopercentage;
	public float startammo;
	GunScript gun;
    string lasthitby; // new
    int kills; //new
    public GameObject killer; //new
    bool killconfirm; //new
    bool tr;
    public GameObject timer;

    void Start()
    {
        killconfirm = true; //new
        kills = 0; //new
        lasthitby = "N/A"; // new
        starthealth = 10;
        nspeed = 10;
        sspeed = nspeed * 1.5f;
        myrigidbody = this.GetComponent<Rigidbody>();
        playerhud = GameObject.FindGameObjectWithTag(HudTag);
        pcamera = this.gameObject.GetComponentInChildren<Camera>().gameObject;
        animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        uicrosshair = playerhud.transform.GetChild(0).gameObject;
        curhealth = starthealth;
        gun = pcamera.transform.parent.GetChild(1).gameObject.GetComponent<GunScript>();
        startammo = gun.ammo;
        timer = GameObject.FindGameObjectWithTag("Timer");
		tr = false;
    }

    
	void Update () 
	{
		if (!tr) 
		{
			print ("true");
			tr = timer.GetComponent<Timer> ().timer;
		}
        if(curhealth <= 0)
        {

            killer = GameObject.FindGameObjectWithTag(lasthitby); // new
            if (killconfirm == true)
            {
                killer.GetComponentInChildren<Charactercontroller>().Addkill(); // new
                killconfirm = false;
            }
            StartCoroutine(respawn());
        }
        UI_Health();
        if (tr)
       	{
            character_movement();
        }
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
			print (myrigidbody.velocity);
			jump = false;
		}
		if (myrigidbody.velocity.y < 0) {
			myrigidbody.velocity += Vector3.up * Physics.gravity.y * (2f - 1) * Time.deltaTime;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
        if (collision.gameObject.tag == "Bullet")
        {
            curhealth = curhealth - bulletStrength;
            lasthitby = collision.gameObject.GetComponent<KillBullet>().Getfiretag(); // new

            collision.gameObject.SetActive(false);
        }
    }

    public void UI_Health()
    {
		if (playerhud != null) {
			Image himage = playerhud.transform.GetChild (1).GetChild(0).GetComponent<Image> ();
			float hpper = curhealth;
			hpper = hpper / starthealth;
			himage.fillAmount = hpper;
			Image aimage = playerhud.transform.GetChild (2).GetComponent<Image> ();
			ammopercentage = gun.ammo / startammo ;
			aimage.fillAmount = ammopercentage;
			aimage.transform.GetChild (0).GetComponent<Text> ().text = (ammopercentage * 100).ToString();
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
            transform.Rotate(shootdirection * 2.0f);
        }
        //up/down rotation
        vrotation += Input.GetAxisRaw(controllerVerticalRight);
		vrotation = Mathf.Clamp(vrotation, -vrangeu, vranged);
		pcamera.transform.parent.position = shoulder.transform.position;
		pcamera.transform.parent.localRotation = Quaternion.Euler (vrotation, 0, 0);

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
        if (Input.GetButton(controllerMap))
        {
           // UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}

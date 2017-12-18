using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Charactercontroller : MonoBehaviour {
	private GameObject pcamera;
    private GameObject playerhud;
    private GameObject uicrosshair;
    public AnimationClip deathClip;
    //GameObject spawn;
	Vector3 movementh, movementv;
	Vector3 movement;
    private Animator animator;
	private Vector3 shootdirection;
	private Rigidbody myrigidbody;
	public string controllerHorizontal, controllerVertical, controllerHorizontalRight, controllerVerticalRight, controllerJump, controllerLeftClick, controllerMap;
	public string controllerEscape;
	private float moveh, movev, vrotation;
    public float vrangeu = 40f, vranged = 20f;
	private float nspeed, sspeed;
	private bool sprint;
	private int curhealth;
    private bool grounded;
    private bool doublejump, jumpkey;
	public float jumpheight;
    private bool jump;
    public int bulletStrength;
    public Text textbox;
    private float speed;
    int starthealth;
	public GameObject shoulder;

	void Start () 
	{
		starthealth = 10;
		nspeed = 10;
		sspeed = nspeed * 1.5f;
		myrigidbody = this.GetComponent<Rigidbody> ();
        pcamera = this.gameObject.GetComponentInChildren<Camera>().gameObject;
		animator = transform.GetChild(0).gameObject.GetComponent<Animator>();
        uicrosshair = GameObject.FindGameObjectWithTag("P1hud").transform.GetChild(0).gameObject;
        curhealth = starthealth;
    }
    
	void Update () 
	{
        if(curhealth <= 0)
        {
            StartCoroutine(respawn());
        }
        UI_Health();
        character_movement();
	}

	void FixedUpdate()
	{
		//applies movements through physics update instead of framerate update
		if (grounded) {
			myrigidbody.velocity = movement;
		}
		if (jump) 
		{
            //myrigidbody.AddForce(Vector3.up * jumpheight, ForceMode.VelocityChange);
			jump = false;
		}
	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.tag == "Bullet") 
		{
            curhealth = curhealth - bulletStrength;
		}
	}

    public void UI_Health()
    {
        textbox.text = "Health: " + curhealth.ToString();
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

		transform.GetChild(0).rotation = Quaternion.identity;
		transform.GetChild(0).position = transform.position;
        curhealth = starthealth;
        playerSpawning.spawn(gameObject);
    }

    public void character_movement()
    {
        //sets input direction and jump
        moveh = Input.GetAxisRaw(controllerHorizontal);
        movev = -Input.GetAxisRaw(controllerVertical);
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
		if (Physics.CheckSphere(transform.position, 0.5f))
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
                playerhud.SetActive(true);
                uicrosshair.SetActive(false);
            }
            else if (Input.GetButtonUp(controllerMap))
            {
                playerhud.SetActive(false);
                uicrosshair.SetActive(true);
            }
        }
        if (Input.GetButton(controllerMap))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        }
    }
}

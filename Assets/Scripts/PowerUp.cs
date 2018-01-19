using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool PU_active;
    public float speedBoost;
    public int healthBoost;
    public int damageboost;
    public int ammo;
    public float timer = 5;
    public bool powerUpCollecter = false;
	public bool drop = false;
    public Charactercontroller bob;
    Collider b_collider;
    Renderer b_rend;
    Renderer b_part_rend;


	// Use this for initialization
	void Start ()
    {
        b_collider = GetComponent<Collider>();
        b_rend = transform.GetChild(1).gameObject.GetComponent<Renderer>();
        b_part_rend = transform.GetChild(0).gameObject.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update ()
    {
       
        if (PU_active == true)
        {
            timer -= Time.deltaTime;
           // print(timer);
            if (timer < 0)
            {
                PU_active = false;
                if (gameObject.CompareTag("Speedy"))
                {
                    bob.nspeed -= speedBoost;
                }
                if (gameObject.CompareTag("Hurty"))
                {
                    bob.bulletStrength -= damageboost;
                }
            }
		
        }
        
	}

    public void OnTriggerEnter(Collider other)
    {

        //bob = other.transform.getcomponent(charectorcontroller);
        Charactercontroller bobtemp = other.transform.GetComponent<Charactercontroller>();

        if (other.gameObject.CompareTag("Player") && bobtemp != null)
        {
            bob = bobtemp;

            if (gameObject.CompareTag("Healthy"))
            {
                PU_active = true;
                PU_Health();
                b_collider.enabled = false;
                b_rend.enabled = false;
                b_part_rend.enabled = false;
				if (!drop) 
				{
					powerUpCollecter = true;
					StartCoroutine(Powerupreset());
				}
            }
            if (gameObject.CompareTag("Speedy"))
            {
                PU_active = true;
                PU_speed();
                b_collider.enabled = false;
                b_rend.enabled = false;
				b_part_rend.enabled = false;
				if (!drop) 
				{
					powerUpCollecter = true;
					StartCoroutine(Powerupreset());
				}
            }
            if (gameObject.CompareTag("Hurty"))
            {
                PU_active = true;
                PU_Hurty_Ammo();
                b_collider.enabled = false;
                b_rend.enabled = false;
				b_part_rend.enabled = false;
				if (!drop) 
				{
					powerUpCollecter = true;
					StartCoroutine(Powerupreset());
				}
            }
            if (gameObject.CompareTag("Ammo"))
            {
                PU_active = true;
                PU_ammo();
                b_collider.enabled = false;
                b_rend.enabled = false;
                b_part_rend.enabled = false;
				if (!drop) 
				{
					powerUpCollecter = true;
					StartCoroutine(Powerupreset());
				}
            }
        }
    }

    void PU_speed()
    {
        if (PU_active == true)
        {
            bob.nspeed += speedBoost;
        }

       
    }

    void PU_Health()
    {
        if (PU_active == true)
        {
            bob.curhealth += healthBoost;
        }
       
    }

    void PU_Hurty_Ammo()
    {
        if (PU_active == true)
        {
            bob.bulletStrength += damageboost;
        }
       
    }

    void PU_ammo()
    {
        if (PU_active == true)
        {
            bob.ammoadder = true;
            bob.ammoHolder += ammo;
        }
    }

    IEnumerator Powerupreset()
    {
        if (powerUpCollecter == true)
        {
            yield return new WaitForSeconds(10);
            b_collider.enabled = true;
            b_rend.enabled = true;
            b_part_rend.enabled = true;
            powerUpCollecter = false;
        }
    }
}

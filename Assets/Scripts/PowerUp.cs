﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public bool PU_active;
    public float speedBoost;
    public int healthBoost;
    public int damageboost;
    public float timer = 5;
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
            print(timer);
            if (timer < 0)
            {
                PU_active = false;
               /* if (gameObject.CompareTag("Healthy"))
                {
                    bob.nspeed -= speedBoost;
                }
                else if (gameObject.CompareTag("Speedy"))
                {
                    bob.curhealth -= healthBoost;
                }*/
                if (gameObject.CompareTag("Hurty"))
                {
                    bob.bulletStrength -= damageboost;
                }


            }
		
        }
        
	}

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("Healthy"))
        {
            PU_active = true;
            PU_Health();
            b_collider.enabled = false;
            b_rend.enabled = false;
            b_part_rend.enabled = false;
        }
        if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("Speedy"))
        {
            PU_active = true;
            PU_speed();
            b_collider.enabled = false;
            b_rend.enabled = false;
            b_part_rend.enabled = false;
        }
        if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("Hurty"))
        {
            PU_active = true;
            PU_Hurty_Ammo();
            b_collider.enabled = false;
            b_rend.enabled = false;
            b_part_rend.enabled = false;
        }

    }

    void PU_speed()
    {
        /*if (PU_active == true)
        {
            bob.nspeed += speedBoost;
        }*/

       
    }

    void PU_Health()
    {
        /*if (PU_active == true)
        {
            bob.curhealth += healthBoost;
        }*/
       
    }

    void PU_Hurty_Ammo()
    {
        if (PU_active == true)
        {
            bob.bulletStrength += damageboost;
        }
       
    }
}

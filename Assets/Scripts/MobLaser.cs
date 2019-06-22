using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobLaser : MobIA
{
    // Start is called before the first frame update
    private bool shooting, lockcharge, antispam;
    public int OrienBall, blinking;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        vie = 3;

        Player = GameObject.Find("Heros").transform;
        pathfinder = GameObject.Find("PathFindAstar").GetComponent<AStarPathfinding>();
        anim = this.GetComponent<Animator>();
        astargrid = GameObject.Find("GridAStar").GetComponent<AStarGrid>();
        Player = GameObject.Find("Heros").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (activated)
        {
            if(lockcharge == true)
            {
                if (antispam != true)
                {
                    Invoke("Unlock", 3f);
                    antispam = true;

                }
            }
            if (lockcharge != true)
            {
                timer += Time.deltaTime;
                //   anim.SetBool("activated", true);
                // #region Animations mob + gestion de l'attaque
                Node MobNode = new Node(false, astargrid.NodeFromWorldPoint(this.transform.position).posX, astargrid.NodeFromWorldPoint(this.transform.position).posY);

                if (timer <= 0.1f)
                {
                    anim.SetInteger("EyePos", 0);
                    this.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);

                }
                else if (timer > 0.1f && timer <= 0.5f)
                {
                    anim.SetInteger("EyePos", 1);
                    this.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 315);

                }
                else if (timer > 0.5f && timer <= 1f)
                {
                    anim.SetInteger("EyePos", 2);
                    this.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 270);

                }
                else if (timer > 1f && timer <= 1.5f)
                {
                    anim.SetInteger("EyePos", 3);
                    this.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 225);

                }
                else if (timer > 1.5f && timer <= 2f)
                {
                    anim.SetInteger("EyePos", 4);
                    this.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 180);

                }
                else if (timer > 2f && timer <= 2.5f)
                {
                    anim.SetInteger("EyePos", 5);
                    this.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 135);

                }
                else if (timer > 2.5f && timer <= 3f)
                {
                    anim.SetInteger("EyePos", 6);
                    this.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 90);

                }
                else if (timer > 3f && timer <= 3.5f)
                {
                    anim.SetInteger("EyePos", 7);
                    this.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 45);

                }
                else if (timer > 3.5f && timer <= 4f)
                {
                    anim.SetInteger("EyePos", 0);
                    this.transform.GetChild(0).rotation = Quaternion.Euler(0, 0, 0);

                }
                if (timer >= 4)
                {
                    timer = 0;

                }




            }
        }
  
    } 

    public void Lock()
    {
        lockcharge = true;
        BlinkYellow();
        
    }
    public void Unlock()
    {
        blinking = 0;
        lockcharge = false;
        antispam = false;
        this.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(false);

    }
    public void LaserBeam()
    {
        this.transform.GetChild(0).GetChild(0).GetChild(0).gameObject.SetActive(true);
    }

    public void BlinkYellow()
    {
        this.GetComponent<SpriteRenderer>().material.color = Color.yellow;
        Invoke("BlinkBack", 0.5f);


    }
    public void BlinkBack()
    {
        blinking += 1;
        this.GetComponent<SpriteRenderer>().material.color = Color.white;
        if (blinking != 2)
        {
            BlinkYellow();
        }
        else if(blinking >= 2)
        {
            LaserBeam();
        }

    }


}

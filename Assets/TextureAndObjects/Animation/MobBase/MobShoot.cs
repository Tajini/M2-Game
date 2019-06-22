using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobShoot : MobIA
{
    private bool shooting, locked;
    public int OrienBall;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        if (this.name.Contains("MidBoss"))
        {
            vie = 15;

        }
        else
        {
            vie = 3;
        }

        Player = GameObject.Find("Heros").transform;
        pathfinder = GameObject.Find("PathFindAstar").GetComponent<AStarPathfinding>();
        anim = this.GetComponent<Animator>();
        astargrid = GameObject.Find("GridAStar").GetComponent<AStarGrid>();
        Player = GameObject.Find("Heros").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (activated)
        {
            #region Animations mob + gestion de l'attaque
            Node MobNode = new Node(false, astargrid.NodeFromWorldPoint(this.transform.position).posX, astargrid.NodeFromWorldPoint(this.transform.position).posY);

            if (MobNode.posX < PlayerNode.posX)
            {
                if (locked != true) { OrienBall = 1; }
                anim.SetInteger("Orientation", 1);
                transform.eulerAngles = new Vector2(0, 180);

            }
            if (MobNode.posX > PlayerNode.posX)
            {
                if (locked != true) { OrienBall = 2; }
                anim.SetInteger("Orientation", 1);
                transform.eulerAngles = new Vector2(0, 0);

            }
            try
            {

                if (shooting == false)
                {
                    float step = speed * Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position, pathfinder.WorldPointFromNode(PathFindNodes(this.transform.position, Player.position)), step);
                }
                if(shooting == true)
                {
                    Shoot();
                }
                //Récupération du noeud du joueur et du noeud du mob +1 afin de déterminer l'orientation haut ou bas du joueur
          
                if (MobNode.posY > PlayerNode.posY)
                {
                    anim.SetInteger("Orientation", 0);
                    if(locked != true){OrienBall = 4;}
                }
                if (MobNode.posY < PlayerNode.posY)
                {
                    if (locked != true) { OrienBall = 3; }
                    anim.SetInteger("Orientation", 2);

                }

                if(MobNode.posY == PlayerNode.posY)
                {
                    shooting = true;                 
                }

                if (MobNode.posX == PlayerNode.posX)
                {
                    shooting = true;

                }
            }
            catch (Exception exp) { print(exp); }

                #endregion
        }

    }
    private void Shoot()
    {
        locked = true;
        timer += Time.deltaTime;
        switch (OrienBall)
        {
            case 1: 
            case 2: this.gameObject.transform.GetChild(0).transform.Translate(Vector2.left * 0.3f); break;
            case 3: this.gameObject.transform.GetChild(0).transform.Translate(Vector2.up * 0.3f); break;
            case 4: this.gameObject.transform.GetChild(0).transform.Translate(Vector2.down * 0.3f); break;
            default: break;


        }

        if(timer >= 5)
        {
            UnLock();
        }
        if (timer >= 2)
        {
            DeShoot();
        }

    }
    private void DeShoot()
    {
        shooting = false;
        locked = false;

    }
    private void UnLock()
    {
        this.gameObject.transform.GetChild(0).position = this.transform.position;
        timer = 0;

    }
  
    // for (compteur = 0; compteur < PathNodes.Count; compteur++)
    // {

    //if (transform.position != NextNode)
    //{


    //}


    // }

}

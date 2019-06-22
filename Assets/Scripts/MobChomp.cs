using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobChomp : MobIA
{
    // Start is called before the first frame update
    public GameObject Heros;
    private bool shooting, locked, lockcharge;
    public int OrienBall;
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
    void Update()
    {
    if(vie <= 0)
        {
            Destroy(this.transform.parent.gameObject,0.01f);
        }
        if (activated)
        {
            #region Animations mob + gestion de l'attaque
            Node MobNode = new Node(false, astargrid.NodeFromWorldPoint(this.transform.position).posX, astargrid.NodeFromWorldPoint(this.transform.position).posY);

            if (MobNode.posX < PlayerNode.posX)
            {
                if (locked != true) { OrienBall = 1; transform.eulerAngles = new Vector2(0, 180); }


            }
            if (MobNode.posX > PlayerNode.posX)
            {
                if (locked != true)
                {
                    OrienBall = 2; transform.eulerAngles = new Vector2(0, 0);
                }

            }
            try
            {

                if (shooting == false || lockcharge == true)
                {
                    //float step = speed * Time.deltaTime;
                    //transform.position = Vector2.MoveTowards(transform.position, pathfinder.WorldPointFromNode(PathFindNodes(this.transform.position, Player.position)), step);
                }
                if (shooting == true)
                {

                    locked = true;
                    Invoke("Shoot", 1f);
                }
                //Récupération du noeud du joueur et du noeud du mob +1 afin de déterminer l'orientation haut ou bas du joueur

                if (MobNode.posY > PlayerNode.posY)
                {
                    if (locked != true) { OrienBall = 4; }
                }
                if (MobNode.posY < PlayerNode.posY)
                {
                    if (locked != true) { OrienBall = 3; }

                }

                if (MobNode.posY == PlayerNode.posY)
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
    private void Shoot() //Le chomp fonce vers le joueur
    {
        timer += Time.deltaTime;
        if (lockcharge != true)
        {
            switch (OrienBall)
            {
                case 1:
                case 2: this.gameObject.transform.Translate(Vector2.left * 0.3f); break;
                case 3: this.gameObject.transform.Translate(Vector2.up * 0.3f); break;
                case 4: this.gameObject.transform.Translate(Vector2.down * 0.3f); break;
                default: break;
            }
        }



        if (timer >= 4)
        {
            DeShoot();
        }
        if (timer >= 5)
        {
            lockcharge = false;
            timer = 0;

        }
    }
    private void DeShoot() //Delocke la position
    {
        lockcharge = true;
        shooting = false;
        locked = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        DeShoot();
        if(collision.gameObject.name == "Heros")
        {

            Heros = collision.gameObject;
            collision.gameObject.GetComponent<MainCharacter>().vie -= 1f;
            Invoke("BlinkRed", 0.1f);
        }
    }
   

}

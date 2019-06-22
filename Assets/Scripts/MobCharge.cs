using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCharge : MobIA
{
    private bool shooting, locked, lockcharge;
    public int OrienBall;
    public float timer;
    // Start is called before the first frame update
    void Start()
    {
        if (this.name.Contains("MidBoss"))
        {
            vie = 10;

        }
        else
        {
            vie = 5;
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
            //Récupération de la position du Mob sur la grille pour les calculs
            Node MobNode = new Node(false, astargrid.NodeFromWorldPoint(this.transform.position).posX, astargrid.NodeFromWorldPoint(this.transform.position).posY);


            //Récupération du noeud du joueur et du noeud du mob +1 afin de déterminer l'orientation haut ou bas du mob
            if (MobNode.posX < PlayerNode.posX)
            {
                if (locked != true) { OrienBall = 1; transform.eulerAngles = new Vector2(0, 180); }
                anim.SetInteger("Orientation", 1);


            }
            if (MobNode.posX > PlayerNode.posX)
            {
                if (locked != true)
                {
                    OrienBall = 2; transform.eulerAngles = new Vector2(0, 0);
                }
                anim.SetInteger("Orientation", 1);

            }
            if (MobNode.posY > PlayerNode.posY)
            {
                anim.SetInteger("Orientation", 0);
                if (locked != true) { OrienBall = 4; }
            }
            if (MobNode.posY < PlayerNode.posY)
            {
                if (locked != true) { OrienBall = 3; }
                anim.SetInteger("Orientation", 2);

            }

            try
            {

                if (shooting == false || lockcharge == true)
                {
                    float step = speed * Time.deltaTime;
                    transform.position = Vector2.MoveTowards(transform.position, pathfinder.WorldPointFromNode(PathFindNodes(this.transform.position, Player.position)), step);
                }
                if (shooting == true)
                {

                    locked = true;
                    Invoke("Shoot", 1f);

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
    private void Shoot() //Fonction pour faire foncer le joueur sur la position lockée du joueur
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

        if (timer >= 2)
        {
            DeShoot();
        }
        if (timer >= 4)
        {
            lockcharge = false;
            shooting = false;
            timer = 0;

        }
    }
    private void DeShoot() // Délocke la position de l'ennemi afin qu'il se redirige vers le joueur 
    {
        lockcharge = true;
        locked = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        DeShoot();
        if (collision.gameObject.transform.name == "Heros")
        {
            collision.gameObject.GetComponent<MainCharacter>().vie -= 0.5f;
            BlinkRed();
        }
    }
}

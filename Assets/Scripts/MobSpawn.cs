using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawn : MobIA
{
    private bool shooting, lockspam;
    public bool vulnerable;
    public int OrienBall;
    public float timer;
    public int poolXL, poolXR, poolYU, poolYD;
    public Node teleportation;
    public Vector2 PlayerPos;
    // Start is called before the first frame update
    void Start()
    {
        vie = 3;
        Player = GameObject.Find("Heros").transform;
        pathfinder = GameObject.Find("PathFindAstar").GetComponent<AStarPathfinding>();
        anim = this.GetComponent<Animator>();
        astargrid = GameObject.Find("GridAStar").GetComponent<AStarGrid>();
        Player = GameObject.Find("Heros").transform;
        Invoke("InitializePool", 1f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (activated)
        {
            #region Animations mob + gestion de l'attaque
            Node MobNode = new Node(false, astargrid.NodeFromWorldPoint(this.transform.position).posX, astargrid.NodeFromWorldPoint(this.transform.position).posY);

            timer += Time.deltaTime;
            
            if(timer <= 3)
            {
                anim.SetInteger("TP", 1);

            }

            if (timer >= 3 && timer < 5)
            {
                //animation sous l'eau/teleportation
                if (lockspam != true)
                {
                    PlayerPos = Player.transform.position; //Récupération de la position du personnage 
                    Teleportation();
                    lockspam = true;
                }

            }
            if(timer >= 5 && timer < 7)
            {
                vulnerable = true;
                float step = Vector3.Distance(this.transform.GetChild(0).position, PlayerPos) / (1 - Time.deltaTime) * Time.deltaTime;

                anim.SetInteger("TP", 2);
                this.transform.GetChild(0).position = Vector2.MoveTowards(this.transform.GetChild(0).position, PlayerPos, step);


            }

            if(timer >= 7 && timer < 10)
            {
                this.gameObject.transform.GetChild(0).position = new Vector2(this.transform.position.x, this.transform.position.y);

            }
            if (timer >= 10)
            {
                vulnerable = false;
                lockspam = false;
                timer = 0;
            }
           
                //Récupération du noeud du joueur et du noeud du mob +1 afin de déterminer l'orientation haut ou bas du joueur

     
  

            #endregion
        }

    }

  

    private void Teleportation()
    {
        //Random entre les pools autour du mob afin de déterminer la prochaine position, utilisation des noeuds de la grille
        teleportation = new Node(false,UnityEngine.Random.Range(poolXL, poolXR), UnityEngine.Random.Range(poolYU, poolYD));

       
        this.transform.position = new Vector2(astargrid.WorldPointFromNode(teleportation).x, astargrid.WorldPointFromNode(teleportation).y); 



    }

    public void InitializePool()
    {
        Node MobNode = new Node(false, astargrid.NodeFromWorldPoint(this.transform.position).posX, astargrid.NodeFromWorldPoint(this.transform.position).posY);

        poolXR = MobNode.posX + 3;
        poolXL = MobNode.posX + 3;
        poolYU = MobNode.posY + 3;
        poolYD = MobNode.posY - 3;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCac : MobIA
{
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
        if (activated)
        {
            #region Animations mob + gestion de l'attaque

            if (this.transform.position.x < Player.position.x)
            {
                PositionAttack("horizontal");
                anim.SetInteger("Orientation", 1);
                transform.eulerAngles = new Vector2(0, 180);

            }
            if (this.transform.position.x > Player.position.x)
            {
                PositionAttack("horizontal");
                anim.SetInteger("Orientation", 1);
                transform.eulerAngles = new Vector2(0, 0);

            }
            try
            {
                //Récupération du noeud du joueur et du noeud du mob +1 afin de déterminer l'orientation haut ou bas du joueur
                Node MobNode = new Node(false, astargrid.NodeFromWorldPoint(this.transform.position).posX, astargrid.NodeFromWorldPoint(this.transform.position).posY);

                if (MobNode.posY > PlayerNode.posY)
                {
                    PositionAttack("down");
                    anim.SetInteger("Orientation", 0);
                }
                if (MobNode.posY < PlayerNode.posY)
                {
                    PositionAttack("up");
                    anim.SetInteger("Orientation", 2);

                }
            }
            catch (Exception exp) { print(exp); }
            #endregion

            float step = speed * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, pathfinder.WorldPointFromNode(PathFindNodes(this.transform.position, Player.position)), step);

        }

    }
    private void PositionAttack(string position)
    {
        //Modifie le collider activé en fonction de l'orientation du mob et la direction de son épée
        switch (position)
        {
            case "up":
                if (this.transform.GetChild(1) != null)
                {
                    this.transform.GetChild(1).gameObject.SetActive(true);
                }
                this.transform.GetChild(0).gameObject.SetActive(false);
                this.transform.GetChild(2).gameObject.SetActive(false);
                break;
            case "down":
                if (this.transform.GetChild(2) != null)
                {
                    this.transform.GetChild(2).gameObject.SetActive(true);
                }
                this.transform.GetChild(0).gameObject.SetActive(false);
                this.transform.GetChild(1).gameObject.SetActive(false); break;
            case "horizontal":
                if (this.transform.GetChild(0) != null)
                {
                    this.transform.GetChild(0).gameObject.SetActive(true);
                }
                this.transform.GetChild(1).gameObject.SetActive(false);
                this.transform.GetChild(2).gameObject.SetActive(false); break;
            default: break;
        }
    }
}
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSnake : MobIA
{
    // Start is called before the first frame update
    private bool firstapproach, shooting, cadrer;
    private Vector3 PlayerPos;
    private Vector2 TempPos;
    private Vector2 CurrentTarget;
    public Node MobNode;
    public int OrienBall;
    public float timer,step,timer2;
    public List<Node> NodeList = new List<Node>();
    
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
        if (vie <= 0)
        {
            Destroy(this.transform.parent.gameObject, 0f);
        }
            if (activated)
        {
             step = Vector3.Distance(this.transform.position, PlayerPos) / (5 - Time.deltaTime) * Time.deltaTime;

            //step = 0.3f * Time.deltaTime;
            #region rotation tete
            PlayerPos.x = Player.position.x - this.transform.position.x;
            PlayerPos.y = Player.position.y - this.transform.position.y;
            PlayerPos.Normalize();
            float angle = angle = Mathf.Atan2(PlayerPos.y, PlayerPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle -90));
            #endregion

            #region Animations mob + gestion de l'attaque
             MobNode = new Node(false, astargrid.NodeFromWorldPoint(this.transform.position).posX, astargrid.NodeFromWorldPoint(this.transform.position).posY);


            //Quand le mob repère le joueur, il se dirige vers le périmètre autour de celui-ci (Pool des positions et Target actuelle)
            if (firstapproach != true)
            {
                GridMove();
                CurrentTarget = astargrid.WorldPointFromNode(NodeList[UnityEngine.Random.Range(0, NodeList.Count)]);
                Invoke("Cadrage", 2f);
                firstapproach = true;
            }

            if (cadrer == true)
            {
                timer += Time.deltaTime;
                timer2 += Time.deltaTime;

                if (timer >= 2 && timer < 5 && shooting == false)
                {
                    Rodage();
                    TempPos = Player.position;
                }
                if(timer2 >= 5 && timer2 < 7)
                {
                    shooting = true;
                }
                if(timer2 >= 7)
                {
                    shooting = false;
                    timer = 0;
                }

                if(timer2 > 9)
                {
                    DeShoot();
                }
               //MEMO : DOUBLER CE MONSTRE LORS DE SON APPARITION // REPRENDRE LA STRUCTURE INITIALE DE CE MONSTRE MAIS FAIRE LES GRILLES EN +2, BLOQUER L'AXE ET FAIRE L'ATTAQUE QUI FONCE
             }

        
           if(shooting == true)
                {
                transform.position = Vector2.MoveTowards(transform.position, TempPos , step);
                }
            else if(shooting != true)
            {
                transform.position = Vector2.MoveTowards(transform.position, CurrentTarget, step);

            }
            //Récupération du noeud du joueur et du noeud du mob +1 afin de déterminer l'orientation haut ou bas du joueur


        }


            #endregion
        }

   

    private void DeShoot()
    {

        shooting = false;
        firstapproach = false;
        cadrer = false;
        timer2 = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform.name == "Heros")
        {
            collision.gameObject.GetComponent<MainCharacter>().vie -= 0.5f;
            BlinkRed();
        }
        DeShoot();
    }
    public void GridMove()
    {
        //Ajout à la liste de tous les noeuds présents autour du joueur  en +2 
      //  PlayerNode = astargrid.NodeFromWorldPoint(Player.position);
      
        NodeList.Add(new Node(false, PlayerNode.posX +2, PlayerNode.posY));
        NodeList.Add(new Node(false, PlayerNode.posX -2, PlayerNode.posY));
        NodeList.Add(new Node(false, PlayerNode.posX, PlayerNode.posY +2));
        NodeList.Add(new Node(false, PlayerNode.posX, PlayerNode.posY-2));

        NodeList.Add(new Node(false, PlayerNode.posX + 2, PlayerNode.posY +1));
        NodeList.Add(new Node(false, PlayerNode.posX + 2, PlayerNode.posY +2));
        NodeList.Add(new Node(false, PlayerNode.posX + 2, PlayerNode.posY -1));
        NodeList.Add(new Node(false, PlayerNode.posX + 2, PlayerNode.posY -2));

        NodeList.Add(new Node(false, PlayerNode.posX - 2, PlayerNode.posY + 1));
        NodeList.Add(new Node(false, PlayerNode.posX - 2, PlayerNode.posY + 2));
        NodeList.Add(new Node(false, PlayerNode.posX - 2, PlayerNode.posY - 1));
        NodeList.Add(new Node(false, PlayerNode.posX - 2, PlayerNode.posY - 2));

        NodeList.Add(new Node(false, PlayerNode.posX + 1, PlayerNode.posY + 2));
        NodeList.Add(new Node(false, PlayerNode.posX + 1, PlayerNode.posY - 2));
        NodeList.Add(new Node(false, PlayerNode.posX - 1, PlayerNode.posY + 2));
        NodeList.Add(new Node(false, PlayerNode.posX - 1, PlayerNode.posY - 2));




    }

    private void Cadrage()
    {
        //NodeList.Clear();
       // GridMove();
        cadrer = true;
        

    }
    private void Rodage()
    {
        //Nouvelle position random dans le cadre
        Node NewPos = NodeList[UnityEngine.Random.Range(0, NodeList.Count)];
        //Force le monstre à rester sur une ligne de son cadre, et ne bouger que lorsqu'il est aux jointures, afin de le faire rôder autour du joueur avant d'attaquer
        if (NewPos.posX == MobNode.posX && NewPos.posY != PlayerNode.posY + 3 || NewPos.posY == MobNode.posY && NewPos.posX != PlayerNode.posY + 3)
        {
            CurrentTarget = astargrid.WorldPointFromNode(NewPos);
            timer = 0;

        }
        else
        {
            Rodage();
        }
    }
    public void DestroyBall()
    {
        switch (vie)
        {
            case 1: Destroy(this.transform.parent.GetChild(1).gameObject, 1f);  ;break;
            case 2: Destroy(this.transform.parent.GetChild(2).gameObject, 1f); break;
            case 3: Destroy(this.transform.parent.GetChild(3).gameObject, 1f); break;
            case 4: Destroy(this.transform.parent.GetChild(4).gameObject, 1f); break;

        }
    }
  


}

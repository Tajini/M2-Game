using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBall : MobIA
{
    private bool firstapproach, shooting, locked, cadrer;
    private Vector3 PlayerPos;
    private Vector2 CurrentTarget;
    public Node MobNode;
    public int OrienBall;
    public float timer, step, timer2;
    public List<Node> NodeList = new List<Node>();

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

            step = Vector3.Distance(this.transform.position, PlayerPos) / (2 - Time.deltaTime) * Time.deltaTime;
            #region rotation tete
            PlayerPos.x = Player.position.x - this.transform.position.x;
            PlayerPos.y = Player.position.y - this.transform.position.y;
            PlayerPos.Normalize();
            float angle = angle = Mathf.Atan2(PlayerPos.y, PlayerPos.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
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
            transform.position = Vector2.MoveTowards(transform.position, CurrentTarget, step);

            if (cadrer == true)
            {
                timer += Time.deltaTime;
                timer2 += Time.deltaTime;

                if (timer >= 2)
                {
                    Rodage();
                }

                //Réinitialisation du script au tout début afin de recentrer les mobs autour du joueur si celui-ci s'est éloigné
                if(timer2 >= 6)
                {
                    DeShoot();
                }
                /*if(timer >= 3)
                {

                    DeShoot();
                }*/
                //MEMO : DOUBLER CE MONSTRE LORS DE SON APPARITION 
            }


            //Récupération du noeud du joueur et du noeud du mob +1 afin de déterminer l'orientation haut ou bas du joueur


        }


        #endregion
    }



    private void DeShoot()
    {

        cadrer = false;
        firstapproach = false;
        NodeList.Clear();
        timer = 0;
        timer2 = 0;

    }


    public void GridMove()
    {
        //Ajout à la liste de tous les noeuds présents autour du joueur 
        //  PlayerNode = astargrid.NodeFromWorldPoint(Player.position);
        NodeList.Add(new Node(false, PlayerNode.posX + 1, PlayerNode.posY));
        NodeList.Add(new Node(false, PlayerNode.posX - 1, PlayerNode.posY));
        NodeList.Add(new Node(false, PlayerNode.posX, PlayerNode.posY + 1));
        NodeList.Add(new Node(false, PlayerNode.posX, PlayerNode.posY - 1));

        NodeList.Add(new Node(false, PlayerNode.posX + 1, PlayerNode.posY + 1));
        NodeList.Add(new Node(false, PlayerNode.posX + 1, PlayerNode.posY - 1));
        NodeList.Add(new Node(false, PlayerNode.posX - 1, PlayerNode.posY + 1));
        NodeList.Add(new Node(false, PlayerNode.posX - 1, PlayerNode.posY - 1));

    }

    private void Cadrage()
    {
        //NodeList.Clear();
        // GridMove();
        cadrer = true;


    }
    private void Rodage()
    {
        Node NewPos = NodeList[UnityEngine.Random.Range(0, NodeList.Count)];
        //Force le monstre à rester sur une ligne de son cadre, et ne bouger que lorsqu'il est aux jointures, afin de le faire rôder autour du joueur avant d'attaquer
        //Lock autour du héros, avec une chance de passer au centre (1/3 quand il est sur 4/8 des cases)
        if (NewPos.posX == MobNode.posX || NewPos.posY == MobNode.posY)
        {
            CurrentTarget = astargrid.WorldPointFromNode(NewPos);
            timer = 0;

        }
        else
        {
            Rodage();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.transform.name + " ball");
        if (collision.gameObject.transform.name == "Heros")
        {
            
            Player.GetComponent<MainCharacter>().vie -= 0.5f;
            Invoke("BlinkRed", 0.1f);
        }
    }


    

}

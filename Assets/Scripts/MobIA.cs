using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MobIA : MonoBehaviour
{
    #region Parametres 

    public int vie;
    public List<Node> PathNodes;
    public Animator anim;
    public AStarPathfinding pathfinder;
    public AStarGrid astargrid;
    public float speed = 2;
    public bool activated;
    public Transform Player;
    public Vector3 NextNode;
    public int compteur;
    public Node PlayerNode;
    private float startTime;
    // Start is called before the first frame update
    #endregion

    void Start()
    {
        vie = 3;
        pathfinder = GameObject.Find("PathFindAstar").GetComponent<AStarPathfinding>();
        anim = this.GetComponent<Animator>();
        astargrid = GameObject.Find("GridAStar").GetComponent<AStarGrid>();
        Player = GameObject.Find("Heros").transform;
        startTime = Time.time;
    }

    void LateUpdate()
    {
        if (vie <= 0)
        {
            SpawnHeart();
            Destroy(this.gameObject, 0f);
            
        }
   
    }
    

    public Node PathFindNodes(Vector2 MobPos, Vector2 PlayerPos)
    {
        //Création de la liste de noeuds du a* script afin de se diriger vers le joueur en évitant les colliders
        PathNodes = pathfinder.FindPath(MobPos, PlayerPos);

        try
        {
            return PathNodes[0]; //Retourne la position la plus proche afin que le mob s'y dirige
        }
        //Exception quand le joueur est collé au monstre, retourne la position du monstre pour qu'il ne bouge plus
        catch
        {
            return new Node(false, astargrid.NodeFromWorldPoint(this.transform.position).posX, astargrid.NodeFromWorldPoint(this.transform.position).posY);
        }

    }

    #region Triggers 
    private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.name == "Heros")
            {
                activated = true;
            Player = collision.transform;
            NextNode = pathfinder.WorldPointFromNode(PathFindNodes(this.transform.position, Player.position));
               PlayerNode = new Node(false, astargrid.NodeFromWorldPoint(Player.position).posX, astargrid.NodeFromWorldPoint(Player.position).posY);
            //Position du joueur au moment ou il rentre dans le trigger
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
     

        if (collision.name == "Heros")
        {
            activated = true;
            NextNode = pathfinder.WorldPointFromNode(PathFindNodes(Player.position, Player.position));
            PlayerNode = new Node(false, astargrid.NodeFromWorldPoint(Player.position).posX, astargrid.NodeFromWorldPoint(Player.position).posY);
            
        }
    }

    #endregion

    #region blink heros 
    public void BlinkRed()
    {
        Player.GetComponent<SpriteRenderer>().material.color = Color.red;
        Invoke("BlinkWhite", 0.2f);


    }
    public void BlinkWhite()
    {
        Player.GetComponent<SpriteRenderer>().material.color = Color.white;

    }
    #endregion

    public void SpawnHeart()
    {
        int random = UnityEngine.Random.Range(0, 100);
        if(random > 80) { GameObject Coeur = Instantiate(Resources.Load<GameObject>("CoeurUp"), this.transform.position, Quaternion.identity); }
        if (random < 10) { GameObject Coeur = Instantiate(Resources.Load<GameObject>("CoeurMax"), this.transform.position, Quaternion.identity); }
        if (this.gameObject.name.Contains("MidBoss")) { GameObject Coeur = Instantiate(Resources.Load<GameObject>("CoeurMax"), this.transform.position, Quaternion.identity); }
    }
}


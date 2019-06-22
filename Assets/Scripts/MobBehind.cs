using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobBehind : MobIA
{
    public GameObject Heros;
    public bool pause;
    // Start is called before the first frame update
    void Start()
    {
        if (this.name.Contains("MidBoss"))
        {
            vie = 10;

        }
        else
        {
            vie = 3;
        }
       
        speed = 1;
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

            try
            {
                Node MobNode = new Node(false, astargrid.NodeFromWorldPoint(this.transform.position).posX, astargrid.NodeFromWorldPoint(this.transform.position).posY);

                #region Animations mob + gestion de l'attaque

                if (pause != true)
                {
                    if (this.transform.position.x < Player.position.x)
                    {
                        pause = true;
                        StartCoroutine(SwitchPos("Left", 1.5f));
                    }
                    if (this.transform.position.x > Player.position.x)
                    {
                        pause = true;
                        StartCoroutine(SwitchPos("Right", 1.5f));


                    }
                    if (MobNode.posY > PlayerNode.posY)
                    {
                        pause = true;
                        StartCoroutine(SwitchPos("Down", 1.5f));


                    }
                    if (MobNode.posY < PlayerNode.posY)
                    {
                        pause = true;
                        StartCoroutine(SwitchPos("Up", 1.5f));
                    }
                }

                float step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, pathfinder.WorldPointFromNode(PathFindNodes(this.transform.position, Player.position)), step);



            }
            catch (Exception exp) { print(exp); }
            #endregion

           
          
                
        }

    }

    
    private void Depause()
    {
        pause = false;
    }

   
    IEnumerator SwitchPos(string WichToStop, float delay)
    {
        yield return new WaitForSeconds(delay);

        switch (WichToStop)
        {
            case "Up":
                this.transform.GetChild(0).position = new Vector2(this.transform.position.x, this.transform.position.y - 0.75f);
                anim.SetInteger("Orientation", 2); pause = false;  break;
            case "Down":
                this.transform.GetChild(0).position = new Vector2(this.transform.position.x, this.transform.position.y + 0.75f);
                anim.SetInteger("Orientation", 0); pause = false; break;
            case "Left":
                anim.SetInteger("Orientation", 1);
                transform.eulerAngles = new Vector2(0, 180);
                this.transform.GetChild(0).position = new Vector2(this.transform.position.x - 0.75f, this.transform.position.y); pause = false; break;
            case "Right":
                anim.SetInteger("Orientation", 1);
                transform.eulerAngles = new Vector2(0, 0);
                this.transform.GetChild(0).position = new Vector2(this.transform.position.x + 0.75f, this.transform.position.y); pause = false; break;

            default: break;
        }

    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform.name == "Heros")
        {
            collision.gameObject.GetComponent<MainCharacter>().vie -= 1f;
            Invoke("BlinkRed", 0.1f);
        }
    }
}

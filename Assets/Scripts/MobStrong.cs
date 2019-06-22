using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobStrong : MobIA
{
    public GameObject Heros;
    private float timer;
    public bool vulnerable;
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
            timer += Time.deltaTime;

            if (timer < 6) {
                vulnerable = false;
                float step = speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, pathfinder.WorldPointFromNode(PathFindNodes(this.transform.position, Player.position)), step);
            }
            if (timer > 6)
            {
                vulnerable = true;
                anim.SetInteger("Orientation", 1);
            }

            if(timer > 9)
            {
                timer = 0;
                anim.SetInteger("Orientation", 0);
            }
        
        }

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform.name == "Heros" && vulnerable != true)
        {
            Heros = collision.gameObject;
            collision.gameObject.GetComponent<MainCharacter>().vie -= 1f;
            Invoke("BlinkRed", 0.1f);
        }
    }


}

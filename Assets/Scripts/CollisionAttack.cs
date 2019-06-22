using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollisionAttack : MainCharacter
{
    public string Collision, UsedAttack;
    public bool vulnerable;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.transform.parent.gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }



    public void OnCollisionEnter2D(Collision2D collision)
    {
        try
        {
            print(collision.gameObject.name);
            //Récupère le script de l'ennemi touché afin de lui retirer une vie.
            GameObject MobTouche = collision.gameObject;
            switch (collision.gameObject.tag)
            {
                // MobTouche.GetComponent<"">(); collision.gameObject.tag>();

                case "MobCac": MobTouche.GetComponent<MobCac>().vie -= 1;
                    StartCoroutine(BlinkRed(collision.gameObject, 0.1f));
                    break;

                case "MobShoot": collision.gameObject.GetComponent<MobShoot>().vie -= 1;
                    StartCoroutine(BlinkRed(collision.gameObject, 0.1f));
                    break;

                case "MobCharge": collision.gameObject.GetComponent<MobCharge>().vie -= 1;
                    StartCoroutine(BlinkRed(collision.gameObject, 0.1f));
                    break;

                case "MobLaser": collision.transform.gameObject.GetComponent<MobLaser>().vie -= 1;
                    StartCoroutine(BlinkRed(collision.gameObject, 0.1f));
                    break;

                case "MobBall": collision.gameObject.GetComponent<MobBall>().vie -= 1;
                    StartCoroutine(BlinkRed(collision.gameObject, 0.1f));
                    break;

                case "MobSnake":
                    collision.transform.gameObject.GetComponent<MobSnake>().vie -= 1;
                    collision.transform.gameObject.GetComponent<MobSnake>().DestroyBall();
                    StartCoroutine(BlinkRed(collision.gameObject, 0.1f));
                    break;

                case "MobChomp":
                    collision.transform.GetChild(0).gameObject.GetComponent<MobChomp>().vie -= 1;
                    StartCoroutine(BlinkRed(collision.transform.GetChild(0).gameObject, 0.1f));
                    StartCoroutine(BlinkRed(collision.gameObject, 0.1f));
                    break;

                case "MobBehind":
                    if (collision.gameObject.name == "MobBack")
                    {
                        collision.transform.parent.gameObject.GetComponent<MobBehind>().vie -= 1;
                        StartCoroutine(BlinkRed(collision.transform.parent.gameObject, 0.1f));
                    }
                    break;

                case "MobSpawn":
                    if (collision.gameObject.GetComponent<MobSpawn>().vulnerable == true)
                    {
                        vulnerable = true;
                        collision.transform.gameObject.GetComponent<MobSpawn>().vie -= 1;
                        StartCoroutine(BlinkRed(collision.gameObject, 0.1f));

                    }
                    break;

                case "MobStrong":
                    if (collision.gameObject.GetComponent<MobStrong>().vulnerable == true)
                    {
                        vulnerable = true;
                        collision.gameObject.GetComponent<MobStrong>().vie -= 1;
                        StartCoroutine(BlinkRed(collision.gameObject, 0.1f));

                    }
                    break;


            }
           

        }
        catch { }


    }


    IEnumerator BlinkRed(GameObject MobTouchay, float delay)
    {

        MobTouchay.GetComponent<SpriteRenderer>().material.color = Color.red;
        yield return new WaitForSeconds(delay);
        StartCoroutine(BlinkWhite(MobTouchay, 0.1f));


    }
    IEnumerator BlinkWhite(GameObject MobTouchay, float delay)
    {


        MobTouchay.GetComponent<SpriteRenderer>().material.color = Color.white;
        yield return new WaitForSeconds(delay);

    }
}
 

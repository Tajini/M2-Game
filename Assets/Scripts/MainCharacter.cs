using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public bool AttackL,AttackR, invincible; //Empêche le sprite de se tourner lorsque le joueur attaque
    public Animator anim;
    public float speed; 
    public float timer;
    public float vie, viemax, vietemp;
 
    
    // Start is called before the first frame update
    void Start()
    {
        vie = 5;
        viemax = 5;
        anim = this.GetComponent<Animator>();
        speed = 0.1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (invincible) { vie = vietemp; }

        #region déplacements et animations
        if (Input.GetKey(KeyCode.LeftArrow))
            {
            if (AttackR != true) { transform.eulerAngles = new Vector2(0, 0); transform.Translate(-1 * speed, 0, 0); } //Empêche le héros de se retourner pendant qu'il attaque
            if (AttackR == true) { transform.Translate(1 * speed, 0, 0); }


            anim.SetInteger("Move", 1);

                if (Input.GetKey(KeyCode.UpArrow)) { transform.Translate(0, 1 * speed, 0); }
                if (Input.GetKey(KeyCode.DownArrow)) { transform.Translate(0, -1 * speed, 0); }
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
            // -1 aussi pour le translate à cause du eulerAngles qui inverse les directions
            if (AttackL != true)  {    transform.eulerAngles = new Vector2(0, 180); transform.Translate(-1 * speed, 0, 0);       }
            if (AttackL == true) {  transform.Translate(1 * speed, 0, 0); }

            anim.SetInteger("Move", 1);

                if (Input.GetKey(KeyCode.UpArrow)) { transform.Translate(0, 1 * speed, 0); }
                if (Input.GetKey(KeyCode.DownArrow)) { transform.Translate(0, -1 * speed, 0); }
            }
            else if (Input.GetKey(KeyCode.UpArrow))
            {
                transform.Translate(0, 1 * speed, 0);
                anim.SetInteger("Move", 3);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                transform.Translate(0, -1 * speed, 0);
                anim.SetInteger("Move", 4);
            }
            else
            {
                anim.SetInteger("Move", 0);
                anim.SetInteger("Attack", 0);

            }
        #endregion



        #region Attaques
        switch (Input.inputString)
            {
                case "z":
                try
                {
                    anim.SetInteger("Attack", 3);
                    this.transform.GetChild(0).gameObject.SetActive(true);
                    StartCoroutine(StopAttack("Up", 1f));
                }
                catch { }
                break;

                case "q":
                try
                {
                    anim.SetInteger("Attack", 1);
                    this.transform.GetChild(2).gameObject.SetActive(true);
                    StartCoroutine(StopAttack("Horizontal", 1f));

                    transform.eulerAngles = new Vector2(0, 0);
                    AttackL = true;
                }
                catch { }
                    break;

                case "s":
                try
                {

                    anim.SetInteger("Attack", 2);
                    StartCoroutine(StopAttack("Down", 1f));
                    this.transform.GetChild(1).gameObject.SetActive(true);
                }
                catch { }
                
                //Invoke("StopAttack", 1f);
                break;

                case "d":
                try
                {
                    AttackR = true;
                    transform.eulerAngles = new Vector2(0, 180);
                    anim.SetInteger("Attack", 1);
                    this.transform.GetChild(2).gameObject.SetActive(true);
                    StartCoroutine(StopAttack("Horizontal", 1f));
                }
                catch { }
                break;

                case "":  
              
               

                break;

                default:
                    break;
            }
        #endregion

    }

    IEnumerator StopAttack(string WichToStop, float delay)
    {
        yield return new WaitForSeconds(delay);

        switch (WichToStop)
        {
            case "Up" :  this.transform.GetChild(0).gameObject.SetActive(false);  break;
            case "Down": this.transform.GetChild(1).gameObject.SetActive(false);  break;
            case "Horizontal": this.transform.GetChild(2).gameObject.SetActive(false); AttackL = false; AttackR = false; break;
            default: break;
        }
        anim.SetInteger("Attack", 0);

    }

    public void BlinkRed()
    {
        invincible = true;
        vietemp = vie;

        this.GetComponent<SpriteRenderer>().material.color = Color.red;
        Invoke("BlinkWhite", 0.2f);


    }
    public void BlinkWhite()
    {
        this.GetComponent<SpriteRenderer>().material.color = Color.white;
        Invoke("Vulnerable", 1f);
    }

    public void Vulnerable()
    {
        invincible = false;
    }
}

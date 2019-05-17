using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter : MonoBehaviour
{
    public Animator anim;
    public float speed;
      public float timer;
    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
        speed = 0.15f;
    }

    // Update is called once per frame
    void Update()
    {
        // Deplacements horizontaux
     
           
     
        #region Mouvements 
        if (Input.GetKey(KeyCode.LeftArrow))
        {
           
            transform.Translate(-1 * speed, 0, 0);
            transform.eulerAngles = new Vector2(0, 0);
            anim.SetInteger("Move", 1);

            if (Input.GetKey(KeyCode.UpArrow)) { transform.Translate(0, 1 * speed, 0); }
            if (Input.GetKey(KeyCode.DownArrow)) { transform.Translate(0, -1 * speed, 0); }
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.eulerAngles = new Vector2(0, 180);
            // -1 aussi pour le translate à cause du eulerAngles qui inverse les directions
            transform.Translate(-1 * speed, 0, 0);
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
            transform.Translate(0, - 1 * speed, 0);
            anim.SetInteger("Move", 4);
        }
        else
        {
            anim.SetInteger("Move", 0);
        }
        
        #endregion old
    

    }

}

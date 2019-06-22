using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAttack : MonoBehaviour
{
    public GameObject Heros;
    public MobIA MobIA;
    // Start is called before the first frame update
    void Start()
    {
        //MobIA = this.transform.parent.GetComponent<MobIA>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.transform.name == "Heros")
        {
            if (collision.GetComponent<MainCharacter>().invincible != true)
            {
                collision.GetComponent<MainCharacter>().vie -= 0.5f;
                collision.GetComponent<MainCharacter>().BlinkRed();
            }
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform.name == "Heros")
        {
            if (collision.gameObject.GetComponent<MainCharacter>().invincible != true)
            {
                collision.gameObject.GetComponent<MainCharacter>().vie -= 0.5f;
                collision.gameObject.GetComponent<MainCharacter>().BlinkRed();
            }
        }
    }
   

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionCoeurs : MonoBehaviour
{
    // Start is called before the first frame update
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Heros")
        {
            if (this.gameObject.name.Contains("Max"))
            {
                collision.GetComponent<MainCharacter>().viemax += 1;
                Destroy(this.gameObject, 0.1f);

            }
            if (this.gameObject.name.Contains("Up"))
            {
                if(collision.GetComponent<MainCharacter>().vie + 0.5f < collision.GetComponent<MainCharacter>().viemax)
                {
                    collision.GetComponent<MainCharacter>().vie += 1;
                }
                else if (collision.GetComponent<MainCharacter>().vie + 0.5f == collision.GetComponent<MainCharacter>().viemax)
                {
                    collision.GetComponent<MainCharacter>().vie += 0.5f;

                }
                Destroy(this.gameObject, 0.1f);

            }
        }
    }
}

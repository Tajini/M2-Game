using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GestionVieUI : MonoBehaviour
{
    public MainCharacter Player;
    public float vie, vieremplie;
    public List<Sprite> HeartList = new List<Sprite>();
    public Vector2 PlaceCoeur;
    public int i, viemax, moitievie;
    public bool locked;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        HeartList.Add(Resources.Load<Sprite>("CoeurEmpty"));
        HeartList.Add(Resources.Load<Sprite>("CoeurFull"));
        HeartList.Add(Resources.Load<Sprite>("CoeurMiddle"));
        //Resources.Load<GameObject>("Coeur");
        Player = GameObject.Find("Heros").GetComponent<MainCharacter>();
        vie = 0;
       // viemax = 1;
    }

    // Update is called once per frame
    void Update()
    {
        vie = Player.vie;
        if (this.transform.childCount < Player.viemax)
        {
           // viemax = int.Parse((Player.viemax).ToString());
            CoeursBase();

        }
       
        if(vieremplie < Player.viemax)
        {
            CoeursRempli();
        }

        if (this.vie.Equals(0))
        {
            Debug.Log(this.vie);
            SceneManager.LoadScene("MainMenu");

            /*if (Time.timeScale == 1.0)
            {
                Time.timeScale = 0.0f;
            }
            GameObject.Find("UIManager").GetComponent<Button>().ShowGameOverPanel();
            */
        }


    }

    private void CoeursBase()
    {
        
          Player.vie = Player.viemax; //Remet la vie du joueur au max

          //Instantiation d'un nouveau coeur dans l'UI
          PlaceCoeur = new Vector2(this.transform.GetChild(this.transform.childCount -1).position.x + 50, this.transform.GetChild(this.transform.childCount -1).position.y);
          GameObject Coeur = Instantiate(Resources.Load<GameObject>("Coeur"), PlaceCoeur, Quaternion.identity);
          Coeur.transform.parent = this.transform;      
    }

    private void CoeursRempli()
    {      
        //Boucle sur le nombre total de coeurs du joueur
            for (int i = 0; i < Player.viemax; i++)
            {
            float j = i + 0.5f;
            //Gestion des demi vies, si un 0,5 est repéré un coeur a moitié vide apparaît           
            if (j == Player.vie)                
            {
                this.transform.GetChild(i).GetComponent<Image>().sprite = HeartList[2];
            }
            //Tant que j n'est pas une demi vie, instancier des coeurs
            if (j != Player.vie)
            {
                //Tant qu'on est en dessous de la vie, mettre des sprites coeur sur les gameObject
                if (i <= Player.vie )
                {
                    this.transform.GetChild(i).GetComponent<Image>().sprite = HeartList[1];
                }
                //Si on dépasse la vie et qu'on est pas au max, mettre des coeurs vides
                if (i >= Player.vie && i < Player.viemax)
                {
                    this.transform.GetChild(i).GetComponent<Image>().sprite = HeartList[0];

                }
            }
    }
 
}
}

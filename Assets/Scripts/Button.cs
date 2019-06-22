using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    public GameObject GameOverPanel;



    // Use this for initialization
    void Start()
    {
        GameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
      //  if(GameOverPanel.active == true)
        //{
          //  GameOverPanel.SetActive(false);

        //}
    }
    public void Restart()
    {
        SceneManager.LoadScene("Main");
        GameOverPanel.SetActive(false);

    }

    public void Quit()
    {
        SceneManager.LoadScene("MainMenu");
    }


    public void ShowGameOverPanel()
    {
        GameOverPanel.SetActive(true);
    }
}

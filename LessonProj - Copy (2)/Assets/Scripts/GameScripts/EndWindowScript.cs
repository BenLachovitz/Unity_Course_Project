using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndWindowScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MainMenuButton()
    {
        Time.timeScale= 1.0f;
        GlobalManagement.currentHealth = 100;
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {

    }
}

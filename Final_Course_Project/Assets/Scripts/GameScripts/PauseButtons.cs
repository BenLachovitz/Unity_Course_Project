using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButtons : MonoBehaviour
{
    public GameObject lifeBackground;
    public GameObject CrossHair;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ContinueButton()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        lifeBackground.SetActive(true);
        CrossHair.SetActive(true); 
        Cursor.lockState = CursorLockMode.Locked;
        AudioListener.pause = false;
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1.0f;
        GlobalManagement.currentHealth = 100;
        SceneManager.LoadScene(0);
        AudioListener.pause = false;
    }
}

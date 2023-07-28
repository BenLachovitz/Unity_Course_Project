using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startButton()
    {
        if (dropdown.value == 0)
        {
            GlobalManagement.maxEnemyHealth = 100;
            GlobalManagement.maxBossHealth = 100;
        }
        if (dropdown.value == 1)
        {
            GlobalManagement.maxEnemyHealth = 120;
            GlobalManagement.maxBossHealth = 125;
        }
        if (dropdown.value == 2)
        {
            GlobalManagement.maxEnemyHealth = 150;
            GlobalManagement.maxBossHealth = 175;
        }
        SceneManager.LoadScene(1);
    }
}

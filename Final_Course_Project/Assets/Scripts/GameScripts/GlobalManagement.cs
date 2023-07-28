using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GlobalManagement : MonoBehaviour
{
    static GlobalManagement instance;
    public static float currentHealth = 100;
    public static float maxEnemyHealth = 100;
    public static float maxBossHealth = 100;
   // public Text goldText;

    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }   
        else
        {
            Destroy(gameObject);
        }
       // goldText.text = "Gold: " + gold;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

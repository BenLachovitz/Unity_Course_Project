using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //for text

public class CoinMotion : MonoBehaviour
{
    public AudioSource sound;
   // public static int gold=GlobalManagement.gold;
    public Text goldText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
       // gold++;
       // goldText.text = "Gold: " + gold;
        sound.Play();
        gameObject.SetActive(false);
    }
}

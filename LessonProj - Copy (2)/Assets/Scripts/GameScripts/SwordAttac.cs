using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttac : MonoBehaviour
{
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
        if (other.tag=="NPC")
        {
            Debug.Log("Hello");

            PaladinMotion e = other.GetComponent<PaladinMotion>();
            e.GetHit();
        }
    }
}

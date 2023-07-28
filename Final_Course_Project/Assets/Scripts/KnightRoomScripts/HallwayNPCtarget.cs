using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HallwayNPCtarget : MonoBehaviour
{
    public GameObject npc;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        float x, y = 4, z;
        if (other.gameObject == npc.gameObject)
        {
                x = Random.Range(0, 100);
                if ((x >= 40 && x <= 48) || (x >= 67 && x <= 74) || (x >= 17 && x <= 22))
                    x += 10;
                z = Random.Range(0, 80);

            transform.position = new Vector3(x, y, z);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        
    }
}

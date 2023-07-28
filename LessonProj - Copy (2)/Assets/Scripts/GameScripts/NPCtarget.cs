using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCtarget : MonoBehaviour
{
    public GameObject npc;
    public GameObject keyInBackpack;
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
        float x, y = 0, z;
        if (other.gameObject == npc.gameObject)
        {
            if (keyInBackpack.activeInHierarchy)
            {
                x = Random.Range(-300, 100);
                z = Random.Range(-65, 110);
                if ((x >= -120 && x <= 100))
                {
                    if (z >= -100 && z <= 120)
                        y = 10;
                }
                else
                    y = Terrain.activeTerrain.SampleHeight(new Vector3(x, 0, z)) + 2;
            }
            else
            {
                x = Random.Range(-107, 82);
                z = Random.Range(-80, 55);
                y = Terrain.activeTerrain.SampleHeight(new Vector3(x, 0, z));
            }
            transform.position = new Vector3(x, y, z);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        
    }
}

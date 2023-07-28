using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerMiddleTargetScript : MonoBehaviour
{
    public GameObject nextTarget;
    public GameObject prevTarget;
    public FarmerMotionScript farmer;
    private int count=0;
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
        if(other.tag=="farmer")
        {
            if (count == 0)
            {
                nextTarget.SetActive(true);
                farmer.changeTarget(nextTarget);
                count = 1;
                gameObject.SetActive(false);
            }
            else
            {
                prevTarget.SetActive(true);
                farmer.changeTarget(prevTarget);
                count = 0;
                gameObject.SetActive(false);
            }
        }
    }
}

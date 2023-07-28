using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerTargetScript : MonoBehaviour
{
    public GameObject newTarget;
    public FarmerMotionScript farmer;
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
            newTarget.SetActive(true);
            farmer.changeTarget(newTarget);
            gameObject.SetActive(false);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FarmerMotionScript : MonoBehaviour
{
    private AudioSource sound;
    private GameObject target;
    private NavMeshAgent agent;
    public GameObject firstTarget;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.enabled= true;
        target = firstTarget;
        sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!sound.isPlaying) 
        {
            sound.Play();
        }
        agent.SetDestination(target.transform.position);
    }

    public void changeTarget(GameObject newTarget)
    {
        target= newTarget;
    }
}

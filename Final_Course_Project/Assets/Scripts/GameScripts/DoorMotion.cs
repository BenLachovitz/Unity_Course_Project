using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DoorMotion : MonoBehaviour
{
    public Animator animator;
    AudioSource doorSound;
    public GameObject outCollide;
    public int enterCount = 0;
    private bool isOpen = false;
    private DoorMotionSecondWay otherColScript;

    // Start is called before the first frame update
    void Start()
    {
        doorSound = GetComponent<AudioSource>();
        otherColScript = outCollide.GetComponent<DoorMotionSecondWay>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPC" || other.tag == "Player")
        {
            enterCount++;
            otherColScript.enterCount = enterCount;
            Debug.Log("1: " + enterCount + " 2: " + otherColScript.enterCount);

        }
        if (!isOpen)
        {
            isOpen = true;
            animator.SetInteger("openDoor", 1);
            doorSound.PlayDelayed(0.04f);
            if (other.tag == "NPC")
                StartCoroutine(npcWait(other.gameObject));
            outCollide.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPC" || other.tag == "Player")
        {
            enterCount--;
            otherColScript.enterCount = enterCount;
            Debug.Log("1: " + enterCount + " 2: " + otherColScript.enterCount);

        }
        if (enterCount == 0)
        {
            isOpen = false;
            animator.SetInteger("openDoor", 2);
            doorSound.PlayDelayed(0.04f);
            StartCoroutine(doorCoolDown());
        }
    }

    private IEnumerator doorCoolDown()
    {
        yield return new WaitForSeconds(2f);
        outCollide.SetActive(true);
        animator.SetInteger("openDoor", 0);

    }

    private IEnumerator npcWait(GameObject npc)
    {
        PaladinMotion c = npc.GetComponent<PaladinMotion>();
        yield return new WaitForSeconds(0.3f);
        c.agent.enabled = false;
        c.animator.SetInteger("KnightState", 0);
        c.isWaiting = true;
        yield return new WaitForSeconds(2.7f);
        c.isWaiting = false;
        c.animator.SetInteger("KnightState", 1);
        c.agent.enabled = true;
    }
}

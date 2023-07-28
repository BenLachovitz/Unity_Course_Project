using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BridgeMotion : MonoBehaviour
{
    private Animator animator;
    private AudioSource doorSound;
    public GameObject keyInHand;
    public GameObject keyInBackpack;
    public Text keyBackpackText;
    public Text bridgeText;
    private int enterCount;
    public SteelGateMotion gate;
    private Animator steelGate;
    public GameObject sword;
    private PlayerMotion playerScript;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        doorSound = GetComponent<AudioSource>();
        enterCount = 0;
        steelGate = gate.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            enterCount++;
            if (keyInHand.activeInHierarchy)
            {
                keyInHand.SetActive(false);
                sword.SetActive(true);
                playerScript = other.gameObject.GetComponent<PlayerMotion>();
                playerScript.playerAnimator.SetBool("HaveKey", false);
                playerScript.canAttack = true;
                playerScript.canBlock = true;
                keyInBackpack.SetActive(true);
                keyBackpackText.text = "The key is in you'r backpack";
            }

            if (keyInHand.activeInHierarchy || keyInBackpack.activeInHierarchy)
            {
                if (!animator.GetBool("BridgeIsOpening"))
                {
                    animator.SetBool("BridgeIsOpening", true);
                    doorSound.PlayDelayed(0.04f);
                    gate.OpenGate();
                }
            }
            else
                bridgeText.text = "You need the key to open the bridge";
        }
        else
        {
            if (other.tag == "NPC")
            {
                enterCount++;
                if (!animator.GetBool("BridgeIsOpening"))
                {
                    if (keyInBackpack.activeInHierarchy)
                        StartCoroutine(brigeOpen(other.gameObject, false));
                    else
                        StartCoroutine(brigeOpen(other.gameObject, true));
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            enterCount--;
            bridgeText.text = "";
        }
        else
        {
            if (other.tag == "NPC")
                enterCount--;
        }

        if (enterCount == 0)
        {
            animator.SetBool("BridgeIsOpening", false);
            doorSound.PlayDelayed(0.04f);
            gate.CloseGate();
        }
    }

    IEnumerator brigeOpen(GameObject h,bool gateOnly)
    {
        if (!gateOnly)
        {
            animator.SetBool("BridgeIsOpening", true);
            doorSound.PlayDelayed(0.04f);
        }
        gate.OpenGate();
        PaladinMotion c = h.GetComponent<PaladinMotion>();

        yield return new WaitForSeconds(0.5f);
        c.agent.enabled = false;
        c.animator.SetInteger("KnightState", 0);
        c.isWaiting= true;
        yield return new WaitForSeconds(3.7f);
        c.isWaiting = false;
        c.animator.SetInteger("KnightState", 1);
        c.agent.enabled = true;
    }
}

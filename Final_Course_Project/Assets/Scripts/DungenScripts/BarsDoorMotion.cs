using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BarsDoorMotion : MonoBehaviour
{
    private Animator animator;
    private AudioSource doorSound;
    public GameObject keyInHand;
    public Text BarText;
    public GameObject key;
    public DungeonPaladinMotion enemy;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        doorSound= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (keyInHand.activeInHierarchy)
        {
            animator.SetBool("DoorIsOpening", true);
            doorSound.PlayDelayed(0.2f);
            key.SetActive(false);
        }
        else
        {
            if (!animator.GetBool("DoorIsOpening"))
            {
                BarText.text = "You need to pick up the key to open the cell";
                enemy.agent.enabled = true;
                enemy.start = false;
                enemy.animator.SetInteger("KnightState", 1);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        BarText.text = "";
    }
}

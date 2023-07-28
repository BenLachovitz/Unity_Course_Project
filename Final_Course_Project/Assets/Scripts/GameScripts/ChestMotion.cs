using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChestMotion : MonoBehaviour
{
    private Animator animator;
    private AudioSource sound;
    public AudioSource keySound;
    public Text drawerText;
    public GameObject crossHair;
    public GameObject crossHairFocus;
    public GameObject playerEye;
    public GameObject chestBox;
    //bool inCollider;
    public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
        //inCollider = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerEye.activeInHierarchy)
        {
            //if the player focus is on the drawer, than change the crosshair
            RaycastHit hit;
            if (Physics.Raycast(playerEye.transform.position, playerEye.transform.forward, out hit, 10, mask))
            {
                // if (inCollider)
                //{
                if (hit.collider.gameObject == chestBox.gameObject)
                {
                    crossHair.SetActive(false);
                    crossHairFocus.SetActive(true);
                    //only if the focus is on drawer then if it is closed show text "press E to to open"
                    //otherwise show text "press C to close"
                    if (!animator.GetBool("ChestIsOpening"))
                    {
                        drawerText.text = "press SPACE to open";
                        if (Input.GetKey(KeyCode.Space))
                        {
                            animator.SetBool("ChestIsOpening", true);
                            sound.PlayDelayed(1f);
                            if (keySound.gameObject.activeInHierarchy)
                            {
                                keySound.PlayDelayed(1f);
                            }
                        }
                    }
                    else
                        drawerText.text = "press C to close";
                    if (Input.GetKey(KeyCode.C))
                    {
                        animator.SetBool("ChestIsOpening", false);
                        sound.PlayDelayed(0.7f);
                    }
                }
                // }
            }
            else
            {
                crossHair.SetActive(true);
                crossHairFocus.SetActive(false);
                //the focus is not on drawer
                drawerText.text = "";
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //inCollider = true;
    }
    private void OnTriggerExit(Collider other)
    {
        //inCollider = false;
    }

}

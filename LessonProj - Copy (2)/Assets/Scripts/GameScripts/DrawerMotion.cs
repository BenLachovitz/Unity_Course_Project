using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawerMotion : MonoBehaviour
{
    public GameObject crossHair;
    public GameObject crossHairFocus;
    Animator animator;
    public GameObject playerEye;
    public Text drawerText;
    //public GameObject gunBox;
    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // if the player focus is on drawer then change the crosshair
        RaycastHit hit;
        if (Physics.Raycast(playerEye.transform.position, playerEye.transform.forward, out hit))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                crossHair.SetActive(false);
                crossHairFocus.SetActive(true);
                // if the focus is on drawer then if it is close display text "press E to open"
                //otherwise show text "press Q to close"
                if (!animator.GetBool("OpenDrawer"))
                {
                    drawerText.text = "Press E to open";
                    if (Input.GetKey(KeyCode.E))
                    {
                        animator.SetBool("OpenDrawer", true);
                    }
                }
                else
                {
                    drawerText.text = "Press Q to close";
                    if (Input.GetKey(KeyCode.Q))
                    {
                        animator.SetBool("OpenDrawer", false);
                    }
                }
            }
            else
            {
                crossHair.SetActive(true);
                crossHairFocus.SetActive(false);
                // the focus is not on drawer
                drawerText.text = "";
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeverScript : MonoBehaviour
{
    public GameObject eye;
    public Text drawerText;
    public GameObject crossHair;
    public GameObject crossHairFocus;
    public LayerMask mask;
    public Animator trapDoorAnimator;
    private Animator animator;
    public AudioSource trapDoorSound;
    public GameObject portal;
    // Start is called before the first frame update
    void Start()
    {
        animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(eye.transform.position, eye.transform.forward, out hit, 10, mask))
        {

            if (hit.collider.gameObject == gameObject)
            {
                crossHair.SetActive(false);
                crossHairFocus.SetActive(true);
                //only if the focus is on drawer then if it is closed show text "press E to to open"
                //otherwise show text "press C to close"
                if (gameObject.activeInHierarchy)
                {
                    drawerText.text = "press E to use the lever";
                    if (Input.GetKey(KeyCode.E))
                    {
                        animator.SetTrigger("MoveLever");
                        trapDoorSound.PlayDelayed(0.2f);
                        trapDoorAnimator.SetTrigger("OpenDoor");
                        crossHair.SetActive(true);
                        crossHairFocus.SetActive(false);
                        portal.SetActive(true);
                        //the focus is not on drawer
                        drawerText.text = "";
                    }
                }
            }
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

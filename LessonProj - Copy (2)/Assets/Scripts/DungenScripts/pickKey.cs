using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class pickKey : MonoBehaviour
{
    private MeshRenderer theKey;
    public GameObject key;
    public GameObject eye;
    public Text drawerText;
    public GameObject crossHair;
    public GameObject crossHairFocus;
    public LayerMask mask;
    public DungeonPaladinMotion enemy;
    private Animator animator;
    private AudioSource sound;
    // Start is called before the first frame update
    void Start()
    {
        theKey= GetComponent<MeshRenderer>();
        animator= gameObject.GetComponent<Animator>();
        sound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //if the player focus is on the drawer, than change the crosshair
        if (theKey.isVisible)
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
                        drawerText.text = "press SPACE to collect key";
                        if (Input.GetKey(KeyCode.Space))
                        {
                            gameObject.SetActive(false);
                            key.SetActive(true);
                            crossHair.SetActive(true);
                            crossHairFocus.SetActive(false);
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="NPC")
        {
            animator.SetBool("KeyFall", true);
            enemy.changeTarget();
            StartCoroutine(keyVisibleDelay());
            sound.PlayDelayed(0.75f);
        }
    }

    IEnumerator keyVisibleDelay()
    {
        yield return new WaitForSeconds(0.5f);
        theKey.enabled = true;
    }
}


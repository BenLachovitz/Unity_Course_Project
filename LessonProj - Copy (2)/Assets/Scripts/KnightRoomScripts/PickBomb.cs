using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickBomb : MonoBehaviour
{
    public Animator player;
    public GameObject sword;
    public GameObject bombInHand;
    public GameObject eye;
    public Text drawerText;
    public GameObject crossHair;
    public GameObject crossHairFocus;
    public LayerMask mask;
    //private AudioSource sound;

    // Start is called before the first frame update
    void Start()
    {
        //sound = gameObject.GetComponent<AudioSource>();
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
                    drawerText.text = "press SPACE to pick up the bomb";
                    if (Input.GetKey(KeyCode.Space))
                    {
                        player.SetBool("HaveKey", true);
                        sword.SetActive(false);
                        gameObject.SetActive(false);
                        bombInHand.SetActive(true);
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

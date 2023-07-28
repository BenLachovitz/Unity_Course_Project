using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PickSword : MonoBehaviour
{
    public GameObject sword;
    public GameObject eye;
    public Text drawerText;
    public GameObject crossHair;
    public GameObject crossHairFocus;
    public LayerMask mask;
    public DungeonPlayerMotion player;
    public DungeonPaladinMotion enemy;
    public Collider table;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if the player focus is on the drawer, than change the crosshair
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
                    drawerText.text = "press SPACE to collect sword";
                    if (Input.GetKey(KeyCode.Space))
                    {
                        gameObject.SetActive(false);
                        sword.SetActive(true);
                        crossHair.SetActive(true);
                        crossHairFocus.SetActive(false);
                        //the focus is not on drawer
                        drawerText.text = "";
                        enemy.animator.SetBool("TableState", false);
                        enemy.playerTargeting();
                        enemy.agent.enabled = true;
                        table.enabled = false;
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

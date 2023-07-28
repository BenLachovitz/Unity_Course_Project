using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KeyMotion : MonoBehaviour
{
    public GameObject crossHair;
    public GameObject crossHairFocus;
    public GameObject keyInHand;
    public Text pickTheKey;
    public GameObject playerEye;
    public PlayerMotion player;
    public GameObject keySpotLight;
    public LayerMask mask;
    public GameObject sword;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    { 
        RaycastHit hit;
        if (Physics.Raycast(playerEye.transform.position, playerEye.transform.forward, out hit,5,mask))
        {
            if (hit.collider.gameObject == this.gameObject)
            {
                pickTheKey.text = "press E to pick the key";
                crossHair.SetActive(false);
                crossHairFocus.SetActive(true);
                if(Input.GetKey(KeyCode.E))
                {
                    player.canAttack = false;
                    player.canBlock= false;
                    player.playerAnimator.SetBool("HaveKey", true);
                    sword.SetActive(false);
                    pickTheKey.text = "";
                    this.gameObject.SetActive(false);
                    keyInHand.SetActive(true);
                    keySpotLight.SetActive(false);
                }
            }
            else
            {
                crossHair.SetActive(true);
                crossHairFocus.SetActive(false);
                //the focus is not on key
                pickTheKey.text = "";
            }
        }
    }
}

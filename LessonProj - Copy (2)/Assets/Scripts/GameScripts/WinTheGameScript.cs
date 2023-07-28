using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class WinTheGameScript : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject finishCamera;
    public GameObject player;
    public GameObject finishPlayer;
    private Animator playerAnimator;
    private Animator finishCameraAnimator;
    public Animator boatAnimator;
    public RawImage fade;
    public GameObject life;
    public GameObject crossHair;
    public GameObject crossHairFocus;
    private bool startFadeIn = false;
    private bool startFadeOut = false;
    private float fadeSpeed = 0.8f;
    private float fadeOutSpeed = 0.2f;
    public Text drawerText;
    private bool theEnd = false;
    public GameObject EndWindow;
    public LayerMask mask;
    public GameObject textBackground;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = finishPlayer.GetComponent<Animator>();
        finishCameraAnimator= finishCamera.GetComponent<Animator>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (!theEnd)
        {
            RaycastHit hit;
            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, 10, mask))
            {

                if (hit.collider.gameObject == gameObject)
                {
                    crossHair.SetActive(false);
                    crossHairFocus.SetActive(true);

                    //only if the focus is on drawer then if it is closed show text "press E to to open"
                    //otherwise show text "press C to close"
                    if (gameObject.activeInHierarchy)
                    {
                        drawerText.text = "press Q to escape";
                        if (Input.GetKey(KeyCode.Q))
                        {
                            textBackground.SetActive(false);
                            theEnd = true;
                            crossHairFocus.SetActive(false);
                            drawerText.text = "";
                            startFinishScene();
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

        if (startFadeIn)
        {
            float tempAlphaIn = fade.color.a;
            tempAlphaIn += fadeSpeed * Time.deltaTime;
            fade.color = new Color(fade.color.r,fade.color.g,fade.color.b,tempAlphaIn);
        }

        if (startFadeOut)
        {
            float tempAlphaOut = fade.color.a;
            tempAlphaOut -= fadeOutSpeed * Time.deltaTime;
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, tempAlphaOut);
        }
    }

    public void startFinishScene()
    {
        startFadeIn = true;
        crossHair.SetActive(false);
        life.SetActive(false);
        StartCoroutine(switchDelay());
    }

    private IEnumerator switchDelay()
    {
        yield return new WaitForSeconds(1.5f);
        startFadeIn = false;
        startFadeOut = true;
        yield return new WaitForSeconds(0.55f);
        mainCamera.SetActive(false);
        finishCamera.SetActive(true);
        player.SetActive(false);
        finishPlayer.SetActive(true);
        finishCameraAnimator.SetBool("movaCamera", true);
        yield return new WaitForSeconds(0.6f);
        playerAnimator.SetBool("Finish", true);
        boatAnimator.SetBool("startMoving", true);
        yield return new WaitForSeconds(5f);
        Time.timeScale = 0f;
        UnityEngine.Cursor.lockState = CursorLockMode.Confined;
        EndWindow.SetActive(true);
    }
}

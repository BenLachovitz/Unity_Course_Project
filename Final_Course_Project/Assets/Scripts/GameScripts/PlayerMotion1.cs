using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.XR;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMotion1 : MonoBehaviour
{
    CharacterController controller;
    float speed = 10, angularSpeed = 75;
    public GameObject aCamera; // must be connected to real camera in game (Unity)
    public Animator playerAnimator;
    AudioSource stepSound;
    private float maxHealth = 100;
    private float currentHealth;
    public bool isBlocking = false;
    public bool isAttacked = false;
    private PaladinMotion enemy;
    private GameObject[] allEnemys;
    private bool canAttack = true;
    public bool isAttacking = false;
    private float fillAmount = 100;
    public Image healthBarSprite;
    public Text life;
    public bool isDead=false;
    public Image hitFrameAnim;
    private float duration = 0.45f;
    private float fadeSpeed = 2f;
    private float durationTimer;
    public GameObject sword;
    public GameObject boss;

    // Start is called before the first frame update
    void Start()
    {
        allEnemys = GameObject.FindGameObjectsWithTag("NPC");
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        stepSound = GetComponent<AudioSource>();
        currentHealth = maxHealth;
        durationTimer = 0f;
        hitFrameAnim.color = new Color(hitFrameAnim.color.r, hitFrameAnim.color.g, hitFrameAnim.color.b, 0f);

    }

    // Update is called once per frame
    void Update()
    {
        healthBarSprite.fillAmount = Mathf.MoveTowards(healthBarSprite.fillAmount, fillAmount, 1.2f * Time.deltaTime);

        float dx =0f, dz=0f;
        float rotationAboutY;//of player
        float rotationAboutX;//of camera

        //rotate player by Y-axis
        rotationAboutY = angularSpeed * Time.deltaTime * Input.GetAxis("Mouse X");
        transform.Rotate(0, rotationAboutY, 0);

        //rotate camera by X-axis
        rotationAboutX = angularSpeed * Time.deltaTime * Input.GetAxis("Mouse Y");
        aCamera.transform.Rotate(rotationAboutX, 0, 0);

        if(sword.activeInHierarchy)
            canAttack= true;
        else canAttack= false;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D))
        {
            playerAnimator.SetBool("Walking", true);
            if (Input.GetKey(KeyCode.LeftShift))
            {
                dz = 16 * Time.deltaTime * Input.GetAxis("Vertical");
                stepSound.pitch = 0.85f;
                playerAnimator.SetBool("isSprint", true);
            }
            else
            {
                dz = speed * Time.deltaTime * Input.GetAxis("Vertical"); //can be -1,0 or +1
                stepSound.pitch = 0.6f;
                playerAnimator.SetBool("isSprint", false);
            }
            dx = speed * Time.deltaTime * Input.GetAxis("Horizontal"); //can be -1,0 or 
            if(Input.GetMouseButtonDown(0) && canAttack)
            {
                canAttack= false;
                isAttacking= true;
                playerAnimator.SetTrigger("AttackWalk");
                StartCoroutine(ResetAttack());
                StartCoroutine(Attack());
            }
            if (Input.GetMouseButton(1) && sword.activeInHierarchy)
            {
                isBlocking = true;
                playerAnimator.SetBool("IsBlocking", true);
            }
            else
            {
                isBlocking = false;
                playerAnimator.SetBool("IsBlocking", false);

            }
        }
        else
        {
            playerAnimator.SetBool("Walking", false);
            if (Input.GetMouseButtonDown(0)&& canAttack)
            {
                canAttack = false;
                isAttacking = true;
                playerAnimator.SetTrigger("AttackIdle");
                StartCoroutine(ResetAttack());
                StartCoroutine(Attack());
            }
            if (Input.GetMouseButton(1))
            {
                isBlocking = true;
                playerAnimator.SetBool("IsBlocking", true);
            }
            else
            {
                isBlocking = false;
                playerAnimator.SetBool("IsBlocking", false);

            }

        }

        //simple motion forward
        //this.transform.Translate(new Vector3(0,0,0.01f));

        Vector3 motion = new Vector3(dx, -0.2f, dz);
        motion = transform.TransformDirection(motion);

            controller.Move(motion);

        if (Mathf.Abs(dz) > 0.1 || Mathf.Abs(dx) > 0.1) // we are moving
            if (!stepSound.isPlaying)
                stepSound.Play();

        if(currentHealth <= 0)
        {
            Cursor.lockState = CursorLockMode.None;
            isDead = true;
            playerAnimator.SetBool("isDead", true);
            StartCoroutine(dead());
        }

        hitFrameFade();
    }

    IEnumerator Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(aCamera.transform.position, aCamera.transform.forward, out hit,6f))
        {
            if (hit.collider.gameObject == boss.gameObject)
            {
                BossMotion theBoss = boss.GetComponent<BossMotion>();
                yield return new WaitForSeconds(0.5f);
                theBoss.GetHit();
            }
            else
            {
                for (int i = 0; i < allEnemys.Length; i++)
                {
                    if (hit.collider.gameObject == allEnemys[i].gameObject)
                    {
                        enemy = allEnemys[i].GetComponent<PaladinMotion>();
                        yield return new WaitForSeconds(0.5f);
                        StartCoroutine(enemy.GetHit());
                    }
                }
            }
        }
        yield return new WaitForSeconds(0.005f);
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(1.15f);
        isAttacking= false;
        canAttack = true;
    }

    IEnumerator getHit()
    {
        yield return new WaitForSeconds(0.25f);
        if(!isBlocking)
        {
            currentHealth -= 10;
            hitFrameAnim.color = new Color(hitFrameAnim.color.r, hitFrameAnim.color.g, hitFrameAnim.color.b, 1);
            durationTimer = 0;
            updateHealthBar(maxHealth, currentHealth);
        }
    }

    public void hitFrameFade()
    {
        if(hitFrameAnim.color.a > 0)
        {
            durationTimer += Time.deltaTime;
            if(durationTimer>duration)
            {
                float tempAlpha = hitFrameAnim.color.a;
                tempAlpha -= Time.deltaTime*fadeSpeed;
                hitFrameAnim.color = new Color(hitFrameAnim.color.r, hitFrameAnim.color.g, hitFrameAnim.color.b, tempAlpha);
            }
        }
    }

    IEnumerator dead()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Game Over");

    }

    public void updateHealthBar(float maxHealth, float currentHealth)
    {
        fillAmount = currentHealth / maxHealth;
        life.text = "Life: " + currentHealth + "%";
    }

    public void OnTriggerEnter(Collider other)
    {
            if (other.tag == "Sword" && isAttacked)
            {
                StartCoroutine(getHit());
            }
    }
}

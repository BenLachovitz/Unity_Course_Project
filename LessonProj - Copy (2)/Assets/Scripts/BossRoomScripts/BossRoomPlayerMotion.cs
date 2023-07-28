using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.XR;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class BossRoomPlayerMotion : MonoBehaviour
{
    CharacterController controller;
    float speed = 10, angularSpeed = 2.5f;
    public GameObject aCamera; // must be connected to real camera in game (Unity)
    private Quaternion cameraRotation;
    public Animator playerAnimator;
    AudioSource stepSound;
    private float maxHealth = 100;
    public static float currentHealth = GlobalManagement.currentHealth;
    public bool isBlocking = false;
    public bool isAttacked = false;
    public BossMotion enemy;
    public bool canAttack = true;
    public bool canBlock = true;
    public bool isAttacking = false;
    private float fillAmount = 100;
    public Image healthBarSprite;
    public TextMeshProUGUI life;
    public bool isDead=false;
    public Image hitFrameAnim;
    private float duration = 0.45f;
    private float fadeSpeed = 2f;
    private float durationTimer;
    public GameObject sword;
    private float regenAmount = 0.3f;
    public AudioSource swordSlash;
    public AudioSource swordBlock;
    public AudioSource deathSound;
    public ParticleSystem blockEffect;
    public GameObject pauseWin;
    public GameObject lifeBackground;
    public GameObject CrossHair;
    private bool wasPaused = false;
    private void Awake()
    {
        currentHealth = GlobalManagement.currentHealth;
    }
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        stepSound = GetComponent<AudioSource>();
        //currentHealth = maxHealth;
        durationTimer = 0f;
        hitFrameAnim.color = new Color(hitFrameAnim.color.r, hitFrameAnim.color.g, hitFrameAnim.color.b, 0f);
        cameraRotation = aCamera.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        healthBarSprite.fillAmount = Mathf.MoveTowards(healthBarSprite.fillAmount, fillAmount, 1.2f * Time.deltaTime);

        float dx =0f, dz=0f;
        float rotationAboutY;//of player
        //float rotationAboutX;//of camera

        if (Time.timeScale != 0)
        {
            //rotate player by Y-axis
            rotationAboutY = angularSpeed * Input.GetAxisRaw("Mouse X");
            transform.Rotate(0, rotationAboutY, 0);

            //rotate camera by X-axis
            cameraRotation.x += angularSpeed * Input.GetAxisRaw("Mouse Y");
            cameraRotation.x = Mathf.Clamp(cameraRotation.x, -30f, 50f);

            aCamera.transform.localRotation = Quaternion.Euler(cameraRotation.x, 0, 0);

        }

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)|| Input.GetKey(KeyCode.A)|| Input.GetKey(KeyCode.D))
        {
            if (!stepSound.isPlaying)
                stepSound.Play();
            if (!playerAnimator.GetBool("HaveKey"))
                playerAnimator.SetBool("Walking", true);
            else
                playerAnimator.SetBool("KeyWalk", true);
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
                if (!playerAnimator.GetBool("HaveKey"))
                    playerAnimator.SetBool("isSprint", false);
            }
            dx = speed * Time.deltaTime * Input.GetAxis("Horizontal"); //can be -1,0 or 
            if (Input.GetMouseButtonDown(0))
            {
                if (canAttack)
                {
                    swordSlash.Play();
                    canAttack = false;
                    isAttacking = true;
                    playerAnimator.SetTrigger("AttackWalk");
                    StartCoroutine(Attack());
                    StartCoroutine(ResetAttack());
                }
            }
            if (Input.GetMouseButton(1) && canBlock)
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
            if (!playerAnimator.GetBool("HaveKey"))
                playerAnimator.SetBool("Walking", false);
            else
                playerAnimator.SetBool("KeyWalk", false);
            if (Input.GetMouseButtonDown(0))
            {
                if (canAttack)
                {
                    swordSlash.Play();
                    canAttack = false;
                    isAttacking = true;
                    playerAnimator.SetTrigger("AttackIdle");
                    StartCoroutine(Attack());
                    StartCoroutine(ResetAttack());
                }
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

        if(currentHealth <= 0)
        {
            if (!deathSound.isPlaying)
                deathSound.Play();
            isDead = true;
            life.text = "Life: 0%";
            Cursor.lockState = CursorLockMode.None;
            playerAnimator.SetBool("isDead", true);
            StartCoroutine(dead());
        }

        if (currentHealth < 100 && currentHealth > 0)
        {
            currentHealth += regenAmount * Time.deltaTime;
            updateHealthBar(maxHealth, currentHealth);
        }

        hitFrameFade();

        if (Input.GetKey(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
            AudioListener.pause = true;
            pauseWin.SetActive(true);
            CrossHair.SetActive(false);
            lifeBackground.SetActive(false);
            canAttack = false;
            canBlock = false;
            wasPaused = true;
        }
        else
        {
            if (Time.timeScale > 0f && wasPaused)
            {
                wasPaused = false;
                canAttack = true;
                canBlock = true;
            }
        }
    }

    IEnumerator Attack()
    {
        RaycastHit hit;
        if (Physics.Raycast(aCamera.transform.position, aCamera.transform.forward, out hit, 6f))
        {
            if (hit.collider.gameObject == enemy.gameObject)
            {
                yield return new WaitForSeconds(0.5f);
                StartCoroutine(enemy.GetHit());
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
        if (isBlocking)
        {
            blockEffect.Play();
            swordBlock.Play();
        }

        else
        {
            isAttacked = false;
            yield return new WaitForSeconds(0.25f);
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
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("Game Over");

    }

    public void updateHealthBar(float maxHealth, float currentHealth)
    {
        fillAmount = currentHealth / maxHealth;
        life.text = "Life: " + (int)currentHealth + "%";
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sword" && isAttacked && !enemy.isDead)
            StartCoroutine(getHit());
    }
}

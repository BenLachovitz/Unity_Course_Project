using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ExitDoor : MonoBehaviour
{
    private AudioSource sound;
    private Animator animator;
    public DungeonPaladinMotion enemy;
    public RawImage fade;
    private float fadeSpeed = 1.25f;
    private bool startFade = false;
    // Start is called before the first frame update
    void Start()
    {
        sound = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (startFade)
        {
            float tempAlpha = fade.color.a;
            tempAlpha += Time.deltaTime * fadeSpeed;
            fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, tempAlpha);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enemy.animator.GetInteger("KnightState") == 2 && other.tag=="Player")
        {
            animator.SetBool("OpenDoor", true);
            sound.PlayDelayed(0.2f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (enemy.animator.GetInteger("KnightState") == 2)
        {
            animator.SetBool("OpenDoor", false);
            sound.PlayDelayed(0.2f);
            StartCoroutine(StartTrasition());
        }
    }

    IEnumerator StartTrasition()
    {
        /*GlobalManagement.spawningPoint = player.transform.position;
        GlobalManagement.spawningPoint.x -= 10;
        GlobalManagement.gold = CoinMotion.gold;*/
        //animator.SetTrigger("StartFadeIn");
        GlobalManagement.currentHealth = DungeonPlayerMotion.currentHealth;
        fade.color= fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, 0.01f);
        startFade = true;
        yield return new WaitForSeconds(2.5f); // delay 5 sec
        SceneManager.LoadScene(2);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalToKnightRoom : MonoBehaviour
{
    public RawImage fade;
    private float fadeSpeed = 1.25f;
    private bool startFade = false;
    // Start is called before the first frame update
    void Start()
    {
        
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
        StartCoroutine(StartTrasition());
    }
    IEnumerator StartTrasition()
    {
        /*GlobalManagement.spawningPoint = player.transform.position;
        GlobalManagement.spawningPoint.x -= 10;
        GlobalManagement.gold = CoinMotion.gold;*/
        //animator.SetTrigger("StartFadeIn");
        GlobalManagement.currentHealth = HallwayPlayerMotion.currentHealth;
        fade.color = fade.color = new Color(fade.color.r, fade.color.g, fade.color.b, 0.01f);
        startFade = true;
        yield return new WaitForSeconds(2.5f); // delay 5 sec
        SceneManager.LoadScene(3);
    }
}

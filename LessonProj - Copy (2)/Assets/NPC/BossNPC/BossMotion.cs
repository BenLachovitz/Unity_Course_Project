using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class BossMotion : MonoBehaviour
{
    private Animator BossAnimator;
    public bool isFight=false;
    private NavMeshAgent agent;
    private bool canAttack = true;
    private bool gotHit=false;
    public GameObject player;
    private BossRoomPlayerMotion playerScript;
    private GameObject target;
    private float maxHealth = GlobalManagement.maxBossHealth;
    private float currentHealth;
    private float fillAmount = 1;
    private float x;
    private float y;
    private float z;
    public Image healthBackground;
    public Image healthBarSprite;
    public Text life;
    public bool isDead = false;
    public Collider axeCol;
    public ParticleSystem blood;
    public AudioSource swordSlash;
    public AudioSource damageSound;
    // Start is called before the first frame update
    void Start()
    {
        agent= GetComponent<NavMeshAgent>();
        target = player;
        currentHealth = maxHealth;
        BossAnimator= GetComponent<Animator>();
        playerScript = player.GetComponent<BossRoomPlayerMotion>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBarSprite.fillAmount = Mathf.MoveTowards(healthBarSprite.fillAmount, fillAmount, 1.2f * Time.deltaTime);
        if(agent.enabled==true && !isDead)
            agent.SetDestination(target.transform.position);

        x = Math.Abs(gameObject.transform.position.x - player.transform.position.x);
        y = Math.Abs(gameObject.transform.position.y - player.transform.position.y);
        z = Math.Abs(gameObject.transform.position.z - player.transform.position.z);

        if (isFight && !isDead)
        { 
            agent.enabled= true;
        }

        if(agent.enabled && !isDead)
        {
            gameObject.transform.LookAt(player.transform.position);
            if (x <= 9 && z <= 9 && y <= 20)
            {
                BossAnimator.SetInteger("BossState", 2);
                if (canAttack && !gotHit)
                {
                    swordSlash.PlayDelayed(0.6f);
                    canAttack = false;
                    playerScript.isAttacked = true;
                    BossAnimator.SetTrigger("BossAttack");
                    StartCoroutine(bossAttackCoolDown());
                }
            }
            else
            {
                BossAnimator.SetInteger("BossState", 1);
                axeCol.enabled = false;
            }
        }
        if(currentHealth<=0)
        {
            axeCol.enabled = false;
        }
    }

    public void updateHealthBar(float maxHealth, float currentHealth)
    {
        fillAmount = currentHealth / maxHealth;
        life.text = "Boss: " + Math.Round((100*currentHealth)/maxHealth) + "%";
    }
    public IEnumerator GetHit()
    {
        gotHit = true;
        axeCol.enabled = false;
        blood.Play();
        damageSound.Play();
        yield return new WaitForSeconds(0.2f);
        currentHealth -= 10;
        updateHealthBar(maxHealth, currentHealth);

        if (currentHealth <= 0)
        {
            axeCol.enabled = false;
            isDead = true;
            agent.enabled = false;
            healthBackground.gameObject.SetActive(false);
            BossAnimator.SetInteger("BossState", 3);
        }
        else
            BossAnimator.SetTrigger("GotHit");
        yield return new WaitForSeconds(3f);
        axeCol.enabled = true;
        gotHit = false;
    }

    public IEnumerator bossAttackCoolDown()
    {
        yield return new WaitForSeconds(0.5f);
        axeCol.enabled = true;
        yield return new WaitForSeconds(2.5f);
        canAttack = true;
    }
}

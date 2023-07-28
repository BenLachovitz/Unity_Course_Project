using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class DungeonPaladinMotion : MonoBehaviour
{
    public GameObject enemyRotation;
    public Image healthBarSprite;
    public Canvas healthBarCanvas;
    public NavMeshAgent agent;
    public Animator animator;
    private bool canAttack = true;
    public GameObject TableTarget;
    public GameObject KeyTarget;
    public GameObject playerTarget;
    private float maxHealth = GlobalManagement.maxEnemyHealth;
    private float currentHealth;
    private float fillAmount = 1;
    private GameObject target;
    private float x;
    private float y;
    private float z;
    private DungeonPlayerMotion player;
    public ParticleSystem blood;
    public AudioSource swordSlash;
    public AudioSource damageSound;
    public bool start;
    //public LineRenderer lr;
    public bool isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        target = KeyTarget;
        currentHealth = maxHealth;
        player = playerTarget.GetComponent<DungeonPlayerMotion>();
        animator.SetInteger("KnightState", 0);
        agent.enabled = false;
        start = true;
        //lr = gameObject.AddComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBarCanvas.transform.rotation = Quaternion.LookRotation(transform.position - playerTarget.transform.position);
        healthBarSprite.fillAmount = Mathf.MoveTowards(healthBarSprite.fillAmount, fillAmount, 1.2f * Time.deltaTime);
        bool isPlayerDead = player.isDead;
        if (agent.enabled)
        {
            agent.SetDestination(target.transform.position);
            //lr.positionCount = agent.path.corners.Length;
            //lr.SetPositions(agent.path.corners);
        }

        x = Math.Abs(gameObject.transform.position.x - playerTarget.transform.position.x);
        y = Math.Abs(gameObject.transform.position.y - playerTarget.transform.position.y);
        z = Math.Abs(gameObject.transform.position.z - playerTarget.transform.position.z);
        if (!start)
        {
            if (animator.GetInteger("KnightState") != 2 && !isPlayerDead)
            {
                if (x <= 8 && z <= 8 && y <= 7 && target == playerTarget)
                {
                    //target = playerTarget;
                    animator.SetInteger("KnightState", 0);
                    enemyRotation.transform.LookAt(playerTarget.transform.position);
                    if (canAttack)
                    {
                        swordSlash.Play();
                        canAttack = false;
                        player.isAttacked = true;
                        animator.SetTrigger("Attack");
                        StartCoroutine(enemyAttackCoolDown());
                    }
                }
                else
                {
                    animator.SetInteger("KnightState", 1);
                    //target = SeatTarget;
                }
            }
        }
        if (currentHealth <= 0)
        {
            healthBarCanvas.enabled = false;
            agent.enabled = false;
        }
        if(isPlayerDead)
        {
            target = TableTarget;
            animator.SetInteger("KnightState", 1);
        }
    }

    public void updateHealthBar(float maxHealth, float currentHealth)
    {
        fillAmount = currentHealth / maxHealth;
    }

    public IEnumerator GetHit()
    {
        blood.Play();
        damageSound.Play();
        yield return new WaitForSeconds(0.2f);
        currentHealth -= 25;
        updateHealthBar(maxHealth, currentHealth);
        canAttack= false;
        if (currentHealth <= 0)
        {
            isDead = true;
            animator.SetInteger("KnightState", 2);
            agent.enabled = false;
            yield return new WaitForSeconds(0.3f);
            blood.Stop();
            
        }
        else
            animator.SetTrigger("Impact");
    }

    public IEnumerator enemyAttackCoolDown()
    {
        yield return new WaitForSeconds(3f);
        canAttack = true;
    }

    public void changeTarget()
    {
        target = TableTarget;
    }
    public void playerTargeting()
    {
        target = playerTarget;
    }
}

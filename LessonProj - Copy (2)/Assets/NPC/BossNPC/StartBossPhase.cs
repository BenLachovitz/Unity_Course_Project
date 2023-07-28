using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartBossPhase : MonoBehaviour
{
    public GameObject Boss;
    private Animator BossAnimator;
    private BossMotion bossScript;
    // Start is called before the first frame update
    void Start()
    {
        BossAnimator= Boss.GetComponent<Animator>();
        bossScript= Boss.GetComponent<BossMotion>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            BossAnimator.SetInteger("BossState", 0);
            StartCoroutine(StartFight());
        }
    }

    private IEnumerator StartFight()
    {
        yield return new WaitForSeconds(1.75f);
        bossScript.isFight= true;
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToScene1 : MonoBehaviour
{
    public Animator animator;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        //animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        
        animator.SetTrigger("StartFadeIn");
        yield return new WaitForSeconds(4); // delay 5 sec
        SceneManager.LoadScene(1);
    }
}

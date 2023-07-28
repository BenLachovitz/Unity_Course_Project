using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalToScene0 : MonoBehaviour
{
    public Animator animator;
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
        animator.SetTrigger("StartFadeIn");
        StartCoroutine(StartTrasition());
    }

    IEnumerator StartTrasition()
    {
        yield return new WaitForSeconds(4); // delay 5 sec
        SceneManager.LoadScene(0);
    }
}

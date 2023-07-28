using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GunShooting : MonoBehaviour
{
    public GameObject eye;
    public GameObject target;
    public GameObject muzzle;
    private LineRenderer line;
    private AudioSource shoot;
    private PaladinMotion enemy;
    private GameObject[] allEnemys;
    // Start is called before the first frame update
    void Start()
    {
        allEnemys = GameObject.FindGameObjectsWithTag("NPC");
        line = GetComponent<LineRenderer>();
        shoot= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
      //  if(Input.GetKeyDown(KeyCode.Space))
      // {

      // }
        if(Input.GetMouseButtonDown(0)&&this.gameObject.activeInHierarchy) 
        {
            StartCoroutine(Shooting());
        }
    }

    IEnumerator Shooting()
    {
        RaycastHit hit;
        if (Physics.Raycast(eye.transform.position, eye.transform.forward, out hit))
        {
            for (int i = 0; i < allEnemys.Length; i++)
            {
                if (hit.collider.gameObject == allEnemys[i].gameObject)
                {
                    enemy = allEnemys[i].GetComponent<PaladinMotion>();
                    StartCoroutine(enemy.GetHit());
                }
            }
            line.enabled = true;
            target.transform.position = hit.point;
            line.SetPosition(0, muzzle.transform.position);
            line.SetPosition(1, target.transform.position);
            shoot.Play();
            yield return new WaitForSeconds(0.025f);
            line.enabled = false;
        }
    }
}

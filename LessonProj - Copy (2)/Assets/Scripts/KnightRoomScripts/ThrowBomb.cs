using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThrowBomb : MonoBehaviour
{
    public Animator player;
    public GameObject sword;
    public Transform eye;
    public Transform attackPoint;
    public GameObject bombHidden;
    public GameObject bombOnFloor;
    public MeshRenderer ms;
    public GameObject part;
    public AudioSource sound;
    public ParticleSystem explosion;
    private Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        //sound= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.G))    
        {
            direction = eye.transform.forward * 30f + transform.up * 12f;
            direction.Normalize();
            player.SetBool("ThrowGrenade",true);
            StartCoroutine(throwGrenade());
        }
    }

    public IEnumerator Explode()
    {
        yield return new WaitForSeconds(1.5f);
        Collider[] cls = Physics.OverlapSphere(bombHidden.transform.position, 40);
        for (int i = 0; i < cls.Length; i++)
        {
            Rigidbody r = cls[i].GetComponent<Rigidbody>();
            if(r != null)
            {
                r.AddExplosionForce(7500, bombHidden.transform.position, 40);
            }
        }
        bombOnFloor.SetActive(true);
        explosion.transform.position = bombHidden.transform.position;
        explosion.Play();
        sound.PlayDelayed(0.15f);
        gameObject.SetActive(false);
        bombHidden.SetActive(false);
        part.SetActive(true);
        ms.enabled = true;
    }

    public IEnumerator throwGrenade()
    {
        yield return new WaitForSeconds(0.75f);
        bombHidden.SetActive(true);
        bombHidden.transform.position = new Vector3(gameObject.transform.position.x+2f,gameObject.transform.position.y,gameObject.transform.position.z-4f);
        //this.gameObject.SetActive(false);
        ms.enabled = false;
        part.SetActive(false);
        Rigidbody rb = bombHidden.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.useGravity = true;
        rb.AddForce(direction, ForceMode.VelocityChange);
        player.SetBool("ThrowGrenade",false);
        player.SetBool("HaveKey", false);
        StartCoroutine(Explode());
        yield return new WaitForSeconds(0.15f);
        sword.SetActive(true);
    }
}

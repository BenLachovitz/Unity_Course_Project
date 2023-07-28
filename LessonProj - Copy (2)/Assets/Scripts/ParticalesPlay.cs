using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticalesPlay : MonoBehaviour
{
    private ParticleSystem effect;
    private AudioSource soundEffect;
    // Start is called before the first frame update
    void Start()
    {
        effect= GetComponent<ParticleSystem>();
        soundEffect= GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "hammer")
        {
            soundEffect.Play();
            effect.Play();
        }

    }
}

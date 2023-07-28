using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteelGateMotion : MonoBehaviour
{
    Animator animator;
    AudioSource doorSound;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        doorSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenGate()
    {
        animator.SetBool("GateIsOpening", true);
        doorSound.PlayDelayed(0.04f);
    }

    public void CloseGate()
    {
        animator.SetBool("GateIsOpening", false);
        doorSound.PlayDelayed(0.04f);
    }
}

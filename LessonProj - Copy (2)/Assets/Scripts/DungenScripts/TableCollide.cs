using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableCollide : MonoBehaviour
{
    public DungeonPaladinMotion enemy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        enemy.animator.SetBool("TableState", true);
        enemy.agent.enabled = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMusicDontDestroy : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] musicObject = GameObject.FindGameObjectsWithTag("GameMusic");
        if(musicObject.Length>1)
            Destroy(this.gameObject);
        else
            DontDestroyOnLoad(this.gameObject);
    }
}

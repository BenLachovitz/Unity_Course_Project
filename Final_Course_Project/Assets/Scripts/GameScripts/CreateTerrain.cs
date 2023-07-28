using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateTerrain : MonoBehaviour
{
    private Terrain tr;
    int width, height;
    float [,]heightMaps;
    // Start is called before the first frame update
    void Start()
    {
        int i, j;
        tr = Terrain.activeTerrains[1];

        width = height = tr.terrainData.heightmapResolution;
        heightMaps = new float[height, width];

        for (i = 0; i < height/2;i++)
            for (j=0;j<width;j++)
                heightMaps[i, j] = 0;

        for (i = height/2; i < height; i++)
            for (j = 0; j < width; j++)
                heightMaps[i, j] = 1;

        tr.terrainData.SetHeights(0, 0, heightMaps);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

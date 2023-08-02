using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDensity : MonoBehaviour
{
    public Terrain terrain;

    private TerrainData tData;
    private TerrainDetector terrainDetector;

    // Start is called before the first frame update
    void Start()
    {
        terrainDetector = new TerrainDetector();
        //tData = terrain.terrainData;
        //tData.
        //Debug.Log("Width: " + tData.alphamapWidth);
        //Debug.Log("Height: " + tData.alphamapHeight);
        //Debug.Log(tData.GetHeights(0, 0, tData.alphamapWidth, tData.alphamapHeight));
    }

    // Update is called once per frame
    void Update()
    {
        //int terrainTextureIndex = terrainDetector.GetActiveTerrainTextureIdx(transform.position);
        //Debug.Log(terrainTextureIndex);
    }
}

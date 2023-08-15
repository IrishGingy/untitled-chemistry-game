using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDensity : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private TextureDensity[] textureDensities;
    [SerializeField] public float currentDensity;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckGround();
    }

    private void CheckGround()
    {
        Debug.DrawRay(transform.position, Vector3.down * 5f, Color.red);
        if (Input.GetKeyDown(KeyCode.Space) && Physics.Raycast(transform.position,
            Vector3.down,
            out RaycastHit hit,
            5f,
            groundLayer)
            )
        {
            if (hit.collider.TryGetComponent<Terrain>(out Terrain terrain))
            {
                CalculateDensity(terrain, hit.point);
            }
            else if (hit.collider.TryGetComponent<Renderer>(out Renderer renderer))
            {
                //Debug.Log("Hit renderer component");
            }
        }
    }

    private void CalculateDensity(Terrain terrain, Vector3 hitPoint)
    {
        // Find terrain position of our hitpoint
        Vector3 terrainPosition = hitPoint - terrain.transform.position;
        // Calculate offset
        Vector3 splatMapPosition = new Vector3(
            terrainPosition.x / terrain.terrainData.size.x,
            0,
            terrainPosition.z / terrain.terrainData.size.z
        );

        // x and z coordinates of the alphamap
        int x = Mathf.FloorToInt(splatMapPosition.x * terrain.terrainData.alphamapWidth);
        int z = Mathf.FloorToInt(splatMapPosition.z * terrain.terrainData.alphamapHeight);

        // Getting alphamap at those coordinates with a 1 pixel texture size sample
        float[,,] alphaMap = terrain.terrainData.GetAlphamaps(x, z, 1, 1);

        int primaryIndex = 0;
        for (int i = 1; i < alphaMap.Length; i++)
        {
            // Find out which texture is most prevalent at this spot
            if (alphaMap[0, 0, i] > alphaMap[0, 0, primaryIndex])
            {
                primaryIndex = i;
            }
        }

        foreach(TextureDensity textureDensity in textureDensities)
        {
            if (textureDensity.albedo == terrain.terrainData.terrainLayers[primaryIndex].diffuseTexture)
            {
                currentDensity = textureDensity.density;
                break;
            }
        }
    }

    [System.Serializable]
    private class TextureDensity
    {
        public Texture albedo;
        public float density;
    }

    //public Terrain terrain;

    //private TerrainData tData;
    //private TerrainDetector terrainDetector;

    //// Start is called before the first frame update
    //void Start()
    //{
    //    terrainDetector = new TerrainDetector();
    //    //tData = terrain.terrainData;
    //    //tData.
    //    //Debug.Log("Width: " + tData.alphamapWidth);
    //    //Debug.Log("Height: " + tData.alphamapHeight);
    //    //Debug.Log(tData.GetHeights(0, 0, tData.alphamapWidth, tData.alphamapHeight));
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    //int terrainTextureIndex = terrainDetector.GetActiveTerrainTextureIdx(transform.position);
    //    //Debug.Log(terrainTextureIndex);
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public GameObject player;
    public GameObject gameControllerObject;

    public int detailCountPerDetailPixel = 2;
    public int affectedTextureIndex = 0;

    private Terrain terrain;
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
        gameController = gameControllerObject.GetComponent<GameController>();
        terrain = Terrain.activeTerrain;
        if (!PlayerPrefs.HasKey("grassGenerated"))
        {
            SpawnGrass();
            PlayerPrefs.SetInt("grassGenerated", 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerMovement>().IsRiding())
        {
            CutGrass(player.transform.position, 4);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            SpawnGrass();
        }
    }

    private void CutGrass(Vector3 position, float radius)
    {
        int TerrainDetailMapSize = terrain.terrainData.detailResolution;    

        float PrPxSize = TerrainDetailMapSize / terrain.terrainData.size.x;

        Vector3 TexturePoint3D = position - terrain.transform.position;
        TexturePoint3D = TexturePoint3D * PrPxSize;

        float[] xymaxmin = new float[4];
        xymaxmin[0] = TexturePoint3D.z + radius;
        xymaxmin[1] = TexturePoint3D.z - radius;
        xymaxmin[2] = TexturePoint3D.x + radius;
        xymaxmin[3] = TexturePoint3D.x - radius;


        int[,] map = terrain.terrainData.GetDetailLayer(0, 0, terrain.terrainData.detailWidth, terrain.terrainData.detailHeight, 0);
     
        for (int y = Mathf.FloorToInt(xymaxmin[3]); y < Mathf.CeilToInt(xymaxmin[2]); y++)
        {
            for (int x = Mathf.FloorToInt(xymaxmin[1]); x < Mathf.CeilToInt(xymaxmin[0]); x++)
            {
                map[x, y] = 0;
            }
        }

        terrain.terrainData.SetDetailLayer(0, 0, 0, map);
    }

    private void SpawnGrass()
    {
        int alphamapWidth = terrain.terrainData.alphamapWidth;
        int alphamapHeight = terrain.terrainData.alphamapHeight;
        int detailWidth = terrain.terrainData.detailResolution;
        int detailHeight = detailWidth;

        float resolutionDiffFactor = (float)alphamapWidth / detailWidth;


        float[,,] splatmap = terrain.terrainData.GetAlphamaps(0, 0, alphamapWidth, alphamapHeight);


        int[,] newDetailLayer = new int[detailWidth, detailHeight];

        //find where the texture is present
        for (int j = 0; j < detailWidth; j++)
        {
            for (int k = 0; k < detailHeight; k++)
            {
                float alphaValue = splatmap[(int)(resolutionDiffFactor * j), (int)(resolutionDiffFactor * k), affectedTextureIndex];

                newDetailLayer[j, k] = (int)Mathf.Round(alphaValue * ((float)detailCountPerDetailPixel)) + newDetailLayer[j, k];
            }
        }

        terrain.terrainData.SetDetailLayer(0, 0, 0, newDetailLayer);
    }
}

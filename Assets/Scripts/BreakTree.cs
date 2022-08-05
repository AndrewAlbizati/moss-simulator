using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class BreakTree : MonoBehaviour
{
    private List<TreeInstance> TreeInstances;

    void Start()
    {
        Terrain.activeTerrain.GetComponent<TerrainCollider>().enabled = false;
        Terrain.activeTerrain.terrainData.treeInstances = new TreeInstance[0];
        for (int i = 0; i < 4000; i++)
        {
            CreateTree();
        }

        Terrain.activeTerrain.GetComponent<TerrainCollider>().enabled = true;
        Terrain.activeTerrain.Flush();
        TreeInstances = new List<TreeInstance>(Terrain.activeTerrain.terrainData.treeInstances);
    }

    // Update is called once per frame
    void Update()
    {
        // Left click event
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("left click");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 10.0f))
            {
                Debug.Log("raycast");
                // Check if click was aimed at a tree
                if (hit.collider.name != Terrain.activeTerrain.name)
                {
                    return;
                }
                Debug.Log("aimed at tree1");
                float sampleHeight = Terrain.activeTerrain.SampleHeight(hit.point);

                // Check if the ground was clicked
                if (hit.point.y <= sampleHeight + 0.01f)
                {
                    return;
                }
                Debug.Log("aimed at tree2");
                //Debug.Log(DateTime.Now);

                TerrainData terrain = Terrain.activeTerrain.terrainData;
                TreeInstance[] treeInstances = terrain.treeInstances;

                float maxDistance = float.MaxValue;
                int closestTreeIndex = 0;

                Debug.Log("find closest tree");
                // Find the closest tree
                for (int i = 0; i < treeInstances.Length; i++)
                {
                    TreeInstance currentTree = treeInstances[i];
                    Vector3 currentTreeWorldPosition = Vector3.Scale(currentTree.position, terrain.size) + Terrain.activeTerrain.transform.position;

                    float distance = Vector3.Distance(currentTreeWorldPosition, hit.point);

                    if (distance < maxDistance)
                    {
                        maxDistance = distance;
                        closestTreeIndex = i;
                    }
                }
                Debug.Log("done finding closest tree");
                //Debug.Log(System.DateTime.Now);

                TreeInstances.RemoveAt(closestTreeIndex);

                // Disable the tree collider
                Terrain.activeTerrain.GetComponent<TerrainCollider>().enabled = false;

                terrain.treeInstances = TreeInstances.ToArray();

                // Refresh the trees
                float[,] heights = terrain.GetHeights(0, 0, 0, 0);
                terrain.SetHeights(0, 0, heights);

                Terrain.activeTerrain.GetComponent<TerrainCollider>().enabled = true;
                Terrain.activeTerrain.Flush();

                // Enable the tree collider
                Debug.Log("done!");
                //Debug.Log(System.DateTime.Now);
            }
        }
    }

    void CreateTree()
    {
        // TODO Add capsule collider for tree
        Terrain terrain = Terrain.activeTerrain;

        TreeInstance tempInstance = new TreeInstance();

        tempInstance.prototypeIndex = UnityEngine.Random.Range(0, 3);
        //tempInstance.prototypeIndex = 0;

        tempInstance.color = Color.white;

        tempInstance.heightScale = 1;

        tempInstance.widthScale = 1;

        tempInstance.position = GeneratePosition();

        terrain.AddTreeInstance(tempInstance);

        terrain.Flush();
    }


    Vector3 GeneratePosition()
    {
        Vector3 pos;
        Vector3 worldPos;
        do
        {
            float x = UnityEngine.Random.value;
            float z = UnityEngine.Random.value;

            pos = new Vector3(x, 0, z);

            worldPos = Vector3.Scale(pos, Terrain.activeTerrain.terrainData.size) + Terrain.activeTerrain.transform.position;
        } while (worldPos.x > -360 && worldPos.x < 100 && worldPos.z > 10 && worldPos.z < 300);

        return pos;
    }
}
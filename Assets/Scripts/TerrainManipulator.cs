using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManipulator : MonoBehaviour
{

    
    [SerializeField] private float brushRadius;
    [SerializeField] private float radius;
    [SerializeField] private float raisePower;
    [SerializeField] private float timeScale;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitCollider in hitColliders)
        {
            TerrainCollider TC = hitCollider.GetComponent<TerrainCollider>();
            if (TC  != null) {
                TerrainData TD = TC.terrainData;
                int intR = (int)brushRadius;
                float scale = TD.heightmapResolution / TD.size.x;
                int mapX = (int)(transform.position.x * scale);
                int mapY = (int)(transform.position.z * scale);
                float[,] heights = TD.GetHeights((int)transform.position.x - intR/2, (int)transform.position.z - intR/2, intR, intR);
                for (int i = 0; i < intR; i++) {
                    for (int j = 0; j < intR; j++) {
                        heights[i,j] += Mathf.Abs(transform.position.y - heights[i,j]) / TD.size.y;
                    }
                }
                
                // heights[0,0] += Mathf.Abs(transform.position.y - heights[0,0]) / TD.size.y;
                TD.SetHeightsDelayLOD(mapX- intR/2, mapY- intR/2, heights);
                TD.SyncHeightmap();
            }
        }
    }
}

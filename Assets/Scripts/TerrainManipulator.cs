using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManipulator : MonoBehaviour
{

    
    [SerializeField] private float brushRadius;
    [SerializeField] private float radius;
    [SerializeField] private float stddev;
    [SerializeField] private float n;
    [SerializeField] private bool addMode;

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
                float[,] heights = TD.GetHeights(mapX- intR/2, mapY- intR/2, intR, intR);
                Debug.Log(heights);
                for (int i = 0; i < intR; i++) {
                    int radI = i - intR/2;
                    for (int j = 0; j < intR; j++) {
                        // heights[i,j] += Mathf.Abs(transform.position.y - heights[i,j]) / TD.size.y;
                        int radJ = j - intR/2;
                        float elevate = (1 / (Mathf.Pow(stddev, n) * Mathf.Pow(2*Mathf.PI, n/2))) * Mathf.Exp(-(radI*radI + radJ*radJ) / (2*stddev*stddev)) * Mathf.Abs(transform.position.y - heights[i,j]) / TD.size.y;
                        // Debug.Log(elevate);
                        // Debug.Log(heights[i,j]);
                        
                        if (elevate >= heights[i,j] && addMode) {
                            heights[i,j] = elevate;
                        } 
                        if (elevate < heights[i,j] && !addMode) {
                            heights[i,j] = elevate;
                        }
                    }
                }
                
                // heights[0,0] += Mathf.Abs(transform.position.y - heights[0,0]) / TD.size.y;
                // TD.SetHeightsDelayLOD(mapX- intR/2, mapY- intR/2, heights);
                TD.SetHeights(mapX- intR/2, mapY- intR/2, heights);
                TD.SyncHeightmap();
            }
        }
    }
}

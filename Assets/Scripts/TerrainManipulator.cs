using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; 

public class TerrainManipulator : MonoBehaviour
{

    
    [SerializeField] private float brushRadius;
    [SerializeField] private float radius;
    [SerializeField] private float stddev;
    [SerializeField] private float n;
    [SerializeField] private bool gaussian;
    [SerializeField] private bool custom;
    [SerializeField] private bool addMode;
    private TextMeshProUGUI _text;

    // Start is called before the first frame update
    void Start()
    {   
        custom = false;
        gaussian = true;
        _text = GetComponent<TextMeshProUGUI>();
        UpdateText();
        
    }

    public void NormalBrush() {
        gaussian = false;
        custom = false;
    }

    public void CustomBrush() {
        custom = true;
        gaussian = false;
    }

    public void UpdateText() 
    {
        // _text.text = radius.ToString();
    }

    public void AddRadius() 
    {
        radius += 1f;
        UpdateText();
    }

    public void SubtractRadius() 
    {
        radius -= 1f;
        UpdateText();
    }

    public void LargeBrush() 
    {
        radius = 9f;
        UpdateText();
    }

    public void SmallBrush() 
    {
        radius = 6f;
        UpdateText();
    }

    public void AddTerrain() 
    {
        addMode = true;
    }

    public void RemoveTerrain() 
    {
        addMode = false;
    }

    public void GaussianMode()
    {
        gaussian = true;
        custom = false;
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
                        float elevate = transform.position.y/TD.size.y; 
                        if (gaussian) {
                            elevate = (1 / (Mathf.Pow(stddev, n) * Mathf.Pow(2*Mathf.PI, n/2))) * Mathf.Exp(-(radI*radI + radJ*radJ) / (2*stddev*stddev)) * Mathf.Abs(transform.position.y - heights[i,j]) / TD.size.y;
                        }
                        if (custom) {
                            // spikey thing?
                            // elevate = transform.position.y*(intR/(i/2f+1f)*(j/2f+1f))/TD.size.y;
                            if (radJ == 0 || radI == 0) {
                                elevate = transform.position.y/TD.size.y;
                            } else {
                                elevate = transform.position.y*(intR/radJ)*(intR/radI)/TD.size.y;
                            }
                        }
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

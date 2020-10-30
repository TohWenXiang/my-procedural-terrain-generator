using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public int octavesCount;
    public float lacunarity;
    public float persistance;

    public bool autoUpdate;

    private MapDisplay display = null;

    public void Awake()
    {
        GenerateMap();
    }   

    public void GenerateMap()
    {
        if (display == null)
        {
            display = FindObjectOfType<MapDisplay>();
        }

        float[,] noiseMap = Noise.PerlinNoise2D(mapWidth, mapHeight, noiseScale, octavesCount, lacunarity, persistance);
        
        display.DrawNoiseMap(noiseMap);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    private MapDisplay display;

    public void Awake()
    {
        display = GetComponent<MapDisplay>();
    }

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.PerlinNoise2D(mapWidth, mapHeight, noiseScale);

        if (!display)
        {
            display = FindObjectOfType<MapDisplay>();
        }

        display.DrawNoiseMap(noiseMap);
    }
}

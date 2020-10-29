using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int mapWidth;
    public int mapHeight;
    public float noiseScale;

    public void GenerateMap()
    {
        float[,] perlinNoiseMap = PerlinNoise.GeneratePerlinNoiseMap(mapWidth, mapHeight, noiseScale);
    }
}

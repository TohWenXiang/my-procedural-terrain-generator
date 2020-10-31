using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//completed until episode 2 3:39
public static class Noise
{
    public static float[,] PerlinNoise2D(int width, int height, int seed, float scale, int octavesCount, float lacunarity, float persistence, Vector2 manualOffset)
    {
        int rngRange = 100000;
        System.Random rng = new System.Random(seed);
        float[,] perlinNoiseMap = new float[width, height];
        Vector2[] octaveOffsets = new Vector2[octavesCount];

        for (int i = 0; i < octavesCount; i++)
        {
            float offsetX = rng.Next(-rngRange, rngRange) + manualOffset.x;
            float offsetY = rng.Next(-rngRange, rngRange) + manualOffset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        //prevent divide by zero error
        scale = scale <= 0 ? Mathf.Epsilon : scale;

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = width / 2f;
        float halfHeight = height / 2f;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float totalNoiseHeight = 0;

                for (int o = 0; o < octavesCount; o++)
                {
                    //convert x to non integral value by dividing it by scale
                    //sampling at integral value will return the same result
                    //frequency affect the spread of sampling points
                    //offset sampling point for each octaves to a random position to get a unique noise map
                    //modified to zoom into the center when scale is increase
                    float samplingPointX = (x - halfWidth) / scale * frequency + octaveOffsets[o].x;
                    float samplingPointY = (y - halfHeight) / scale * frequency + octaveOffsets[o].y;

                    //mapping the range from 0.. 1 to -1..1
                    float perlinNoiseValue = Mathf.PerlinNoise(samplingPointX, samplingPointY) * 2 - 1;

                    //total noise height consist of the perlin noise value of each subsequent octaves
                    //amplitude affect noise height
                    totalNoiseHeight += perlinNoiseValue * amplitude;

                    //amplitude will decrease for each subsequent octave
                    amplitude *= persistence;
                    //and frequency will increase for each subsequent octave
                    frequency *= lacunarity; 
                }

                //find the hightes and lowest noise height
                if (totalNoiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = totalNoiseHeight;
                }
                else if (totalNoiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = totalNoiseHeight;
                }
                perlinNoiseMap[x, y] = totalNoiseHeight;
            }
        }

        //normalize value back to 0.. 1
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                perlinNoiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, perlinNoiseMap[x, y]);
            }
        }

        return perlinNoiseMap;
    }
}

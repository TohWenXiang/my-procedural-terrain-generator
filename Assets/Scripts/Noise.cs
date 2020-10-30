using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//completed until episode 2 3:39
public static class Noise
{
    public static float[,] PerlinNoise2D(int width, int height, float scale, int octavesCount, float lacunarity, float persistence)
    {
        float[,] perlinNoiseMap = new float[width, height];

        //prevent divide by zero error by clamping lower bounds to epsilon
        scale = scale <= 0 ? Mathf.Epsilon : scale;

        //keep track of highest and lowest noise height
        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                //Noise height current location
                float totalNoiseHeight = 0;

                for (int o = 0; o < octavesCount; o++)
                {
                    //convert x to non integral value by dividing it by scale
                    //sampling at integral value will return the same result
                    //frequency affect the spread of sampling points
                    float samplingPointX = x / scale * frequency;
                    float samplingPointY = y / scale * frequency;

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
                maxNoiseHeight = totalNoiseHeight > maxNoiseHeight ? totalNoiseHeight : maxNoiseHeight;
                minNoiseHeight = totalNoiseHeight < minNoiseHeight ? totalNoiseHeight : minNoiseHeight;
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

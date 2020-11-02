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

        /*
         *  This is to get a uniquely random noise map that is controlled by a seed and a manual offset         
         */
        for (int i = 0; i < octavesCount; i++)
        {
            float offsetX = rng.Next(-rngRange, rngRange) + manualOffset.x;
            float offsetY = rng.Next(-rngRange, rngRange) + manualOffset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        //protect against divide by zero error
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

                /*
                 * For every subsequent octaves, 
                 * amplitude will be decrease due to persistance
                 * frequency will be increase due to lacunarity
                 */
                for (int o = 0; o < octavesCount; o++)
                {
                    /*
                     *  Sampling perlin noise at every integral value will result in the same value, 
                     *  thus x is divided by scale to convert it into an non-integral value.
                     *  
                     *  Before scaling, offset by half width/height to zoom into the center.
                     *  
                     *  Frequency controls the spread of sampling points.
                     */
                    float samplingPointX = (x - halfWidth) / scale * frequency + octaveOffsets[o].x;
                    float samplingPointY = (y - halfHeight) / scale * frequency + octaveOffsets[o].y;

                    float perlinNoiseValue = Mathf.PerlinNoise(samplingPointX, samplingPointY);
                    float remappedPerlinNoiseValue = perlinNoiseValue * 2 - 1;

                    float currentOctaveNoiseHeight = remappedPerlinNoiseValue * amplitude;
                    totalNoiseHeight += currentOctaveNoiseHeight;

                    amplitude *= persistence;
                    frequency *= lacunarity;
                }

                //keep track of the highest and lowest noise height
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

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float normalizedPerlinNoiseValue = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, perlinNoiseMap[x, y]);
                perlinNoiseMap[x, y] = normalizedPerlinNoiseValue;
            }
        }

        return perlinNoiseMap;
    }
}

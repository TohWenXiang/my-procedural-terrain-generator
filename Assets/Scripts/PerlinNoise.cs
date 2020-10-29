using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//completed until episode 2 3:39
public static class PerlinNoise
{
    public static float[,] GeneratePerlinNoiseMap(int width, int height, float scale)
    {
        float[,] perlinNoiseMap = new float[width, height];

        //prevent divide by zero error by clamping lower bounds to epsilon
        if (scale <= 0)
        {
            scale = Mathf.Epsilon;
        }

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                //convert x to non integral value by dividing it by scale
                //sampling at integral value will return the same result
                float samplingPointX = x / scale;
                float samplingPointY = y / scale;

                float perlinNoiseValue = Mathf.PerlinNoise(samplingPointX, samplingPointY);
                perlinNoiseMap[x, y] = perlinNoiseValue;
            }
        }

        return perlinNoiseMap;
    }
}

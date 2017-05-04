using UnityEngine;
using System.Collections;

public static class Noise
{

    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistence, float lacunarity, Vector2 offset)
    {

        //2d array of floats dependent on width and height of map
        float[,] noiseMap = new float[mapWidth, mapHeight];

        //system random for the seed int
        System.Random prng = new System.Random(seed);

        //setting the offsets for the octaves so they sample from difference posisions each time
        Vector2[] octaveOffsets = new Vector2[octaves];
        for (int i = 0; i < octaves; i++)
        {//the numbers in here are just from what sebastian said in the video - no reason given except from his own personal experimentation
            //the offset.x and .y are from the offset you added up there so you can have some peronal contorl over offset.
            float offsetX = prng.Next(-100000, 100000)+offset.x;
            float offsetY = prng.Next(-100000, 100000)+offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        //you'll be dividing by scale later so lets stop it being 0
        if(scale<=0)
        {
            scale = 0.0001f;
        }

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2f;
        float halfHright = mapHeight / 2f;

        //looping through each map unit
        for (int y = 0 ; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                //creating the variables that will affect the octaves
                //frequency is lacunarity ^ whatever number it should be for that octave - i.e octave 1 has frequency of lacunarity ^0, oc 2 has freq = lac^1 etc
                //amplitude is multiplyed by persistence to make sure it decreases each octave - meaning higher octaves have less influence over the overall Noisemap
                //if you get confused watch the intro vid to the series again as it is explained well - just thought trying to type it to yourself might help get it in your mad head.
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = (x-halfWidth) / scale * frequency + octaveOffsets[i].x;
                    float sampleY = (y-halfHright) / scale * frequency+ octaveOffsets[i].y;

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;
                    //setting the height of the noise
                    noiseHeight += perlinValue * amplitude;

                    //decrementing the amplitude, persistence should be a 0 to 1 number so always makes aplitude smaller
                    amplitude *= persistence;
                    //incrementing frequency, lacunarity should always be >1
                    frequency *= lacunarity;
                }

                //resetting the max and min noise heights each time a new max or min is reached
                if (noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if(noiseHeight<minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;
            }
        }


        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {//normalize the noise map between 0 and 1 - inverse lerp does this
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]);
            }
        }
                return noiseMap;

    }

}

using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

    //enum to decide whether we draw the noisemap or the colourmap
    public enum DrawMode { NoiseMap, Colourmap, Mesh};
    public DrawMode drawMode;

    const int mapChunkSize = 241;
    [Range(0,6)]
    public int levelofDetail;
  
    public float noiseScale;

    public int octaves;
    [Range(0,1)]
    public float persistence;
    public float lacunarity;

    public int seed;
    public Vector2 offset;

    public float meshHeightMultiplier;
    public AnimationCurve meshHeightCurve;
      

    public bool autoUpdate;

    public TerrainType[] regions;

    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapChunkSize, 
            mapChunkSize,seed, noiseScale, octaves,persistence,lacunarity, offset);


        //these for loops apply the colours to the regions

        //first create a 1d array fo colours like we did in the mapDisplay class
        Color[] colourMap = new Color[mapChunkSize * mapChunkSize];
        for (int y = 0; y < mapChunkSize; y++)
        {
            for (int x = 0; x < mapChunkSize; x++)
            {
                float currentHeight = noiseMap[x, y];

                for (int i = 0; i < regions.Length; i++)
                {
                    if(currentHeight<=regions[i].height)
                    {
                        colourMap[y * mapChunkSize + x] = regions[i].colour;
                        break;
                    }
                }
            }
        }

        mapDisplay display = FindObjectOfType<mapDisplay>();
        if (drawMode == DrawMode.NoiseMap)
        {
            display.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        }
        else if(drawMode ==DrawMode.Colourmap)
        {
            display.DrawTexture(TextureGenerator.TextureFromColourMap(colourMap,
            mapChunkSize, mapChunkSize));
        }
        else if( drawMode == DrawMode.Mesh)
        {
            display.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, 
            meshHeightMultiplier, meshHeightCurve, levelofDetail), 
            TextureGenerator.TextureFromColourMap(colourMap, mapChunkSize, mapChunkSize));
        }

    }


    //on volidate is called whenever a variable changes
    void OnValidate()
    {
 
        if (lacunarity < 1)
        {
            lacunarity = 1;
        }
        if (octaves < 0)
        {
            octaves = 0;
        }
        
    }

    //struct for adding colours to regions - serializable so we can get at it in the editor
    [System.Serializable]
    public struct TerrainType
    {
        
        public string name;
        public float height;
        public Color colour;

    }
}

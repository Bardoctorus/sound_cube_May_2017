using UnityEngine;
using System.Collections;

public static class MeshGenerator
{

    public static MeshData GenerateTerrainMesh(float[,] heightMap, 
        float heightMultiplier, AnimationCurve heightCurve, int levelofDetail)
    {
        int width = heightMap.GetLength(0);
        int height = heightMap.GetLength(1);

        float topleftX = (width - 1) / -2f;
        float topleftZ = (height - 1) / 2f;

        int MeshSimplifactionIncrement = (levelofDetail ==0)?1: levelofDetail * 2;
        int verticesPerLine = (width - 1) / MeshSimplifactionIncrement + 1;

        MeshData meshData = new MeshData(verticesPerLine, verticesPerLine);
        int vertexIndex = 0;

        for (int y = 0; y < height; y+= MeshSimplifactionIncrement)
        {
            for (int x = 0; x < width; x+= MeshSimplifactionIncrement)
            {
                meshData.vertices[vertexIndex] = new Vector3(topleftX + x, 
                heightCurve.Evaluate( heightMap[x, y]) * heightMultiplier, topleftZ - y);

                meshData.uvs[vertexIndex] = 
                    new Vector2(x / (float)width, y / (float)height);

                if (x < width - 1 && y < height - 1)
                {//this looks confusing as fuck but it makes sense - 
                 //if you are at the top left of a square you draw the first 
                 //triangel from the top corner (position:vertexIndex or home), 
                 //down diagonally to the bottom right corner 
                 //(position vertextIndex + width +1 or Along one down one), 
                 //then back up to the top right (Vertexindex plus width OR one along).
                    meshData.AddTriangle(vertexIndex, vertexIndex + verticesPerLine + 1, 
                        vertexIndex + verticesPerLine);
                    meshData.AddTriangle(vertexIndex + verticesPerLine + 1, 
                        vertexIndex, vertexIndex + 1);
                }

                vertexIndex++;

            }
        }
        return meshData;
    }
}

    public class MeshData
{
        public Vector3[] vertices;
        public int[] triangles;
        public Vector2[] uvs;
        int triangleIndex;

        public MeshData(int meshWidth, int meshHeight)
        {
            vertices = new Vector3[meshWidth * meshHeight];
            triangles = new int[(meshWidth - 1) * (meshHeight - 1) * 6];
            uvs = new Vector2[meshWidth * meshHeight];

        }



        public void AddTriangle(int a, int b, int c)
        {
            triangles[triangleIndex] = a;
            triangles[triangleIndex + 1] = b;
            triangles[triangleIndex + 2] = c;
            triangleIndex += 3;
        }

        public Mesh CreateMesh()
        {
            Mesh mesh = new Mesh();
            mesh.vertices = vertices;
             mesh.triangles = triangles;
            mesh.uv = uvs;
            mesh.RecalculateNormals();
            return mesh;
        }

}



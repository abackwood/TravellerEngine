using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (MeshFilter))]
[RequireComponent (typeof (MeshCollider))]
[RequireComponent (typeof (MeshRenderer))]
public class MapChunk3D : MonoBehaviour {
	public TileMap3D map;

	float[,] heightData;
	public float GetHeight(int x, int z) {
		return heightData[x,z];
	}

	MeshRenderer meshRenderer;

	public void BuildMesh(float[,] newHeightData) {
		//Save new height data
		heightData = newHeightData;

		//Get references to relevant components & calculate relevant values
		MeshFilter filter = GetComponent<MeshFilter>();
		MeshCollider collider = GetComponent<MeshCollider>();

		int chunkSizeX = map.chunkSizeX;
		int chunkSizeZ = map.chunkSizeZ;
		float tileSizeX = map.tileSizeX;
		float tileSizeZ = map.tileSizeZ;
		
		int numOfTiles = chunkSizeX * chunkSizeZ;
		int numOfTriangles = numOfTiles * 2;
		
		int vertexSizeX = chunkSizeX + 1;
		int vertexSizeZ = chunkSizeZ + 1;
		int numOfVertices = vertexSizeX * vertexSizeZ;

		if(heightData.GetLength(0) != vertexSizeX || heightData.GetLength(1) != vertexSizeZ) {
			throw new System.ArgumentException("HeightData is not the right size");
		}
		
		float uvFractionX = 1.0f / vertexSizeX;
		float uvFractionY = 1.0f / vertexSizeZ;
		
		//Construct vertex data
		Vector3[] vertices = new Vector3[numOfVertices];
		Vector3[] normals = new Vector3[numOfVertices];
		Vector2[] uvs = new Vector2[numOfVertices];
		int[] triangles = new int[numOfTriangles*3];
		
		for(int x = 0 ; x < vertexSizeX ; x++) {
			for(int z = 0 ; z < vertexSizeZ ; z++) {
				int idx = x * vertexSizeZ + z;
				float y = heightData[x,z];
				vertices[idx] = new Vector3(x*tileSizeX, y, z*tileSizeZ);
				uvs[idx]      = new Vector2(x*uvFractionX, z*uvFractionY);

				//The normal is the average of the cross-products of vectors to the four neighbouring points
				float heightEast  = x == vertexSizeX - 1 ? y : heightData[x+1,z];
				float heightWest  = x == 0               ? y : heightData[x-1,z];
				float heightNorth = z == vertexSizeZ - 1 ? y : heightData[x,z+1];
				float heightSouth = z == 0               ? y : heightData[x,z-1];

				Vector3 east =  new Vector3(1,heightEast,0);
				Vector3 west =  new Vector3(-1,heightWest,0);
				Vector3 north = new Vector3(0,heightNorth,1);
				Vector3 south = new Vector3(0,heightSouth,-1);

				Vector3 normal1 = Vector3.Cross(west,north);
				Vector3 normal2 = Vector3.Cross(east,south);
				normals[idx] = (normal1 + normal2) / 2;
			}
		}
		
		for(int x = 0 ; x < chunkSizeX ; x++) {
			for(int z = 0 ; z < chunkSizeZ ; z++) {
				int tile_idx = x * chunkSizeZ + z;
				int triangle_idx = tile_idx * 6;
				int offset = x * vertexSizeZ + z;
				
				triangles[triangle_idx + 0] = offset;
				triangles[triangle_idx + 1] = offset + vertexSizeX + 1;
				triangles[triangle_idx + 2] = offset + vertexSizeX;
				
				triangles[triangle_idx + 3] = offset;
				triangles[triangle_idx + 4] = offset + 1;
				triangles[triangle_idx + 5] = offset + vertexSizeX + 1;
			}
		}
		
		//Assign values to mesh
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.normals = normals;
		mesh.uv = uvs;
		mesh.triangles = triangles;
		
		//Assign mesh to components
		filter.mesh = mesh;
		collider.sharedMesh = mesh;
	}

	public void SetTexture(Texture2D texture) {
		meshRenderer.material.SetTexture("_MainTex",texture);
	}

	void Awake() {
		meshRenderer = GetComponent<MeshRenderer>();
	}
}

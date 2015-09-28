using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkedTileMap : MonoBehaviour {
	public MapChunk chunkTemplate;
	public float tileSizeX, tileSizeZ;

	Dictionary<IntVector2,MapChunk> chunks;
	public MapChunk Chunk(int x, int z) {
		IntVector2 c = new IntVector2(x,z);
		return chunks.ContainsKey(c) ? chunks[c] : null;
    }

	public void AddChunk(int chunkSizeX, int chunkSizeZ, int x, int y) {
		if(Chunk(x,y) != null) {
			throw new System.ArgumentException("A chunk at this position already exists.");
		}
		else {
			MapChunk chunk = GameObject.Instantiate(chunkTemplate);
			chunk.name = "Chunk(" + x + "," + y + ")";
			chunk.transform.SetParent(transform);
			chunk.transform.localPosition = new Vector3(x*chunkSizeX*tileSizeX, 0, y*chunkSizeZ*tileSizeZ);
			chunk.transform.localScale = Vector3.one;
			chunk.handler = this;
			BuildMesh(chunk,chunkSizeX,chunkSizeZ);
            
			chunks[new IntVector2(x,y)] = chunk;
        }
	}

	void BuildMesh(MapChunk chunk, int sizeX, int sizeZ) {
		//Get references to relevant components & calculate relevant values
		MeshFilter filter = chunk.GetComponent<MeshFilter>();
		MeshCollider collider = chunk.GetComponent<MeshCollider>();
		MeshRenderer renderer = chunk.GetComponent<MeshRenderer>();
		
		int numOfTiles = sizeX * sizeZ;
		int numOfTriangles = numOfTiles * 2;
		
		int vertexSizeX = sizeX + 1;
		int vertexSizeZ = sizeZ + 1;
		int numOfVertices = vertexSizeX * vertexSizeZ;
		
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
				vertices[idx] = new Vector3(x*tileSizeX, 0, z*tileSizeZ);
				normals[idx]  = Vector3.up;
				uvs[idx]      = new Vector2(x*uvFractionX, z*uvFractionY);
			}
		}
		
		for(int x = 0 ; x < sizeX ; x++) {
			for(int z = 0 ; z < sizeZ ; z++) {
				int tile_idx = x * sizeZ + z;
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

	// Use this for initialization
	void Start () {
		chunks = new Dictionary<IntVector2,MapChunk>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

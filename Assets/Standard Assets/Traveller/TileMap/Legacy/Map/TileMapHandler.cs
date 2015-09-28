using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileMapHandler : MonoBehaviour {
	/*
	public Chunk chunkTemplate;
	public float tileSizeX, tileSizeZ;

	Dictionary<Coordinate,GameObject> chunks;
	public GameObject Chunk(int x, int z) {
		Coordinate c = new Coordinate(x,z);
		return chunks.ContainsKey(c) ? chunks[c] : null;
	}

	public void GenerateMap(int chunksX, int chunksZ, int chunkSizeX, int chunkSizeZ, int numOfBiomePoints) {
		//Clear chunks
		foreach(GameObject chunk in chunks.Values) {
			GameObject.Destroy(chunk);
		}
		chunks = new Dictionary<Coordinate, GameObject>();

		//Clear biome points
		WorldData data = GameObject.Find("WorldData").GetComponent<WorldData>();
		foreach(BiomePoint bp in data.GetComponentsInChildren<BiomePoint>()) {
			GameObject.Destroy(bp.gameObject);
		}

		float rangeX = chunksX*chunkSizeX*tileSizeX;
		float rangeY = chunksZ*chunkSizeZ*tileSizeZ;
		GenerateBiomePoints(data, numOfBiomePoints, rangeX, rangeY);
		GenerateChunks(chunksX,chunksZ,chunkSizeX,chunkSizeZ);
		CalculateTextures(chunkSizeX*tileSizeX, chunkSizeZ*tileSizeZ);
	}

	public void AddChunk(int chunkSizeX, int chunkSizeZ, int x, int y) {
		if(Chunk(x,y) != null) {
			throw new System.ArgumentException("A chunk at this position already exists.");
		}
		else {
			Chunk chunk = GameObject.Instantiate(chunkTemplate);
			chunk.name = "Chunk(" + x + "," + y + ")";
			chunk.transform.SetParent(transform);
			chunk.transform.localPosition = new Vector3(x*chunkSizeX*tileSizeX, 0, y*chunkSizeZ*tileSizeZ);
			chunk.transform.localScale = Vector3.one;
			chunk.handler = this;
			BuildMesh(chunk,chunkSizeX,chunkSizeZ);

			chunks[new Coordinate(x,y)] = chunk.gameObject;
		}
	}

	void BuildMesh(Chunk chunk, int sizeX, int sizeZ) {
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
		chunks = new Dictionary<Coordinate, GameObject>();
	}

	struct Coordinate {
		public int i,j;
		public override int GetHashCode () {
			return i*971 + j*937;
		}
		public override bool Equals (object obj) {
			if(!obj.GetType().Equals(typeof(Coordinate))) {
				return false;
			}
			else {
				Coordinate c = (Coordinate)obj;
				return i == c.i && j == c.j;
			}
		}
		public Coordinate(int i, int j) {
			this.i = i;
			this.j = j;
		}
	}
	*/
}

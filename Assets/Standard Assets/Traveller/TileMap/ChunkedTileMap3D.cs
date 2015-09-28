using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkedTileMap3D : MonoBehaviour {
	public MapChunk3D chunkTemplate;
	public int chunkSizeX, chunkSizeZ;
	public float tileSizeX, tileSizeZ;

	Dictionary<IntVector2,MapChunk3D> chunks;
	public MapChunk3D Chunk(int x, int z) {
		IntVector2 c = new IntVector2(x,z);
		return chunks.ContainsKey(c) ? chunks[c] : null;
    }

	public void AddChunk(int x, int y, float[,] heightData) {
		if(Chunk(x,y) != null) {
			throw new System.ArgumentException("A chunk at this position already exists.");
		}
		else {
			MapChunk3D chunk = GameObject.Instantiate(chunkTemplate);
			chunk.name = "Chunk(" + x + "," + y + ")";
			chunk.transform.SetParent(transform);
			chunk.transform.localPosition = new Vector3(x*chunkSizeX*tileSizeX, 0, y*chunkSizeZ*tileSizeZ);
			chunk.transform.localRotation = Quaternion.identity;
			chunk.transform.localScale = Vector3.one;
			chunk.map = this;
			chunk.BuildMesh(heightData);
            
			chunks[new IntVector2(x,y)] = chunk;
        }
	}
	public void RemoveChunk(int x, int y) {
		IntVector2 key = new IntVector2(x,y);
		GameObject.Destroy(chunks[key].gameObject);
		chunks[key] = null;
	}

	void Awake() {
		chunks = new Dictionary<IntVector2,MapChunk3D>();
	}
}

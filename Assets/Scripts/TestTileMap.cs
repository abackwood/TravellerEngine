using UnityEngine;
using System.Collections;

public class TestTileMap : TileMap2D {
	public GameObject spawnee;

	protected override void OnMouseDown (Vector3 point) {
		int chunk_x = (int)(point.x / (chunkSizeX * tileSizeX));
		int chunk_y = (int)(point.y / (chunkSizeY * tileSizeY));
		Chunk(chunk_x,chunk_y).GetComponent<MeshRenderer>().material.color = Color.red;
	}
}

using UnityEngine;
using System.Collections;

public class MapTester : MonoBehaviour {

	// Use this for initialization
	void Start () {
		ChunkedTileMap3D map = GetComponent<ChunkedTileMap3D>();

		for(int i = 0 ; i < 2 ; i++) {
			for(int j = 0 ; j < 2 ; j++) {
				float[,] heightData = new float[map.chunkSizeX + 1, map.chunkSizeZ + 1];
				Texture2D texture = new Texture2D(map.chunkSizeX + 1,map.chunkSizeZ + 1);
				
				for(int x = 0 ; x < heightData.GetLength(0) ; x++) {
					for(int z = 0 ; z < heightData.GetLength(1) ; z++) {
						heightData[x,z] = UnityEngine.Random.Range (-.25f, .25f);
						texture.SetPixel(x,z,new Color(0.5f + 2*heightData[x,z],0,0));
					}
				}
				texture.Apply();

				map.AddChunk(i,j,heightData);
				map.Chunk (i,j).SetTexture(texture);
			}
		}
	}
}

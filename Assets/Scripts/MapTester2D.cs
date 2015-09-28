using UnityEngine;
using System.Collections;

public class MapTester2D : MonoBehaviour {

	// Use this for initialization
	void Start () {
		TileMap2D map = GetComponent<TileMap2D>();
		
		for(int i = 0 ; i < 2 ; i++) {
			for(int j = 0 ; j < 1 ; j++) {
				Texture2D texture = new Texture2D(map.chunkSizeX,map.chunkSizeY);
				
				for(int x = 0 ; x < texture.width ; x++) {
					for(int y = 0 ; y < texture.height ; y++) {
						texture.SetPixel(x,y,new Color(UnityEngine.Random.value,UnityEngine.Random.value,UnityEngine.Random.value));
					}
				}
				texture.filterMode = FilterMode.Point;
				texture.Apply();
				
				map.AddChunk(i,j);
				map.Chunk (i,j).SetTexture(texture);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

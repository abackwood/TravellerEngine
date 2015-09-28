using UnityEngine;
using System.Collections;

[RequireComponent (typeof (MeshFilter))]
[RequireComponent (typeof (MeshCollider))]
[RequireComponent (typeof (MeshRenderer))]
public class MapChunk : MonoBehaviour {
	public ChunkedTileMap handler;

	MeshRenderer meshRenderer;

	/*
	public void SetTexture(float worldSizeX, float worldSizeZ) {
		meshRenderer = GetComponent<MeshRenderer>();

		Texture2D texture = new Texture2D(1024,1024);
		WorldData data = GameObject.Find("WorldData").GetComponent<WorldData>();

		Point2D offset = new Point2D(transform.localPosition.x, transform.localPosition.z);
		float stepX = worldSizeX / texture.width;
		float stepZ = worldSizeZ / texture.height;
		for(int x = 0 ; x < texture.width ; x++) {
			for(int y = 0 ; y < texture.height ; y++) {
				Point2D localPosition = offset + new Point2D(stepX * x, stepZ * y);
				int biome = data.BiomeAt(localPosition);

				try {
					Color color = data.textures[biome].GetPixel(x,y);
					texture.SetPixel(x,y,color);
				}
				catch(System.NullReferenceException e) {
					Debug.Log (data + ", " + biome + ", (" + x + "," + y + ")");
				}
			}
		}
		texture.Apply ();

		meshRenderer.material.SetTexture("_MainTex",texture);
	}
	*/
}

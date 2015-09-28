using UnityEngine;
using System.Collections;

[RequireComponent (typeof (MeshFilter))]
[RequireComponent (typeof (MeshCollider))]
[RequireComponent (typeof (MeshRenderer))]
public class MapChunk2D : MonoBehaviour {
	public TileMap2D map;

	MeshRenderer meshRenderer;

	public void BuildMesh() {
		MeshFilter filter = GetComponent<MeshFilter>();
		MeshCollider collider = GetComponent<MeshCollider>();

		//Calculate relevant values
		float width = map.chunkSizeX * map.tileSizeX;
		float height = map.chunkSizeY * map.tileSizeY;

		//Construct vertex data
		Vector3[] vertices = new Vector3[4];
		Vector3[] normals = new Vector3[4];
		Vector2[] uvs = new Vector2[4];
		int[] triangles = new int[6];

		vertices[0] = new Vector3(0,0,0);
		normals[0] = Vector3.back;
		uvs[0] = new Vector2(0,0);

		vertices[1] = new Vector3(0,height,0);
		normals[1] = Vector3.back;
		uvs[1] = new Vector2(0,1);

		vertices[2] = new Vector3(width,0,0);
		normals[2] = Vector3.back;
		uvs[2] = new Vector2(1,0);

		vertices[3] = new Vector3(width,height,0);
		normals[3] = Vector3.back;
		uvs[3] = new Vector2(1,1);

		triangles[0] = 0;
		triangles[1] = 3;
		triangles[2] = 2;
		triangles[3] = 0;
		triangles[4] = 1;
		triangles[5] = 3;

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

	// Use this for initialization
	void Awake () {
		meshRenderer = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

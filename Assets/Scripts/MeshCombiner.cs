using UnityEngine;
using System.Collections.Generic;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshCombiner : MonoBehaviour {

	private void Start(){
		Combine();
		Debug.Log("Combined");
	}

	// Use this for initialization
	public void Combine() {
		foreach(Transform child in transform){
			child.position += transform.position;
		}
		transform.position = Vector3.zero;
		transform.rotation = Quaternion.identity;
		
		var meshFilters = GetComponentsInChildren<MeshFilter>();
		CombineInstance[] combine = new CombineInstance[meshFilters.Length-1];
		int index = 0;
		for(var i = 0; i < meshFilters.Length; i++){
			if(meshFilters[i].sharedMesh == null) continue;
			combine[index].mesh = meshFilters[i].sharedMesh;
			combine[index++].transform = meshFilters[i].transform.localToWorldMatrix;
			meshFilters[i].renderer.enabled = false;
		}
		Mesh meshObject = GetComponent<MeshFilter>().mesh = new Mesh();
		meshObject.CombineMeshes(combine,true,true);
		//Weld(meshObject, 0.001f);
		renderer.sharedMaterial = meshFilters[1].renderer.sharedMaterial;
		
		Debug.Log("Combination complete");
	}
	
	private void Weld(Mesh mesh, float threshold){
		Vector3[] verts = mesh.vertices;
		
		List<Vector3> newVerts = new List<Vector3>();
		List<Vector2> newUVs = new List<Vector2>();
		
		int k = 0;
		
		foreach(Vector3 vert in verts){
			foreach(Vector3 newVert in newVerts){
				if(Vector3.Distance(newVert, vert) <= threshold){
					goto skipToNext;
				}
			}
			newVerts.Add(vert);
			newUVs.Add(mesh.uv[k]);
		skipToNext:;
			++k;
		}
		
		int[] tris = mesh.triangles;
		for(int i = 0; i< tris.Length; ++i){
			for(int j = 0; j < newVerts.Count; ++j){
				if(Vector3.Distance(newVerts[j], verts[ tris[i] ]) <= threshold){
					tris[i] = j;
					break;
				}
			}
		}
		
		mesh.Clear();
		mesh.vertices = newVerts.ToArray();
		mesh.triangles = tris;
		mesh.uv = newUVs.ToArray();
		mesh.RecalculateBounds();
	}
}

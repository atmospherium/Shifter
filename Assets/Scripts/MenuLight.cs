using UnityEngine;
using System.Collections;

public class MenuLight : MonoBehaviour {

	Vector3 lightCenter;
	Vector3 destination;

	float intensityDestination = 1;

	float xRange = 0.5f;
	float yRange = 0.5f;
	float zRange = 0.0f;

	Light light;

	// Use this for initialization
	void Start () {
		lightCenter = transform.position;
		destination = lightCenter;
		light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(destination,transform.position)<0.05){
			destination = new Vector3(Random.Range(lightCenter.x+xRange,lightCenter.x-xRange),
			                          Random.Range(lightCenter.z+xRange,lightCenter.y-yRange),
			                          Random.Range(lightCenter.z+xRange,lightCenter.z-zRange));
		}
		transform.position = Vector3.Lerp(transform.position, destination, 0.01f);
		if(Random.Range(0f,1f)>0.8f){
			intensityDestination = Random.Range(1.75f,2.15f);
		}
		light.intensity = Mathf.Lerp(light.intensity, intensityDestination*1.5f, 0.5f);
	}
}

using UnityEngine;
using System.Collections;

public class DangerBlock : MonoBehaviour {

	private GameObject camera;

	private Light lightObject;
	private float lightRange;
	
	public LensFlare flareObject;
	private float flareBrightness;

	// Use this for initialization
	void Start () {

		lightObject = transform.FindChild("PlatformLight").light;
		lightRange = lightObject.range;
		
		flareObject = lightObject.GetComponentInChildren<LensFlare>();
		flareBrightness = flareObject.brightness;
	}
	
	// Update is called once per frame
	void Update () {
		lightObject.range = Mathf.Lerp(lightObject.range,lightRange,3*Time.deltaTime);
		flareObject.brightness = Mathf.Lerp(flareObject.brightness,flareBrightness,3*Time.deltaTime);
	}
	
	public void Burst(){
		CameraScript camScript = GameObject.Find("CameraRig").GetComponentInChildren<CameraScript>();
		audio.Play();
		camScript.Shake();
		lightObject.range = 10;
		flareObject.brightness = 1f;
	}
}

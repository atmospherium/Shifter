using UnityEngine;
using System.Collections;

public class MenuAnimate : MonoBehaviour {

	Color colorTarget;

	GameObject mainCamera;
	int qSamples = 4096;
	private float[] samples;

	// Use this for initialization
	void Start () {
		mainCamera = GameObject.Find("Main Camera");
		samples = new float[qSamples];
		colorTarget = renderer.material.GetColor("_TintColor");
	}
	
	// Update is called once per frame
	void Update () {
		Color currentColor = renderer.material.GetColor("_TintColor");

		float floorRange = 200;
		float ceilRange = 255;

		float vol = GetRMS(0) + GetRMS(1);

		float blur = (vol/2)+0.5f;
		float alpha = (vol*vol*vol)+0.5f;
		colorTarget = new Color(blur,blur,blur,alpha);
		renderer.material.SetColor("_TintColor",colorTarget);

	}

	float GetRMS(int channel){
		AudioListener.GetOutputData(samples,channel);
		float sum = 0;
		for(var i=0; i<qSamples; i++){
			sum+= samples[i]*samples[i];
		}
		return Mathf.Sqrt(sum/qSamples);
	}
}

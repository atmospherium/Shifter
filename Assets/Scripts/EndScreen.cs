using UnityEngine;
using System.Collections;

public class EndScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
		audio.timeSamples = 1410000;
		audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (audio.timeSamples);
	}
}

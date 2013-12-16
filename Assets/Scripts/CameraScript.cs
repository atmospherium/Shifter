using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {

	private Transform player;
	private float shakeLength = 2f;

	Transform mainCamera;

	Quaternion defaultRotation;
	Vector3 defaultPosition;
	float shakeIntensity = .04f;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").transform;
		mainCamera = transform.FindChild("Main Camera");
		defaultRotation = mainCamera.transform.localRotation;
		defaultPosition = mainCamera.transform.localPosition;
	}
	
	// Update is called once per frame
	void LateUpdate () {
		transform.position = Vector3.Lerp(transform.position, new Vector3(player.position.x,player.position.y,transform.position.z), 0.04f);
	}

	public void Shake(){
		StartCoroutine("Shaker");
	}

	IEnumerator Shaker(){
		float shakeCurrent = 0f;
		while(shakeCurrent<shakeLength){
			float differential = shakeLength-shakeCurrent;
			mainCamera.transform.localPosition = defaultPosition + Random.insideUnitSphere * shakeIntensity * differential;
			mainCamera.transform.localRotation = new Quaternion(defaultRotation.x + Random.Range(-shakeIntensity, shakeIntensity)*.05f*differential,
			                                    defaultRotation.y + Random.Range(-shakeIntensity, shakeIntensity)*.05f * differential,
			                                    defaultRotation.z + Random.Range(-shakeIntensity, shakeIntensity)*.05f * differential,
			                                    defaultRotation.w + Random.Range(-shakeIntensity, shakeIntensity)*.05f * differential);
			Debug.Log ("RUNNING");
			shakeCurrent += Time.deltaTime;
			yield return null;
		}
		mainCamera.transform.localRotation = defaultRotation;
		mainCamera.transform.localPosition = defaultPosition;
	}
}

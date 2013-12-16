using UnityEngine;
using System.Collections;

public class CreateBlock : MonoBehaviour {

	public GameObject movementPlatform;

	private Light lightObject;
	private float lightRange;

	public bool[] sampleEnable = new bool[7];

	public Dir childDirection;
	public float childSpeed = 3f;

	public LensFlare flareObject;
	private float flareBrightness;

	// Use this for initialization
	void Start () {
		lightObject = transform.FindChild("PlatformLight").light;
		lightRange = lightObject.range;
		/*for(var i = 0; i < spawnBeat.Length; i++){
			InvokeRepeating("Create", (4f/16)*spawnBeat[i], 4f);
		}*/

		if(sampleEnable[0]){
			InvokeRepeating("Create", 0.01f, 4f);
			InvokeRepeating("Create", 1.25f, 4f);
			InvokeRepeating("Create", 1.75f, 4f);
			InvokeRepeating("Create", 2.00f, 4f);
		}
		
		if(sampleEnable[1]){
			InvokeRepeating("Create", 3.75f, 4f);
		}
		
		if(sampleEnable[2]){
			InvokeRepeating("Create", 2.75f, 4f);
			InvokeRepeating("Create", 3.25f, 4f);
		}
		
		if(sampleEnable[3]){
			InvokeRepeating("Create", 0.25f, 4f);
			InvokeRepeating("Create", 0.75f, 4f);
			InvokeRepeating("Create", 2.25f, 4f);
			
		}
		
		if(sampleEnable[4]){
			InvokeRepeating("Create", 1.50f, 4f);
			InvokeRepeating("Create", 2.50f, 4f);
		}
		
		if(sampleEnable[5]){
			InvokeRepeating("Create", 0.50f, 4f);
			InvokeRepeating("Create", 3.50f, 4f);
		}
		
		if(sampleEnable[6]){
			InvokeRepeating("Create", 1.00f, 4f);
			InvokeRepeating("Create", 3.00f, 4f);
		}

		flareObject = lightObject.GetComponentInChildren<LensFlare>();
		flareBrightness = flareObject.brightness;
	}
	
	// Update is called once per frame
	void Update () {
		lightObject.range = Mathf.Lerp(lightObject.range,lightRange,3*Time.deltaTime);
		flareObject.brightness = Mathf.Lerp(flareObject.brightness,flareBrightness,3*Time.deltaTime);
	}

	void Create(){

		GameObject blocku = Instantiate(movementPlatform,transform.position,Quaternion.identity) as GameObject;
		MovementPlatform movementScriptu = blocku.GetComponent<MovementPlatform>();
		movementScriptu.platformDir = Dir.up;
		movementScriptu.Speed = childSpeed;
		blocku.transform.parent = transform;

		GameObject blockd = Instantiate(movementPlatform,transform.position,Quaternion.identity) as GameObject;
		MovementPlatform movementScriptd = blockd.GetComponent<MovementPlatform>();
		movementScriptd.platformDir = Dir.down;
		movementScriptd.Speed = childSpeed;
		blockd.transform.parent = transform;

		GameObject blockr = Instantiate(movementPlatform,transform.position,Quaternion.identity) as GameObject;
		MovementPlatform movementScriptr = blockr.GetComponent<MovementPlatform>();
		movementScriptr.platformDir = Dir.right;
		movementScriptr.Speed = childSpeed;
		blockr.transform.parent = transform;

		GameObject blockl = Instantiate(movementPlatform,transform.position,Quaternion.identity) as GameObject;
		MovementPlatform movementScriptl = blockl.GetComponent<MovementPlatform>();
		movementScriptl.platformDir = Dir.left;
		movementScriptl.Speed = childSpeed;
		blockl.transform.parent = transform;

		lightObject.range = 10;
		flareObject.brightness = 2.5f;
	}
}

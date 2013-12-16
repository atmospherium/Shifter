using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	GameController gameController;

	Transform animTarget;

	public Vector3 checkPoint;

	public Dir playerDir = Dir.none;
	public Light lightObject;
	private float lightRange;

	public LensFlare flareObject;
	private float flareBrightness;

	public bool moving = false;

	// Use this for initialization
	void Start () {

		checkPoint = transform.position;

		animTarget = transform.FindChild("Object");
		lightObject = transform.FindChild("PlayerLight").light;
		lightRange = lightObject.range;

		flareObject = lightObject.GetComponentInChildren<LensFlare>();
		flareBrightness = flareObject.brightness;
	}

	public void ResetPlayer(){
		transform.position = checkPoint;
		
		GameObject textHolder = transform.FindChild("Text").gameObject;
		textHolder.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown (KeyCode.R)){
			Application.LoadLevel(Application.loadedLevel);
		}

		lightObject.range = Mathf.Lerp(lightObject.range,lightRange,3*Time.deltaTime);
		flareObject.brightness = Mathf.Lerp(flareObject.brightness,flareBrightness,3*Time.deltaTime);
		if(moving){
			playerDir = Dir.none;
		}else{
			if(playerDir == Dir.none){
				if(Input.GetKeyDown("up")){
					playerDir = Dir.up;
				}else if(Input.GetKeyDown("down")){
					playerDir = Dir.down;
				}else if(Input.GetKeyDown("right")){
					playerDir = Dir.right;
				}else if(Input.GetKeyDown("left")){
					playerDir = Dir.left;
				}
			}
		}

		switch(playerDir){
		case Dir.up:
			animTarget.renderer.material.mainTextureOffset = new Vector2(0.0f,0.25f);
			break;
		case Dir.down:
			animTarget.renderer.material.mainTextureOffset = new Vector2(0.25f,0.25f);
			break;
		case Dir.right:
			animTarget.renderer.material.mainTextureOffset = new Vector2(0.75f,0.25f);
			break;
		case Dir.left:
			animTarget.renderer.material.mainTextureOffset = new Vector2(0.5f,0.25f);
			break;
		default:
			animTarget.renderer.material.mainTextureOffset = new Vector2(0.25f,0.75f);
			break;
		}
	}

	void SecondaryControls(){
		if(Input.GetKeyDown (KeyCode.R)){
			Application.LoadLevel(Application.loadedLevel);
		}
		if(Input.GetKeyDown("f12")){
				Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height,true);
		}
	}
}

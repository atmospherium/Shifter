using UnityEngine;
using System.Collections;

public class MovementPlatform : MonoBehaviour {

	GameObject gameControllerObject;
	GameController gameControllerScript;

	Vector3 tempCheckpoint;

	Vector2 startPos;
	float distanceCeil = 0;
	float distance = 0;
	Collider2D collider;
	GameObject destinationObject;

	bool carryingPlayer = false;

	bool isDanger = false;
	bool isExit = false;

	GameObject player;
	PlayerController playerController;

	float lifeLength = 0;

	private float _speed = 3f;
	public float Speed{
		get{
			return _speed*Time.deltaTime;
		}
		set{
			_speed = value;
		}
	}

	public Dir platformDir = Dir.none;

	// Use this for initialization
	void Start () {
		gameControllerObject = GameObject.Find ("_GameController");
		gameControllerScript = gameControllerObject.GetComponentInChildren<GameController>();
		startPos = transform.position;
		player = GameObject.Find("Player");
		playerController = player.GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {



		lifeLength += Time.deltaTime;
		Vector3 trans;
		switch(platformDir){
		case Dir.up:
			trans = new Vector3(0,Speed,0);
			break;
		case Dir.down:
			trans = new Vector3(0,-Speed,0);
			break;
		case Dir.right:
			trans = new Vector3(Speed,0,0);
			break;
		case Dir.left:
			trans = new Vector3(-Speed,0,0);
			break;
		default:
			trans = new Vector3(0,0,0);
			break;
		}
		transform.Translate(trans);
		distance = Vector2.Distance(startPos,transform.position);

		if(carryingPlayer){
			player.transform.position = transform.position;
		}

		if(distanceCeil>0 && distance > distanceCeil){
			if(carryingPlayer){
				if(isExit){
					gameControllerScript.LevelComplete();
				}else if(isDanger){
					destinationObject.gameObject.GetComponent<DangerBlock>().Burst();
					playerController.ResetPlayer();
				}else{
					playerController.checkPoint = transform.position;
				}
			}
			DestroySelf();
		}

		/*if(Mathf.Abs(distance)>10){
			ResetPlayer();
			DestroySelf();
		}*/
	}

	void OnTriggerEnter2D(Collider2D c){
		collider = c;
		if(c.name=="Player"){
			if(lifeLength<0.1f){
				if(platformDir == playerController.playerDir && carryingPlayer == false){
					carryingPlayer = true;
					playerController.lightObject.range = 15f;
					playerController.flareObject.brightness = 2.5f;
					playerController.playerDir = Dir.none;
					playerController.moving = true;
					player.audio.Play();
					player.transform.FindChild("Text").gameObject.SetActive(false);
					CameraScript camScript = GameObject.Find("CameraRig").GetComponentInChildren<CameraScript>();
					camScript.Shake();
				}
			}
		}else if(c.name=="MovementCube(Clone)"){
		}else if(distance>1f){
			if(carryingPlayer&&c.name=="DangerCube"){
				isDanger = true;
			}
			if(carryingPlayer&&c.name=="Exit"){
				isExit = true;
			}
			destinationObject = c.gameObject;
			distanceCeil = Mathf.Ceil(distance);
		}
	}

	void DestroySelf(){

		if(carryingPlayer){
			player.transform.position = new Vector3(Mathf.Round(player.transform.position.x),Mathf.Round(player.transform.position.y),0);
			playerController.moving = false;

			string message = "";
			Debug.Log(destinationObject.name);
			if(destinationObject.gameObject.GetComponentInChildren<MessageHolder>()){
				message = destinationObject.GetComponentInChildren<MessageHolder>().message;
				GameObject textHolder = player.transform.FindChild("Text").gameObject;
				if(message!=null){
					textHolder.transform.GetComponent<TextMesh>().text = message;
				}
				textHolder.SetActive(true);
			}

			playerController.moving = false;
			
		}

		Destroy(transform.gameObject);
	}
}

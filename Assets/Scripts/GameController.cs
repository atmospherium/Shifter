using UnityEngine;
using System.Collections;


public enum Dir { none, up, down, left, right };
public class GameController : MonoBehaviour {

	GameObject audioController;
	GameObject exit;
	float fadeBack = 1.6f;
	float fadeForward = -1.2f;
	float fadeSpeed = 3.0f;

	GameObject fader;

	// Use this for initialization
	void Start () {
		audioController = GameObject.Find("_AudioController");
		//audioController.audio.volume = 0;
		fader = GameObject.Find ("Fader");
		//StartCoroutine("FadeIn");
		//StartCoroutine("FadeInMusic");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void LevelComplete(){
		StartCoroutine("FadeOut");
	}

	private IEnumerator FadeInMusic(){
		Debug.Log("Audio Fade");
		
		while(audioController.audio.volume<1){
			Debug.Log ("Fading Audio");
			audioController.audio.volume += 0.02f;
			yield return null;
		}
		audioController.audio.volume = 1;
	}

	private IEnumerator FadeIn(){
		fader.transform.position = new Vector3(fader.transform.position.x,fader.transform.position.y,fadeForward);
		float countdown = 0.7f;
		while(countdown>0){
			countdown-=Time.deltaTime;
			yield return null;
		}
		while(fader.transform.localPosition.z<fadeBack){
			fader.transform.position = Vector3.Lerp(fader.transform.position,new Vector3(0,0,fadeBack),0.05f);
			yield return null;
		}
	}

	private IEnumerator FadeOut(){
		exit = GameObject.Find("Exit");
		exit.GetComponentInChildren<AudioSource>().Play();
		string message = exit.GetComponent<MessageHolder>().message;
		while(audioController.audio.volume>0){
			Debug.Log ("Fading Audio");
			audioController.audio.volume -= 0.25f;
			yield return null;
		}
		while(fader.transform.localPosition.z>fadeForward){
			Debug.Log ("Fading");
			fader.transform.Translate(new Vector3(0,0,-fadeSpeed*Time.deltaTime));
			yield return null;
		}

		int nextLevel;
		string completeText;
		float counter;

		if(Application.levelCount==Application.loadedLevel+1){
			nextLevel=0;
			counter = 5;
		}else{
			nextLevel = Application.loadedLevel+1;
			counter = 0.7f;
		}

		//GameObject levelComplete = GameObject.Find("LevelComplete");
		//levelComplete.GetComponent<TextMesh>().text = message;

		while(counter>0){
			counter-=Time.deltaTime;
			yield return null;
		}

		Application.LoadLevel(nextLevel);

	}
}

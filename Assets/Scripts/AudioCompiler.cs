using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class AudioCompiler : MonoBehaviour {
	
	// Set this sample in the editor.
	public AudioClip[] sample = new AudioClip[7];	
	public bool[] sampleEnable = new bool[7];
	
	// Some parameters we can tweak.
	const int frequency = 44100;
	const float trackDuration = 4.0f;
	const int trackSteps = 16;
	
	// The PCM data buffer we'll build the track in.	
	private float[] PCMBuffer = new float[ Mathf.CeilToInt( frequency * trackDuration ) * 2 ]; 	
	
	// Our audio clip to hold the track we'll create.
	private AudioClip track;
		
	// MonoBehaviour Start
	void Start() {
		float creationStart = Time.realtimeSinceStartup;

		if(sampleEnable[0]){
			CompositeSampleOntoTrack( 0.00f, 1f, 0 );
			CompositeSampleOntoTrack( 1.25f, 1f, 0 );
			CompositeSampleOntoTrack( 1.75f, 1f, 0 );
			CompositeSampleOntoTrack( 2.00f, 1f, 0 );
		}

		if(sampleEnable[1]){
			CompositeSampleOntoTrack( 3.75f, 1f, 1 );
		}

		if(sampleEnable[2]){
			CompositeSampleOntoTrack( 2.75f, 1f, 2 );
			CompositeSampleOntoTrack( 3.25f, 1f, 2 );
		}

		if(sampleEnable[3]){
			CompositeSampleOntoTrack( 0.25f, 1f, 3 );
			CompositeSampleOntoTrack( 0.75f, 1f, 3 );
			CompositeSampleOntoTrack( 2.25f, 1f, 3 );

		}

		if(sampleEnable[4]){
			CompositeSampleOntoTrack( 1.50f, 1f, 4 );
			CompositeSampleOntoTrack( 2.50f, 1f, 4 );
		}

		if(sampleEnable[5]){
			CompositeSampleOntoTrack( 0.50f, 1f, 5 );
			CompositeSampleOntoTrack( 3.50f, 1f, 5 );
		}

		if(sampleEnable[6]){
			CompositeSampleOntoTrack( 1.00f, 1f, 6 );
			CompositeSampleOntoTrack( 3.00f, 1f, 6 );
		}

		
		float creationEnd = Time.realtimeSinceStartup;
		//Debug.Log( "Creating track took " + ( creationEnd - creationStart ) + "s" );
		
		// We have PCM Data. Next step is to turn it into an audio clip.
		track = AudioClip.Create( "Music", Mathf.CeilToInt( frequency * trackDuration ), 2, frequency, false, false );
		track.SetData( PCMBuffer, 0 );
		
		// Now play it.
		GetComponent<AudioSource>().clip = track;
		GetComponent<AudioSource>().Play();
	}
	
	// A utility function to composite our sample onto our track at a given time.
	void CompositeSampleOntoTrack( float time, float volume, int index ) {
		int startSample = Mathf.CeilToInt( time * frequency ) * 2; // 2 channels because we're in stereo
		
		// Read the PCM data out of our sample.
		float[] sampleData = new float[sample[index].samples];
		sample[index].GetData( sampleData, 0 );
		
		for( int sampleIndex = 0; sampleIndex < sampleData.Length; sampleIndex++ ) {
			// For each individual sample point in our sample, add it's magnitude to the magnitude in the track.
			int targetIndex = startSample + sampleIndex;
			if( targetIndex >= PCMBuffer.Length ) {
				targetIndex -= PCMBuffer.Length;
			}
			
			PCMBuffer[targetIndex] += sampleData[sampleIndex] * volume;
		}
	}
}

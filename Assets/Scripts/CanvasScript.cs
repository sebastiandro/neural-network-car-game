using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CanvasScript : MonoBehaviour {

	// Use this for initialization


	Text text;
	UnityAction listener;

	void Start () {
		text = GetComponent<Text> ();

		listener = new UnityAction (newGeneration);

		EventManager.StartListening ("new_generation", listener);
	}


	void newGeneration() {
		text.text = "Generation " + CarRayCaster.generationNumber;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

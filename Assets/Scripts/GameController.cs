using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Vehicles.Car;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameController : MonoBehaviour {


	public GameObject car;
	public Rigidbody carRigid;
	public CarController carController;

	public Text genomeText;
	public Text generationText;

	private float distanceTraveled = 0;
	private int generationNumber = 1;

	private UnityAction listener;

	private NeuroEvolution neuro;
	private Genome currentGenome;
	private Generation currentGeneration;
	private List<Genome> currentGenomes = new List<Genome>();
	private Vector3 lastPosition;
	private int currentGenomeIndex = 0;

	public CarSensor leftSensor1;
	public CarSensor leftSensor2;
	public CarSensor leftSensor3;
	public CarSensor leftSensor4;
	public CarSensor leftSensor5;
	public CarSensor frontSensor;
	public CarSensor rightSensor1;
	public CarSensor rightSensor2;
	public CarSensor rightSensor3;
	public CarSensor rightSensor4;
	public CarSensor rightSensor5;

	// Use this for initialization

	void Awake () {
		neuro = new NeuroEvolution (11, new int[]{ 7 }, 2);

		Generation gen = neuro.nextGeneration ();
		currentGeneration = gen;
		List<Genome> genomes = gen.getGenomes ();
		currentGenomes = genomes;
		currentGenome = genomes[currentGenomeIndex];

		genomeText.text = "Genome 1";
		generationText.text = "Generation 1";

		listener = new UnityAction (GameOver);

	}

	void OnEnable() {
		EventManager.StartListening ("gameover", listener);
	}
	
	// Update is called once per frame
	void Update () {
		distanceTraveled += Vector3.Distance (car.transform.position, lastPosition);
		lastPosition = car.transform.position;
	}

	private void FixedUpdate() {


	}

	void GameOver() {
		carRigid.velocity = new Vector3(0,0,0);

		currentGenome.setScore ((int)distanceTraveled);
		distanceTraveled = 0;

		currentGenomeIndex++;


		if (currentGenomeIndex == NeuroEvolution.population) {

			Debug.Log ("Generation " + generationNumber);
			currentGenomes.ForEach (((Genome genome) => {
				Debug.Log ("Score: " + genome.getScore ());
			}));

			currentGeneration = neuro.nextGeneration ();
			currentGenomes = currentGeneration.getGenomes ();

			currentGenomeIndex = 0;

			generationNumber++;

			EventManager.TriggerEvent ("new_generation");
			Debug.Log ("New generation");
		}

		currentGenome = currentGenomes[currentGenomeIndex];

		updateCanvasText ();

	}

	private void updateCanvasText() {
		genomeText.text = "Genome " + (currentGenomeIndex + 1);
		generationText.text = "Generation " + currentGeneration;
	}
}

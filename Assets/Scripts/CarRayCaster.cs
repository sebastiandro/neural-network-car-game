using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityStandardAssets.Vehicles.Car;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Events;

public class CarRayCaster : MonoBehaviour
{

	public GameObject car;

	public Rigidbody carRigid;
	int rayDistance = 5;
	int rayDistanceFront = 40;
	NeuralNetwork neu = new NeuralNetwork ();
	NeuroEvolution neuro;
	Genome currentGenome;
	Generation currentGeneration;
	List<Genome> currentGenomes = new List<Genome>();
	int currentGenomeIndex = 0;
	public 	CarController m_Car;
	public float distanceTraveled = 0;
	Vector3 lastPosition;
	int generationNumber = 1;

	private UnityAction listener;


	void Awake() {
		Debug.Log ("Awake");

		neuro = new NeuroEvolution (3, new int[]{ 5 }, 2);

		Generation gen = neuro.nextGeneration ();

		currentGeneration = gen;

		List<Genome> genomes = gen.getGenomes ();

		currentGenomes = genomes;
		currentGenome = genomes[currentGenomeIndex];

		m_Car = GetComponent<CarController>();

		/*
		for (int i = 0; i < layers.Count; i++) {
			NeuralNetwork.Neuron[] layerNeurons = layers [i].getNeurons ();
			for (int j = 0; j < layerNeurons.Length; j++) {
				NeuralNetwork.Neuron neuron = layerNeurons[j];

				double[] weights = neuron.getWeights ();
				for (int k = 0; k < weights.Length; k++) {
					print ("Layer " + i + " Neuron " + j + " Weight " + k +  ": " + weights [k]);
				}
			}
		}
		*/

		listener = new UnityAction (gameOver);


	}

	void OnEnable() {
		EventManager.StartListening ("gameover", listener);
	}

	void Update(){
		distanceTraveled += Vector3.Distance (car.transform.position, lastPosition);
		lastPosition = car.transform.position;

		//Debug.Log ((int)distanceTraveled);
	}

	void gameOver() {
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
		}

		currentGenome = currentGenomes[currentGenomeIndex];

		//neuro.networkScore (currentGenome.getNeuralNetwork(), (int)distanceTraveled);


	}

	private void FixedUpdate() {


		RaycastHit hit;

		Ray frontRay = new Ray (car.transform.position, transform.forward * 2 * rayDistance);
		Ray rightRay = new Ray (car.transform.position, Quaternion.Euler(0, 45, 0) * transform.forward * rayDistance);
		Ray leftRay = new Ray (car.transform.position, Quaternion.Euler(0, -45, 0) * transform.forward * rayDistance);

		double frontDistance = 0;
		double leftDistance = 0;
		double rightDistance = 0;

		// Mark collision from front
		if (Physics.Raycast(frontRay, out hit, rayDistanceFront)) {
			Debug.DrawRay(car.transform.position, transform.forward * rayDistanceFront, Color.red);
			//Debug.Log ("Front distance: " + hit.distance);

			frontDistance = hit.distance / rayDistanceFront;
		} else {
			Debug.DrawRay(car.transform.position, transform.forward * rayDistanceFront, Color.green);
		}

		// Mark collision from right
		if (Physics.Raycast(rightRay, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, 45, 0) * transform.forward * rayDistance, Color.red);
			//Debug.Log ("Right distance: " + hit.distance);

			rightDistance = hit.distance / rayDistance;
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, 45, 0) * transform.forward * rayDistance, Color.green);
		}

		// Mark collision from left
		if (Physics.Raycast(leftRay, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -45, 0) * transform.forward * rayDistance, Color.red);
			//Debug.Log ("Left distance: " + hit.distance);
			leftDistance = hit.distance / rayDistance;
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -45, 0) * transform.forward * rayDistance, Color.green);
		}
			

		double currentSpeed = MathHelpers.limitToRange ((carRigid.velocity.magnitude * 3.6) / 15, 0, 1);
		//print ("Left distance: " + leftDistance + " Front Distance: " + frontDistance + " Right Distance: " + rightDistance + " Speed: " + currentSpeed);
		double[] output = currentGenome.getNeuralNetwork().compute (new double[]{ leftDistance, frontDistance, rightDistance});


		//Debug.Log ("Output 1: " + output[0] + " Output 2: " + output[1] + " Output 3: " + output[2] + " Output 4:" + output[3] );

		/*
		for (int i = 0; i < output.Length; i++) {
			//Debug.Log ("Output " + i + ": " + output[i]);
		}*/

		int output1 = output [0] > 0.5 ? 1 : 0; // speed
		int output2 = output [1] > 0.5 ? 1 : 0; // steer

		Debug.Log ("Output 1: " + output[0] + " Output2: " + output[1]);


		//Debug.Log ();

		//Debug.Log (m_Car.CurrentSteerAngle);

		//Debug.Log (h);

		m_Car.Move ((float)(output[1] * 2 - 1), (float)output[0], (float)output[0], 0);
	
		Debug.Log ((float)(output[1] * 2 - 1));
		Debug .Log(m_Car.CurrentSteerAngle);
	}

}
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
	int rayDistance = 10;
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
	public static double[] output;
	public static int generationNumber = 1;

	private UnityAction listener;


	void Awake() {
		Debug.Log ("Awake");

		neuro = new NeuroEvolution (11, new int[]{ 7 }, 2);

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


			Debug.Log ("Generation " + CarRayCaster.generationNumber);
			currentGenomes.ForEach (((Genome genome) => {
				Debug.Log ("Score: " + genome.getScore ());
			}));

			currentGeneration = neuro.nextGeneration ();
			currentGenomes = currentGeneration.getGenomes ();

			currentGenomeIndex = 0;

			CarRayCaster.generationNumber++;

			EventManager.TriggerEvent ("new_generation");
			Debug.Log ("New generation");
		}

		currentGenome = currentGenomes[currentGenomeIndex];

		//neuro.networkScore (currentGenome.getNeuralNetwork(), (int)distanceTraveled);


	}

	private void FixedUpdate() {
		RaycastHit hit;

		Ray frontRay = new Ray (car.transform.position, transform.forward * 2 * rayDistance);

		Ray rightRay = new Ray (car.transform.position, Quaternion.Euler(0, 45, 0) * transform.forward * rayDistance);
		Ray rightFrontRay = new Ray (car.transform.position, Quaternion.Euler(0, 35, 0) * transform.forward * rayDistance);
		Ray rightFrontRay2 = new Ray (car.transform.position, Quaternion.Euler(0, 25, 0) * transform.forward * rayDistance);
		Ray rightFrontRay3 = new Ray (car.transform.position, Quaternion.Euler(0, 15, 0) * transform.forward * rayDistance);
		Ray rightFrontRay4 = new Ray (car.transform.position, Quaternion.Euler(0, 5, 0) * transform.forward * rayDistance);

		Ray leftRay = new Ray (car.transform.position, Quaternion.Euler(0, -45, 0) * transform.forward * rayDistance);
		Ray leftFrontRay = new Ray (car.transform.position, Quaternion.Euler(0, -35, 0) * transform.forward * rayDistance);
		Ray leftFrontRay2 = new Ray (car.transform.position, Quaternion.Euler(0, -25, 0) * transform.forward * rayDistance);
		Ray leftFrontRay3 = new Ray (car.transform.position, Quaternion.Euler(0, -15, 0) * transform.forward * rayDistance);
		Ray leftFrontRay4 = new Ray (car.transform.position, Quaternion.Euler(0, -5, 0) * transform.forward * rayDistance);

		double frontDistance = 0;

		double leftDistance = 0;
		double rightFrontDistance = 0;
		double rightFrontDistance2 = 0;
		double rightFrontDistance3 = 0;
		double rightFrontDistance4 = 0;

		double rightDistance = 0;
		double leftFrontDistance = 0;
		double leftFrontDistance2 = 0;
		double leftFrontDistance3 = 0;
		double leftFrontDistance4 = 0;

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

		// Mark collision from right
		if (Physics.Raycast(rightFrontRay, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, 35, 0) * transform.forward * rayDistance, Color.red);
			//Debug.Log ("Right distance: " + hit.distance);

			rightFrontDistance = hit.distance / rayDistance;
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, 35, 0) * transform.forward * rayDistance, Color.green);
		}



		// Mark collision from right
		if (Physics.Raycast(rightFrontRay2, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, 25, 0) * transform.forward * rayDistance, Color.red);
			//Debug.Log ("Right distance: " + hit.distance);

			rightFrontDistance2 = hit.distance / rayDistance;
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, 25, 0) * transform.forward * rayDistance, Color.green);
		}

		// Mark collision from right
		if (Physics.Raycast(rightFrontRay3, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, 15, 0) * transform.forward * rayDistance, Color.red);
			//Debug.Log ("Right distance: " + hit.distance);

			rightFrontDistance3 = hit.distance / rayDistance;
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, 15, 0) * transform.forward * rayDistance, Color.green);
		}

		// Mark collision from right
		if (Physics.Raycast(rightFrontRay4, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, 5, 0) * transform.forward * rayDistance, Color.red);
			//Debug.Log ("Right distance: " + hit.distance);

			rightFrontDistance4 = hit.distance / rayDistance;
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, 5, 0) * transform.forward * rayDistance, Color.green);
		}


		// Mark collision from left
		if (Physics.Raycast(leftRay, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -45, 0) * transform.forward * rayDistance, Color.red);
			//Debug.Log ("Left distance: " + hit.distance);
			leftDistance = hit.distance / rayDistance;
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -45, 0) * transform.forward * rayDistance, Color.green);
		}


		// Mark collision from right
		if (Physics.Raycast(leftFrontRay, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -35, 0) * transform.forward * rayDistance, Color.red);
			//Debug.Log ("Right distance: " + hit.distance);

			leftFrontDistance = hit.distance / rayDistance;
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -35, 0) * transform.forward * rayDistance, Color.green);
		}

		// Mark collision from right
		if (Physics.Raycast(leftFrontRay2, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -25, 0) * transform.forward * rayDistance, Color.red);
			//Debug.Log ("Right distance: " + hit.distance);

			leftFrontDistance2 = hit.distance / rayDistance;
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -25, 0) * transform.forward * rayDistance, Color.green);
		}

		// Mark collision from right
		if (Physics.Raycast(leftFrontRay3, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -15, 0) * transform.forward * rayDistance, Color.red);
			//Debug.Log ("Right distance: " + hit.distance);

			leftFrontDistance3 = hit.distance / rayDistance;
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -15, 0) * transform.forward * rayDistance, Color.green);
		}

		// Mark collision from right
		if (Physics.Raycast(leftFrontRay4, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -5, 0) * transform.forward * rayDistance, Color.red);
			//Debug.Log ("Right distance: " + hit.distance);

			leftFrontDistance4 = hit.distance / rayDistance;
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -5, 0) * transform.forward * rayDistance, Color.green);
		}
			

		double currentSpeed = MathHelpers.limitToRange ((carRigid.velocity.magnitude * 3.6) / 15, 0, 1);


		leftDistance = leftDistance > 0 ? 1 - leftDistance : 0;
		leftFrontDistance = leftFrontDistance > 0 ? 1 - leftFrontDistance : 0;
		leftFrontDistance2 = leftFrontDistance2 > 0 ? 1 - leftFrontDistance2 : 0;
		leftFrontDistance3 = leftFrontDistance3 > 0 ? 1 - leftFrontDistance3 : 0;
		leftFrontDistance4 = leftFrontDistance3 > 0 ? 1 - leftFrontDistance4 : 0;

		frontDistance = frontDistance > 0 ? 1 - frontDistance : 0;

		rightDistance = rightDistance > 0 ? 1 - rightDistance : 0;
		rightFrontDistance = rightFrontDistance > 0 ? 1 - rightFrontDistance : 0;
		rightFrontDistance2 = rightFrontDistance2 > 0 ? 1 - rightFrontDistance2 : 0;
		rightFrontDistance3 = rightFrontDistance3 > 0 ? 1 - rightFrontDistance3 : 0;
		rightFrontDistance4 = rightFrontDistance4 > 0 ? 1 - rightFrontDistance4 : 0;



		print ("Left distance: " + leftDistance + " Front Distance: " + frontDistance + " Right Distance: " + rightDistance);

		double[] output = currentGenome.getNeuralNetwork().compute (new double[]{leftDistance, leftFrontDistance, leftFrontDistance2, leftFrontDistance3, leftFrontDistance4, frontDistance, rightDistance, rightFrontDistance, rightFrontDistance2, rightFrontDistance3, rightFrontDistance4});


		//Debug.Log ("Output 1: " + output[0] + " Output 2: " + output[1] + " Output 3: " + output[2] + " Output 4:" + output[3] );

		/*
		for (int i = 0; i < output.Length; i++) {
			//Debug.Log ("Output " + i + ": " + output[i]);
		}*/

		int output1 = output [0] > 0.5 ? 1 : 0; // speed
		int output2 = output [1] > 0.5 ? 1 : 0; // steer

		//Debug.Log ("Output 1: " + output[0] + " Output2: " + output[1]);


		//Debug.Log ();

		//Debug.Log (m_Car.CurrentSteerAngle);

		//Debug.Log (h);

		CarRayCaster.output = output;
		EventManager.TriggerEvent ("output_network");

		m_Car.Move ((float)(output[1] * 7 - 3.5), (float)output[0], (float)output[0], 0);
	
		//Debug.Log ((float)(output[1] * 2 - 1));
		Debug .Log(m_Car.CurrentSteerAngle);
	}

}
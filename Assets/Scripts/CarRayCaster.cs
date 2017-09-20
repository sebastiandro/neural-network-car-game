using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarRayCaster : MonoBehaviour
{

	public GameObject car;
	public Rigidbody carRigid;
	int rayDistance = 5;
	int rayDistanceFront = 20;
	NeuralNetwork neu = new NeuralNetwork ();
	Genome currentGenome;

	void Start() {
		Debug.Log ("Start");

		NeuroEvolution neuro = new NeuroEvolution (4, new int[]{ 5, 10 }, 4);

		Generation gen = neuro.nextGeneration ();

		List<Genome> genomes = gen.getGenomes ();

		currentGenome = genomes[0];

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

	}

	void Update(){
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

		//print ("Left distance: " + leftDistance + " Front Distance: " + frontDistance + " Right Distance: " + rightDistance);

		double currentSpeed = MathHelpers.limitToRange ((carRigid.velocity.magnitude * 3.6) / 70, 0, 1);
		double[] output = currentGenome.getNeuralNetwork().compute (new double[]{ leftDistance, frontDistance, rightDistance, currentSpeed});


		Debug.Log ("Output 1: " + output[0] + " Output 2: " + output[1] + " Output 3: " + output[2] + " Output 4:" + output[3] );

		/*
		for (int i = 0; i < output.Length; i++) {
			//Debug.Log ("Output " + i + ": " + output[i]);
		}*/

	}

}
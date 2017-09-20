using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarRayCaster : MonoBehaviour
{

	public GameObject car;
	int rayDistance = 5;
	int rayDistanceFront = 20;
	NeuralNetwork neu = new NeuralNetwork ();

	void Start() {
		Debug.Log ("Start");

		//NeuralNetwork neu = new NeuralNetwork ();
		neu.perceptronGeneration (3, new int[]{4, 3, 6}, 5);
		List<Layer> layers = neu.getLayers ();

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

		List<double> weights = neu.getWeightsList ();
		for (int j = 0; j < weights.Count; j++) {
			print (weights [j]);
		}

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

		print ("Left distance: " + leftDistance + " Front Distance: " + frontDistance + " Right Distance: " + rightDistance);

		double[] output = neu.compute (new double[]{ leftDistance, frontDistance, rightDistance });

		for (int i = 0; i < output.Length; i++) {
			Debug.Log ("Output " + i + ": " + output[i]);
		}

	}

}
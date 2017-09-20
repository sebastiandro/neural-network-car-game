using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CarRayCaster : MonoBehaviour
{

	public GameObject car;
	int rayDistance = 5;
	int rayDistanceFront = 20;

	void Start() {
		Debug.Log ("Start");

		NeuralNetwork neu = new NeuralNetwork ();
		neu.perceptronGeneration (5, new int[]{4, 3, 6}, 5);
		List<NeuralNetwork.Layer> layers = neu.getLayers ();

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
	}

	void Update(){
		RaycastHit hit;

		Ray frontRay = new Ray (car.transform.position, transform.forward * 2 * rayDistance);
		Ray rightRay = new Ray (car.transform.position, Quaternion.Euler(0, 45, 0) * transform.forward * rayDistance);
		Ray leftRay = new Ray (car.transform.position, Quaternion.Euler(0, -45, 0) * transform.forward * rayDistance);

		// Mark collision from front
		if (Physics.Raycast(frontRay, out hit, rayDistanceFront)) {
			Debug.DrawRay(car.transform.position, transform.forward * rayDistanceFront, Color.red);
			Debug.Log ("Front distance: " + hit.distance);
		} else {
			Debug.DrawRay(car.transform.position, transform.forward * rayDistanceFront, Color.green);
		}

		// Mark collision from right
		if (Physics.Raycast(rightRay, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, 45, 0) * transform.forward * rayDistance, Color.red);
			Debug.Log ("Right distance: " + hit.distance);
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, 45, 0) * transform.forward * rayDistance, Color.green);
		}

		// Mark collision from left
		if (Physics.Raycast(leftRay, out hit, rayDistance)) {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -45, 0) * transform.forward * rayDistance, Color.red);
			Debug.Log ("Left distance: " + hit.distance);
		} else {
			Debug.DrawRay(car.transform.position, Quaternion.Euler(0, -45, 0) * transform.forward * rayDistance, Color.green);
		}

	}

}
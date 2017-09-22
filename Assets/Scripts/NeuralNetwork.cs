using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NeuralNetwork {

	// Options
	private int[] network;
	private List<Layer> layers = new List<Layer>();


	public NeuralNetwork(){
		network = new int[]{ 1, 1, 1 };
	}


	public void perceptronGeneration(int input, int[] hiddens, int output) {
		int index = 0;
		int previousNeurons = 0;
		Layer layer = new Layer ();

		layer.populate (input, previousNeurons);

		previousNeurons = input;

		layers.Add (layer);
		index++;

		for (int i = 0; i < hiddens.Length; i++) {
			Layer nextLayer = new Layer(index);

			nextLayer.populate (hiddens [i], previousNeurons);
			previousNeurons = hiddens [i];
			layers.Add (nextLayer);
			index++;
		}

		// Output layer
		Layer outputLayer = new Layer (index);
		outputLayer.populate (output, previousNeurons);

		layers.Add(outputLayer);

	}

	public int[] getNumberOfNeuronsPerLayer() {
		int[] nbNeuronsPerLayer = new int[layers.Count];

		for (int i = 0; i < layers.Count; i++) {
			Layer layer = layers [i];
			nbNeuronsPerLayer [i] = layer.getNeurons ().Length;
		}

		return nbNeuronsPerLayer;
	}

	public List<double> getWeightsList() {

		List<double> weights = new List<double>();

		for (int i = 0; i < layers.Count; i++) {
			Layer layer = layers [i];
			Neuron[] layerNeurons = layer.getNeurons();
			for (int j = 0; j < layerNeurons.Length; j++) {
				double[] neuronWeights = layerNeurons [j].getWeights ();
				for (int k = 0; k < neuronWeights.Length; k++) {
					weights.Add (neuronWeights [k]);
				}
			}
		}

		return weights;
	}


	public void setNeuronsAndWeights(int[] nbNeuronsPerLayer, List<double> weights){
		int previousNeurons = 0;
		int indexWeights = 0;

		layers = new List<Layer> ();

		for (int i = 0; i < nbNeuronsPerLayer.Length; i++) {
			Layer layer = new Layer (i);
			layer.populate (nbNeuronsPerLayer [i], previousNeurons);
			Neuron[] neurons = layer.getNeurons ();

			for (int j = 0; j < neurons.Length; j++) {

				for (int k = 0; k < neurons [j].getWeights ().Length; k++) {
					neurons [j].setWeightAtIndex (weights [indexWeights], k);
					indexWeights++;
				}
			}

			previousNeurons = nbNeuronsPerLayer [i];

			layers.Add (layer);
		}
	}

	public double[] compute(double[] inputs){
		Layer inputLayer = this.layers[0];
		Neuron[] inputLayerNeurons = inputLayer.getNeurons ();

		// Set values for input layer
		for (int i = 0; i < inputs.Length; i++) {
			inputLayerNeurons [i].setValue (inputs [i]);
		}

		Layer prevLayer = inputLayer;
		for (int j = 1; j < this.layers.Count; j++) {

			Neuron[] layerNeurons = this.layers [j].getNeurons();

			for (int k = 0; k < layerNeurons.Length; k++) {

				double sum = 0;

				for (int l = 0; l < prevLayer.getNeurons ().Length; l++) {
					sum += prevLayer.getNeurons () [l].getValue () * layerNeurons [k].getWeights () [l];
				}

				layerNeurons [k].setValue (MathHelpers.activation (sum));
			}

			prevLayer = this.layers [j];
		}
			
		Layer lastLayer = layers [layers.Count - 1];
		double[] output = new double[lastLayer.getNeurons ().Length];

		for (int m = 0; m < lastLayer.getNeurons ().Length; m++) {
			output [m] = lastLayer.getNeurons () [m].getValue ();
		}

		return output;

	}

	public void setNetwork(int[] network) {
		this.network = network;
	}

	public void setLayers (List<Layer> layers) {
		this.layers = layers;
	}

	public List<Layer> getLayers() {
		return layers;
	}

	public NeuralNetwork Clone(){
		NeuralNetwork newNeuralNetwork = new NeuralNetwork ();

		int[] newNetwork = network;
		List<Layer> newLayers = new List<Layer>(layers);

		newNeuralNetwork.setNetwork (newNetwork);
		newNeuralNetwork.setLayers (newLayers);

		return newNeuralNetwork;
	}
		
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NeuralNetwork {

	// Options
	private int[] network = new int[] {1,{1},1};
	private int population = 50;
	private float elitsm = 0.2;
	private float randomBehaviour = 0.2;
	private float mutationRate = 0.1;
	private float mutationRange = 0.5;
	private int scoreSort = -1;
	private int nbChild = 1;

	// Network
	Layer layers = new Layer[]{};


	private double activation(double x) {
		return (1 / (Math.Exp(x) + 1));
	}

	private static double randomClamped() {
		return new System.Random ().NextDouble () * 2 - 1;
	}

	public NeuralNetwork(){

		//Debug.Log(activation(rnd.Next(1,1000)));
		Debug.Log("Random Clamped: " + this.randomClamped());
	}

	private void perceptronGeneration(int input, int hiddens, int output) {
		int index = 0;
		int previousNeurons = 0;
		Layer layer = new Layer ();

		layer.populate (input, previousNeurons);

		previousNeurons = input;

		layers [index] = layer;
		index++;

		for (int i = 0; i < hiddens; i++) {
			Layer nextLayer = new Layer(index);

			nextLayer.populate (hiddens [i], previousNeurons);
			previousNeurons = hiddens [i];
			layers [index] = nextLayer;
			index++;
		}

		// Output layer

		Layer outputLayer = new Layer (index);
		outputLayer.populate (output, previousNeurons);

		layers [index] = outputLayer;

	}

	private class Neuron {
		private int value = 0;
		private int[] weights = new int[]{ };

		public void populate(int nb){
			for (int i = 0; i < nb; i++) {
				weights [i] = NeuralNetwork.randomClamped ();
			}
		}
	}

	private class Layer {
		private int id;
		private Neuron[] neurons = new Neuron[] {};

		public Layer(int index) {
			id = index || 0;
		}

		public void populate(int nbNeurons, int nbInputs) {
			neurons = new Neuron[]{};
			for (int i = 0; i < nbNeurons; i++) {
				Neuron n = new Neuron ();
				n.populate (nbInputs);
				this.neurons [i] = n;
			}
		}
	}




}

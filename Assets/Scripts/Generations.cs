using System;
using System.Collections.Generic;
using UnityEngine;

public class Generations
{

	List<Generation> generations = new List<Generation>();
	Generation currentGeneration = new Generation();

	public Generations () {}


	public Generation firstGeneration() {

		Generation firstGeneration = new Generation ();

		for (var i = 0; i < NeuroEvolution.population; i++) {
			var nn = new NeuralNetwork ();

			nn.perceptronGeneration (NeuroEvolution.inputs, NeuroEvolution.hiddenLayers, NeuroEvolution.outputs);

			firstGeneration.addGenome (new Genome (nn));

		}

		generations.Add (firstGeneration);
		return firstGeneration;
	}

	public Generation nextGeneration() {
		if (this.generations.Count == 0) {
			throw new Exception ("Must create first Generation");
		}

		Generation nextGen = this.generations [this.generations.Count - 1].generateNextGeneration ();

		generations.Add (nextGen);

		return nextGen;

	}


	public void addGenome(Genome genome) {
		if (this.generations.Count == 0) {
			throw new Exception ("Must create first Generation");
		}

		this.generations [this.generations.Count - 1].addGenome (genome);
	}

	public List<Generation> getGenerations() {
		return generations;	
	}


}



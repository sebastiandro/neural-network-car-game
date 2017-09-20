using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Generation {

	List<Genome> genomes = new List<Genome>();

	private double mutationRange = 0.5;
	private double mutationRate = 0.1;
	private double elitism = 0.2;
	private double population = 10;
	private double randomBehaviour = 0.2;
	private int nbChildren = 1;


	public Generation() {
	}

	// Add genome sorted
	public void addGenome(Genome genome) {
		int index = 0;
		for (int i = 0; i < genomes.Count; i++) {
			if (genome.getScore () > genomes [i].getScore ()) {
				break;
			}
			index++;
		}

		genomes.Insert (index, genome);
	}
	public List<Genome> getGenomes() {
		return genomes;
	}

	public Genome[] breed(Genome g1, Genome g2, int nbChildren){

		Genome[] children = new Genome[nbChildren];

		for (int i = 0; i < nbChildren; i++) {
			
			Genome g1Clone = g1.Clone();
			List<double> g1Weights = g1Clone.getNeuralNetwork ().getWeightsList ();
			List<double> g2Weights = g2.getNeuralNetwork ().getWeightsList ();

			// Genetic Crossover
			for (int j = 0; j < g2Weights.Count; j++) {
				
				if (MathHelpers.randomNumber () <= 0.5) {
					g1Weights [j] = g2Weights [j];
				}

			}

			// Preform Mutations on random weights
			for (int k = 0; k < g1Weights.Count; k++) {
				if (MathHelpers.randomNumber() <= mutationRate) {
					g1Weights [k] += MathHelpers.randomNumber () * mutationRange * 2 - mutationRange;
				}
			}

			children [i] = g1Clone;
		}

		return children;
	}

	public Generation generateNextGeneration() {

		Generation nextGeneration = new Generation ();

		// Elitism decides the individuals that will stay alive
		for (int i = 0; i < Math.Round(elitism * population); i++) {
			if (nextGeneration.getGenomes ().Count < population) {
				nextGeneration.addGenome (this.getGenomes () [i].Clone());
			}
		}

		// Random Behaviour
		for (int j = 0; j < Math.Round(randomBehaviour * population); j++) {
			Genome randomGenome = this.genomes [0].Clone ();	
			NeuralNetwork genomeNetwork = randomGenome.getNeuralNetwork ();
			List<double> randomWeights = new List<double> ();

			for (int k = 0; k < genomeNetwork.getWeightsList().Count; k++) {
				randomWeights [k] = MathHelpers.randomClamped ();
			}

			genomeNetwork.setNeuronsAndWeights (genomeNetwork.getNumberOfNeuronsPerLayer (), randomWeights);

			if (nextGeneration.getGenomes ().Count < population) {
				nextGeneration.getGenomes ().Add (randomGenome);
			}
		}


		// Strongest pairls 0 and 1 | 0 and 2 + 1 and 2 | 0 and 3 + 1 and 3 + 2 and 3 ... repeat until we've filled population
		int max = 0;
		while (true) {
			for (int i = 0; i < max; i++) {
				Genome[] children = breed (genomes [i], genomes [max], nbChildren);
				for (int j = 0; j < children.Length; j++) {
					nextGeneration.addGenome (children [j]);
					if (nextGeneration.getGenomes ().Count >= population) {
						return nextGeneration;
					}
				}
			}
			max++;
			if (max >= this.genomes.Count - 1) {
				max = 0;
			}
		}

		return nextGeneration;

	}

}


using System;

public class Genome
{

	private int score = 0;
	private NeuralNetwork neuralNetwork;

	public Genome (NeuralNetwork neuralNetwork)
	{
	}

	public Genome (NeuralNetwork neuralNetwork, int score)
	{
		this.neuralNetwork = neuralNetwork;
		this.score = score;
	}

	public int getScore() {
		return score;
	}

	public void setScore(int score) {
		this.score = score;
	}

	public NeuralNetwork getNeuralNetwork() {
		return neuralNetwork;
	}

	public void deleteNeuralNetwork() {
		neuralNetwork = null;
	}

	public Genome Clone() {
		Genome newGenome = new Genome (neuralNetwork.Clone(), score);

		return newGenome;
	}
}


using System;

public class Genome
{

	private int score = 0;
	private NeuralNetwork network;

	public Genome ()
	{
	}

	public Genome (NeuralNetwork network, int score)
	{
		this.network = network;
		this.score = score;
	}

	public int getScore() {
		return score;
	}

	public void setScore(int score) {
		this.score = score;
	}

	public Genome Clone() {
		Genome newGenome = new Genome (network.Clone(), score);

		return newGenome;
	}
}


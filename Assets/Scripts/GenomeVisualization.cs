using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class GenomeVisualization : MonoBehaviour {

	public GameController gameController;
	private int startingX = 35;
	private int startingY = 750;
	List<List<Image>> paintedNodes = new List<List<Image>> ();
	Sprite nodeSprite;

	// Use this for initialization
	void Start () {
		nodeSprite = AssetDatabase.LoadAssetAtPath ("Assets/Resources/circle2.png", typeof(Sprite)) as Sprite;
		SetUpNodes ();
	}

	private void SetUpNodes() {
		Genome genome = gameController.GetCurrentGenome ();
		int[] neuronsPerLayer = genome.getNeuralNetwork ().getNumberOfNeuronsPerLayer ();

		for (int i = 0; i < neuronsPerLayer.Length; i++) {
			paintedNodes.Add (new List<Image> ());

			for (int j = 0; j < neuronsPerLayer [i]; j++) {

				int yMultiplier = 0;
				if (i > 0 && neuronsPerLayer [i] < neuronsPerLayer [i - 1]) {
					yMultiplier = (int)Mathf.Floor ((float)((neuronsPerLayer [i - 1] - neuronsPerLayer [i]) / 2));
					Debug.Log (yMultiplier);
				}

				PaintNode (startingX + (100 * i), startingY - (30 * j) - (yMultiplier * 30), i);
			}
		}
	}

	private void PaintNode(int x, int y, int layerIndex) {
		GameObject NewObj = new GameObject ();
		Image NewImage = NewObj.AddComponent<Image>();
		NewImage.sprite = nodeSprite;

		Color color = Color.green;
		NewImage.color = color;

		RectTransform rect = NewObj.GetComponent<RectTransform> ();
		rect.SetParent (gameObject.transform);

		rect.SetPositionAndRotation (new Vector3 (x, y, 0), new Quaternion (0, 0, 0, 0));
		rect.sizeDelta = new Vector2 (25, 25);

		paintedNodes[layerIndex].Add (NewImage);

		NewObj.SetActive (true);
	}
	
	// Update is called once per frame
	private void FixedUpdate () {
		Genome genome = gameController.GetCurrentGenome ();

		int[] neuronsPerLayer = genome.getNeuralNetwork ().getNumberOfNeuronsPerLayer ();

		for (int layerIndex = 0; layerIndex < neuronsPerLayer.Length; layerIndex++) {
			for (int neuronIndex = 0; neuronIndex < neuronsPerLayer [layerIndex]; neuronIndex++) {
				UpdateNodeValue (layerIndex, neuronIndex, genome.getNeuralNetwork ().GetNeuronInLayer(layerIndex,neuronIndex).getValue());
			}
		}
	}

	private void UpdateNodeValue(int layerIndex, int neuronIndex, double value) {
		Image neuronImage = paintedNodes [layerIndex][neuronIndex];

		Color color = neuronImage.color;

		if (layerIndex == 0) {
			value = value + 0.2;
		}

		if (value > 0.5) {
			color = Color.red;
		} else {
			color = Color.yellow;
		}

		color.a = value > 0 ? (float)value : 0.05f;
		neuronImage.color = color;
	}
}

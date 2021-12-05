using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StandingWaves : MonoBehaviour {
	public GameObject square, incidentSquare, reflectedSquare, startButton, textMesh;
	public int size;
	public float distance, L, T, A;
	private float startTime;
	public Slider periodSlider, wavelengthSlider, amplitudeSlider;
	public Text periodText, wavelengthText, amplitudeText, sizeText, distanceText;
	private List<SquareInfo> squaresList = new List<SquareInfo>();
	private List<SquareInfo> incidentSquareList = new List<SquareInfo>();
	private List<SquareInfo> reflectedSquareList = new List<SquareInfo>();
	private List<Transform> nodesAntinodesList = new List<Transform>();
	private bool isSimulatingIncident, isSimulatingReflected, isSimulatingMain=false;
	SquareInfo SpawnSquare(int i, GameObject square) {
		GameObject clone = Instantiate(square);
		clone.transform.position = new Vector3(i*distance, 0f, 0f);
		clone.transform.rotation = Quaternion.identity;
		SquareInfo currentInfo = clone.GetComponent<SquareInfo>();
		clone.transform.position = new Vector3(clone.transform.position.x, 0f, 0f);
		return currentInfo;
	}
	public void StartSimulation() {
		size = int.Parse(sizeText.text);
		distance = float.Parse(distanceText.text); //This is considered slow but it works
		Destroy(sizeText.gameObject.transform.parent.gameObject);
		Destroy(distanceText.gameObject.transform.parent.gameObject); //This is so weird it's funny
		Destroy(startButton);
		startTime = Time.time;
		for(int i=0; i<size; i++) {
			SquareInfo mainInfo = SpawnSquare(i, square);
			SquareInfo incidentInfo = SpawnSquare(i, incidentSquare);
			SquareInfo reflectedInfo = SpawnSquare(i, reflectedSquare);
			squaresList.Add(mainInfo);
			incidentSquareList.Add(incidentInfo);
			reflectedSquareList.Add(reflectedInfo);
			incidentInfo.gameObject.SetActive(false);
			reflectedInfo.gameObject.SetActive(false);
		}
		DrawNodesAntinodes();
		ShowHideNodesAntinodes();
		isSimulatingMain = true;
	}
	void Update() {
		if(isSimulatingMain) {
		foreach(SquareInfo info in squaresList) {
			info.A = 2f*A*Mathf.Cos(Mathf.PI/L*(2f*info.transform.position.x+L/2f));
			float y = info.A*Mathf.Sin((Time.time-startTime)*2f*Mathf.PI/T);
			info.transform.position = new Vector3(info.transform.position.x, y, 0f);
		}
		}
		if(isSimulatingIncident) {
		foreach(SquareInfo info in incidentSquareList) {
			float y = A*Mathf.Sin(2f*Mathf.PI*((Time.time-startTime)/T-(size*distance-info.transform.position.x)/L)+Mathf.PI/2f);
			info.transform.position = new Vector3(info.transform.position.x, y, 0f);
		}
		}
		if(isSimulatingReflected) {
		foreach(SquareInfo info in reflectedSquareList) {
			float y = A*Mathf.Sin(2f*Mathf.PI*((Time.time-startTime)/T-(info.transform.position.x)/L)+Mathf.PI+Mathf.PI/2f);
			//I am not sure why I need to add Mathf.PI/2f here and for the incident wave, but otherwise it will not work
			info.transform.position = new Vector3(info.transform.position.x, y, 0f);
		}
		}
	}
	void DrawNodesAntinodes() {
		foreach(Transform current in nodesAntinodesList) {
			Destroy(current.gameObject, 0.01f);
		}
		nodesAntinodesList = new List<Transform>();
		int i=0;
		while(true) {
			if(2f*i*L/4f>size*distance||(2f*i+1)*L/4f>size*distance) {
				break;
			}
			GameObject clone = Instantiate(textMesh);
			clone.transform.position = new Vector3((2f*i)*L/4f, -0.5f, 0f);
			GameObject clone2 = Instantiate(textMesh);
			clone2.transform.position = new Vector3((2f*i+1)*L/4f, -0.5f, 0f);
			clone.GetComponent<TextMesh>().text = "N" + (i+1) + "\n" + clone.transform.position.x.ToString("0.00") + " m";
			clone2.GetComponent<TextMesh>().text = "V" + (i+1) + "\n" + clone2.transform.position.x.ToString("0.00") + " m";
			nodesAntinodesList.Add(clone.transform);
			nodesAntinodesList.Add(clone2.transform);
			i++;
		}
	}
	public void ShowHideNodesAntinodes() {
		foreach(Transform current in nodesAntinodesList) {
			current.gameObject.SetActive(!current.gameObject.activeSelf);
		}
	}
	public void UpdateParameters(string whatToUpdate) {
		switch (whatToUpdate) {
			case "T":
			T = periodSlider.value;
			periodText.text = T.ToString("0.00") + " s";
			break;
			case "L":
			L = wavelengthSlider.value;
			wavelengthText.text = L.ToString("0.00") + " m";
			if(nodesAntinodesList[0].gameObject.activeSelf) { //This is a weird way of checking if the nodes or antinodes text is visible
			DrawNodesAntinodes();
			}
			break;
			case "A":
			A = amplitudeSlider.value;
			amplitudeText.text = A.ToString("0.00") + " m";
			break;
		}
	}
	public void ShowHideSquares(string whatWave) {
		switch (whatWave) {
			case "I":
			isSimulatingIncident = !isSimulatingIncident;
			foreach(SquareInfo info in incidentSquareList) {
				info.gameObject.SetActive(!info.gameObject.activeSelf);
			}
			break;
			case "R":
			isSimulatingReflected = !isSimulatingReflected;
			foreach(SquareInfo info in reflectedSquareList) {
				info.gameObject.SetActive(!info.gameObject.activeSelf);
			}
			break;
			case "S":
			isSimulatingMain = !isSimulatingMain;
			foreach(SquareInfo info in squaresList) {
				info.gameObject.SetActive(!info.gameObject.activeSelf);
			}
			break;
		}
	}
}

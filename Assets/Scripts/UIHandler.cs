using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

	private GameMasterScript gmScript;

	[SerializeField]
	private GameObject startMenu;

	[SerializeField]
	private Slider diffSlider;

	[SerializeField]
	private Text diffText;

	private int difficulty = 6;

	// Use this for initialization
	void Start () {
		gmScript = GetComponent<GameMasterScript>();
		diffSlider.onValueChanged.AddListener(delegate {changeDifficulty();});

	}
	
	// Update is called once per frame
	void Update () {
	

		//difficulty = (int)diffSlider.value;
		//print(difficulty);
	}

	public void startGame() {
		gmScript.startGame(difficulty);
		startMenu.SetActive(false);
	}

	public void changeDifficulty() {
		difficulty = (int)diffSlider.value;
		diffText.text = "" + difficulty;
		//print(difficulty);
	}


}

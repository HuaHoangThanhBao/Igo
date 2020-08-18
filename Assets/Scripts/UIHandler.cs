using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour {

	private GameMasterScript gmScript;

	private int difficulty = -1;

	void Start () {
		gmScript = GetComponent<GameMasterScript>();

		if(SaveData.GetLevel() == 0)
		{
			difficulty = 3;
		}
		else if (SaveData.GetLevel() == 1)
		{
			difficulty = 6;
		}
		else if (SaveData.GetLevel() == 2)
		{
			difficulty = 12;
		}
		gmScript.startGame(difficulty);
	}
}

using UnityEngine;
using System.Collections;

public class PieceScript : MonoBehaviour {
	private enum PieceRotation {
		White = 0,
		Black = 180
	}

	[SerializeField]
	private PieceColor currentColor;

	private Animator mAnim;

	private GameMasterScript gmScript;

	void Start () {
		mAnim = GetComponent<Animator>();
		gmScript = GameObject.Find("Main Camera").GetComponent<GameMasterScript>();
	}

	//Set color cho cờ
	public void setColor(PieceColor color) {
		currentColor = color;
	}

	//Thực hiện animation Flip
	public PieceColor Flip() {
		if (currentColor == PieceColor.White) {
			mAnim.SetTrigger("Wht2BlkFlip");
			currentColor = PieceColor.Black;
			gmScript.addBlack();
		}
		else {
			mAnim.SetTrigger("Blk2WhtFlip");
			currentColor = PieceColor.White;
			gmScript.addWhite();
		}
		return currentColor;
	}
}

public enum PieceColor
{
	Null,
	White,
	Black
}

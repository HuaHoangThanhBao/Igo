using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMasterScript : MonoBehaviour {

	[SerializeField]
	private PieceColor[,] board;

	[SerializeField]
	private GameObject[,] boardObjects;
	private const int BOARD_SIZE = 7;


	[SerializeField]
	private GameObject whitePiece;
	[SerializeField]
	private GameObject blackPiece;
	[SerializeField]
	private GameObject dummyPiece;

	private int whiteScore = 0;
	private int blackScore = 0;

	private enum turn {
		player,
		AI
	};

	private turn currentTurn;

	private enum gameState {
		Menu, 
		Playing,
		End
	};

	private gameState currentState;

	private MiniMax AIscript;

	private int AIDiff;

	[SerializeField]
	private GameObject playingCanvas;

	[SerializeField]
	private GameObject endCanvas;

	[SerializeField]
	private Text currentTurnText;

    private void Awake()
	{
		currentTurnText.text = "Your Turn (Black)";
	}

    void Start () {

		//Khởi tạo mảng rỗng (các trạng thái của ô cờ)
		board = new PieceColor[,] {
			{PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null}, 
			{PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null}, 
			{PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null}, 
			{PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.White, PieceColor.Black, PieceColor.Null, PieceColor.Null, PieceColor.Null}, 
			{PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Black, PieceColor.White, PieceColor.Null, PieceColor.Null, PieceColor.Null}, 
			{PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null}, 
			{PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null}, 
			{PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null, PieceColor.Null}
		};


		//Khởi tạo mảng rỗng (các object)
		boardObjects = new GameObject[,] {
			{null, null, null, null, null, null, null, null},
			{null, null, null, null, null, null, null, null},
			{null, null, null, null, null, null, null, null},
			{null, null, null, null, null, null, null, null},
			{null, null, null, null, null, null, null, null},
			{null, null, null, null, null, null, null, null},
			{null, null, null, null, null, null, null, null},
			{null, null, null, null, null, null, null, null}
		};

		//Khởi tạo các quân cờ ban đầu (2 trắng, 2 đen)
		for(int j = 0; j <= BOARD_SIZE; j++) {
			for (int i = 0; i <= BOARD_SIZE; i++) {
				if (board[i, j] == PieceColor.Black) {
					boardObjects[i,j] = (GameObject) Instantiate(blackPiece, new Vector3(i, 1, j), Quaternion.identity);
					blackScore++;
					//print (i + ", " + j);
				}
				else if (board[i, j] == PieceColor.White) {
					boardObjects[i, j] = (GameObject) Instantiate(whitePiece, new Vector3(i, 1, j), Quaternion.identity);
					whiteScore++;
				}
			}
		}

		currentTurn = turn.player;
		currentState = gameState.Menu;

		playingCanvas.SetActive(false);
		endCanvas.SetActive(false);

		AIscript = GetComponent<MiniMax>();

	}
	
	void Update () {

		//Game chưa kết thúc
		if (currentState == gameState.Playing) {

			if (currentTurn == turn.player && GameObject.FindGameObjectsWithTag("Dummy").Length == 0) {
				isGameOver();
			}

			if (Input.GetMouseButtonDown(0) && currentTurn == turn.player) {
				GameObject[] allDummy = GameObject.FindGameObjectsWithTag("Dummy");
				foreach(GameObject gm in allDummy) {
					Destroy(gm);
				}

				Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				getClick(pos.x, pos.z);
			}

			//if (Input.GetKeyDown(KeyCode.M)) {
			//	doEnemyMove();
			//}
		}
	}

	/// <summary>
	/// Thực hiện đặt cờ tại tọa độ x, y
	/// </summary>
	void getClick(float x, float y) {

		int newX = Mathf.RoundToInt(x);
		int newY = Mathf.RoundToInt(y);
		//Khai báo hướng xung quanh ô hiện tại
		DirectionalPieces dPiece = isValidMove(newX, newY, PieceColor.Black, board);
		//Nếu 1 trong 8 hướng có ô khác màu (có thể ăn) thì đặt cờ
		if (dPiece.down || dPiece.left || dPiece.up || dPiece.right || dPiece.leftDown || dPiece.leftUp || dPiece.rightDown || dPiece.rightUp) 
		{
			//Cập nhật turn cho bot
			currentTurn = turn.AI;
			//Khai báo tọa độ
			Vector3 spawnPos = new Vector3(newX, 1, newY);
			//Instantiate cờ và gán vào mảng boardObjects
			boardObjects[newX, newY] = (GameObject) Instantiate(blackPiece, spawnPos, Quaternion.identity);
			//Gán màu vào mảng board
			board[newX, newY] = PieceColor.Black;
			//Cộng điểm cho player
			blackScore++;
			//Đặt cờ
			doFlips(newX, newY, PieceColor.Black, dPiece);
			//Set text
			currentTurnText.text = "AI Turn (White)";
			//Chuyển lượt cho bot
			StartCoroutine(enemyMove());
		}
	}

	IEnumerator enemyMove() {
		yield return new WaitForSeconds(2);
		doEnemyMove();
	}

	void doEnemyMove() 
	{
		//1 = 1 depth, chưa thông minh
		//2 = 1 depth, bắt đầu thông minh
		//10 = 5 depth, rất thông minh
		move bestMove;

		if (AIDiff % 2 == 0) {
			bestMove = AIscript.miniMaxBetter(board, Mathf.CeilToInt(AIDiff/2.0F), true, -99999, 99999, 0, 0);
		}
		else {
			bestMove = AIscript.miniMax(board, Mathf.CeilToInt(AIDiff/2.0F), true, -99999, 99999, 0, 0);
		}

		if (bestMove.i != -1 && bestMove.j != -1)
		{
			//Cập nhật turn cho player
			currentTurn = turn.player;
			//Khai báo hướng xung quanh ô hiện tại
			DirectionalPieces dPiece = isValidMove(bestMove.i, bestMove.j, PieceColor.White, board);
			//Instantiate cờ và gán vào mảng boardObjects
			boardObjects[bestMove.i, bestMove.j] = (GameObject) Instantiate(whitePiece, new Vector3(bestMove.i, 1, bestMove.j), Quaternion.identity);
			//Gán màu vào mảng board
			board[bestMove.i, bestMove.j] = PieceColor.White;
			//Cộng điểm cho bot
			whiteScore++;
			//Đặt cờ
			doFlips(bestMove.i, bestMove.j, PieceColor.White, dPiece);
			//Set text
			currentTurnText.text = "Your Turn (Black)";
			//Nếu ko còn nước đặt thì kết thúc game
			if (isGameOver()) 
			{
				print("PLAYER CAN'T MOVE!");
				endGame();
			}

		}
		else 
		{
			print(bestMove.value + ", " + bestMove.i + ", " + bestMove.j);
			print("GAME OVER I CAN'T MOVE");
			endGame();
		}
	}


	//Kiểm tra nếu đặt quân cờ với màu truyền vào tại vị trí x, y, z có hợp lệ hay ko? (xét 8 ô xung quanh ô hiện tại)
	public DirectionalPieces isValidMove(int x, int y, PieceColor thisColor, PieceColor[,] board) {

		DirectionalPieces dPieces;

		dPieces.left = false;
		dPieces.right = false;
		dPieces.up = false;
		dPieces.down = false;
		dPieces.leftDown = false;
		dPieces.leftUp = false;
		dPieces.rightDown = false;
		dPieces.rightUp = false;
		dPieces.points = 0;

		PieceColor otherColor = PieceColor.Null;

		if (thisColor == PieceColor.Black) otherColor = PieceColor.White;
		if (thisColor == PieceColor.White) otherColor = PieceColor.Black;

		if (otherColor == PieceColor.Null) print("ERROR");

		int tempPoints = 0;

		//Nếu ô hiện tại rỗng
		if (x >= 0 && x <= BOARD_SIZE && y >= 0 && y <= BOARD_SIZE && board[x,y] == PieceColor.Null) 
		{
			//Phía trên
			if (x >= 1) {
				if (board[x-1, y] == otherColor) {
					
					//Tìm cờ đen để ăn
					for (int i = x-1; i >= 0; i--) {
						tempPoints++;
						if (board[i, y] == PieceColor.Null) break;
						if (board[i, y] == thisColor) {
							dPieces.up = true;
							dPieces.points+= tempPoints;
							tempPoints = 0;
							break;
						}
					}
				}
			}

			//Phía dưới
			if (x <= BOARD_SIZE-1) {
				if (board[x+1, y] == otherColor) {

					//Tìm cờ đen để ăn
					for (int i = x+1; i <= BOARD_SIZE; i++) {
						tempPoints++;
						if (board[i, y] == PieceColor.Null) break;
						if (board[i, y] == thisColor) {
							dPieces.down = true;
							dPieces.points+= tempPoints;
							tempPoints = 0;
							break;
						}
					}

				}
			}

			//Bên trái
			if (y >= 1) {
				if (board[x, y-1] == otherColor) {

					//Tìm cờ đen để ăn
					for (int i = y-1; i >= 0; i--) {
						tempPoints++;
						if (board[x, i] == PieceColor.Null) break;
						if (board[x, i] == thisColor) {
							dPieces.left = true;
							dPieces.points+= tempPoints;
							tempPoints = 0;
							break;
						}
					}

				}
			}

			//Bên phải
			if (y <= BOARD_SIZE-1) {
				if (board[x, y+1] == otherColor) {

					//Tìm cờ đen để ăn
					for (int i = y+1; i <= BOARD_SIZE; i++) {
						tempPoints++;
						if (board[x, i] == PieceColor.Null) break;
						if (board[x, i] == thisColor) {
							dPieces.right = true;
							dPieces.points+= tempPoints;
							tempPoints = 0;
							break;
						}
					}

				}
			}

			//Bên trái - phía trên
			if (y >= 1 && x >= 1) {
				if (board[x-1, y-1] == otherColor) {

					//Tìm cờ đen để ăn
					int i = x-1;
					int j = y-1;
					while (i >= 0 && j >= 0) {
						tempPoints++;
						if (board[i, j] == PieceColor.Null) break;
						if (board[i, j] == thisColor) {
							dPieces.leftUp = true;
							dPieces.points+= tempPoints;
							tempPoints = 0;
							break;
						}
						i--;
						j--;
					}
				}
			}

			//Bên phải - phía dưới
			if (y <= BOARD_SIZE-1 && x <= BOARD_SIZE-1) {
				if (board[x+1, y+1] == otherColor) {

					//Tìm cờ đen để ăn
					int i = x+1;
					int j = y+1;
					while (i != BOARD_SIZE && j != BOARD_SIZE) {
						tempPoints++;
						if (board[i, j] == PieceColor.Null) break;
						if (board[i, j] == thisColor) {
							dPieces.rightDown = true;
							dPieces.points+= tempPoints;
							tempPoints = 0;
							break;
						}
						i++;
						j++;
					}
				}
			}

			//Bên phải - phía trên
			if (y <= BOARD_SIZE-1 && x >= 1) {
				if (board[x-1, y+1] == otherColor) {

					//Tìm cờ đen để ăn
					int i = x-1;
					int j = y+1;
					while (i >= 0 && j != BOARD_SIZE) {
						tempPoints++;
						if (board[i, j] == PieceColor.Null) break;
						if (board[i, j] == thisColor) {
							dPieces.rightUp = true;
							dPieces.points+= tempPoints;
							tempPoints = 0;
							break;
						}
						i--;
						j++;
					}
				}
			}

			//Bên trái - phía dưới
			if (y >= 1 && x <= BOARD_SIZE-1) {
				if (board[x+1, y-1] == otherColor) {

					//Tìm cờ đen để ăn
					int i = x+1;
					int j = y-1;
					while (i != BOARD_SIZE && j >= 0) {
						tempPoints++;
						if (board[i, j] == PieceColor.Null) break;
						if (board[i, j] == thisColor) {
							dPieces.leftDown = true;
							dPieces.points+= tempPoints;
							tempPoints = 0;
							break;
						}
						i++;
						j--;
					}
				}
			}
		}
			
		return dPieces;
	}

	//Thực hiện lật cờ cùng màu
	void doFlips(int x, int y, PieceColor thisColor, DirectionalPieces dPieces) {

		PieceColor otherColor = PieceColor.Null;

		if (thisColor == PieceColor.Black) otherColor = PieceColor.White;
		if (thisColor == PieceColor.White) otherColor = PieceColor.Black;

		if (otherColor == PieceColor.Null) print("ERROR");

		if (dPieces.up) {
			for (int i = x-1; i >= 0; i--) {
				if (board[i, y] == thisColor || board[i, y] == PieceColor.Null) break;
				else board[i, y] = boardObjects[i,y].GetComponentInChildren<PieceScript>().Flip();
			}
		}

		if (dPieces.down) {
			for (int i = x+1; i <= BOARD_SIZE; i++) {
				if (board[i, y] == thisColor || board[i, y] == PieceColor.Null) break;
				else board[i, y] = boardObjects[i,y].GetComponentInChildren<PieceScript>().Flip();
			}
		}

		if (dPieces.left) {
			for (int i = y-1; i > 0; i--) {
				if (board[x, i] == thisColor || board[x, i] == PieceColor.Null) break;
				else board[x, i] = boardObjects[x,i].GetComponentInChildren<PieceScript>().Flip();
			}
		}

		if (dPieces.right) {
			for (int i = y+1; i <= BOARD_SIZE; i++) {
				if (board[x, i] == thisColor || board[x, i] == PieceColor.Null) break;
				else board[x, i] = boardObjects[x,i].GetComponentInChildren<PieceScript>().Flip();
			}
		}

		if (dPieces.leftUp) {
			int i = x-1;
			int j = y-1;
			while (i >= 0 && j >= 0) {
				if (board[i, j] == thisColor || board[i, j] == PieceColor.Null) break;
				else board[i, j] = boardObjects[i,j].GetComponentInChildren<PieceScript>().Flip();
				i--;
				j--;
			}
		}

		if (dPieces.rightDown) {
			int i = x+1;
			int j = y+1;
			while (i != BOARD_SIZE && j != BOARD_SIZE) {
				if (board[i, j] == thisColor || board[i, j] == PieceColor.Null) break;
				else board[i, j] = boardObjects[i,j].GetComponentInChildren<PieceScript>().Flip();
				i++;
				j++;
			}
		}

		if (dPieces.rightUp) {
			int i = x-1;
			int j = y+1;
			while (i >= 0 && j != BOARD_SIZE) {
				if (board[i, j] == thisColor || board[i, j] == PieceColor.Null) break;
				else board[i, j] = boardObjects[i,j].GetComponentInChildren<PieceScript>().Flip();
				i--;
				j++;
			}
		}

		if (dPieces.leftDown) {
			int i = x+1;
			int j = y-1;
			while (i != BOARD_SIZE && j >= 0) {
				if (board[i, j] == thisColor || board[i, j] == PieceColor.Null) break;
				else board[i, j] = boardObjects[i,j].GetComponentInChildren<PieceScript>().Flip();
				i++;
				j--;
			}
		}

	}

	/// <summary>
	/// Kiểm tra kết thúc game
	/// </summary>
	/// <returns></returns>
	bool isGameOver() {

		bool moveFound = true;
		for(int j = 0; j <= BOARD_SIZE; j++) {
			for (int i = 0; i <= BOARD_SIZE; i++) {
				DirectionalPieces dPiece = isValidMove(i, j, PieceColor.Black, board);
				if (dPiece.down || dPiece.left || dPiece.up || dPiece.right || dPiece.leftDown || dPiece.leftUp || dPiece.rightDown || dPiece.rightUp) {
					moveFound = false;
					Instantiate(dummyPiece, new Vector3(i, 1, j), Quaternion.identity);
				}
			}
		}
		return moveFound;
	}

	//Tính điểm cho quân đen
	public void addBlack() {
		blackScore++;
		whiteScore--;
	}

	//Tính điểm cho quân trắng
	public void addWhite() {
		whiteScore++;
		blackScore--;
	}

	//Khởi tạo game
	public void startGame(int difficulty) {
		AIDiff = difficulty;
		currentState = gameState.Playing;
		playingCanvas.SetActive(true);
		isGameOver();
		print("Starting game with difficulty: " + difficulty);
	}

	//Restart game
	public void restartGame() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	//Thoát game
	public void quitGame() {
		Application.Quit();
	}

	//Kết thúc game
	void endGame() {
		currentState = gameState.End;
		playingCanvas.SetActive(false);
		endCanvas.SetActive(true);
		if (whiteScore > blackScore) {
			endCanvas.transform.GetChild(1).GetComponent<Text>().text = "You Lose...";
		}
		print ("final score: BLACK: " + blackScore + " WHITE: " + whiteScore);
	}

	//Vẽ 3 khung label + set text cho chúng
	void OnGUI() {
		if (currentState == gameState.Playing || currentState == gameState.End)
		{
			GUI.Label(new Rect(20, 20, 200, 100), "Black: " + blackScore);
			GUI.Label(new Rect(20, 40, 200, 100), "White: " + whiteScore);
			GUI.Label(new Rect(20, 60, 200, 100), "AI Difficulty: " + AIDiff);
		}
	}
}

//This holds which 8 directions (if any) pieces will be flipped. the class name is terrible
public struct DirectionalPieces {
	public bool left;
	public bool right;
	public bool up;
	public bool down;
	public bool leftUp;
	public bool rightUp;
	public bool leftDown;
	public bool rightDown;
	public int points;
}

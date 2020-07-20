using UnityEngine;
using System.Collections;

public class MiniMax : MonoBehaviour {

	private int[,] boardPoints;

	private const int BOARD_SIZE = 7;

	private GameMasterScript gmScript;

	private int[,] currentBoard;

	void Start () {
		//Mảng chứa các phần tử cho AI, số cao hơn cho vị trí đặt tốt hơn.
		boardPoints = new int[,] {
			{5, 1, 4, 4, 4, 4, 1, 5},
			{1, 1, 2, 2, 2, 2, 1, 1},
			{4, 2, 3, 3, 3, 3, 2, 4},
			{4, 2, 3, 3, 3, 3, 2, 4},
			{4, 2, 3, 3, 3, 3, 2, 4},
			{4, 2, 3, 3, 3, 3, 2, 4},
			{1, 1, 2, 2, 2, 2, 1, 1},
			{5, 1, 4, 4, 4, 4, 1, 5}
		};

		gmScript = GetComponent<GameMasterScript>();
	}

	//Hàm minimax cải tiến
	public move miniMaxBetter(PieceColor [,] currentBoard, int depth, bool maximizingPlayer, int alpha, int beta, int i2, int j2) {

		if (depth == 0 && maximizingPlayer) {
			move endMove;
			endMove.value = 0;
			endMove.i = i2;
			endMove.j = j2;
			endMove.points = 0;
			return endMove;
		}
		if (depth == 0 && !maximizingPlayer) {
			move endMove;
			endMove.value = 0;
			endMove.i = i2;
			endMove.j = j2;
			endMove.points = 0;
			return endMove;
		}

		//Nếu là bot
		if (maximizingPlayer) {
			move bestMove;
			bestMove.value = -99999;
			bestMove.i = -1;
			bestMove.j = -1;
			bestMove.points = -1;
			for (int j = 0; j <= BOARD_SIZE; j++) {
				for (int i = 0; i <= BOARD_SIZE; i++)
				{
					//Khai báo hướng xung quanh ô hiện tại
					DirectionalPieces dPiece =  gmScript.isValidMove(i, j, PieceColor.White, currentBoard);
					//Nếu 1 trong 8 hướng có ô khác màu (có thể ăn) thì đặt cờ
					if (dPiece.down || dPiece.left || dPiece.up || dPiece.right || dPiece.leftDown || dPiece.leftUp || dPiece.rightDown || dPiece.rightUp)
					{
						//Gán màu vào mảng board
						currentBoard[i, j] = PieceColor.White;
						//Quay lui tìm nước đi tốt nhất
						move currentMove = miniMaxBetter(currentBoard, depth-1, false, alpha, beta, i, j);
						//Tính tổng điểm trên mỗi nước đi
						currentMove.value += boardPoints[i, j];
						//Tính tổng giá trị trên mỗi bước đi
						currentMove.points += dPiece.points;

						if (currentMove.value > bestMove.value) {
							bestMove.value = currentMove.value;
							bestMove.i = i;
							bestMove.j = j;
							bestMove.points = currentMove.points;

						}
						else if (currentMove.value == bestMove.value){
							if (currentMove.points > bestMove.points) {
								bestMove.value = currentMove.value;
								bestMove.i = i;
								bestMove.j = j;
								bestMove.points = currentMove.points;
							}
						}
						//Trả ô đã đi thử về trạng thái ban đầu
						currentBoard[i, j] = PieceColor.Null;

						//alpha beta pruning
						alpha = Mathf.Max(alpha, bestMove.value);

						if (beta <= alpha) {
							return bestMove;
						}
					}
				}
			}
			return bestMove;
		}

		//Nếu là player
		else {
			move bestMove;
			bestMove.value = 99999;
			bestMove.i = -1;
			bestMove.j = -1;
			bestMove.points = -1;
			for (int j = 0; j <= BOARD_SIZE; j++) {
				for (int i = 0; i <= BOARD_SIZE; i++)
				{
					//Khai báo hướng xung quanh ô hiện tại
					DirectionalPieces dPiece =  gmScript.isValidMove(i, j, PieceColor.Black, currentBoard);
					//Nếu 1 trong 8 hướng có ô khác màu (có thể ăn) thì đặt cờ
					if (dPiece.down || dPiece.left || dPiece.up || dPiece.right || dPiece.leftDown || dPiece.leftUp || dPiece.rightDown || dPiece.rightUp)
					{
						//Gán màu vào mảng board
						currentBoard[i, j] = PieceColor.Black;
						//Quay lui tìm nước đi tốt nhất
						move currentMove = miniMaxBetter(currentBoard, depth-1, true, alpha, beta, i, j);
						//Tính tổng điểm trên mỗi nước đi
						currentMove.value -= boardPoints[i, j];
						//Tính tổng giá trị trên mỗi nước đi
						currentMove.points -= dPiece.points;

						if (currentMove.value < bestMove.value) {
							bestMove.value = currentMove.value;
							bestMove.i = i;
							bestMove.j = j;
							bestMove.points = currentMove.points;

						}
						else if (currentMove.value == bestMove.value){
							if (currentMove.points < bestMove.points) {
								bestMove.value = currentMove.value;
								bestMove.i = i;
								bestMove.j = j;
								bestMove.points = currentMove.points;
							}
						}
						//Trả ô đã đi thử về trạng thái ban đầu
						currentBoard[i, j] = PieceColor.Null;

						//alpha beta pruning
						beta = Mathf.Min(beta, bestMove.value);

						if (beta <= alpha) {
							return bestMove;
						}
					}
				}
			}
			return bestMove;
		}
	}


	public move miniMax(PieceColor [,] currentBoard, int depth, bool maximizingPlayer, int alpha, int beta, int i2, int j2) {

		if (depth == 0 && maximizingPlayer) {
			move endMove;
			endMove.value = 0;
			endMove.i = i2;
			endMove.j = j2;
			endMove.points = 0;
			return endMove;
		}
		if (depth == 0 && !maximizingPlayer) {
			move endMove;
			endMove.value = 0;
			endMove.i = i2;
			endMove.j = j2;
			endMove.points = 0;
			return endMove;
		}

		//AI bot
		if (maximizingPlayer) {
			move bestMove;
			bestMove.value = -99999;
			bestMove.i = -1;
			bestMove.j = -1;
			bestMove.points = -1;
			for (int j = 0; j <= BOARD_SIZE; j++) 
			{
				for (int i = 0; i <= BOARD_SIZE; i++)
				{
					//Khai báo hướng xung quanh ô hiện tại
					DirectionalPieces dPiece =  gmScript.isValidMove(i, j, PieceColor.White, currentBoard);
					//Nếu 1 trong 8 hướng có ô khác màu (có thể ăn) thì đặt cờ
					if (dPiece.down || dPiece.left || dPiece.up || dPiece.right || dPiece.leftDown || dPiece.leftUp || dPiece.rightDown || dPiece.rightUp)
					{
						//Gán màu vào mảng board
						currentBoard[i, j] = PieceColor.White;
						//Quay lui tìm nước đi tốt nhất
						move currentMove = miniMax(currentBoard, depth-1, false, alpha, beta, i, j);
						//Tính tổng điểm trên mỗi nước đi
						currentMove.value += boardPoints[i, j];

						if (currentMove.value > bestMove.value) {
							bestMove.value = currentMove.value;
							bestMove.i = i;
							bestMove.j = j;

						}
						//Trả ô đã đi thử về trạng thái ban đầu
						currentBoard[i, j] = PieceColor.Null;

						//alpha beta pruning
						alpha = Mathf.Max(alpha, bestMove.value);

						if (beta <= alpha) {
							return bestMove;
						}
					}
				}
			}
			return bestMove;
		}

		//Player
		else {
			move bestMove;
			bestMove.value = 99999;
			bestMove.i = -1;
			bestMove.j = -1;
			bestMove.points = 0;
			for (int j = 0; j <= BOARD_SIZE; j++) 
			{
				for (int i = 0; i <= BOARD_SIZE; i++)
				{
					//Khai báo hướng xung quanh ô hiện tại
					DirectionalPieces dPiece =  gmScript.isValidMove(i, j, PieceColor.Black, currentBoard);
					//Nếu 1 trong 8 hướng có ô khác màu (có thể ăn) thì đặt cờ
					if (dPiece.down || dPiece.left || dPiece.up || dPiece.right || dPiece.leftDown || dPiece.leftUp || dPiece.rightDown || dPiece.rightUp)
					{
						//Gán màu vào mảng board
						currentBoard[i, j] = PieceColor.Black;
						//Quay lui tìm nước đi tốt nhất
						move currentMove = miniMax(currentBoard, depth-1, true, alpha, beta, i, j);
						//Tính tổng điểm trên mỗi nước đi
						currentMove.value -= boardPoints[i, j];

						if (currentMove.value < bestMove.value) {
							bestMove.value = currentMove.value;
							bestMove.i = i;
							bestMove.j = j;
							bestMove.points = currentMove.points;

						}
						//Trả ô đã đi thử về trạng thái ban đầu
						currentBoard[i, j] = PieceColor.Null;

						//alpha beta pruning
						beta = Mathf.Min(beta, bestMove.value);

						if (beta <= alpha) {
							return bestMove;
						}
					}
				}
			}
			return bestMove;
		}

	}
}

public struct move {
	public int i;
	public int j;
	public int value;
	public int points;
}
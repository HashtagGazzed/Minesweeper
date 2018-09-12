using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour {

	public static int width = 11;
	public static int height = 11;
	public static Square[,] squares = new Square[width, height];

	public static bool tntLocation(int x, int y) {
		if (x < width && y < height && x >= 0 && y >= 0) {
			return squares[x,y].tnt;
		}
		return false;
	}

	public static int adjacentTnt(int x, int y) {
		int count = 0;
		if (tntLocation(x, y + 1)) {
			++count;
		}
		if (tntLocation(x + 1, y + 1)) {
			++count;
		}
		if (tntLocation(x + 1, y)) {
			++count;
		}
		if (tntLocation(x + 1, y - 1)) {
			++count;
		}
		if (tntLocation(x, y - 1)) {
			++count;
		}
		if (tntLocation(x - 1, y - 1)) {
			++count;
		}
		if (tntLocation(x - 1, y)) {
			++count;
		}
		if (tntLocation(x - 1, y + 1)) {
			++count;
		}
		return count;
	}

	public static void revealTnt () {
		foreach (Square square in squares) {
			if (square.tnt) {
				square.loadTexture(0);
			}
		}
	}

	public static void revealStack(int x, int y, bool[,] clicked) {
		if (x < width && y < height && x >= 0 && y >= 0) {
			if (clicked[x, y]) {
				return;
			}

			squares[x, y].loadTexture(adjacentTnt(x, y));

			if (adjacentTnt(x, y) > 0) {
				return;
			}

			clicked[x, y] = true;
			revealStack(x - 1, y, clicked);
			revealStack(x + 1, y, clicked);
			revealStack(x, y - 1, clicked);
			revealStack(x, y + 1, clicked);
		}
	}

	public static bool checkWin() {
		foreach(Square square in squares) {
			if (square.isGrass() && !square.tnt) {
				return false;
			}
		}
		return true;
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour {

	public bool tnt;

	public Sprite[] emptyTextures;
	public Sprite tntTexture;

	void Start() {
		tnt = Random.value < 0.15;
		int x = (int)transform.position.x;
		int y = (int)transform.position.y;
		Grid.squares[x, y] = this;
	}

	public void loadTexture(int adjacentCount) {
		if (tnt) {
			GetComponent<SpriteRenderer>().sprite = tntTexture;
		} else {
			GetComponent<SpriteRenderer>().sprite = emptyTextures[adjacentCount];
		}
	}

	public bool isGrass() {
		return GetComponent<SpriteRenderer>().sprite.texture.name == "master-tileset";
	}

	void OnMouseUpAsButton () {
		if (tnt) {
			Grid.revealTnt();
			print("Game Over");
		} else {
			int x = (int)transform.position.x;
			int y = (int)transform.position.y;
			loadTexture(Grid.adjacentTnt(x, y));

			Grid.revealStack(x, y, new bool[Grid.width, Grid.height]);

			if (Grid.checkWin()) {
				print("Congratulations, You Win!");
			}
		}
	}

}

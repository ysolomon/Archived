using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

	// TODO
	// 1. combat behavior


	public static GameManager gm;

	public GameObject tilePrefab;
	public GameObject playerPrefab;

	public int mapSize = 10;

	public List<List<Tile>> map = new List<List<Tile>>();

	public List <Player> players = new List <Player>();

	public int currentPlayerIndex = 0;

	void Awake() {
		gm = this;
	}

	// Use this for initialization
	void Start () {
		generateMap ();
		generateActors ();
	}
	
	// Update is called once per frame
	void Update () {
		players [currentPlayerIndex].TurnUpdate ();
	}

	void generateMap () {
		for (int i = 0; i < mapSize; i++) {
			List<Tile> rows = new List<Tile> ();
			for (int j = 0; j < mapSize; j++) {
				Tile t = ((GameObject)Instantiate (tilePrefab, new Vector3 (i - Mathf.Floor (mapSize / 2), 0, -j + Mathf.Floor (mapSize / 2)), Quaternion.Euler (new Vector3 (-90, 45, 0)))).GetComponent<Tile> ();
				t.coordinates = new Vector3(i, 0, j);
				rows.Add (t);
			}
			map.Add (rows);
		}
	}

	void generateActors() {
		Player p;
		p = ((GameObject) Instantiate (playerPrefab, new Vector3 (0 - Mathf.Floor(mapSize / 2), 0.4f, 0 + Mathf.Floor(mapSize / 2)), Quaternion.Euler(new Vector3(0, 225, 0)))).GetComponent<Player>();
		p.coordinates = new Vector3 (0, 0, 0);
		// Debug.Log (p.playerName + " is at " + p.coordinates.x + ", " + p.coordinates.z);
		players.Add (p);

		p = ((GameObject) Instantiate (playerPrefab, new Vector3 ((mapSize - 1) - Mathf.Floor(mapSize / 2), 0.4f, -(mapSize - 1) + Mathf.Floor(mapSize / 2)), Quaternion.Euler(new Vector3(0, 225, 0)))).GetComponent<Player>();
		p.coordinates = new Vector3 ((mapSize - 1), 0, (mapSize - 1));
		p.playerName = "Kyle";
		// Debug.Log (p.playerName + " is at " + p.coordinates.x + ", " + p.coordinates.z);
		players.Add (p);

		p = ((GameObject) Instantiate (playerPrefab, new Vector3 (5 - Mathf.Floor(mapSize / 2), 0.4f, -5 + Mathf.Floor(mapSize / 2)), Quaternion.Euler(new Vector3(0, 225, 0)))).GetComponent<Player>();
		p.coordinates = new Vector3 (5, 0, 5);
		p.playerName = "Lars";
		// Debug.Log (p.playerName + " is at " + p.coordinates.x + ", " + p.coordinates.z);
		players.Add (p);
	}

	public void nextTurn() {
		if (currentPlayerIndex + 1 < players.Count) {
			// Change color back at end of turn
			players [currentPlayerIndex].GetComponent<SpriteRenderer> ().color = Color.white;
			currentPlayerIndex++;
		} else {
			// end of round
			players [currentPlayerIndex].GetComponent<SpriteRenderer> ().color = Color.white;
			currentPlayerIndex = 0;
		}
	}

	// for GUIButton to call for highlight logic

	// Write recursive function
	public void highlightTiles(Tile root, int maxRange, Color color) {
		if (maxRange < 0) {
			return;
		}

		if (root.isBlocked) {
			return;
		}

		if (root.GetComponent<SpriteRenderer>().color != color && !(root.isBlocked)) {
			root.GetComponent<SpriteRenderer> ().color = color;
			root.currentColor = color;
		}

		for (int i = 0; i < root.adjacentTiles.Count; i++) {
			highlightTiles (root.adjacentTiles [i], maxRange - 1, color);
		}
		return;
	}

	public void removeHighlightTiles() {
		//implementation A
		for (int i = 0; i <= mapSize - 1; i++) {
			for (int j = 0; j <= mapSize - 1; j++) {
				if (!(map [i][j].isBlocked)) {
					map[i][j].transform.GetComponent<SpriteRenderer> ().color = Color.white;
					map[i][j].currentColor = Color.white;
				}
			}
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {

	public Vector3 coordinates = Vector3.zero;

	public List <Tile> adjacentTiles = new List<Tile> ();

	public bool isBlocked = false;

	public Color currentColor;

	// TODO
	// 1. come up with recursive structure for finding adjacent tiles
	// 

	// Use this for initialization
	void Start () {
		/** 
		 * Unity doesn't allow for the rotation of box colliders unless attached to empty game objects,
		 * whose rotation you can freely manipulate so I made tiny boxes that fit in the isometric diamonds
		 * to register clickable logic... see in editor in real time... possible work around is easily done
		 * by adding code here creating an empty game object and attaching box col to it.
		 */
		BoxCollider TileCollider = gameObject.AddComponent<BoxCollider>();
		TileCollider.size = new Vector3 (0.3f, 0.15f, 0.2f);
		TileCollider.isTrigger = true;

		currentColor = transform.GetComponent<SpriteRenderer> ().color;

		findAdjacentTiles ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseEnter() {
		transform.GetComponent<SpriteRenderer> ().color = Color.grey;
		/**
		 *Use this to make sure that the logical/local grid lines up with the 
		 *world position grid
		 */
		// Debug.Log ("(X " + coordinates.x + ", Z " + coordinates.z + ")");
	}

	void OnMouseExit() {
		transform.GetComponent<SpriteRenderer> ().color = currentColor;
	}

	void OnMouseDown() {
		if (GameManager.gm.players [GameManager.gm.currentPlayerIndex].moving) {            //moving
			Debug.Log("clicked on "  + this.coordinates);
			GameManager.gm.players [GameManager.gm.currentPlayerIndex].move (this);
		} else if (GameManager.gm.players [GameManager.gm.currentPlayerIndex].attacking) {    //attacking
			GameManager.gm.players [GameManager.gm.currentPlayerIndex].attack (this);
		} else if (Input.GetKey("b")) {                                            //block tile using left+right click, if done again on the same tile the tile is then unblocked.
			if (transform.GetComponent<SpriteRenderer> ().color == Color.magenta) {
				transform.GetComponent<SpriteRenderer> ().color = Color.white;
				currentColor = Color.white;
				isBlocked = false;
			} else {
				transform.GetComponent<SpriteRenderer> ().color = Color.magenta;
				currentColor = Color.magenta;
				isBlocked = true;
			}
		}
	}

	public bool playerOnTile(Player player) {
		return (this.coordinates == player.coordinates);
	}

	public void findAdjacentTiles() {
		// left
		if (coordinates.x > 0 ) {
			adjacentTiles.Add (GameManager.gm.map [(int)Mathf.Round (coordinates.x - 1)] [(int)Mathf.Round (coordinates.z)]);
		}
		// right
		if (coordinates.x < GameManager.gm.mapSize - 1) {
			adjacentTiles.Add(GameManager.gm.map[(int)Mathf.Round(coordinates.x + 1)][(int)Mathf.Round(coordinates.z)]);
		}
		// down
		if (coordinates.z > 0) {
			adjacentTiles.Add (GameManager.gm.map [(int)Mathf.Round (coordinates.x)] [(int)Mathf.Round (coordinates.z - 1)]);
		}
		// up
		if (coordinates.z < GameManager.gm.mapSize - 1) {
			adjacentTiles.Add(GameManager.gm.map[(int)Mathf.Round(coordinates.x)][(int)Mathf.Round(coordinates.z + 1)]);
		}
	}
}

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : Actors {
	public bool attacking = false;
	public bool moving = false;

	public float moveSpeed = 10.0f;
	public float dmgRollDice = 6; // d6

	public float dmgReduction = 0.15f;
	public float hitChance = 0.85f;
	public float baseDamage = 5;

	public int hitPoints = 30;
	// can be separated into a moveaction and regular action if needed
	public int actionPoints = 2;

	public int maxMoveRange = 3;
	public int maxAttackRange = 1;

	public string playerName = "Bob";

	public Tile playerTile;

	List<Vector3> positionQueue = new List<Vector3>();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.gm.players [GameManager.gm.currentPlayerIndex] == this) {
			this.GetComponent<SpriteRenderer> ().color = Color.green;
		}         if (hitPoints <= 0){
			this.GetComponent<SpriteRenderer>().color = Color.red;
		}
	}

	// handles turn logic
	public override void TurnUpdate() {
		//positionQueue == actorPosition
		if (positionQueue.Count > 0) {
			transform.position += (positionQueue[0] - transform.position).normalized * moveSpeed * Time.deltaTime;

			if (Vector3.Distance (positionQueue[0], transform.position) <= 0.1f) {
				transform.position = positionQueue[0];
				//Debug.Log (this.coordinates);
				positionQueue.RemoveAt(0);
			}
		}
		base.TurnUpdate ();
	}

	public void updatePlayerTile() {
		playerTile = GameManager.gm.map [(int)Mathf.Round (this.coordinates.x)] [(int)Mathf.Round (this.coordinates.z)];
	}

	public void move(Tile targetTile) {
		if (moving && targetTile.currentColor == Color.blue && !(targetTile.isBlocked)) {
			findPath (positionQueue, targetTile);
			actionPoints--;
			moving = false;
		}
		GameManager.gm.removeHighlightTiles ();
	} 

	public void attack(Tile targetTile) {
		//check if player is in range and able to attack
		if (attacking && targetTile.currentColor == Color.red) {
			Player enemy = null;
			foreach (Player p in GameManager.gm.players) {
				if (targetTile.playerOnTile(p)) {
					enemy = p;
				}
			}
			//there is an enemy..
			if (enemy != null) {
				//..if the enemy is on the targetTile..
				if (targetTile.playerOnTile(enemy)) {

					//able to hit so decrement
					actionPoints--;

					//..check if player hits/misses..
					bool hit = Random.Range (0.0f, 1.0f) <= hitChance;

					//..hit..
					if (hit) {
						//damage calculation. subject to change.
						int amountOfDamage = (int)Mathf.Floor (baseDamage + Random.Range (0, dmgRollDice));

						enemy.hitPoints -= amountOfDamage;

						Debug.Log (playerName + " successfully hit " + enemy.playerName + " for " + amountOfDamage + " damage");
					} else {
						Debug.Log (playerName + " missed " + enemy.playerName);
					} 
				} 
			} else {
				Debug.Log ("not an enemy");
			}
		} else {
			Debug.Log ("invalid target");
		}
		GameManager.gm.removeHighlightTiles ();
	}

	public void findPath(List<Vector3> path, Tile targetTile) {
		if (playerTile.coordinates == targetTile.coordinates) {
			return;
		}

		float minDistance = Vector3.Distance (playerTile.coordinates, targetTile.coordinates);
		Vector3 nextWorldPosition = playerTile.transform.position;
		Vector3 nextGridPosition = playerTile.coordinates;

		foreach (Tile t in playerTile.adjacentTiles) {
			//is the position not already in the path list, and is it not blocked
			if (!path.Contains (t.transform.position) && !t.isBlocked) {
				//which tile is closest target tile
				if (Vector3.Distance (t.coordinates, targetTile.coordinates) < minDistance) {
					//update
					minDistance = Vector3.Distance (t.coordinates, targetTile.coordinates);
					nextWorldPosition = t.transform.position;
					nextGridPosition = t.coordinates;
				}
			}
		}

		path.Add (nextWorldPosition + 0.4f * Vector3.up);
		GameManager.gm.players [GameManager.gm.currentPlayerIndex].coordinates = nextGridPosition;
		GameManager.gm.players [GameManager.gm.currentPlayerIndex].updatePlayerTile();
		findPath (path, targetTile);
	}
}

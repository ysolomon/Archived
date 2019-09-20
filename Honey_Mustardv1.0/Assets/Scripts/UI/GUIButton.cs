using UnityEngine;
//using UnityEngine.UI;
using System.Collections;

public class GUIButton : MonoBehaviour {

	void Awake() {
		//buttonPrefab = GetComponent<Button> ();
	}

	// Use this for initialization
	void Start () {
		//GameObject moveButton = Instantiate (buttonPrefab);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void MoveButton() {
		if (GameManager.gm.players[GameManager.gm.currentPlayerIndex].actionPoints > 1) {
			GameManager.gm.removeHighlightTiles ();
			GameManager.gm.players[GameManager.gm.currentPlayerIndex].moving = true;
			GameManager.gm.players[GameManager.gm.currentPlayerIndex].attacking = false;
			// highlight logic here
			GameManager.gm.players[GameManager.gm.currentPlayerIndex].updatePlayerTile();
			GameManager.gm.highlightTiles (GameManager.gm.players[GameManager.gm.currentPlayerIndex].playerTile, GameManager.gm.players[GameManager.gm.currentPlayerIndex].maxMoveRange, Color.blue);
		}
	}

	public void AttackButton() {
		if (GameManager.gm.players[GameManager.gm.currentPlayerIndex].actionPoints > 0) {
			GameManager.gm.removeHighlightTiles ();
			GameManager.gm.players[GameManager.gm.currentPlayerIndex].moving = false;
			GameManager.gm.players[GameManager.gm.currentPlayerIndex].attacking = true;
			GameManager.gm.players[GameManager.gm.currentPlayerIndex].updatePlayerTile();
			GameManager.gm.highlightTiles (GameManager.gm.players[GameManager.gm.currentPlayerIndex].playerTile, GameManager.gm.players[GameManager.gm.currentPlayerIndex].maxAttackRange, Color.red);
		}
	}

	public void EndTurnButton() {
		GameManager.gm.players[GameManager.gm.currentPlayerIndex].moving = false;
		GameManager.gm.players[GameManager.gm.currentPlayerIndex].attacking = false;
		GameManager.gm.players[GameManager.gm.currentPlayerIndex].actionPoints = 2;
		Debug.Log (GameManager.gm.players[GameManager.gm.currentPlayerIndex].playerName + "'s turn is over");
		GameManager.gm.removeHighlightTiles ();
		GameManager.gm.nextTurn ();
	}

	/*public void setButtonName(string newName) {
		buttonPrefab.name = newName;
	}

	public void setButtonText(string newText) {
		buttonPrefab.GetComponentInChildren<Text> ().text = newText;
	}*/
}

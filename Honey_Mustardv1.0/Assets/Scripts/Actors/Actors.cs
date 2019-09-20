using UnityEngine;
using System.Collections;

public class Actors : MonoBehaviour {

	// parent class to all actors in game, player, AI, etc...

	// coordinates is how logical/local subjects will be mapped
	public Vector3 coordinates = Vector3.zero;
	// world position
	public Vector3 actorPosition;

	void Awake() {
		actorPosition = transform.position;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual void TurnUpdate() {}

	//public virtual void TurnMenu() {}
}

using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour {
	private Character thisCharacter;
	private Character target;

	private Vector3 originalPosition;
	private Vector3 currentPosition;
	private Vector3 targetPosition;
	private Vector3 patrolPosition1;
	private Vector3 patrolPosition2;
	private Vector3 patrolPosition3;
	private Vector3 patrolPosition4;

	private float detectRange = 5.0f;
	private float patrolRange = 2.0f;
	//controls the speed of the enemy
	private float duration = 40.0f;
	private int patrolSwitch;


	// --- start and update functions---

	void Start () {

		enemyInitialization ();
	}

	void Update () {
		move ();
		currentPosition = thisCharacter.getCharacterPosition ();
		target = thisCharacter.Target;
		targetPosition = target.getCharacterPosition ();
	}



	// ---Other functions---

	private void enemyInitialization () {
		// initialize this enemy character
		thisCharacter = GetComponent<Character> ();
		originalPosition = thisCharacter.getCharacterPosition();
		currentPosition = originalPosition;
		// initialize target of this character
		target = thisCharacter.Target;
		targetPosition = target.getCharacterPosition();
		// patrol locations 1 and 2
		patrolPosition1 = new Vector3 (originalPosition.x + patrolRange, originalPosition.y, originalPosition.z);
		patrolPosition2 = new Vector3 (originalPosition.x - patrolRange, originalPosition.y, originalPosition.z);
		patrolPosition3 = new Vector3 (originalPosition.x, originalPosition.y + patrolRange, originalPosition.z);
		patrolPosition4 = new Vector3 (originalPosition.x, originalPosition.y - patrolRange, originalPosition.z);
		patrolSwitch = 1;
		
	}

	private void move () {
		// when enemy detects a target
		if (Vector3.Distance (currentPosition, targetPosition) < detectRange && Vector3.Distance (currentPosition, targetPosition) > 1.0f) {
			thisCharacter.setCharacterPosition (lerpBetweenPositions(currentPosition, targetPosition, duration));
		} 
		// when enemy doesn't detect a target
		else if (Vector3.Distance (currentPosition, targetPosition) > detectRange) {
			// outside of patrol range and target is not detected
			if (Vector3.Distance (currentPosition, originalPosition) > patrolRange) {
				thisCharacter.setCharacterPosition (lerpBetweenPositions(currentPosition, originalPosition, duration));
			} 
			// inside of patrol range
			else {
				switch (patrolSwitch) {
				case 1:
					if (Vector3.Distance (currentPosition, patrolPosition1) != 0) {
						thisCharacter.setCharacterPosition (lerpBetweenPositions(currentPosition, patrolPosition1, duration));
					}
					else {
						patrolSwitch = Random.Range(1, 5);
					}
					break;
				case 2:
					if (Vector3.Distance (currentPosition, patrolPosition2) != 0) {
						thisCharacter.setCharacterPosition (lerpBetweenPositions(currentPosition, patrolPosition2, duration));
					}
					else {
						patrolSwitch = Random.Range(1, 5);
					}
					break;
				case 3:
					if (Vector3.Distance (currentPosition, patrolPosition3) != 0) {
						thisCharacter.setCharacterPosition (lerpBetweenPositions(currentPosition, patrolPosition3, duration));
					}
					else {
						patrolSwitch = Random.Range(1, 5);
					}
					break;
				case 4:
					if (Vector3.Distance (currentPosition, patrolPosition4) != 0) {
						thisCharacter.setCharacterPosition (lerpBetweenPositions(currentPosition, patrolPosition4, duration));
					}
					else {
						patrolSwitch = Random.Range(1, 5);
					}
					break;
						
				}
			}
		}
	}

	private Vector3 lerpBetweenPositions (Vector3 currentPosition, Vector3 targetPosition, float duration) {
		return Vector3.Lerp (currentPosition, targetPosition, 1 / (duration * (Vector3.Distance (currentPosition, targetPosition))));
	}


}

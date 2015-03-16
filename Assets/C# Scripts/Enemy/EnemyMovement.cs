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

	public float detectRange = 5.0f;
	private float attackRange = 1.5f;
	private float patrolRange = 2.0f;
	//controls the speed of the enemy
	private float patrolSpeed = 40.0f;
	private float chasingSpeed = 20.0f;
	private int patrolSwitch;

	//combat system
	private CombatManager combatManager = new CombatManager();
	private float attackCooldown = 0f;

	// --- start and update functions---

	void Start () {

		enemyInitialization ();
	}

	void Update () {
        if(!thisCharacter.isPaused)
        {
            thisCharacter.getCharacter().animation.enabled = true;
            death();
            
            if(target == null)
            {
                checkForTarget();
                thisCharacter.setAggro(true);
            }
            else if(target.isDead)
            {
                target = null;
                thisCharacter.setAggro(false);
            }
            else if(!target.isenemy)
            {
                thisCharacter.setAggro(true);
            }
            move();

            AttackCooldownDecrement();
            currentPosition = thisCharacter.getCharacterPosition();
            target = thisCharacter.Target;
            if (target != null)
            {
                targetPosition = target.getCharacterPosition();
            }
        }
        else
        {
            thisCharacter.getCharacter().animation.enabled = false;
        }
	}



	// ---Other functions---

	private void enemyInitialization () {
		// initialize this enemy character
		thisCharacter = GetComponent<Character> ();
		originalPosition = thisCharacter.getCharacterPosition();
		currentPosition = originalPosition;
		// initialize target of this character
		target = thisCharacter.Target;
        if(target != null)
        {
            targetPosition = target.getCharacterPosition();
        }
		// patrol locations 1,2,3,4
		patrolPosition1 = new Vector3 (originalPosition.x + patrolRange, originalPosition.y, originalPosition.z);
		patrolPosition2 = new Vector3 (originalPosition.x - patrolRange, originalPosition.y, originalPosition.z);
		patrolPosition3 = new Vector3 (originalPosition.x, originalPosition.y + patrolRange, originalPosition.z);
		patrolPosition4 = new Vector3 (originalPosition.x, originalPosition.y - patrolRange, originalPosition.z);
		patrolSwitch = 1;
		
	}

	void AttackCooldownDecrement ()
	{
		if (attackCooldown > 0)
		{
			attackCooldown -= Time.deltaTime;
		}
		else if (attackCooldown < 0)
		{
			attackCooldown = 0;
		}
	}

	private void move () {
		// when enemy detects a target
		if (target != null && Vector3.Distance (currentPosition, targetPosition) < detectRange && Vector3.Distance (currentPosition, targetPosition) > attackRange) {
			thisCharacter.getCharacter ().animation.Play ("Run");
			moveBetweenPositions (currentPosition, targetPosition, chasingSpeed);
		} 
		else if (target != null && Vector3.Distance (currentPosition, targetPosition) < attackRange) {
			attack ();
		}
		// when enemy doesn't detect a target
		else if (target == null || Vector3.Distance (currentPosition, targetPosition) > detectRange) {
			// outside of patrol range and target is not detected
			if (Vector3.Distance (currentPosition, originalPosition) > patrolRange) {
				thisCharacter.getCharacter ().animation.Play ("Walk");
				moveBetweenPositions(currentPosition, originalPosition, patrolSpeed);
			} 
			// inside of patrol range
			else {
				switch (patrolSwitch) {
				case 1:
					if (Vector3.Distance (currentPosition, patrolPosition1) != 0) {
						thisCharacter.getCharacter ().animation.Play ("Walk");
						moveBetweenPositions(currentPosition, patrolPosition1, patrolSpeed);
					}
					else {
						patrolSwitch = Random.Range(1, 5);
					}
					break;
				case 2:
					if (Vector3.Distance (currentPosition, patrolPosition2) != 0) {
						thisCharacter.getCharacter ().animation.Play ("Walk");
						moveBetweenPositions(currentPosition, patrolPosition2, patrolSpeed);
					}
					else {
						patrolSwitch = Random.Range(1, 5);
					}
					break;
				case 3:
					if (Vector3.Distance (currentPosition, patrolPosition3) != 0) {
						thisCharacter.getCharacter ().animation.Play ("Walk");
						moveBetweenPositions(currentPosition, patrolPosition3, patrolSpeed);
					}
					else {
						patrolSwitch = Random.Range(1, 5);
					}
					break;
				case 4:
					if (Vector3.Distance (currentPosition, patrolPosition4) != 0) {
						thisCharacter.getCharacter ().animation.Play ("Walk");
						moveBetweenPositions(currentPosition, patrolPosition4, patrolSpeed);
					}
					else {
						patrolSwitch = Random.Range(1, 5);
					}
					break;
						
				}
			}
		}
	}

	private void attack () {
		if(attackCooldown == 0 && target != null){
            thisCharacter.getCharacter().animation.Play("Attack_02");
			combatManager.Hit (thisCharacter, target);				
			attackCooldown = thisCharacter.stats.AttackRate;
		}
	}

	private void death() {
		if (thisCharacter.isDead == true) {
            //Destroy(this.gameObject);
            EnemyCollection.removeAndDestroyEnemy(thisCharacter);
			Debug.Log("EnemyMovement.Death()");
			CharacterCollection.heroExpGain(thisCharacter.stats.ExpYield);
		}
	}

	private void moveBetweenPositions (Vector3 currentPosition, Vector3 targetPosition, float duration) {

		Vector3 movementDirection = targetPosition - currentPosition;
		movementDirection.z = 0;
		movementDirection.Normalize();
		thisCharacter.getCharacter().transform.rotation = Quaternion.LookRotation(movementDirection, new Vector3(0, 0, -1.0f));
		thisCharacter.setCharacterPosition(Vector3.Lerp (currentPosition, targetPosition, 1 / (duration * (Vector3.Distance (currentPosition, targetPosition)))));
		//thisCharacter.getCharacter().transform.localPosition += movementDirection * Time.deltaTime * thisCharacter.stats.MoveSpeed;
	}

    private void checkForTarget() {
        for(int i = 0; i < CharacterCollection.NumberOfHeroes(); i++)
        {
            if(!CharacterCollection.getHero(i).isDead)
            {
                if (Vector3.Distance(currentPosition, CharacterCollection.getHero(i).getCharacterPosition()) < detectRange)
                {
                    target = CharacterCollection.getHero(i);
                    targetPosition = target.getCharacterPosition();
                    return;
                }
            }
        }
    }

}

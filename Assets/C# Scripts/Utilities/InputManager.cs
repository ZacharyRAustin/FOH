using UnityEngine;
using System.Collections.Generic;

public class InputManager {

	public Character selected;
	public bool isCharacterUnderMouse;
	public Character characterUnderMouse;
	private CharacterInstance instanceUnderMouse;
	private Vector3 mousePosition;

	int floorMask;
	int characterMask;
	float camRayLength = 100f;

	public void Select (Character c)
	{
		selected = c;
	}

	// Use this for initialization
	void Start () {
	
	}

	public void Awake () {
		floorMask = LayerMask.GetMask ("Floor");
		characterMask = LayerMask.GetMask ("Character");
	}
	

	public void Resolve () {
		//Debug.Log ("Inside inputManager.Resolve()");
		UpdateMousePosition ();
		CancelCheck ();
		SpellTarget ();
		AllocateStatPoints ();

		if (Input.GetButtonDown("View Equipment"))
		{
			ViewEquipment();
		}

		if (Input.GetButtonDown ("Select Character A"))
		{
			selected = CharacterCollection.getHero(0);
			CharacterCollection.getHero(0).is_selected = false;
			selected.is_selected = false;
			Debug.Log ("Character A selected");
		}
		else if (Input.GetButtonDown ("Select Character B"))
		{
            selected = CharacterCollection.getHero(1);
			Debug.Log ("Character B selected");
		}
		else if (Input.GetButtonDown ("Select Character C"))
		{
            selected = CharacterCollection.getHero(2);
			Debug.Log ("Character C selected");
		}
		
		if (Input.GetMouseButtonDown(1) && selected.playerCasting == false) //right click
		{
			//Debug.Log ("Right click");
			CharacterMouseCheck();

			if (isCharacterUnderMouse == false) //player right clicked on ground, issue move order
			{
				if (selected != null)
				{
					if (Input.GetButton ("Queue"))
					{
						selected.Enqueue(mousePosition);
						//Debug.Log ("Queueing move order to " + mousePosition.x + ", " + mousePosition.y);
					}
					else
					{
						selected.Overwrite(mousePosition);
						//Debug.Log ("Clearing queue, queueing move order to " + mousePosition.x + ", " + mousePosition.y);
					}
				}
			}
			else //player right clicked on a character, issue attack order
			{
                if (characterUnderMouse.isenemy)
                {
                    if (Input.GetButton("Queue"))
                    {
                        selected.Enqueue(characterUnderMouse);
                        //Debug.Log ("Queueing attack order on " + characterUnderMouse.name);
                    }
                    else
                    {
                        selected.Overwrite(characterUnderMouse);
                        //Debug.Log ("Clearing queue, queueing attack order on " + characterUnderMouse.name);
                    }

                    selected.Target = characterUnderMouse;
                    Debug.Log("Selected hero " + selected.stats.Name + " has target " + selected.Target.stats.Name);
                }
			}
		}

		if (Input.GetMouseButtonDown (0) && selected.playerCasting == false) { //left click
						CharacterMouseCheck ();
						Debug.Log ("Left click");

						if (isCharacterUnderMouse) {
								for (int i = 0; i <= 2; i++) {
					if (CharacterCollection.getHero(i) != null){
										if (CharacterCollection.getHero (i) == characterUnderMouse) {
												selected = characterUnderMouse;
												Debug.Log (selected.is_selected);
												selected.is_selected = true;
												Debug.Log (selected.is_selected);
												Debug.Log ("Click selected " + selected.name);
										} else {
												CharacterCollection.getHero (i).is_selected = false;
										}
					}
								}
						}
				}

		if (selected.playerCasting == false)
		{
			if (Input.GetButtonDown ("Ability 1"))
			{
				SpellCast(0);
			}
			else if (Input.GetButtonDown ("Ability 2"))
			{
				SpellCast(1);
			}
			else if (Input.GetButtonDown ("Ability 3"))
			{
				SpellCast(2);
			}
			else if (Input.GetButtonDown ("Ability 4"))
			{
				SpellCast(3);
			}
			else if (Input.GetButtonDown ("Ability 5"))
			{
				SpellCast(4);
			}
		}
	}

	void SpellCast (int index)
	{
		RandomAbility spell = selected.stats.abilities.ToArray () [index];
		if (spell == null)
		{
			Debug.Log ("No spell in that slot");
			MyConsole.NewMessage("No spell in that slot");
		}
		else if (spell.remainingCooldownTime > 0)
		{
			Debug.Log (spell.name + " is still on cooldown for " + spell.remainingCooldownTime + " seconds!");
			MyConsole.NewMessage(spell.name + " is still on cooldown for " + spell.remainingCooldownTime + " seconds!");
			selected.playerCasting = false;
		}
		else
		{
			Debug.Log ("Input - " + selected.name + " casting " + spell.name);
			MyConsole.NewMessage("Input - " + selected.name + " casting " + spell.name);
			selected.playerCasting = true;
			selected.SetCastTime (spell.castTime);
			selected.SpellCast (spell);
		}
	}

	void UpdateMousePosition () {
		// Create a ray from the mouse cursor on screen in the direction of the camera.
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		
		// Create a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit floorHit;
		
		// Perform the raycast and if it hits something on the floor layer...
		if(Physics.Raycast (camRay, out floorHit, camRayLength, floorMask))
		{
			mousePosition = floorHit.point;
			mousePosition.z = 0f;
		}
	}

	public void CharacterMouseCheck () {
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit characterHit;

		if (Physics.Raycast (camRay, out characterHit, camRayLength, characterMask))
		{
			isCharacterUnderMouse = true;
			instanceUnderMouse = characterHit.collider.GetComponentInParent <CharacterInstance> ();
			if (instanceUnderMouse == null)
			{
				Debug.Log ("Character layer hit, but failed to register instance");
				MyConsole.NewMessage("Character layer hit, but failed to register instance");
			}
			else
			{
				Debug.Log ("Instance under mouse = " + instanceUnderMouse.name);
			}
			characterUnderMouse = instanceUnderMouse.parentChar;
			Debug.Log ("Character under mouse = " + characterUnderMouse.name);
		}
		else
		{
			isCharacterUnderMouse = false;
			Debug.Log ("Floor click");
		}
	}

	void CancelCheck ()
	{
		if (Input.GetButtonDown("Cancel"))
		{
			Debug.Log ("Cancel");
			selected.actionQueue.Clear();
			selected.PlayerCastInterrupt();
		}
	}

	void AllocateStatPoints()
	{
		if (selected.stats.UnallocatedStatPoints > 0)
		{
			if (Input.GetButtonDown("Level HP"))
			{
				selected.stats.LevelHealth();
				selected.stats.UnallocatedStatPoints -= 1;
				Debug.Log ("Raised HP. " + selected.stats.Name + "'s Max HP is now " + (selected.stats.MaxHealth + 5) + ".");
				MyConsole.NewMessage("Raised HP. " + selected.stats.Name + "'s Max HP is now " + (selected.stats.MaxHealth + 5) + ".");
				Debug.Log (selected.stats.UnallocatedStatPoints + " points left to spend.");
				MyConsole.NewMessage(selected.stats.UnallocatedStatPoints + " points left to spend.");
				selected.stats.InitializeCombatStats();
			}
			else if (Input.GetButtonDown("Level Mana"))
			{
				selected.stats.LevelMana();
				selected.stats.UnallocatedStatPoints -= 1;
				Debug.Log ("Raised Mana. " + selected.stats.Name + "'s Max Mana is now " + (selected.stats.MaxMana + 5) + ".");
				MyConsole.NewMessage("Raised Mana. " + selected.stats.Name + "'s Max Mana is now " + (selected.stats.MaxMana + 5) + ".");
				Debug.Log (selected.stats.UnallocatedStatPoints + " points left to spend.");
				MyConsole.NewMessage(selected.stats.UnallocatedStatPoints + " points left to spend.");

				selected.stats.InitializeCombatStats();
			}
			else if (Input.GetButtonDown("Level Strength"))
			{
				selected.stats.LevelStrength();
				selected.stats.UnallocatedStatPoints -= 1;
				Debug.Log ("Raised Strength. " + selected.stats.Name + "'s Strength is now " + (selected.stats.Strength + 1) + ".");
				MyConsole.NewMessage("Raised Strength. " + selected.stats.Name + "'s Strength is now " + (selected.stats.Strength + 1) + ".");
				Debug.Log (selected.stats.UnallocatedStatPoints + " points left to spend.");
				MyConsole.NewMessage(selected.stats.UnallocatedStatPoints + " points left to spend.");
				selected.stats.CalculateCombatStats();
			}
			else if (Input.GetButtonDown("Level Agility"))
			{
				selected.stats.LevelAgility();
				selected.stats.UnallocatedStatPoints -= 1;
				Debug.Log ("Raised Agility. " + selected.stats.Name + "'s Agility is now " + (selected.stats.Agility + 1) + ".");
				MyConsole.NewMessage("Raised Agility. " + selected.stats.Name + "'s Agility is now " + (selected.stats.Agility + 1) + ".");
				Debug.Log (selected.stats.UnallocatedStatPoints + " points left to spend.");
				MyConsole.NewMessage(selected.stats.UnallocatedStatPoints + " points left to spend.");
				selected.stats.CalculateCombatStats();
			}
			else if (Input.GetButtonDown("Level Intelligence"))
			{
				selected.stats.LevelIntelligence();
				selected.stats.UnallocatedStatPoints -= 1;
				Debug.Log ("Raised Intelligence. " + selected.stats.Name + "'s Intelligence is now " + (selected.stats.Intelligence + 1) + ".");
				MyConsole.NewMessage("Raised Intelligence. " + selected.stats.Name + "'s Intelligence is now " + (selected.stats.Intelligence + 1) + ".");
				Debug.Log (selected.stats.UnallocatedStatPoints + " points left to spend.");
				MyConsole.NewMessage(selected.stats.UnallocatedStatPoints + " points left to spend.");
				selected.stats.CalculateCombatStats();
			}
		}
	}

	void SpellTarget ()
	{
		if (selected != null){
			RandomAbility spell = selected.currentSpell;
			if (selected.playerCasting)
			{
				if (spell.targetOption == AbilityTargetOption.TARGET_ALLY)
				{
					if (Input.GetMouseButtonDown (0))
					{
						CharacterMouseCheck();
						if (isCharacterUnderMouse)
						{
							for (int i = 0; i <= 2; i++)
							{
        	                    if (CharacterCollection.getHero(i) == characterUnderMouse)
								{
									Debug.Log ("Casting " + spell.name + " on " + characterUnderMouse.stats.Name);
									MyConsole.NewMessage("Casting " + spell.name + " on " + characterUnderMouse.stats.Name);
									if (Input.GetButton ("Queue"))
									{
										selected.Enqueue (spell, characterUnderMouse, new Vector3());
									}
									else
									{
										selected.Overwrite (spell, characterUnderMouse, new Vector3());
									}
									selected.playerCasting = false;
								}
							}
						}
					}
				}
				else if (spell.targetOption == AbilityTargetOption.TARGET_ENEMY)
				{
					if (Input.GetMouseButtonDown (0))
					{
						CharacterMouseCheck();
						if (isCharacterUnderMouse)
						{
							for (int i = 0; i < EnemyCollection.NumberOfEnemies(); i++)
							{
								if (EnemyCollection.getEnemy(i) == characterUnderMouse)
								{
									Debug.Log ("Casting " + spell.name + " on " + characterUnderMouse.stats.Name);
									MyConsole.NewMessage("Casting " + spell.name + " on " + characterUnderMouse.stats.Name);
									if (Input.GetButton ("Queue"))
									{
										selected.Enqueue (spell, characterUnderMouse, new Vector3());
									}
									else
									{
										selected.Overwrite (spell, characterUnderMouse, new Vector3());
									}
									selected.playerCasting = false;
								}
							}
						}
					}
				}
				else if (spell.targetOption == AbilityTargetOption.TARGET_LOCATION)
				{
					if (Input.GetMouseButtonDown(0))
					{
						if (Input.GetButton ("Queue"))
						{
							selected.Enqueue(spell, null, mousePosition);
							Debug.Log ("Queueing position-targeted spell at " + mousePosition.x + ", " + mousePosition.y);
						}
						else
						{
							selected.Overwrite (spell, null, mousePosition);
							Debug.Log ("Casting position-targeted spell at " + mousePosition.x + ", " + mousePosition.y);
							Debug.Log ("Player casting: " + selected.playerCasting);
						}
					}
				}
			}
		}
	}	

	void ViewEquipment()
	{
		if (selected != null)
		{
			selected.stats.weapon.Print();
			//selected.stats.weapon.PrintStats();

			for (int i = 0; i <= 2; i++)
			{
				MyConsole.NewMessage(selected.stats.gear[i].name);
				selected.stats.gear[i].PrintStats();
			}
		}
	}
}

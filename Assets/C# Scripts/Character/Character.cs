using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Character : MonoBehaviour {
    public CharacterInstance characterPrefab;
	public CharacterStats stats = new CharacterStats();
	public ActionQueue actionQueue = new ActionQueue();
	private bool actionInProgress = false;
	public bool playerCasting = false; //player is inputting a spell, not character is casting
    private CharacterInstance character;
	private float attackCooldown = 0f;
	private float timeUntilCast = 0f;
	private CombatManager combatManager = new CombatManager();
	private InputManager inputManager = new InputManager();
	public Ability currentSpell;

	private Character target;
    private GameObject cube;
	public bool isPaused;
	public bool isDead = false;

    private int status = CharacterStatus.WAITING;
	


	// ---properties---
	public Character Target {
		get {return target;} 
		set {target = value;}
	}







	public float barDisplay ;//current progress
	public float barDisplay_1;
	public string name;
	public string pause_string;
	public float position_y_health;
	public float position_y;
	public float position_x;
	public Vector2 pos  ;
	public Vector2 size ;
	public string image_name;
	public Texture2D emptyTex;
	public Texture2D image_texture;
	public Texture2D fullTex;
	public Texture2D fullTex_mana;
	public bool isenemy;
	Color redcolor = Color.red;
	Color greencolor = Color.green;
	Color graycolor = Color.grey;
	Color bluecolor = Color.blue;
	GUIStyle style_font = new GUIStyle ();
	GUIStyle style = new GUIStyle();
	GUIStyle style_mana = new GUIStyle();

	
	
	void OnGUI(){
		fullTex_mana.Apply ();
		fullTex.Apply ();
		style.normal.background = fullTex;
		style_mana.normal.background = fullTex_mana;
		style_font.fontSize = 50;
		
		
		float width_x = (Screen.width / 6);
		GUI.BeginGroup (new Rect ((Screen.width/3), (Screen.width/4), width_x*2 + 100 , 1000));
		GUI.Label(new Rect (0, 0, width_x*2, 1000), pause_string,style_font);
		GUI.EndGroup ();

		if (isenemy == false) {
						GUI.BeginGroup (new Rect (position_x, position_y_health, width_x + 100, 80));
						GUI.DrawTexture (new Rect (0, 0, 60, 60), image_texture);
						GUI.BeginGroup (new Rect (60, 0, width_x + 40, size.y));
						GUI.TextArea (new Rect (width_x, 0, 40, size.y), (stats.CurrentHealth * 100 / stats.MaxHealth).ToString () + "%");
						GUI.Box (new Rect (0, 0, width_x, size.y), emptyTex);
						//draw the filled-in part:
						GUI.BeginGroup (new Rect (0, 0, width_x * (stats.CurrentHealth) / (stats.MaxHealth), size.y));
						GUI.Box (new Rect (0, 0, width_x, size.y), new GUIContent (""), style);
						GUI.EndGroup ();
						GUI.EndGroup ();
		
						GUI.BeginGroup (new Rect (60, 30, width_x + 40, size.y));
						GUI.TextArea (new Rect (width_x, 0, 40, size.y), (stats.CurrentMana * 100 / stats.MaxMana).ToString () + "%");
						GUI.Box (new Rect (0, 0, width_x, size.y), emptyTex);
						//GUI.TextArea (new Rect (0,0,60, size.y), character.name);
						//draw the filled-in part:
						GUI.BeginGroup (new Rect (0, 0, width_x * (stats.CurrentMana) / (stats.MaxMana), size.y));
						GUI.Box (new Rect (0, 0, width_x, size.y), new GUIContent (""), style_mana);
						GUI.EndGroup ();
						GUI.EndGroup ();
						GUI.EndGroup ();
				} else {
		
			GUI.BeginGroup (new Rect (Screen.width - width_x - 100, position_y_health, width_x + 100, 80));
			GUI.DrawTexture (new Rect (width_x+40, 0, 60, 60), image_texture);
			GUI.BeginGroup (new Rect (0, 0, width_x + 40, size.y));
			GUI.TextArea (new Rect (0, 0, 40, size.y), (stats.CurrentHealth * 100 / stats.MaxHealth).ToString () + "%");
			GUI.Box (new Rect (40, 0, width_x, size.y), emptyTex);
			//draw the filled-in part:
			GUI.BeginGroup (new Rect (40, 0, width_x * (stats.CurrentHealth) / (stats.MaxHealth), size.y));
			GUI.Box (new Rect (0, 0, width_x, size.y), new GUIContent (""), style);
			GUI.EndGroup ();
			GUI.EndGroup ();
			
			GUI.BeginGroup (new Rect (0, 30, width_x + 40, size.y));
			GUI.TextArea (new Rect (0, 0, 40, size.y), (stats.CurrentMana * 100 / stats.MaxMana).ToString () + "%");
			GUI.Box (new Rect (40, 0, width_x, size.y), emptyTex);
			//GUI.TextArea (new Rect (0,0,60, size.y), character.name);
			//draw the filled-in part:
			GUI.BeginGroup (new Rect (40, 0, width_x * (stats.CurrentMana) / (stats.MaxMana), size.y));
			GUI.Box (new Rect (0, 0, width_x, size.y), new GUIContent (""), style_mana);
			GUI.EndGroup ();
			GUI.EndGroup ();
			GUI.EndGroup ();
			
			
		}
		
	}
	
	// Use this for initialization
	void Start () {
		barDisplay_1 = stats.CurrentHealth;
		barDisplay = stats.CurrentMana;
		
		pos = new Vector2(20,0);
		size = new Vector2(80,20);
		image_texture = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
		image_texture = Resources.Load<Texture2D>(image_name);
		
		fullTex = new Texture2D(1, 1);
		fullTex.SetPixel(1, 1, greencolor);
		fullTex_mana = new Texture2D(1, 1);
		fullTex_mana.SetPixel(1, 1, bluecolor);
		
		
		//int[] left_array = {10, 40, 70,100,130,160,190,220,250};
	}

	public void SetCastTime (float castTime)
	{
		timeUntilCast = castTime;
	}
	
	// Update is called once per frame
	void Update () {
		pause_string = "GAME IS PAUSED";
		if (isPaused == false)
		{
			pause_string = "";
			DeathCheck ();
			stats.ResolveBuffs ();
			actionQueue.Resolve ();
			AttackCooldownDecrement ();
			character_gui_update();
        	//if(target != null)
        	//{
            //	moveToTarget();
        	//}
		}
        removeVelocities();
	}

	void DeathCheck ()
	{
		if (stats.CurrentHealth <= 0)
		{
			stats.CurrentHealth = 0;
			isDead = true;
		}
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
	
	public void Enqueue (Vector3 position) //move order
	{
		actionQueue.Enqueue (position);
	}

	public void Overwrite (Vector3 position)
	{
		actionQueue.Overwrite (position);
	}

	public void Enqueue (Character c) //attack order
	{
		actionQueue.Enqueue (c);
	}

	public void Overwrite (Character c)
	{
		actionQueue.Overwrite (c);
	}

	public void Enqueue (Ability s, Character c, Vector3 p) //cast order
	{
		actionQueue.Enqueue (s, c, p);
	}

	public void Overwrite (Ability s, Character c, Vector3 p)
	{
		actionQueue.Overwrite (s, c, p);
	}

	public void SpellCast (Ability spell)
	{
		if (playerCasting == true)
		{
			if (spell.targetOption == AbilityTargetOption.SELF)
			{
				Enqueue (spell, new Character(), new Vector3());
				playerCasting = false;
			}
			else if (spell.targetOption == AbilityTargetOption.TARGET_ALLY)
			{
				Debug.Log ("Target ally");
				currentSpell = spell;
			}
			else if (spell.targetOption == AbilityTargetOption.TARGET_ENEMY)
			{
				Debug.Log ("Target enemy");
				currentSpell = spell;
			}
			else if (spell.targetOption == AbilityTargetOption.TARGET_LOCATION)
			{
				Debug.Log ("Target location");
			}
			else if (spell.targetOption == AbilityTargetOption.NONE)
			{
				Enqueue (spell, new Character(), new Vector3());
				playerCasting = false;
			}
		}
	}

	//for when the player hits a spell key, but then issues a different command or cancels
	public void PlayerCastInterrupt()
	{
		Debug.Log (stats.Name + " cast interrupt");
		playerCasting = false;
	}

    public void Generate() {
        character = Instantiate(characterPrefab) as CharacterInstance;
        character.transform.parent = transform;
        character.transform.localPosition = new Vector3(0f, 0f, -0.5f);
        cube = character.transform.GetChild(0).gameObject;
    }

    public void Generate(float x, float y) {
        character = Instantiate(characterPrefab) as CharacterInstance;
        character.transform.parent = transform;
		character.transform.localPosition = new Vector3 (x, y, -0.5f);
        cube = character.transform.GetChild(0).gameObject;
    }

    public void MoveForward() {
        character.transform.localPosition += new Vector3(0f, 1f * Time.deltaTime, 0f);
    }

    public void MoveDown() {
        character.transform.localPosition -= new Vector3(0f, 1f * Time.deltaTime, 0f);
    }

    public void setMaterial(Material mat) {
        cube.renderer.material = mat;
    }

    public Vector3 getCharacterPosition() {
        return character.transform.localPosition;
    }

    public void setCharacterPosition(Vector3 pos) {
        character.transform.localPosition = pos;
    }

    public void setTarget(Character c) {
        target = c;
    }

    private int moveToTarget() {
        Vector3? directionVector = getDirectionVector();
        if(directionVector != null)
        {
            float dist = directionVector.Value.magnitude;
            if (dist > stats.AttackRange)
            {
                character.transform.localPosition += new Vector3(directionVector.Value.x * Time.deltaTime * stats.MoveSpeed / dist, 
                                                                 directionVector.Value.y * Time.deltaTime * stats.MoveSpeed / dist, 
                                                                 directionVector.Value.z * Time.deltaTime * stats.MoveSpeed / dist);

                return CharacterStatus.MOVING;
            }
            return CharacterStatus.ARRIVED;
        }
        return CharacterStatus.WAITING;
        
    }

    private Vector3? getDirectionVector() {
        if(target != null)
        {
            Vector3 targetPos = target.getCharacterPosition();
            Vector3 pos = character.transform.localPosition;
            return targetPos - pos;
        }
        return null;
    }

	public void TakeDamage (int damage)
	{
		stats.CurrentHealth -= damage;
		DeathCheck ();
	}

	public void ResolveMovementOrder(MovementOrder currentOrder)
	{
		Vector3 movementDirection = currentOrder.destination - character.transform.localPosition;
		movementDirection.z = 0;
		if (movementDirection.magnitude < .1)
		{
			character.idle();
			actionQueue.Pop();
			Debug.Log ("Character " + name + " completed movement order");
		}
		else
		{
			character.walk();
			movementDirection.Normalize();
			character.transform.rotation = Quaternion.LookRotation(movementDirection, new Vector3(0, 0, -1.0f));
			character.transform.localPosition += movementDirection * Time.deltaTime * stats.MoveSpeed;

		}
	}

	public void ResolveAttackOrder(AttackOrder currentOrder)
	{
		Character attackTarget = currentOrder.target;
		Vector3 attackVector = attackTarget.getCharacterPosition () - character.transform.localPosition;
		float attackDistance = attackVector.magnitude;

		if (attackTarget.isDead == true)
		{
			Debug.Log ("Attack target of " + stats.Name + " is dead. Cancelling attack order");
			actionQueue.Pop ();
		}

		if (attackDistance > stats.AttackRange)
		{ //if out of range, move towards target
			character.walk();
			attackVector.Normalize();
			character.transform.rotation = Quaternion.LookRotation(attackVector, new Vector3(0, 0, -1.0f));
			character.transform.localPosition += attackVector * Time.deltaTime * stats.MoveSpeed;
		}
		else
		{
			if (attackCooldown == 0)
			{
				character.transform.rotation = Quaternion.LookRotation(attackVector, new Vector3(0, 0, -1.0f));
				character.attack();
				combatManager.Hit(this, attackTarget);
				Debug.Log ("Character " + stats.Name + " attacks " + attackTarget.stats.Name + ".");
				Debug.Log (attackTarget.stats.Name + "'s HP is now " + attackTarget.stats.CurrentHealth);
				attackCooldown = stats.AttackRate;
			}
		}
	}

	public void ResolveCastOrder (CastOrder currentOrder)
	{
		Character targetCharacter = currentOrder.targetCharacter;
		Vector3 targetLocation = currentOrder.targetLocation;
		Ability spell = currentOrder.spell;

		Vector3 characterVector = targetCharacter.getCharacterPosition () - character.transform.localPosition;
		float targetDistance = characterVector.magnitude;

		if (stats.CurrentMana < spell.manaCost)
		{
			Debug.Log ("Not enough mana!");
		}
		else if (targetDistance > spell.range)
		{
			characterVector.Normalize();
			character.transform.localPosition += characterVector * Time.deltaTime * stats.MoveSpeed;
		}
		else
		{
			if (timeUntilCast > 0f)
			{
				Debug.Log ("timeUntilCast = " + timeUntilCast);
				timeUntilCast -= Time.deltaTime;
			}
			else
			{
				stats.CurrentMana -= spell.manaCost;
				Debug.Log ("Spell " + spell.name + " resolving");
				spell.Resolve (targetCharacter, targetLocation);
				actionQueue.Pop();
			}
		}
	}

    public void removeVelocities() {
        character.rigidbody.angularVelocity = Vector3.zero;
        character.rigidbody.velocity = Vector3.zero;
    }

    public void clearActionQueue() {
        actionQueue.Clear();
    }

	public void character_gui_update(){
		barDisplay_1 = stats.CurrentHealth*100/stats.MaxHealth;
			barDisplay = stats.CurrentMana;

		if (((stats.CurrentHealth * 100)/stats.MaxHealth) > 40)
		{
			
			fullTex.SetPixel(1, 1, greencolor);
		}
		
		if (((stats.CurrentHealth*100)/stats.MaxHealth) <= 40)
			
		{
			fullTex.SetPixel(1, 1, redcolor);
		}
		
	}

}



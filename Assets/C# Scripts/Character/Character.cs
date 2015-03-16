using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Character : MonoBehaviour
{

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
    public RandomAbility currentSpell;
    private Character target;
    private GameObject cube;
    public bool isPaused;
    public bool isDead = false;
	public bool isclick = false;
    public Image Damage_image;
    private int status = CharacterStatus.WAITING;

    private bool isAggrod = false;
    private float detectRange = 5f;

    // ---properties---
    public Character Target {
        get { return target; }
        set { target = value; }
    }

    public CharacterInstance getCharacter() {
        return character;
    }




    public int count_times;
	public Color greenme = new Color(0f, 1f, 0f, 0.7f);
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public Color flashColour_1 = new Color(1f, 0f, 0f, 0.0f);

    public text_area text_1Prefab;
    public text_area text1;
    public float barDisplay;//current progress
    public float barDisplay_1;
    public string name;
    public string pause_string;
    public float position_y_health;
    public float position_y;
    public float position_x;
    public Vector3 pos;
    public Vector2 size;
    public string image_name;
    public Texture2D emptyTex;
    public Texture2D image_texture;
    public Texture2D fullTex;
    public Texture2D fullTex_mana;
	public Texture2D fullTex_experience;
    public bool isenemy;
    public bool is_selected;
    public Vector2 pos_bar;
    Color redcolor = Color.red;
    Color greencolor = Color.green;
    Color graycolor = Color.grey;
    Color bluecolor = Color.blue;
	Color yellowcolor = Color.yellow;
    GUIStyle style_font = new GUIStyle();
    GUIStyle style = new GUIStyle();
    GUIStyle style_mana = new GUIStyle();
    GUIStyle style_name = new GUIStyle();
	GUIStyle style_no = new GUIStyle();

	GUIStyle style_experience = new GUIStyle ();
	public bool isattack;






    void OnGUI() {


        fullTex_mana.Apply();
        fullTex.Apply();
		fullTex_experience.Apply ();
        style.normal.background = fullTex;
        style_mana.normal.background = fullTex_mana;
		style_experience.normal.background = fullTex_experience;
        style_font.fontSize = 50;
        style_name.fontSize = 30;
		style_name.normal.textColor = graycolor;
		style_no.fontSize = 15;
		float width_x = (Screen.width / 6);
        GUI.BeginGroup(new Rect((Screen.width / 3), (Screen.height / 2), width_x * 2 + 100, 1000));
        GUI.Label(new Rect(0, 0, width_x * 2, 1000), pause_string, style_font);
        GUI.EndGroup();
        //if (isPaused == false)
		if (true)
        {
            if (is_selected == true)
            {
                if (isenemy == false)
                {
                    GUI.BeginGroup(new Rect(Screen.width / 10, Screen.height - 50, 8 * Screen.width / 10, 40));
                    GUI.Label(new Rect(0, 0, Screen.width / 12, 30), name, style_name);
					for (int i = 0; i < stats.abilities.Count; i++)
					{
						float truncatedCooldownTime = stats.abilities.ToArray()[i].remainingCooldownTime;
						truncatedCooldownTime = (float)(Mathf.Floor(truncatedCooldownTime*10.0f) / 10.0f);
						GUI.Button(new Rect((i+1) * Screen.width / 8, 0, Screen.width / 8, 40), stats.abilities.ToArray()[i].name);
						GUI.TextArea(new Rect((i+1) * Screen.width / 8, 0, Screen.width / 8, 40), truncatedCooldownTime.ToString());
					}
					GUI.EndGroup();
					GUI.BeginGroup(new Rect(pos_bar.x - 10, (Screen.height - pos_bar.y) - 40, 20, 5));
					GUI.Box(new Rect(0, 0, 20, 5), emptyTex);
					GUI.BeginGroup(new Rect(0, 0, 20 * (stats.CurrentHealth) / (stats.MaxHealth), 5));
					GUI.Box(new Rect(0, 0, 20, 5), new GUIContent(""), style);
					GUI.EndGroup();
					GUI.EndGroup();
                }
				else{
				GUI.BeginGroup(new Rect(pos_bar.x - 10, (Screen.height - pos_bar.y) - 80, 20, 5));
				GUI.Box(new Rect(0, 0, 20, 5), emptyTex);
				GUI.BeginGroup(new Rect(0, 0, 20 * (stats.CurrentHealth) / (stats.MaxHealth), 5));
				GUI.Box(new Rect(0, 0, 20, 5), new GUIContent(""), style);
				GUI.EndGroup();
				GUI.EndGroup();
				}
            }
            if (isenemy == false)
            { 
				if(playerCasting == true){
		     
					GUI.BeginGroup(new Rect(0,Screen.height/2+200,100,100));
					GUI.TextArea(new Rect(0,0,100,100),(currentSpell.castTime).ToString());
					GUI.EndGroup();
				}


                GUI.BeginGroup(new Rect(position_x, position_y_health, width_x + 100, 80));
				if (GUI.Button(new Rect(0, 0, 60, 60), image_texture)){
						isclick = true;
					//MyConsole.NewMessage("clicked");
				}
				else{
					isclick = false;
				}
//                GUI.DrawTexture(new Rect(0, 0, 60, 60), image_texture);
                GUI.BeginGroup(new Rect(60, 0, width_x + 40, size.y));
				GUI.TextArea(new Rect(width_x, 0, CalculateHPMPBoxWidth((float) stats.MaxHealth), size.y), (stats.CurrentHealth).ToString(),style_no);
				GUI.Box(new Rect(0, 0, width_x, size.y), emptyTex);
                //draw the filled-in part:
                GUI.BeginGroup(new Rect(0, 0, width_x * (stats.CurrentHealth) / (stats.MaxHealth), size.y));
                GUI.Box(new Rect(0, 0, width_x, size.y), new GUIContent(""), style);
                GUI.EndGroup();
                GUI.EndGroup();

                GUI.BeginGroup(new Rect(60,20, width_x + 40, size.y));
				GUI.TextArea(new Rect(width_x, 0, CalculateHPMPBoxWidth((float) stats.MaxMana), size.y), (stats.CurrentMana).ToString(),style_no);
                GUI.Box(new Rect(0, 0, width_x, size.y), emptyTex);
                //GUI.TextArea (new Rect (0,0,60, size.y), character.name);
                //draw the filled-in part:
                GUI.BeginGroup(new Rect(0, 0, width_x * (stats.CurrentMana) / (stats.MaxMana), size.y));
                GUI.Box(new Rect(0, 0, width_x, size.y), new GUIContent(""), style_mana);
                GUI.EndGroup();
                GUI.EndGroup();

				GUI.BeginGroup(new Rect(60,40, width_x + 40, size.y));
				GUI.TextArea(new Rect(width_x, 0, CalculateHPMPBoxWidth((float) stats.MaxMana), size.y), (stats.CurrentExp).ToString(),style_no);
				GUI.Box(new Rect(0, 0, width_x, size.y), emptyTex);
				//GUI.TextArea (new Rect (0,0,60, size.y), character.name);
				//draw the filled-in part:
//<<<<<<< HEAD
//                GUI.BeginGroup(new Rect(0, 0, width_x , size.y));
//                GUI.Box(new Rect(0, 0, width_x * (stats.CurrentExp)/(stats.MaxExp), size.y), new GUIContent(""), style_experience);
//=======
				GUI.BeginGroup(new Rect(0, 0, width_x * (stats.CurrentExp) / (stats.MaxExp), size.y));
				GUI.Box(new Rect(0, 0, width_x, size.y), new GUIContent(""), style_mana);
				GUI.EndGroup();
				GUI.EndGroup();

				GUI.EndGroup();
				
			}
			else //is enemy
			{
				int healthBoxWidth = CalculateHPMPBoxWidth((float) stats.MaxHealth);
                GUI.BeginGroup(new Rect(Screen.width - width_x - 100, position_y_health, width_x + 100, 80));
                GUI.DrawTexture(new Rect(width_x + 40, 0, 60, 60), image_texture);
                GUI.BeginGroup(new Rect(0, 0, width_x + 40, 20));
				GUI.TextArea(new Rect(40 - healthBoxWidth, 0, healthBoxWidth, 20), (stats.CurrentHealth).ToString());
                GUI.Box(new Rect(40, 0, width_x, 20), emptyTex);
                //draw the filled-in part:
                GUI.BeginGroup(new Rect(40, 0, width_x * (stats.CurrentHealth) / (stats.MaxHealth), 20));
                GUI.Box(new Rect(0, 0, width_x, 20), new GUIContent(""), style);
                GUI.EndGroup();
                GUI.EndGroup();

				int manaBoxWidth = CalculateHPMPBoxWidth((float) stats.MaxMana);
                GUI.BeginGroup(new Rect(0, 30, width_x + 40, 20));
				GUI.TextArea(new Rect(40 - manaBoxWidth, 0, manaBoxWidth, 20), (stats.CurrentMana).ToString());
                GUI.Box(new Rect(40, 0, width_x, 20), emptyTex);
                //GUI.TextArea (new Rect (0,0,60, size.y), character.name);
                //draw the filled-in part:
                GUI.BeginGroup(new Rect(40, 0, width_x * (stats.CurrentMana) / (stats.MaxMana), 20));
                GUI.Box(new Rect(0, 0, width_x, 20), new GUIContent(""), style_mana);
                GUI.EndGroup();
                GUI.EndGroup();
                GUI.EndGroup();


            }

        }
    }

    // Use this for initialization
    void Start() {

        //text1.set_string("het=");
        pos_bar = Camera.main.WorldToScreenPoint(character.transform.localPosition);
        //pos_bar = character.transform.localPosition;
        barDisplay_1 = stats.CurrentHealth;
        barDisplay = stats.CurrentMana;

        pos = new Vector2(20, 0);
        size = new Vector2(80, 15);
        image_texture = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
        image_texture = Resources.Load<Texture2D>(image_name);

        fullTex = new Texture2D(1, 1);
        fullTex.SetPixel(1, 1, greenme);
        fullTex_mana = new Texture2D(1, 1);
        fullTex_mana.SetPixel(1, 1, bluecolor);
		fullTex_experience= new Texture2D(1, 1);
		fullTex_experience.SetPixel(1, 1, yellowcolor);
		
		
		//int[] left_array = {10, 40, 70,100,130,160,190,220,250};
    }

    public void SetCastTime(float castTime) {
        timeUntilCast = castTime;
    }

    // Update is called once per frame
    void Update() {
        //count_times = 0;
        pos_bar = Camera.main.WorldToScreenPoint(character.transform.localPosition);
        pause_string = "GAME IS PAUSED";
        if (isPaused == false)
        {
            if(!isenemy)
            {
                character.setEnabled(true);
            }
            pause_string = "";
            DeathCheck();
            if(!isDead)
            {
                checkForAggro();
				stats.SetModifiersNeutral();
				stats.CalculateCombatStats();
                stats.ResolveBuffs();
                actionQueue.Resolve();
                AttackCooldownDecrement();
                SpellCooldownDecrement();
                character_gui_update();
				statPrintCheck();
				stats.MaxExp = 5 + stats.Level*5;
            }
            else
            {
                if(!isenemy)
                {
                    character.idle();
                }
            }
        }
        else
        {
            if(!isenemy)
            {
                character.setEnabled(false);
            }
        }
        removeVelocities();

		if (stats.CurrentHealth > stats.MaxHealth)
		{
			stats.CurrentHealth = stats.MaxHealth;
		}

		if (stats.CurrentMana > stats.MaxMana)
		{
			stats.CurrentMana = stats.MaxMana;
		}
    }

    void DeathCheck() {
        if (stats.CurrentHealth <= 0)
        {
            stats.CurrentHealth = 0;
            isDead = true;
        }
    }

    void AttackCooldownDecrement() {
        if (attackCooldown > 0)
        {
            attackCooldown -= Time.deltaTime;
        }
        else if (attackCooldown < 0)
        {
            attackCooldown = 0;
        }
    }

	void SpellCooldownDecrement()
	{
		foreach (RandomAbility s in stats.abilities)
		{
			s.UpdateRemainingCooldownTime();
		}
	}

    public void Enqueue(Vector3 position) //move order
    {
        actionQueue.Enqueue(position);
    }

    public void Overwrite(Vector3 position) {
        actionQueue.Overwrite(position);
    }

    public void Enqueue(Character c) //attack order
    {
        actionQueue.Enqueue(c);
    }

    public void Overwrite(Character c) {
        actionQueue.Overwrite(c);
    }

    public void Enqueue(RandomAbility s, Character c, Vector3 p) //cast order
    {
        actionQueue.Enqueue(s, c, p);
    }

    public void Overwrite(RandomAbility s, Character c, Vector3 p) {
        actionQueue.Overwrite(s, c, p);
    }

    public void SpellCast(RandomAbility spell) {
        if (playerCasting == true)
        {
            if (spell.targetOption == AbilityTargetOption.SELF)
            {
				if (Input.GetButton ("Queue"))
			    {
	                Enqueue(spell, this, new Vector3());
				}
				else
				{
					Overwrite(spell, this, new Vector3());
				}
                playerCasting = false;
            }
            else if (spell.targetOption == AbilityTargetOption.TARGET_ALLY)
            {
                Debug.Log("Target ally");
				MyConsole.NewMessage("Target ally");
                currentSpell = spell;
            }
            else if (spell.targetOption == AbilityTargetOption.TARGET_ENEMY)
            {
                Debug.Log("Target enemy");
				MyConsole.NewMessage("Target enemy");
                currentSpell = spell;
            }
            else if (spell.targetOption == AbilityTargetOption.TARGET_LOCATION)
            {
                Debug.Log("Target location");
				currentSpell = spell;
				MyConsole.NewMessage("Target location");
            }
            else if (spell.targetOption == AbilityTargetOption.NONE)
            {
                Enqueue(spell, new Character(), new Vector3());
                playerCasting = false;
            }
        }
    }

    //for when the player hits a spell key, but then issues a different command or cancels
    public void PlayerCastInterrupt() {
        Debug.Log(stats.Name + " cast interrupt");
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
        character.transform.localPosition = new Vector3(x, y, -0.5f);
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
        if((c.isenemy && !isenemy) || (!c.isenemy && isenemy))
        {
            target = c;
        }
        
    }

    private int moveToTarget() {
        Vector3? directionVector = getDirectionVector();
        if (directionVector != null)
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
        if (target != null)
        {
            Vector3 targetPos = target.getCharacterPosition();
            Vector3 pos = character.transform.localPosition;
            return targetPos - pos;
        }
        return null;
    }

    public void TakeDamage(int damage) {
        stats.CurrentHealth -= damage;
        DeathCheck();
        count_times = damage;
    }

    public void ResolveMovementOrder(MovementOrder currentOrder) {
        Vector3 movementDirection = currentOrder.destination - character.transform.localPosition;
        movementDirection.z = 0;
        if (movementDirection.magnitude < .1)
        {
            character.idle();
            actionQueue.Pop();
            Debug.Log("Character " + name + " completed movement order");
			MyConsole.NewMessage("Character " + name + " completed movement order");
        }
        else
        {
            character.run();
            movementDirection.Normalize();
            character.transform.rotation = Quaternion.LookRotation(movementDirection, new Vector3(0, 0, -1.0f));
            character.transform.localPosition += movementDirection * Time.deltaTime * stats.MoveSpeed;

        }
    }

    public void ResolveAttackOrder(AttackOrder currentOrder) {
        Character attackTarget = currentOrder.target;

        if (attackTarget.isDead == true || attackTarget == null)
        {
            //character.idle();
            Debug.Log("Attack target of " + stats.Name + " is dead. Cancelling attack order");
			//MyConsole.NewMessage("Attack target of " + stats.Name + " is dead. Cancelling attack order");
            actionQueue.Pop();
			//Debug.Log ("actionQueue.Count(): " + actionQueue.Count());
        }
		else
		{
			Vector3 attackVector = attackTarget.getCharacterPosition() - character.transform.localPosition;
			float attackDistance = attackVector.magnitude;
		
			 if (attackDistance > stats.AttackRange)
        	{ //if out of range, move towards target
            	character.run();
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
					if (stats.AttackRange > 2f) {
						GameObject basicAttackProjectile = (GameObject) Object.Instantiate(Resources.Load("frameBall"), character.transform.localPosition, Quaternion.identity);
						BasicAttackProjectile projectileScript = basicAttackProjectile.GetComponent<BasicAttackProjectile>();
						projectileScript.targetLocation = attackTarget.getCharacterPosition();
					}
        	   
					combatManager.Hit(this, attackTarget);
            	    attackTarget.is_selected = true;
            	    Debug.Log("Character " + stats.Name + " attacks " + attackTarget.stats.Name + ".");
					MyConsole.NewMessage("Character " + stats.Name + " attacks " + attackTarget.stats.Name + ".");
            	    Debug.Log(attackTarget.stats.Name + "'s HP is now " + attackTarget.stats.CurrentHealth);
					MyConsole.NewMessage(attackTarget.stats.Name + "'s HP is now " + attackTarget.stats.CurrentHealth);
            	    attackCooldown = stats.AttackRate;
                    //Debug.Log("Attack cooldown: " + attackCooldown);
            	}
        	}
		}
    }

    public void ResolveCastOrder(CastOrder currentOrder) {
        Character targetCharacter = currentOrder.targetCharacter;
        Vector3 targetLocation = currentOrder.targetLocation;
        RandomAbility spell = currentOrder.spell;
		Vector3 targetVector = Vector3.zero;
		float targetDistance = 0f;

		//Debug.Log ("Resolving cast order");

		if (spell.targetOption == AbilityTargetOption.TARGET_ALLY || spell.targetOption == AbilityTargetOption.TARGET_ENEMY)
		{
			targetVector = targetCharacter.getCharacterPosition() - character.transform.localPosition;
			targetDistance = targetVector.magnitude;
		}
		else if (spell.targetOption == AbilityTargetOption.TARGET_LOCATION)
		{
			targetVector = targetLocation - character.transform.localPosition;
			targetDistance = targetVector.magnitude;
		}
        
        if (stats.CurrentMana < spell.manaCost)
        {
            Debug.Log("Not enough mana!");
			actionQueue.Pop();
        }
        else if (targetDistance > spell.range)
        {
			character.run();
            targetVector.Normalize();
            character.transform.localPosition += targetVector * Time.deltaTime * stats.MoveSpeed;
			character.transform.rotation = Quaternion.LookRotation(targetVector, new Vector3(0, 0, -1.0f));
        }
        else
        {
            if (timeUntilCast > 0f)
            {
				character.attack();
                Debug.Log("timeUntilCast = " + timeUntilCast);
                timeUntilCast -= Time.deltaTime;
            }
            else
            {
                Debug.Log("Spell " + spell.name + " resolving");
				MyConsole.NewMessage("Spell " + spell.name + " resolving");
                spell.Resolve(targetCharacter, targetLocation);
                actionQueue.Pop();
				if (targetCharacter != null)
				{
					if (targetCharacter.isenemy)
					{
						Enqueue(targetCharacter);
					}
				}
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

    public void character_gui_update() {
        barDisplay_1 = stats.CurrentHealth * 100 / stats.MaxHealth;
        barDisplay = stats.CurrentMana;

        if (((stats.CurrentHealth * 100) / stats.MaxHealth) > 40)
        {

            fullTex.SetPixel(1, 1, greencolor);
        }

        if (((stats.CurrentHealth * 100) / stats.MaxHealth) <= 40)
        {
            fullTex.SetPixel(1, 1, redcolor);
        }

    }

    public void setCharacterIdle() {
        character.idle();
    }

    public void flashred() {
        if (count_times > 0)
        {
            Damage_image.color = flashColour;
        }
        else
        {
            Damage_image.color = flashColour_1;
        }


    }

	public void GainExp (int exp)
	{
		if (isenemy == false)
		{
            stats.CurrentExp += exp;
			Debug.Log (stats.Name + " has gained " + exp + " exp! (" + stats.CurrentExp + "/" + stats.MaxExp + ")");
			MyConsole.NewMessage(stats.Name + " has gained " + exp + " exp! (" + stats.CurrentExp + "/" + stats.MaxExp + ")");
			if (stats.CurrentExp >= stats.MaxExp)
			{
				stats.CurrentExp -= stats.MaxExp;
				LevelUp();
			}
		}
	}

	public void LevelUp ()
	{
		stats.Level += 1;
		stats.UnallocatedStatPoints += 5;
		stats.CurrentHealth = stats.MaxHealth;
		stats.CurrentMana = stats.MaxMana;

		Debug.Log (stats.Name + " has advanced to level " + stats.Level + "!");
		MyConsole.NewMessage(stats.Name + " has advanced to level " + stats.Level + "!");
		Debug.Log (stats.Name + " has " + stats.UnallocatedStatPoints + " stat points to spend.");
		MyConsole.NewMessage(stats.Name + " has " + stats.UnallocatedStatPoints + " stat points to spend.");
	}

    public int getLevel() {
        return stats.Level;
    }

    public void setAggro(bool a) {
        isAggrod = a;
    }

    public bool isAggro() {
        return isAggrod;
    }

    public void checkForAggro() {
        for(int i = 0; i < EnemyCollection.NumberOfEnemies(); i++)
        {
            if(!EnemyCollection.getEnemy(i).isDead)
            {
                if(Vector3.Distance(getCharacterPosition(), EnemyCollection.getEnemy(i).getCharacterPosition()) < detectRange)
                {
                    if(EnemyCollection.getEnemy(i).isAggro() && actionQueue.Count() == 0)
                    {
                        Enqueue(EnemyCollection.getEnemy(i));
                    }
                }
            }
        }
    }

	private int CalculateHPMPBoxWidth (float stat)
	{
		//return 10 * Mathf.FloorToInt (Mathf.Log10 (stat));
		if (stat < 10)
		{
			return 10;
		}
		else if (stat < 100)
		{
			return 20;
		}
		else if (stat < 1000)
		{
			return 30;
		}
		else if (stat < 10000)
		{
			return 40;
		}
		else
		{
			return 50;
		}
	}

	void statPrintCheck()
	{
		if (Input.GetButtonDown("Print Stats"))
		{
			Debug.Log ("Strength: " + stats.Strength);
			Debug.Log ("Agility: " + stats.Agility);
			Debug.Log ("Intelligence: " + stats.Intelligence);
		}
	}
}



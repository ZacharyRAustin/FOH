using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class levelchange
{
	private static List< RandomAbility > buttons = new List<RandomAbility>();
	private static List< Equipment > buttons_equip = new List<Equipment>();
	private static Vector2 viewPoint;
	private static GUIStyle style;
	private static GUIStyle style1;
	private static Color flashColour;
	private static Texture2D texture;
	private static bool close =  true;
	private static Texture2D image_texture;
	private static Texture2D image_texture1;
	private static Texture2D image_texture2;
	private static string player_name;
	private static RandomAbility selected_ability;
	private static Equipment selected_equipment;

	public static void start(){
		image_texture = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
		image_texture = Resources.Load<Texture2D>("Hero1_image");
		image_texture1 = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
		image_texture1 = Resources.Load<Texture2D>("Hero2_image");
		image_texture2 = new Texture2D(1024, 1024, TextureFormat.DXT1, false);
		image_texture2 = Resources.Load<Texture2D>("Hero3_image");
		}
	
	public static void Drawlayout(){ 
		style1 = new GUIStyle ();
		texture = new Texture2D (1, 1);
		texture.SetPixel (1, 1, Color.clear);
		style1.normal.background = texture;
		style = new GUIStyle ();
		style.fontSize = 100;
		int i = 0;
		int j = 0;
		if (close == false) {
			GUILayout.BeginArea (new Rect (Screen.width/3, Screen.height - 500, 400, 330),style1);
			if (GUI.Button(new Rect(20, 250, 60, 60), image_texture)){
				player_name = "playerCharA";
				MyConsole.NewMessage("BUTTON CLICKED");
				MyConsole.NewMessage(player_name);
			}
			else{
				//player_name = "";
			}
			if (GUI.Button(new Rect(100, 250, 60, 60), image_texture1)){
				player_name = "playerCharB";

			}						
			else{
				//player_name = "";
			}
			if (GUI.Button(new Rect(180, 250, 60, 60), image_texture2)){
				player_name = "playerCharC";
			}
			else{
				//player_name = "";
			}

			foreach(RandomAbility s in buttons){
				//GUI.Label(new Rect(20, 250, 60, 60), "chec");
				if(i < 4){
					if (GUI.Button(new Rect((i)*90 + 20, 20, 80, 60), s.name)){

						MyConsole.NewMessage(s.name);
						if(selected_ability == null){ 
						selected_ability = s;

						}

						//GUILayout.BeginArea (new Rect (20, 250, 400, 250),style1);

						//GUILayout.EndArea();
			}
					else{
					
					}
					i = i + 1;
				}

			}
			foreach(Equipment r in buttons_equip){
			       if(j<4){

					if (GUI.Button(new Rect((j)*90 + 20,90, 80 , 60), r.name)){
						if(selected_equipment == null){
							selected_equipment = r;
						}

					}
					else{
					
					}

					j =j +1;
			}
			}

			

			if (GUI.Button(new Rect(150, 200, 80, 30), "ENTER")){
				close = true;
				buttons.Clear();
			}
			else{
				close = false;
			}
			GUILayout.EndArea();
		} 
		else{
				}
		//		flashColour = new Color(1f, 0f, 0f, 0.1f);
//		style = new GUIStyle ();
//		style1 = new GUIStyle ();
//		texture = new Texture2D (1, 1);
//		texture.SetPixel (1, 1, flashColour);
//		
//		
//		GUI.DrawTexture (new Rect (Screen.width - 200, Screen.height - 500, 200, 500),texture);
//		GUILayout.BeginArea (new Rect (Screen.width - 200, Screen.height - 500, 200, 500));
//		style.fontSize = 10;
//		style.fontStyle = FontStyle.Normal;
//		viewPoint = GUILayout.BeginScrollView (viewPoint);
//		GUILayout.BeginVertical ();
//		GUILayout.FlexibleSpace ();
//		foreach (string s in messages)
//			GUILayout.Label(s,style);
//		GUILayout.EndVertical();
//		GUILayout.EndScrollView();
//		GUILayout.EndArea();
	}
	
	//this function hides the layout is useless as i have already created a button that does the same but can be added in the code just in case
	public static void hidelayout(){
		close = true;
	}
	//this function shows the layout or the window to select the abilities
	public static void showlayout(){
		close = false;
	}
	//this function pushes new abilities on screen
	public static void push_abilities(RandomAbility name){
		buttons.Add (name);
	}
	public static void push_equipment(Equipment equip){
		buttons_equip.Add (equip);
		}
	//clears all the abilities should be used on each level change and then new abilities should be pushed
	public static void clear(){
		MyConsole.NewMessage ("cleared alll");
		buttons.Clear ();
		buttons_equip.Clear ();
		}
	public static string getplayer(){
		return player_name;
		}
	public static RandomAbility getselectedability(){
		return selected_ability;
		}
	public static Equipment getselectedequipment(){
		return selected_equipment;
		}
	public static void clear_all(){
		selected_ability = null;
		selected_equipment = null;
		player_name = "";
		}
	public static void clear_ability(){
		selected_ability = null;
		}
	public static void clear_equipment(){
		selected_equipment = null;
	}
	public static void clear_name(){
		player_name = "";
	}
	public static void clear_equipfromlist(Equipment e){
		buttons_equip.Remove (e);
//		foreach (Equipment r in buttons_equip) {
//			if(r == e){
//
//			}
//				}
		}
	public static void clear_abilityfromlist(RandomAbility a){
		buttons.Remove (a);
		
	}
}


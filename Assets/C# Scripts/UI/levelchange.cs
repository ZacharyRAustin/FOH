using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class levelchange
{
	private static List< string > buttons = new List<string>();
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
		if (close == false) {
			GUILayout.BeginArea (new Rect (Screen.width/3, Screen.height - 500, 400, 330),style1);
			if (GUI.Button(new Rect(20, 250, 60, 60), image_texture)){
				player_name = "playerCharA";
				
			}
			else{
				player_name = "";
			}
			if (GUI.Button(new Rect(100, 250, 60, 60), image_texture1)){
				player_name = "playerCharB";
			}						
			else{
				player_name = "";
			}
			if (GUI.Button(new Rect(180, 250, 60, 60), image_texture2)){
				player_name = "playerCharC";
			}
			else{
				player_name = "";
			}
			foreach(string s in buttons){
				//GUI.Label(new Rect(20, 250, 60, 60), "chec");
				if(i < 4){
					if (GUI.Button(new Rect((i)*90 + 20, 20, 80, 60), s)){
						//MyConsole.NewMessage(s);

						//GUILayout.BeginArea (new Rect (20, 250, 400, 250),style1);

						//GUILayout.EndArea();
			}
					else{
					
					}
				}
				else if(i>=4 && i<=7){

					if (GUI.Button(new Rect((i-4)*90 + 20,90, 80 , 60), s)){

					}
					else{
					
					}

			}

				i = i + 1;
			}

			if (GUI.Button(new Rect(150, 200, 80, 30), "ENTER")){
				close = true;
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
	public static void push_abilities(string name){
		buttons.Add (name);
	}
	//clears all the abilities should be used on each level change and then new abilities should be pushed
	public static void clear(){
		buttons.Clear ();
		}
	

}


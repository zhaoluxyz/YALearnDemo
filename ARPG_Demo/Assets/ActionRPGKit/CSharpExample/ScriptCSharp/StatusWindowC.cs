﻿using UnityEngine;
using System.Collections;
[RequireComponent (typeof (StatusC))]

public class StatusWindowC : MonoBehaviour {
	
	private bool  show = false;
	public GUIStyle textStyle;
	public GUIStyle textStyle2;
	
	//Icon for Buffs
	public Texture2D braveIcon;
	public Texture2D barrierIcon;
	public Texture2D faithIcon;
	public Texture2D magicBarrierIcon;

	public GUISkin skin;
	public Rect windowRect = new Rect (180, 170, 240, 320);
	private Rect originalRect;

	void Start(){
		originalRect = windowRect;
	}

	void Update (){
		if(Input.GetKeyDown("c")){
			OnOffMenu();
		}
	}
	
	void OnGUI (){
		StatusC stat = GetComponent<StatusC>();
		GUI.skin = skin;
		if(show){
			windowRect = GUI.Window (0, windowRect, StatWindow, "Status");
		}
		
		//Show Buffs Icon
		if(stat.brave){
			GUI.DrawTexture( new Rect(30,200,60,60), braveIcon);
		}
		if(stat.barrier){
			GUI.DrawTexture( new Rect(30,260,60,60), barrierIcon);
		}
		if(stat.faith){
			GUI.DrawTexture( new Rect(30,320,60,60), faithIcon);
		}
		if(stat.mbarrier){
			GUI.DrawTexture( new Rect(30,380,60,60), magicBarrierIcon);
		}
	}

	void  OnOffMenu (){
		//Freeze Time Scale to 0 if Status Window is Showing
		if(!show && Time.timeScale != 0.0f){
			show = true;
			Time.timeScale = 0.0f;
			ResetPosition();
			Screen.lockCursor = false;
		}else if(show){
			show = false;
			Time.timeScale = 1.0f;
			Screen.lockCursor = true;
		}
	}

	void  StatWindow ( int windowID  ){
		StatusC stat = GetComponent<StatusC>();
		GUI.Label ( new Rect(20, 40, 100, 50), "Level" , textStyle);
		GUI.Label ( new Rect(20, 70, 100, 50), "STR" , textStyle);
		GUI.Label ( new Rect(20, 100, 100, 50), "DEF" , textStyle);
		GUI.Label ( new Rect(20, 130, 100, 50), "MATK" , textStyle);
		GUI.Label ( new Rect(20, 160, 100, 50), "MDEF" , textStyle);
		
		GUI.Label ( new Rect(20, 220, 100, 50), "EXP" , textStyle);
		GUI.Label ( new Rect(20, 250, 100, 50), "Next LV" , textStyle);
		GUI.Label ( new Rect(20, 280, 120, 50), "Status Point" , textStyle);
		//Close Window Button
		if (GUI.Button ( new Rect(200,5,30,30), "X")) {
			OnOffMenu();
		}
		//Status
		int lv = stat.Level;
		int atk = stat.atk;
		int def = stat.def;
		int matk = stat.matk;
		int mdef = stat.mdef;
		int exp = stat.Exp;
		int next = stat.MaxExp - exp;
		int stPoint = stat.statusPoint;
		
		GUI.Label ( new Rect(30, 40, 100, 50), lv.ToString() , textStyle2);
		GUI.Label ( new Rect(70, 70, 100, 50), atk.ToString() , textStyle2);
		GUI.Label ( new Rect(70, 100, 100, 50), def.ToString() , textStyle2);
		GUI.Label ( new Rect(70, 130, 100, 50), matk.ToString() , textStyle2);
		GUI.Label ( new Rect(70, 160, 100, 50), mdef.ToString() , textStyle2);
		
		GUI.Label ( new Rect(95, 220, 100, 50), exp.ToString() , textStyle2);
		GUI.Label ( new Rect(95, 250, 100, 50), next.ToString() , textStyle2);
		GUI.Label ( new Rect(95, 280, 100, 50), stPoint.ToString() , textStyle2);
		
		if(stPoint > 0){
			if (GUI.Button ( new Rect(200,70,25,25), "+") && stPoint > 0) {
				GetComponent<StatusC>().atk += 1;
				GetComponent<StatusC>().statusPoint -= 1;
				GetComponent<StatusC>().CalculateStatus();
			}
			if (GUI.Button ( new Rect(200,100,25,25), "+") && stPoint > 0) {
				GetComponent<StatusC>().def += 1;
				GetComponent<StatusC>().MaxHealth += 5;
				GetComponent<StatusC>().statusPoint -= 1;
				GetComponent<StatusC>().CalculateStatus();
			}
			if (GUI.Button ( new Rect(200,130,25,25), "+") && stPoint > 0) {
				GetComponent<StatusC>().matk += 1;
				GetComponent<StatusC>().MaxMana += 3;
				GetComponent<StatusC>().statusPoint -= 1;
				GetComponent<StatusC>().CalculateStatus();
			}
			if (GUI.Button ( new Rect(200,160,25,25), "+") && stPoint > 0) {
				GetComponent<StatusC>().mdef += 1;
				GetComponent<StatusC>().statusPoint -= 1;
				GetComponent<StatusC>().CalculateStatus();
			}
		}
		GUI.DragWindow (new Rect (0,0,10000,10000)); 
	}
	
	void  ResetPosition (){
		//Reset GUI Position when it out of Screen.
		if(windowRect.x >= Screen.width -30 || windowRect.y >= Screen.height -30 || windowRect.x <= -70 || windowRect.y <= -70 ){
			windowRect = originalRect;
		}
		
	}

}
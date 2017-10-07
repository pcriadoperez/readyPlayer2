using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.Text;

public class MovieScript : MonoBehaviour {
	public List<Quote> script;
	public Text scriptText;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void parseFile()
 {
     string scriptText = File.ReadAllText("script");
 
     string[] quotes = script.Split(';');
 
     foreach(string quote in quotes)
     {
		 string character = quote.Split(':',0);
		 string text = quote.Split(':',1);
         script.Add(new Quote(character, text));
     }
}
}
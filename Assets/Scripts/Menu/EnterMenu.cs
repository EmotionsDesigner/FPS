using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnterMenu : MonoBehaviour {

	public Button enter;
    public Button quit;
	// Use this for initialization
	void Start () {
	enter=enter.GetComponent<Button>();
    quit=quit.GetComponent<Button>();
    Cursor.visible = true;
	}
	
	public void Enter(){
        Cursor.visible = false;
        Application.LoadLevel("hitTheLights");
    }
    public void ExitGame(){
        Application.Quit();
    }
}


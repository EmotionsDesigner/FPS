using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOver : MonoBehaviour {
    public Button tryAgain;
    public Button quit;
	// Use this for initialization
	void Start () {
	tryAgain=tryAgain.GetComponent<Button>();
    quit=quit.GetComponent<Button>();
    Cursor.visible = true;
	}
	
	public void TryAgain(){
        Cursor.visible = false;
        Application.LoadLevel("hitTheLights");
    }
    public void ExitGame(){
        Application.Quit();
    }
}

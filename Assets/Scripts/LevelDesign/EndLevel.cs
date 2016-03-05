using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour {

    IEnumerator LoadLevelFinish()
    {
        yield return new WaitForSeconds(1.0f);
        Application.LoadLevel("gameFinish");
    }
	   void OnTriggerEnter(Collider col) {

           if (col.transform.tag == "Player")
               StartCoroutine(LoadLevelFinish());

    }
}

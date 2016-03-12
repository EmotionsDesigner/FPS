using UnityEngine;
using System.Collections;

public class ghulAI : MonoBehaviour {
    //rozszerza mechanikę skryptu EnemyAI

    public AIPath ghulAIPath;


    IEnumerator StartMoving()
    {
        //opóźnienie poruszania o sekundę (animacja wychodzenia ghula z podłogi trwa 2.44s)
        ghulAIPath.canMove = false;
        yield return new WaitForSeconds(2.44f);
        ghulAIPath.canMove = true; ;

    }

    void Start()
    {
        StartCoroutine(StartMoving());
    }
}

using UnityEngine;
using System.Collections;

public class MoveBetweenObjects : MonoBehaviour {

    //referencja do ZombieAI
    public ZombieAI ZombieAI;

      //cele statyczne (między którymi porusza się zombie, dopóki gracz nie będzie w jego zasięgu)
    public GameObject[] staticTargets;
    //w jakiej odległości od celu wybrać już kolejny cel
    public float pickNextTarget;
    //cel statyczny, do którego aktualnie zmierza (null jeżeli zmierza do gracza
    GameObject actualStaticTarget;
    //id w tablicy aktualnego celu
    int actualTargetID;
    GameObject closestTarget;
    //określa czy poruszamy się do kolejnych czy poprzednich el. tablicy
    bool movingForward;
    //liczba statycznych celów
    int targetsAmount;


      //ustawia aktualny cel statyczny na najbliższy potworowi
    void SetNewStaticTarget()
    {
        if (staticTargets.Length>0){
            closestTarget = staticTargets[0];
            float closestDistance = Vector3.Distance(transform.position, staticTargets[0].transform.position);
            int id = 0;
            foreach (GameObject staticTarget in staticTargets)
            {
                //odległość od celu statycznego
                float distance = Vector3.Distance(transform.position, staticTarget.transform.position);
           
                if (distance < closestDistance)
                {
                    closestDistance=distance;
                    closestTarget = staticTarget;
                    actualTargetID = id;
                    actualStaticTarget = closestTarget;
                }
                id++;
            }
         }
    }

    //potwór porusza się między kolejnymi celami z tablicy, dopóki gracz nie znajdzie się w jego zasięgu
    void MoveBetweenStaticTargets() {
  
          //ustalenie aktualnego celu
            ZombieAI.target = actualStaticTarget;
              //odległość od aktualnego celu statycznego
            float targetDistance = Vector3.Distance(ZombieAI.transform.position, actualStaticTarget.transform.position);
            //jeżeli jesteśmy dostatecznie blisko
            if (targetDistance < pickNextTarget)
            {
                //idziemy "do przodu"
                if (movingForward) { 
                        if (actualTargetID < targetsAmount - 1){
                            //ustawienie celu
                            actualTargetID++;
                            actualStaticTarget = staticTargets[actualTargetID];
                        }
                        else{
                            //ustawiamy flagę, od teraz idziemy "do tyłu"
                            movingForward = false;
                            //ustawienie celu
                            actualTargetID--;
                            actualStaticTarget = staticTargets[actualTargetID];
                        }
                }
                //idziemy "do tyłu
                else{
                    if (actualTargetID > 0){
                        //ustawienie celu
                        actualTargetID--;
                        actualStaticTarget = staticTargets[actualTargetID];
                    }
                    else {
                        //ustawiamy flagę, od teraz "do przodu"
                         movingForward = true;
                         //ustawienie celu
                         actualTargetID++;
                         actualStaticTarget = staticTargets[actualTargetID];
                    }
                }
            }
        }
    



	// Use this for initialization
	void Start () {
	if (staticTargets.Length < 2)
            Debug.LogError("min 2 cele statyczne");
    //poruszanie między celami w kolejności takiej jakiej są zapisane w tablicy, gdy dojdzie do końca, cofa się do początku itd.
    actualTargetID = 0;
    actualStaticTarget = staticTargets[actualTargetID];
    movingForward = true;
    targetsAmount = staticTargets.Length;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //gracz jest poza zasięgiem
        if (!ZombieAI.isHunting)
            MoveBetweenStaticTargets();
            //gdy atakujemy gracza, ustawiamy nowy cel statyczny do którego wrócimy, gdy gracz ucieknie
        else
            SetNewStaticTarget();



	}
}

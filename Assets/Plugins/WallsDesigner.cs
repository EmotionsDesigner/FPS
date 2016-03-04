using UnityEngine;
using System.Collections;

public class WallsDesigner : MonoBehaviour {
    public GameObject leftWall;
    public GameObject rightWall;
    //długość korytarza
    public int length;
    float wallLenght = 5.07f;
	// Use this for initialization

    //długość korytarza ustalamy kopiując kolejne ściany i ustawiając ich pozycje
	void Start () {
        if (leftWall!=null && rightWall!=null){
                 Transform l = leftWall.transform;
                 Transform r = rightWall.transform;   
            for (int i = 1; i < length; i++) { 
                //pozycja kolejnego fragmentu to pozycja oryginalnego przesuniętego w osi X
                     //lewa ściana
                    Vector3 positionClone = new Vector3(l.position.x+i*wallLenght, l.position.y,l.position.z);
                    Object leftWallClone = Instantiate(leftWall, positionClone, leftWall.transform.rotation);
                     //prawa ściana
                    positionClone = new Vector3(r.position.x+i*wallLenght, r.position.y,r.position.z);
                    Object rightWallClone = Instantiate(rightWall, positionClone, rightWall.transform.rotation);
            }     
        }	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

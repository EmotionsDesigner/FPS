using UnityEngine;
using System.Collections;



public class WallsDesigner : MonoBehaviour {
    [ExecuteInEditMode]
    public GameObject leftWall;
    public GameObject rightWall;
    //długość korytarza
    public int leftLength;
    public int rightLenght;
    public float wallLenght = 2.11f;
	// Use this for initialization

    

	// Update is called once per frame
	public void BuildCorridor () {
      if (Application.isEditor && !Application.isPlaying) 
        {


	             if (leftWall!=null && rightWall!=null){
                             Transform l = leftWall.transform;
                             Transform r = rightWall.transform;   
                        for (int i = 1; i < leftLength; i++) { 
                            //pozycja kolejnego fragmentu to pozycja oryginalnego przesuniętego w osi X
                                 //lewa ściana
                                Vector3 positionClone = new Vector3(l.position.x+i*wallLenght, l.position.y,l.position.z);
                                GameObject leftWallClone = (GameObject)Instantiate(leftWall, positionClone, leftWall.transform.rotation);
                                leftWallClone.transform.parent = transform;
                            }
                        for (int i = 1; i < rightLenght; i++)
                        {
                            //prawa ściana
                            Vector3 positionClone = new Vector3(r.position.x + i * wallLenght, r.position.y, r.position.z);
                            GameObject rightWallClone = (GameObject)Instantiate(rightWall, positionClone, rightWall.transform.rotation);
                            rightWallClone.transform.parent = transform;
                        }
                            
                    }	
	            } 
            }
    
}

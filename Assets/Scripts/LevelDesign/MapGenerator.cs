using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {
    [ExecuteInEditMode]
    enum direction
    {
        UP,DOWN,RIGHT,LEFT
    }
    //jakiś pusty gameobject przetrzymujący wszystkie ściany jako dzieci
    public GameObject emptyContainer;
    //pojedyńcza ściana (bazowa)
    public GameObject wall;
    //aktualnie rysowana ściana(na jej podstawie wyliczana jest pozycja kolejnej
    public GameObject tempWall;
    //kierunek budowania poprzedniej ściany 
    //inaczej obliczamy pozycje kolejnej ściany, jeżeli budujemy corner ,a inaczej jeżeli jest to któraś kolejna ściana budowana w danym kierunku
    direction tempDirection=direction.UP;


    //wymiar pojedyńczej ściany
    public Vector3 wallSize = new Vector3(2.0f, 2.0f, 0.1f);

	// Use this for initialization
	void Start () {
        tempWall = wall;
	}
	
	// Update is called once per frame


    GameObject CreateWall(Vector3 position, Quaternion quaternion)
    {
        tempWall=(GameObject)Instantiate(wall, position, quaternion);
        tempWall.transform.parent = emptyContainer.transform;
        return tempWall;
    }
    //generowanie kolejnych ścian skierowanych w odpowiednich kierunkach
	public void GenerateUp () {
         //wyjściowa pozycja to pozycja poprzedniej ściany
         Vector3 position = (tempWall.transform.position);
         Quaternion quaternion = tempWall.transform.rotation;
        //jeżeli ostatnia ściana była budowana "w górę"
        if (tempDirection == direction.UP)
        {     
            //kolejną ścianę budujemy na pozycji przesuniętej względem pozycji temp
                position.x += wallSize.x;
                CreateWall(position, quaternion);
                tempDirection = direction.UP;
       }
        else if (tempDirection == direction.RIGHT)
        {
              position.x += wallSize.x / 2;
              position.x += wallSize.z / 2;
              position.z -= wallSize.x / 2;
              position.z += wallSize.z / 2;
              CreateWall(position, quaternion);
              tempWall.transform.Rotate(new Vector3(0, -90, 0));
        }

      else if (tempDirection == direction.LEFT)
        {
              position.x += wallSize.x / 2;
              position.x += wallSize.z / 2;
              position.z += wallSize.x / 2;
              position.z -= wallSize.z / 2;
              CreateWall(position, quaternion);
              tempWall.transform.Rotate(new Vector3(0, -270, 0));
        }
        else
        {
             Debug.LogError("Na docelowym miejscu jest już ściana!");
        }
              
        tempDirection = direction.UP;
	}
    public void GenerateDown () {
        Vector3 position = (tempWall.transform.position);
        Quaternion quaternion = tempWall.transform.rotation;
        if (tempDirection==direction.UP)
        {
            Debug.LogError("Na docelowym miejscu jest już ściana!");
            return;
        }
        else if (tempDirection == direction.DOWN)
        {     
            position.x -= wallSize.x;
            CreateWall(position, quaternion);

        }
        else if (tempDirection == direction.RIGHT)
        {           
              position.x -= wallSize.x / 2;
              position.x -= wallSize.z / 2;
              position.z -= wallSize.x / 2;
              position.z += wallSize.z / 2;
              CreateWall(position, quaternion);
              tempWall.transform.Rotate(new Vector3(0, 90, 0));
        }

       else if (tempDirection == direction.LEFT)
        {           
              position.x -= wallSize.x / 2;
              position.x += wallSize.z/2;
              position.z += wallSize.x / 2;
              position.z += wallSize.z / 2;
              CreateWall(position, quaternion);
              tempWall.transform.Rotate(new Vector3(0, -90, 0));
        }
        tempDirection = direction.DOWN;
	}
      public void GenerateRight () {
           Vector3 position = (tempWall.transform.position);
           Quaternion quaternion = tempWall.transform.rotation;
          if (tempDirection == direction.UP)
          {               
                position.x += wallSize.x / 2;
                position.x -= wallSize.z / 2;
                position.z -= wallSize.x / 2;
                position.z -= wallSize.z / 2;
                CreateWall(position, quaternion);
                tempWall.transform.Rotate(new Vector3(0, 90, 0));
         }
          else if (tempDirection == direction.RIGHT)
          {
              position.z -= wallSize.x;
              CreateWall(position, quaternion);
          }
          else if (tempDirection == direction.DOWN)
          {
              position.x -= wallSize.x / 2;
              position.x -= wallSize.z / 2;
              position.z -= wallSize.x / 2;
              position.z += wallSize.z / 2;
              CreateWall(position, quaternion);
              tempWall.transform.Rotate(new Vector3(0, -90, 0));
          }
          else
          {
              Debug.LogError("Kierunek budowania jest wyznaczany zakładając, że obserwator patrzy w kierunku osi X");
          }
                tempDirection = direction.RIGHT;
	}
      public void GenerateLeft () {
          Vector3 position = (tempWall.transform.position);
          Quaternion quaternion = tempWall.transform.rotation;
          if (tempDirection == direction.LEFT)
          {
                position.z += wallSize.x;
                CreateWall(position, quaternion);
          }
          else if (tempDirection == direction.DOWN)
          {
              position.x -= wallSize.x / 2;
              position.x -= wallSize.z / 2;
              position.z += wallSize.x / 2;
              position.z -= wallSize.z / 2;
              CreateWall(position, quaternion);
              tempWall.transform.Rotate(new Vector3(0, -270, 0));
          }
         else if (tempDirection == direction.UP)
          {
              position.x += wallSize.x / 2;
              position.x -= wallSize.z / 2;
              position.z += wallSize.x / 2;
              position.z += wallSize.z / 2;
              CreateWall(position, quaternion);
              tempWall.transform.Rotate(new Vector3(0, -90, 0));
          }
       else
          {
                Debug.LogError("Na docelowym miejscu jest już ściana!");
          }
            
            tempDirection = direction.LEFT;
	}
}

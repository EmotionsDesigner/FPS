using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapGenerator : MonoBehaviour {
    [ExecuteInEditMode]
    enum direction
    {
        UP,DOWN,RIGHT,LEFT
    }
    //czy tempWall==baseWall (true tylko przy pierwszej ścianie) 
    static bool tempIsBase=true;
    //jakiś pusty gameobject przetrzymujący wszystkie ściany jako dzieci
    static GameObject emptyContainer;
    //pojedyńcza ściana (bazowa)
    static GameObject wall;
    //aktualnie rysowana ściana(na jej podstawie wyliczana jest pozycja kolejnej
    //aktualny obiekt klasy
    MapGenerator prev;
    GameObject tempWall;
    //kierunek budowania poprzedniej ściany 
    //inaczej obliczamy pozycje kolejnej ściany, jeżeli budujemy corner ,a inaczej jeżeli jest to któraś kolejna ściana budowana w danym kierunku
    direction tempDirection=direction.UP;
    //każda ściana posiada referencję na poprzednika (aby można było cofać zmiany
    static List<GameObject> list = new List<GameObject>();
    //wymiar pojedyńczej ściany
    public Vector3 wallSize = new Vector3(2.0f, 2.0f, 0.1f);

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame

    void PrepareGenerator()
    {
        //warunek spełniony tylko, gdy nie wygrnerowaliśmy jeszcze żadnej ściany
        if (tempIsBase)
        {
          
            //ustawiamy odpowiednie referencje
            tempIsBase = false;
            wall = this.gameObject;
            tempWall = wall;
            list.Add(tempWall);
            emptyContainer = new GameObject("Walls");
            wall.transform.parent = emptyContainer.transform;

        }
    }

    //gdy chcemy zakończyć budowę korytarza, rozpocząć inny
    public void ResetGenerator()
    {
        tempIsBase = true;
        tempDirection=direction.UP;
        list.Clear();

        Debug.Log("Reset generatora (używać jeżeli rozpoczynamy nowy korytarz)");

    }

    //krok do tyłu (usunięcie ostatnio zbudowanej ściany)
    public void Undo()
    {
        if (list.Count > 1)
        {
            //pobieramy ostatni element z listy
            GameObject temp = list[list.Count - 1];
            //ustawiamy nowego tempa jako przedostatni element listy
            tempWall = list[list.Count - 2];
            tempDirection = tempWall.GetComponent<MapGenerator>().tempDirection;
            //usuwamy ostatni z listy
            list.RemoveAt(list.Count - 1);
            //i sceny  
            DestroyImmediate(temp);
            Debug.Log(list.Count);
        }
        else
        {
             DestroyImmediate(this.tempWall);
             GameObject a = emptyContainer;
             DestroyImmediate(a);
             ResetGenerator();
        }
       
    }
 
     

    
    GameObject CreateWall(Vector3 position, Quaternion quaternion)
    {
 
        tempWall=(GameObject)Instantiate(wall, position, quaternion);
        list.Add(tempWall);
        tempWall.transform.parent = emptyContainer.transform;
        return tempWall;
    }
    //generowanie kolejnych ścian skierowanych w odpowiednich kierunkach
	public void GenerateUp () {
        PrepareGenerator();
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
              position.x -= wallSize.z / 2;
              position.z -= wallSize.x / 2;
              position.z -= wallSize.z / 2;
              CreateWall(position, quaternion);
              tempWall.transform.Rotate(new Vector3(0, -90, 0));
        }

      else if (tempDirection == direction.LEFT)
        {
              position.x += wallSize.x / 2;
              position.x -= wallSize.z / 2;
              position.z += wallSize.x / 2;
              position.z += wallSize.z / 2;
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
        PrepareGenerator();
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
              position.x += wallSize.z / 2;
              position.z -= wallSize.x / 2;
              position.z -= wallSize.z / 2;
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
           PrepareGenerator();
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
              position.x += wallSize.z / 2;
              position.z -= wallSize.x / 2;
              position.z -= wallSize.z / 2;
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
          PrepareGenerator();
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
              position.x += wallSize.z / 2;
              position.z += wallSize.x / 2;
              position.z += wallSize.z / 2;
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

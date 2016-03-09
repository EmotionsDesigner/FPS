using UnityEngine;
using System.Collections;

public class DoorsOpener : MonoBehaviour {
    public GameObject Door;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //prototype version, improve it !
             Door.transform.Rotate(Vector3.up);

            
	}
}

using UnityEngine;
using System.Collections;

public class BodyDamage : MonoBehaviour {
    public EnemyAI Enemy;
    public float damage;
     void OnCollisionEnter(Collision col)
    {

        if (col.transform.tag == "Bullet" && Enemy.health>0 && col.transform.name!="BulletBase")
        {
            Debug.Log("Ghul dostał w " + this.gameObject.name);
              Enemy.health -= damage ;
              AudioSource a = GameObject.Find("Hit").GetComponent<AudioSource>();
              a.Play();

        }
        else
        {//zombie natrafi na przeszkode
            Enemy.speed = 0f;
        }
          
        
    }
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //if (Zombie.Health <= 0)
            //Destroy(gameObject);
	}
}

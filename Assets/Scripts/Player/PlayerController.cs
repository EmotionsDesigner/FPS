using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    public bool alreadyShooting = false;
    public float shootingDelay=0.3f;
    public GameObject ammo;
    public bool alive;
    public GameObject weapon;
    Animator animator;

    bool deathmoment = false;

    IEnumerator LoadGameOver()
    {
        yield return new WaitForSeconds(4.5f);
        Application.LoadLevel("overMenu");
    }
    void PlayAudio(string name)
    {

          AudioSource audio = GameObject.Find(name).GetComponent<AudioSource>();
           audio.Play();


    }
    
    IEnumerator Shoot()
        {
               
                alreadyShooting = true;
                AudioSource Shoot = GameObject.Find("GunShot").GetComponent<AudioSource>();
                Shoot.Play();          
                //utworzenie pocisku
                GameObject Bullet = (GameObject)Instantiate(ammo, ammo.transform.position, ammo.transform.rotation);
                //wystrzał
                //odmrożenie pozycji pocisku, zamrożenie rotacji 
                Bullet.GetComponent<Rigidbody>().constraints &= RigidbodyConstraints.FreezeRotation;
                Bullet.GetComponent<Rigidbody>().AddForce(ammo.transform.up*4000);

                yield return new WaitForSeconds(shootingDelay);
                alreadyShooting = false;
        }

	// Use this for initialization
	void Start () {
         Cursor.lockState = CursorLockMode.Locked;
         animator = GetComponent < Animator>();
         animator.enabled = false;
         alive = true;
	}


	// Update is called once per frame
	void FixedUpdate () {
        //śmierć gracza
        if (!alive)
        {

            //uaktualnienie flagi śmierci gracza w kontrolerze animacji
            animator.enabled = true;
             animator.SetBool("alive", alive);
            if (deathmoment==false)
               PlayAudio("PlayerDead");
            deathmoment = true;
            StartCoroutine(LoadGameOver());

           
        }
          
        Cursor.visible = false;  
        //strzał
        if (alreadyShooting == false)
        {
           if( Input.GetButtonDown("Fire1") ==false && Input.GetButton("Fire1")==false && Input.GetButtonUp("Fire1")==true)
                    StartCoroutine(Shoot());
        }
     
     
	
	}
}

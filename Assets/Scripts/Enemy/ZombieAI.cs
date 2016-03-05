using UnityEngine;
using System.Collections;
using Pathfinding;

public class ZombieAI : MonoBehaviour {
    public CharacterController controller;
    public GameObject Player;
    public PlayerController PlayerController;
    //referencja do wyszukiwacza ścieżki 
    public AIPath AIPath;

    //cel AKUTALNY
    public GameObject target;

    public float playerDistance;
    Vector3 TargetPosition;

  

    //właściwości obiektu
    public float attackDistance;
    //poniżej tej odległości zombie zaczyna podążać za graczem
    public float huntingRange;
    public float health;
    public float speed;
    float approximation=1f;
    Animator animator;
    bool attackPlaying = false;
    //w momencie śmierci przyjmuje wartość true
    bool deathMoment=false;
    bool attack = false;

    //czy aktualnie atakuje/poluje na gracza
    public bool isHunting;



    IEnumerator KillPlayer()
    {
        //po sekundzie ataku, gracz umiera
        yield return new WaitForSeconds(2.0f);
        if (attack && health>0)
            PlayerController.alive = false;
    }

    void OnCollisionEnter(Collision col)
    {
        //Debug.Log(col.transform.name);
    }

    void StartHunting()
    {
        isHunting = true;
        target = Player;
    }
    void StopMoving()
    {
        isHunting= false;
         //przerwanie pościgu i wyszukiwania ścieżki
            AIPath.canMove = false;
            AIPath.canSearch = false;
    }


    struct direction
    {
        public bool x;
        public bool y;
        public bool z;
      public void setX(){
          x=true;
          y=false;
          z=false;
      }
        public void setY(){
          x=false;
          y=true;
          z=false;
      }
        public void setZ(){
          x=false;
          y=false;
          z=true;
      }
    };

    //zwraca wartość, do którego dana współrzędna powinna dążyć
    float ChooseDirection(direction dir)
    {
        if (dir.x)
        {
            if (transform.position.x+approximation < TargetPosition.x)
                return speed;
            else if (transform.position.x-approximation > TargetPosition.x)
                return -speed;
            else
                return 0;
        }
       if (dir.y)
        {
            if (transform.position.y+approximation < TargetPosition.y)
                return speed;
            else if (transform.position.y-approximation > TargetPosition.y)
                return -speed;
            else return 0;
        }
        if (dir.z)
        {
            if (transform.position.z +approximation< TargetPosition.z)
                return speed;
            else if (transform.position.z-approximation > TargetPosition.z)
                return -speed;
            else return 0;
        }
       return 0f;//zła wartość (tylko x,y)
    }

    IEnumerator AudioPlay(string name, float delay)
    {

        attackPlaying = true;
        AudioSource audio = GameObject.Find(name).GetComponent<AudioSource>();
        audio.Play();
        yield return new WaitForSeconds(delay);
        attackPlaying = false;
    }



	// Use this for initialization
	void Start () { 
        controller = GetComponent<CharacterController>();
        animator = GetComponent < Animator>();
        PlayerController = Player.GetComponent<PlayerController>();

  
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        //uaktualnienie punktów w życia w kontrolerze animacji
        animator.SetFloat("Health", health);
         animator.SetBool("Attack", attack);
        //uaktualnienie odległości od gracza
        TargetPosition = target.transform.position;
        playerDistance = Vector3.Distance(transform.position, Player.transform.position);

        //moment śmierci
        if (health <= 0 && deathMoment==false)
        {
            //zapobiega kolizji z niezyjącym potworem
            controller.enabled = false;
            deathMoment = true;
            StopMoving();

            //dezaktywuje siatki kolizji dla wszystkich części ciała umarłego zombie
            MeshCollider[] meshColliderChildren = gameObject.GetComponentsInChildren<MeshCollider>();
            foreach (MeshCollider meshChildCollider in meshColliderChildren)
                meshChildCollider.enabled = false;      
            AudioPlay("ZombieDead", 0);
        }

        if (health > 0)
        {   
               
                //pościg/atak gracza
            if (playerDistance < huntingRange || health<100)
            {
                StartHunting();
                //atak
                if (playerDistance <= attackDistance)
                {

                    isHunting = false;
                    //odwrócenie w strone gracza
                    transform.LookAt(target.transform);
                    attack = true;
                    if (attackPlaying == false)
                        StartCoroutine(AudioPlay("ZombieAttack", 2f));
                    StartCoroutine(KillPlayer());
                }
                else
                {
                    StartHunting();
                    attack = false;
                }

            }
            else
                isHunting = false;
      }
         //zombie nie żyje
            else {
                 isHunting = false;
            }
	}
}

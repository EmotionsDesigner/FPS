using UnityEngine;
using System.Collections;

public class ghulShoulderHitted : MonoBehaviour {
    //obsługuje animację ghula, gdy otrzyma strzał w lewe/prawe ramię (hitLeft hitRight)
    public EnemyAI enemy;
    public Animator ghulAnimator;
    //gdy ghul otrzyma strzał, musi się na chwile zatrzymać (za poruszanie odpowiada skrypt AIPath)
    public AIPath ghulAIPath;
    ghulAI ghulAI;
    void ghulStop()
    {
          ghulAIPath.canMove = false;
          ghulAIPath.speed = 0;

    }
    void ghulStart()
    {
          ghulAIPath.canMove = true;
          ghulAIPath.speed = ghulAI.speed;

    }
    IEnumerator MoveAgain()
    {
        //po tym czasie ghul dalej się porusza
        //(czas musi być równy czasowi trwania animacji hitLeft/right ghula
        yield return new WaitForSeconds(0.866f);
        ghulStart();
        ghulAnimator.SetBool("LeftHit", false);
        ghulAnimator.SetBool("RightHit", false);

    }
     void OnCollisionEnter(Collision col)
    {
        if (col.transform.tag == "Bullet" && enemy.health>0)
        {
            //jeżeli trafimy w lewe ramię, uruchamiamy odpowiednią animację
            if (this.gameObject.name == "L_shoulder")
            {
                ghulAnimator.SetBool("LeftHit", true);
                ghulStop();
                StartCoroutine(MoveAgain());
            }
            else if (this.gameObject.name == "R_shoulder")
            {
                ghulAnimator.SetBool("RightHit", true);
                ghulStop();
                StartCoroutine(MoveAgain());
            }
           
        }
   
          
        
    }


	// Use this for initialization
	void Start () {
        ghulAI = enemy.GetComponent<ghulAI>();
           
	}
	
	// Update is called once per frame
	void Update () {

	}
}

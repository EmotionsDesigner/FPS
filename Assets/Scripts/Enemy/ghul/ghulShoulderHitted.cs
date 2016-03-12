using UnityEngine;
using System.Collections;

public class ghulShoulderHitted : MonoBehaviour {
    //obsługuje animację ghula, gdy otrzyma strzał w lewe/prawe ramię (hitLeft hitRight)
    public EnemyAI enemy;
    public Animator ghulAnimator;
    //gdy ghul otrzyma strzał, musi się na chwile zatrzymać (za poruszanie odpowiada skrypt AIPath)
    public AIPath ghulAIPath;
    IEnumerator MoveAgain()
    {
        //po tym czasie ghul dalej się porusza
        //(czas musi być równy czasowi trwania animacji hitLeft ghula
        yield return new WaitForSeconds(0.5f);
        ghulAIPath.canMove = true;
        ghulAnimator.SetBool("LeftHit", false);

    }
     void OnCollisionEnter(Collision col)
    {

        if (col.transform.tag == "Bullet" && enemy.health>0)
        {
            //jeżeli trafimy w lewe ramię, uruchamiamy odpowiednią animację
            if (this.gameObject.name == "L_shoulder")
            {
                ghulAnimator.SetBool("LeftHit", true);
                ghulAIPath.canMove = false;
                StartCoroutine(MoveAgain());
            }
           

        }
   
          
        
    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

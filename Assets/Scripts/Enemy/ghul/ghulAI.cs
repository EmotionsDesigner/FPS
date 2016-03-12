using UnityEngine;
using System.Collections;

public class ghulAI : MonoBehaviour {
    //rozszerza mechanikę skryptu EnemyAI
    EnemyAI enemyAI;
    public AIPath ghulAIPath;
    //referencja do prędkości potwora w skrypcie AIPath(odpowiadającym za poruszanie)
    public float speed;
    bool live = true;
    //śmierć ghula
    void ghulDie()
    {
        //wyłączenie wszystkich colliderów i character kontrolera żeby nie bugować animacji śmierci (zawiśnięcie w powietrzu)
        live = false;
           BoxCollider[] boxColliderChildren = gameObject.GetComponentsInChildren<BoxCollider>();
            foreach (BoxCollider boxCollider in boxColliderChildren)
                boxCollider.enabled = false;
            CharacterController controller = GetComponent<CharacterController>();
            controller.enabled = false;
            ghulAIPath.enabled = false;
    }
    void ghulStart()
    {
        ghulAIPath.canMove = true;
        ghulAIPath.speed = speed;
    }
    void ghulStop()
    {
        ghulAIPath.speed = 0;
        ghulAIPath.canMove = false;
    }
    IEnumerator StartMoving()
    {
        //opóźnienie poruszania o sekundę (animacja wychodzenia ghula z podłogi trwa 2.44s)
        ghulStop();
        yield return new WaitForSeconds(2.44f);
        ghulStart();
    }

    void Start()
    {
        enemyAI = GetComponent<EnemyAI>();
        speed = ghulAIPath.speed;
        StartCoroutine(StartMoving());
    }
    void FixedUpdate()
    {
        if (enemyAI.health < 1 && live==true)
            ghulDie();

    }
}

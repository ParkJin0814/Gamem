using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Animator myAnimator;
    EnemyCharater myEnemyCharater;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        myEnemyCharater = gameObject.GetComponentInParent<EnemyCharater>();
    }
    private void Update()
    {
        myEnemyCharater.IsAttacking = myAnimator.GetBool("IsAttacking");
        myAnimator.SetBool("IsWalk", myEnemyCharater.IsWalk);
        myAnimator.SetBool("IsAttack", myEnemyCharater.IsAttack);
    }
}

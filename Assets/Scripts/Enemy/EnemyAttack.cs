using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    public SharedGameObject sharedGameObject;
    public SharedFloat sharedSpeed;

    GameObject player;
    Animator anim;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    BehaviorTree behaviorTree;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();   
    }

    private void Start()
    {
        behaviorTree = GetComponent<BehaviorTree>();
        sharedGameObject.Value = player;
        behaviorTree.SetVariable("Target", sharedGameObject);
        behaviorTree.RegisterEvent("AttackPlayer", Attack);
        behaviorTree.RegisterEvent<float>("SetSpeed", SetSpeed);
    }

    void Update()
    {
        if (playerHealth.currentHealth <= 0)
        {
            anim.SetTrigger("PlayerDead");
        }
    }

    void Attack()
    {
        if (enemyHealth.currentHealth > 0 && playerHealth.currentHealth > 0)
        {
            playerHealth.TakeDamage(attackDamage);
        }
    }

    void SetSpeed(float speed)
    {
        sharedSpeed.Value = speed;
        behaviorTree.SetVariable("MovingSpeed", sharedSpeed);
    }
}

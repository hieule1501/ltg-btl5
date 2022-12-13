using UnityEngine;
using System.Collections;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;

    public SharedGameObject sharedGameObject;
    public SharedInt sharedAttackDamage;


    GameObject player;
    Animator anim;
    PlayerHealth playerHealth;
    EnemyHealth enemyHealth;
    BehaviorTree behaviorTree;
    bool hasUpgradeAttack;

    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent <PlayerHealth> ();
        enemyHealth = GetComponent<EnemyHealth>();
        anim = GetComponent <Animator> ();   
    }

    private void Start()
    {
        hasUpgradeAttack = false;
        behaviorTree = GetComponent<BehaviorTree>();
        sharedGameObject.Value = player;
        behaviorTree.SetVariable("Target", sharedGameObject);
        behaviorTree.RegisterEvent("AttackPlayer", Attack);
        behaviorTree.RegisterEvent("UpgradeAttack", UpgradeAttack);
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

    void UpgradeAttack()
    {
        if (!hasUpgradeAttack)
        {
            attackDamage = attackDamage + 10;
            behaviorTree.SetVariable("AttackDamage", sharedAttackDamage);
        }
    }    
}

﻿using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using DG.Tweening;

public class EnemyHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public float sinkSpeed = 2.5f;
    public int scoreValue = 10;
    public AudioClip deathClip;

    public SharedInt sharedHp;

    Animator anim;
    AudioSource enemyAudio;
    ParticleSystem hitParticles;
    CapsuleCollider capsuleCollider;
    bool isDead;
    bool isSinking;
    BehaviorTree behaviorTree;
    bool hasUpgradeHp;
    EnemyEffect enemyEffect;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        enemyAudio = GetComponent <AudioSource> ();
        hitParticles = GetComponentInChildren <ParticleSystem> ();
        capsuleCollider = GetComponent<CapsuleCollider>();
        enemyEffect = GetComponent<EnemyEffect>();
        currentHealth = startingHealth;
    }

    private void Start()
    {
        behaviorTree = GetComponent<BehaviorTree>();
        behaviorTree.RegisterEvent("UpgradeHP", UpgradeHp);
        SetHPVariable(currentHealth);
        hasUpgradeHp = false;
    }

    void Update ()
    {
        if (isSinking)
        {
            transform.Translate (-Vector3.up * sinkSpeed * Time.deltaTime);
        }
    }


    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isDead) return;

        enemyAudio.Play ();

        currentHealth -= amount;
        SetHPVariable(currentHealth);

        hitParticles.transform.position = hitPoint;
        hitParticles.Play();

        if (currentHealth <= 0)
        {
            Death ();
        }
    }

    void Death ()
    {
        isDead = true;

        capsuleCollider.isTrigger = true;

        anim.SetTrigger ("Dead");

        enemyAudio.clip = deathClip;
        enemyAudio.Play ();
    }

    public void StartSinking ()
    {
        behaviorTree.enabled = false;
        GetComponent <UnityEngine.AI.NavMeshAgent> ().enabled = false;
        GetComponent <Rigidbody> ().isKinematic = true;
        isSinking = true;
        ScoreManager.score += scoreValue;
        enemyEffect.OnDie();
        Destroy (gameObject, 2f);
    }

    void SetHPVariable(int hp)
    {
        sharedHp.Value = hp;
        behaviorTree.SetVariable("HP", sharedHp);
    }

    void UpgradeHp()
    {
        if (!hasUpgradeHp)
        {
            transform.DOScale(Vector3.one * 2f, 2f);
            currentHealth += 200;
            SetHPVariable(currentHealth);
            hasUpgradeHp = true;
            enemyEffect.PlayUpgradeEffect();
            enemyEffect.EnableUpgradeAura();
        }
        
    }
        
}

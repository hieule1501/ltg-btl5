using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public bool IsSuper;


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement playerMovement;
    PlayerShooting playerShooting;
    bool isDead;
    bool damaged;
    PlayerEffect playerEffect;


    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement> ();
        playerShooting = GetComponentInChildren <PlayerShooting> ();
        currentHealth = startingHealth;
        playerEffect = GetComponent<PlayerEffect>();
        IsSuper = false;
    }


    void Update ()
    {
        healthSlider.value = currentHealth;
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
    }


    public void TakeDamage (int amount)
    {
        if (IsSuper) return;
        damaged = true;

        currentHealth -= amount;

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        playerEffect.OnDie();
        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;
    }

    public void RestartLevel ()
    {
        //Application.LoadLevel (Application.loadedLevel);
        SceneManager.LoadScene(0);
    }

    public void HealHP()
    {
        playerEffect.PlayUpgradeHealth();
        currentHealth += 50;
        startingHealth += 50;
        if (startingHealth > 300) startingHealth = 300;
        if (currentHealth > startingHealth) currentHealth = startingHealth;
    }

    public void SuperHealHP()
    {
        currentHealth = startingHealth;
        IsSuper = true;
        StartCoroutine(IECountSuper());
    }

    IEnumerator IECountSuper()
    {
        yield return new WaitForSeconds(5f);
        IsSuper = false;
    }
}

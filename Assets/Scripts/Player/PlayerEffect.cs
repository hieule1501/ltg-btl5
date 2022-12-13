using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem _upgradeHealth;
    [SerializeField] ParticleSystem _upgradeSpeed;
    [SerializeField] ParticleSystem _upgradeAttack;
    [SerializeField] ParticleSystem _upgradeAtkSpeed;
    [SerializeField] ParticleSystem _superUpgrade;

    public void PlayUpgradeHealth()
    {
        _upgradeHealth.Play();
    }

    public void PlayUpgradeSpeed()
    {
        _upgradeSpeed.Play();
    }

    public void PlayUpgradeAttack()
    {
        _upgradeAttack.Play();
    }

    public void PlayUpgradeAtkSpeed()
    {
        _upgradeAtkSpeed.Play();
    }

    public void PlaySuperUpgrade()
    {
        _superUpgrade.Play();
    }

    public void OnDie()
    {
        _upgradeHealth.Stop();
        _upgradeSpeed.Stop();
        _upgradeAttack.Stop();
        _upgradeAtkSpeed.Stop();
    }    
}

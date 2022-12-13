using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem _upgradeEffect;
    [SerializeField] private ParticleSystem _upgradeAura;

    private void Start()
    {
        _upgradeAura.gameObject.SetActive(false);
    }

    public void PlayUpgradeEffect()
    {
        _upgradeEffect.Play();
    }

    public void EnableUpgradeAura()
    {
        _upgradeAura.gameObject.SetActive(true);
    }

    public void OnDie()
    {
        _upgradeEffect?.Stop();
        _upgradeAura?.gameObject.SetActive(false);
    }
}

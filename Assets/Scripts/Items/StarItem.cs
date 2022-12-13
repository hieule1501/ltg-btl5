using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarItem : ItemBase
{
    protected override void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerShooting shoot = collision.gameObject.GetComponentInChildren<PlayerShooting>();
            PlayerEffect effect = collision.gameObject.GetComponent<PlayerEffect>();
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            PlayerMovement move = collision.gameObject.GetComponent<PlayerMovement>();
            if (health.IsSuper || shoot.IsSuper || move.IsSuper) return;
            shoot.UpgradeSuper();
            health.SuperHealHP();
            move.SuperUpgradeSpeed();
            effect.PlaySuperUpgrade();
            ItemManager.Instance.DespawnItem(transform);
        }
    }
}
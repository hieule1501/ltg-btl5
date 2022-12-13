using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubieItem : ItemBase
{
    protected override void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerShooting shoot = collision.gameObject.GetComponentInChildren<PlayerShooting>();
            if (shoot.IsSuper) return;
            shoot.UpgradeAttackSpeed();
            ItemManager.Instance.DespawnItem(transform);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondItem : ItemBase
{
    protected override void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMovement speed = collision.gameObject.GetComponent<PlayerMovement>();
            if (speed.IsSuper) return;
            speed.UpgradeSpeed();
            ItemManager.Instance.DespawnItem(transform);
        }
    }
}


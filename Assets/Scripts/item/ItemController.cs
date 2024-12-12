using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public ItemType _itemtype;
}

public enum ItemType
{
    None,
    Coin,
    SpeedItem,
    HealthItem,
    TimeItem,
    AttackUp,
    AttackItem
}

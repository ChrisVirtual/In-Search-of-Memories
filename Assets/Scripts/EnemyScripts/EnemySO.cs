using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "EnemySO")]
public class EnemySO : ScriptableObject
{
    [SerializeField] private float enemyHealth;
    [SerializeField] private int goldDropped;
    [SerializeField] private int expDropped;
    [SerializeField] private int damage;

    public float Health
    {
        get => enemyHealth;
        set
        {
            enemyHealth = value;
            OnHealthChange?.Invoke(enemyHealth);
        }
    }
    public event Action<float> OnHealthChange;

    public int Gold
    {
        get => goldDropped;
        set
        {
            goldDropped = value;
            OnGoldChange?.Invoke(goldDropped);
        }
    }
    public event Action<int> OnGoldChange;

    public int Exp
    {
        get => expDropped;
        set
        {
            expDropped = value;
            OnExpChange?.Invoke(expDropped);
        }
    }
    public event Action<int> OnExpChange;

    public int Damage
    {
        get => damage;
        set
        {
            damage = value;
            OnDamageChange?.Invoke(damage);
        }
    }
    public event Action<int> OnDamageChange;
}

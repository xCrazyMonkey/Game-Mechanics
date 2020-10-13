using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ZombieScriptableObject", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class ZombieScriptableObject : ScriptableObject
{
    public string zombieName;
    public float health;
    public float moveSpeed;
    public float damage;
    public float attackRange;
    public float attackCoolDown;
    public float viewDistance;

    public void DamageBuff(float damageBonus)
    {
        damage *= damageBonus;
    }
}

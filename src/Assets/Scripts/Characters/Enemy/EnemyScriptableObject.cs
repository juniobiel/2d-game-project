using System;
using UnityEngine;

namespace Assets.Scripts.Characters.Enemy
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/Enemy/CreateEnemy", order = 1)]
    public class EnemyScriptableObject : ScriptableObject
    {
        public Guid EnemyId = Guid.NewGuid();
        public String EnemyName;
        public float EnemyLife;
        public float EnemyHitDamage;
    }
}
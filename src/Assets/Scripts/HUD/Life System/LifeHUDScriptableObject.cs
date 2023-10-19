using System;
using UnityEngine;

namespace Assets.Scripts.HUD.LifeSystem
{
    [CreateAssetMenu(fileName = "LifeHUD", menuName = "ScriptableObjects/HUD/CreateLifeHUD", order = 1)]
    public class LifeHUDScriptableObject : ScriptableObject
    {
        public Guid LifeHUDId = Guid.NewGuid();
        public Sprite LifeBarBackground;
        public Sprite HealthPoint;
        
    }
}
using System;
using UnityEngine;

namespace Assets.Scripts.ScriptableObjects.Level
{
    [CreateAssetMenu(fileName = "Level", menuName = "ScriptableObjects/Level/CreateLevel", order = 1)]
    public class LevelScriptableObject : ScriptableObject
    {
        public Guid LevelId = Guid.NewGuid();
        public int LevelIndex;
        public Sprite LevelIcon;
        public Sprite LevelIconSelected;
        public Sprite LevelIconCompleted;
        public bool IsLevelCompleted = false;
    }
}

using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Manager.Enemies
{
    public class EnemiesManager : MonoBehaviour
    {
        private List<GameObject> EnemiesInPlayerRange;
        private List<GameObject> EnemiesInPlayerOutRange;

        private void OnEnable()
        {
            EnemiesInPlayerRange = new List<GameObject>();
            EnemiesInPlayerOutRange = new List<GameObject>();

            EnemyController.OnPlayerInRange += EnemyInRange;
            EnemyController.OnPlayerOutRange += EnemyInOutRange;
        }

        private void EnemyInRange(GameObject enemy)
        {
            if(EnemiesInPlayerOutRange.Contains(enemy))
                EnemiesInPlayerOutRange.Remove(enemy);
            
            if(!EnemiesInPlayerRange.Contains(enemy))
                EnemiesInPlayerRange.Add(enemy);
        }

        private void EnemyInOutRange(GameObject enemy)
        {
            if(EnemiesInPlayerRange.Contains(enemy))
                EnemiesInPlayerRange.Remove(enemy);

            if(!EnemiesInPlayerOutRange.Contains(enemy))
                EnemiesInPlayerOutRange.Add(enemy);
        }
    }
}

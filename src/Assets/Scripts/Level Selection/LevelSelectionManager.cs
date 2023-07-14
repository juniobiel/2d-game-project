using Assets.Scripts.Helpers;
using Assets.Scripts.LevelSelection;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Level_Selection
{
    public class LevelSelectionManager : Singleton<LevelSelectionManager>
    {

        [SerializeField]
        private GameObject SelectedLevelObject;

        private void OnEnable()
        {
            LevelPoint.OnLevelPointSelected += SelectLevelPoint;
        }

        //private void OnDisable()
        //{
        //    LevelPoint.OnLevelPointSelected -= SelectLevelPoint;
        //}

        private void SelectLevelPoint(GameObject levelPoint)
        {
            levelPoint.gameObject.GetComponent<SpriteRenderer>().color = Color.red;

            if(SelectedLevelObject == null)
                SelectedLevelObject = levelPoint;
            

            var levelPointId = levelPoint.GetComponent<LevelPoint>().LevelInformation.GetInstanceID();
            var selectedLevelId = SelectedLevelObject.GetComponent<LevelPoint>().LevelInformation.GetInstanceID();

            if (!levelPointId.Equals(selectedLevelId))
            {
                SelectedLevelObject.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                SelectedLevelObject = levelPoint;
            }

        }
    }
}

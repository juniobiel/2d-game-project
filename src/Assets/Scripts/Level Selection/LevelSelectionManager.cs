using Assets.Scripts.Helpers;
using Assets.Scripts.LevelSelection;
using UnityEngine;

namespace Assets.Scripts.Level_Selection
{
    public class LevelSelectionManager : Singleton<LevelSelectionManager>
    {

        [SerializeField]
        private GameObject SelectedLevelObject;

        /// <summary>
        /// Subscribing on event that is raised when the touch is colliding with level point selection
        /// </summary>
        private void OnEnable()
        {
            LevelPoint.OnLevelPointSelected += SelectLevelPoint;
        }

        #region SELECTION LEVEL POINT

        /// <summary>
        /// Used to direct the event when the level was pressed
        /// </summary>
        /// <param name="levelPoint">The object that player has select</param>
        private void SelectLevelPoint(GameObject levelPoint)
        {
            var levelIconSelected = levelPoint.GetComponent<LevelPoint>().LevelInformation.LevelIconSelected;

            levelPoint.gameObject.GetComponent<SpriteRenderer>().sprite = levelIconSelected;

            if (SelectedLevelObject == null)
                SelectedLevelObject = levelPoint;

            DeselectPreviousPoint(levelPoint);
        }

        private void DeselectPreviousPoint( GameObject levelPoint )
        {
            var levelPointId = levelPoint.GetComponent<LevelPoint>().LevelInformation.GetInstanceID();
            var selectedLevelId = SelectedLevelObject.GetComponent<LevelPoint>().LevelInformation.GetInstanceID();

            if (!levelPointId.Equals(selectedLevelId))
            {
                var levelUnselectedIcon = SelectedLevelObject.GetComponent<LevelPoint>().LevelInformation.LevelIcon;
                SelectedLevelObject.gameObject.GetComponent<SpriteRenderer>().sprite = levelUnselectedIcon;
                SelectedLevelObject = levelPoint;
            }
        }

        #endregion
    }
}

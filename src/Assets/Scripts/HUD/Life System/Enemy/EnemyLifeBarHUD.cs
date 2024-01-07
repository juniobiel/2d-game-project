using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLifeBarHUD : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> HealthPointsObject;

    private float EnemyLife;

    public void SetEnemyLife(float enemyLife)
    {
        EnemyLife = enemyLife;
    }

    private float GetHealthPointFillAmount(int inferiorLimit)
    {
        var deltaLife = EnemyLife - inferiorLimit;
        return deltaLife / 20;
    }

    private void SetHealthPointInactive(int iterator)
    {
        for (int i = iterator; i < HealthPointsObject.Count; i++)
            HealthPointsObject[i].gameObject.SetActive(false);
    }

    public void RemoveHealthPoint(float enemyLife)
    {
        EnemyLife = enemyLife;

        if(EnemyLife >= 80)
        {
            HealthPointsObject[4].GetComponent<Image>().fillAmount = GetHealthPointFillAmount(80);
        }

        if(EnemyLife < 80 && EnemyLife >= 60)
        {
            SetHealthPointInactive(4);

            HealthPointsObject[3].GetComponent<Image>().fillAmount = GetHealthPointFillAmount(60);
        }

        if(EnemyLife < 60 && EnemyLife >= 40)
        {
            SetHealthPointInactive(3);

            HealthPointsObject[2].GetComponent<Image>().fillAmount = GetHealthPointFillAmount(40);
        }

        if(EnemyLife < 40 && EnemyLife >= 20)
        {
            SetHealthPointInactive(2);

            

            HealthPointsObject[1].GetComponent<Image>().fillAmount = GetHealthPointFillAmount(20);
        }

        if(EnemyLife < 20)
        {
            SetHealthPointInactive(1);

            HealthPointsObject[0].GetComponent<Image>().fillAmount = GetHealthPointFillAmount(0);
        }
    }
}

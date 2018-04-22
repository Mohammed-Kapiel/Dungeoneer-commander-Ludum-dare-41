using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBuildingLogic : MonoBehaviour {

    //public Color selectedColor = new Color(0, 1, 0, 1);
    //public Color defaultColor = new Color(1, 1, 1, 1);

    public Transform spawn;

    public GameObject playerBase;

    public GameObject infantryPrefab;
    public float infantryRate;
    public float InfantryCurrCredit;
    public float InfantryCredit;

    public GameObject tankPrefab;
    public float tankRate;
    public float tankCredit;
    public float tankCurrCredit;



    void Update ()
    {

            float costRate = infantryRate * Time.deltaTime;

            if(InfantryCurrCredit <= 0)
            {
                InfantryCurrCredit = InfantryCredit;
                GameObject tmp = Instantiate(infantryPrefab, spawn.position, spawn.rotation);
                tmp.GetComponent<UnitController>().MoveTo(new Vector2(playerBase.transform.position.x, playerBase.transform.position.y));
        }
        else
        {
            InfantryCurrCredit -= costRate;
        }

         costRate = tankRate * Time.deltaTime;

        if (tankCurrCredit <= 0)
        {
            tankCurrCredit = tankCredit;
            GameObject tmp = Instantiate(tankPrefab, spawn.position, spawn.rotation);
            tmp.GetComponent<UnitController>().MoveTo(new Vector2(playerBase.transform.position.x, playerBase.transform.position.y));
        }
        else
        {
            tankCurrCredit -= costRate;
        }
    }
}

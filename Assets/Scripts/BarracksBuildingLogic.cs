using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarracksBuildingLogic : MonoBehaviour {

    public int hp;


    public Color selectedColor = new Color(0, 1, 0, 1);
    public Color defaultColor = new Color(1, 1, 1, 1);

    public Transform spawn;

    public GameObject buildPannel;
    public GameObject endGamePanel;

    public GameObject rifleMenPrefab;
    public float rifleMenCredit = 100;
    public float rifleMenTime = 5;
    public int rifleMenQueue = 0;
    public float rifleMenCurrCredit = 100;
    public Text rifleMenUI;


    public GameObject rocketeerPrefab;
    public float rocketeerCredit = 300;
    public float rocketeerTime = 10;
    public int rocketeerQueue = 0;
    public float rocketeerCurrCredit = 300;
    public Text rocketeerUI;

    public GameObject engineerPrefab;
    public float engineerCredit = 1000;
    public float engineerTime = 15;
    public int engineerQueue = 0;
    public float engineerCurrCredit = 1000;
    public Text engineerUI;

    private SpriteRenderer[] mySprites = new SpriteRenderer[2];


    void Start ()
    {

        // mySprite = this.GetComponent<SpriteRenderer>();

        mySprites[0] = transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>();
        mySprites[1] = transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>();


    }
	
	void Update ()
    {
        if (rifleMenQueue > 0)
        {
            float costRate = (rifleMenCredit / rifleMenTime) * Time.deltaTime;
            if (ResourceManager.UseCredits(costRate))
            {
                rifleMenCurrCredit -= costRate;
            }
            if(rifleMenCurrCredit <= 0)
            {
                rifleMenQueue--;
                rifleMenUI.text = "" + rifleMenQueue;
                rifleMenCurrCredit = rifleMenCredit;
                Instantiate(rifleMenPrefab, spawn.position, spawn.rotation);
            }
        }

        if (rocketeerQueue > 0)
        {
            float costRate = (rocketeerCredit / rocketeerTime) * Time.deltaTime;
            if (ResourceManager.UseCredits(costRate))
            {
                rocketeerCurrCredit -= costRate;
            }
            if (rocketeerCurrCredit <= 0)
            {
                rocketeerQueue--;
                rocketeerUI.text = "" + rocketeerQueue;
                rocketeerCurrCredit = rocketeerCredit;
                Instantiate(rocketeerPrefab, spawn.position, spawn.rotation);
            }
        }

        if (engineerQueue > 0)
            {
                float costRate = (engineerCredit / rocketeerTime) * Time.deltaTime;
                if (ResourceManager.UseCredits(costRate))
                {
                    engineerCurrCredit -= costRate;
                }
                if (engineerCurrCredit <= 0)
                {
                    engineerQueue--;
                    engineerUI.text = "" + engineerQueue;
                    engineerCurrCredit = engineerCredit;
                    Instantiate(engineerPrefab, spawn.position, spawn.rotation);
                }
            }



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Bullet")
        {
            hp -= collision.transform.GetComponent<BulletLogic>().dmg;

            if (hp <= 0)
            {
                Destroy(gameObject);
                endGamePanel.SetActive(true);
                Time.timeScale = 0;

            }
        }
    }

    public GameObject Select()
    {
        buildPannel.SetActive(true);

        foreach (SpriteRenderer mySprite in mySprites)
        {
            mySprite.color = selectedColor;
        }
       

        return this.gameObject;
    }

    public void DeSelect()
    {
        buildPannel.SetActive(false);

        foreach (SpriteRenderer mySprite in mySprites)
        {
            mySprite.color = defaultColor;
        }
    }

    public void BuildUnit(int type)//TO DO
    {
        switch (type)
        {
            case 1:
                rifleMenQueue++;
                rifleMenUI.text = "" + rifleMenQueue;
                break;
            case 2:
                rocketeerQueue++;
                rocketeerUI.text = "" + rocketeerQueue;
                break;
            case 3:
                engineerQueue++;
                engineerUI.text = "" + engineerQueue;
                break;

            default :
                break;
        }
    }

    public void CancelUnit(int type)//TO DO
    {
        switch (type)
        {
            case 1:
                if(rifleMenQueue > 0)
                {
                    rifleMenQueue--;
                    rifleMenUI.text = "" + rifleMenQueue;
                    ResourceManager.GainCredits(rifleMenCredit - rifleMenCurrCredit);
                    rifleMenCurrCredit = rifleMenCredit;
                }
                    
                break;
            case 2:
                if (rocketeerQueue > 0)
                {
                    rocketeerQueue--;
                    rocketeerUI.text = "" + rocketeerQueue;
                    ResourceManager.GainCredits(rocketeerCredit - rocketeerCurrCredit);
                    rocketeerCurrCredit = rocketeerCredit;
                }
                    
                break;
            case 3:
                if (engineerQueue > 0)
                {
                    engineerQueue--;
                    engineerUI.text = "" + engineerQueue;
                    ResourceManager.GainCredits(engineerCredit - engineerCurrCredit);
                    engineerCurrCredit = engineerCredit;
                }
                    
                break;

            default:
                break;
        }
    }

}

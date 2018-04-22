using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour {

    public static float credits = 1000;
    public static float ticRate = 50;


    void Start () {
        Debug.Log("Start Credits: " + credits);
	}
	
	void Update ()
    {
        credits += ticRate * Time.deltaTime;

    }

    public static void GainCredits(float amount)
    {
        credits += amount;
    }

    public static bool UseCredits(float amount)
    {

        if(amount <= credits)
        {
            credits -= amount;
            return true;
        }

        return false;
        
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceUI : MonoBehaviour {

    public Text txt;


	void Start ()
    {
        txt = this.GetComponent<Text>();
	}
	
	void Update ()
    {
        txt.text = "Credits:" + ResourceManager.credits;


	}
}

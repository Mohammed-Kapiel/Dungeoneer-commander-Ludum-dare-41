using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour {

    public int camSpeed;

    public LayerMask selectables;
    public LayerMask interactables;


    public GameObject selectedUnit;
    public bool isBuilding = false;

	void Start () {
		
	}
	
	void Update ()
    {

        if (Input.GetButtonDown("Select Unit"))
        {
            SelectUnit();
        }

        if (Input.GetButtonDown("Move Unit"))
        {
            if(selectedUnit != null)
            {
                if(Interact() != true)
                {
                    MoveUnit();
                }
                
            }
            
        }

        if (Input.GetAxis("Horizontal") > 0.1)
        {
            transform.Translate(new Vector3(camSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetAxis("Horizontal") < -0.1)
        {
            transform.Translate(new Vector3(-camSpeed * Time.deltaTime, 0, 0));
        }
        if (Input.GetAxis("Vertical") > 0.1)
        {
            transform.Translate(new Vector3(0, camSpeed * Time.deltaTime, 0));
        }
        if (Input.GetAxis("Vertical") < -0.1)
        {
            transform.Translate(new Vector3(0, -camSpeed * Time.deltaTime, 0));
        }

    }

    void SelectUnit()
    {
        Vector2 camPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D rayHit = rayHit = Physics2D.Raycast(camPos, Vector2.zero, 1000, selectables);

        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (selectedUnit != null)
        {

            if (isBuilding)
            {
                if (selectedUnit.GetComponent<BarracksBuildingLogic>() != null)
                {
                    selectedUnit.GetComponent<BarracksBuildingLogic>().DeSelect();
                }
            }
            else
            {
                selectedUnit.GetComponent<UnitController>().DeSelect();
            }

                
            selectedUnit = null;
        }

        

        if (rayHit)
        {
            if(rayHit.transform.tag == "Barracks")
            {
                selectedUnit = rayHit.collider.GetComponent<BarracksBuildingLogic>().Select();
                isBuilding = true;
            }
            else
            {
                if (rayHit.transform.tag == "Player")
                {
                    selectedUnit = rayHit.collider.GetComponent<UnitController>().Select();
                    isBuilding = false;
                }
                    
            }
            
        }

    }

    bool Interact()
    {
        Vector2 camPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        RaycastHit2D rayHit = Physics2D.Raycast(camPos, Vector2.zero, 1000, interactables);

        if (selectedUnit != null && rayHit)
        {

            

            if (rayHit.collider.tag == "Enemy")
            {
                Debug.Log("Enemy hit");
                selectedUnit.GetComponent<UnitController>().Attack(rayHit.collider.gameObject);
                return true;
            }
        }

        return false;
    }

    void MoveUnit()
    {
        if (isBuilding)
        {
            //if(selectedUnit.GetComponent<BarracksBuildingLogic>() != null)
            //{
            //    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //    Vector2 camPos = new Vector2(mousePos.x, mousePos.y);
            //    selectedUnit.GetComponent<BarracksBuildingLogic>().RallyPointTo(camPos);
            //}

        }
        else
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 camPos = new Vector2(mousePos.x, mousePos.y);
            selectedUnit.GetComponent<UnitController>().MoveTo(camPos);
        }
        
    }
}

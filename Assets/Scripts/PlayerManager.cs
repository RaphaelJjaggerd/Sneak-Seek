using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
  RaycastHit hit;
  Ray camRay;
  List<UnitController> selectedUnits = new List<UnitController>();

  [SerializeField]
  private SpriteRenderer SelectionSprite;

  // Update is called once per frame
  void Update()
  {
    //Detect if mouse is down. 0 for left click to select unit
    if (Input.GetMouseButtonDown(0))
    {
      //Create a ray from the camera to our space
      camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
      //Shoot that ray and get the hit data
      if (Physics.Raycast(camRay, out hit))
      {
        //Do something with that data
        // Debug.Log(hit.transform.tag);
        if (hit.transform.CompareTag("PlayerUnit"))
        {
          // Select Unit
          SelectUnit(hit.transform.GetComponent<UnitController>(), Input.GetKey(KeyCode.LeftShift));
        }
        else if (Input.GetMouseButtonDown(0) && selectedUnits.Count > 0)
        {
          camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
          //Shoot that ray and get the hit data
          if (Physics.Raycast(camRay, out hit))
          {
            //Do something with that data 
            //Debug.Log(hit.transform.tag);
            if (hit.transform.CompareTag("Ground"))
            {
              foreach (var selectableObj in selectedUnits)
              {
                selectableObj.MoveUnit(hit.point);
              }
            }
            // else if (hit.transform.CompareTag("EnemyUnit"))
            // {
            //     foreach (var selectableObj in selectedUnits)
            //     {
            //         selectableObj.SetNewTarget(hit.transform);
            //     }
            // }
          }
        }
        else if (hit.transform.CompareTag("Ground"))
        {
          //Deselect units
          DeselectUnits();
        }

      }

    }

    // if (Input.GetMouseButtonDown(0) && selectedUnits.Count > 0)
    // {
    //   camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
    //   //Shoot that ray and get the hit data
    //   if (Physics.Raycast(camRay, out hit))
    //   {
    //     //Do something with that data 
    //     //Debug.Log(hit.transform.tag);
    //     if (hit.transform.CompareTag("Ground"))
    //     {
    //       foreach (var selectableObj in selectedUnits)
    //       {
    //         selectableObj.MoveUnit(hit.point);
    //       }
    //     }
    //     // else if (hit.transform.CompareTag("EnemyUnit"))
    //     // {
    //     //     foreach (var selectableObj in selectedUnits)
    //     //     {
    //     //         selectableObj.SetNewTarget(hit.transform);
    //     //     }
    //     // }
    //   }
    // }
  }


  private void SelectUnit(UnitController unit, bool isMultiSelect = false)
  {
    if (!isMultiSelect)
    {

      DeselectUnits();

    }
    selectedUnits.Add(unit);
    unit.SetSelected(true);
  }
  private void DeselectUnits()
  {
    for (int i = 0; i < selectedUnits.Count; i++)
    {
      // selectedUnits[i].Find("SelectedUnit").gameObject.SetActive(false);
      selectedUnits[i].SetSelected(false);

    }
    selectedUnits.Clear();
  }





}

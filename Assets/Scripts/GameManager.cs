using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    GameObject selectedObject;
    GameObject selectedStand;
    Donut _donut;
    public bool isMove;

    
    public int targetStandCount;
    int completedStandCount;



    void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
            {
                if (hit.collider != null && hit.collider.CompareTag("Stand"))
                {
                    if (selectedObject != null && selectedStand != hit.collider.gameObject)
                    {
                        Stand stand = hit.collider.GetComponent<Stand>();

                        if (stand.donuts.Count != 4 && stand.donuts.Count != 0)
                        {
                            if (_donut.color == stand.donuts[stand.donuts.Count - 1].GetComponent<Donut>().color)
                            {
                                selectedStand.GetComponent<Stand>().changeSocket(selectedObject);
                                _donut.Move("changePos", hit.collider.gameObject, stand.giveAvailableSocket(), stand.movePosition);
                                stand.emptySocket++;
                                stand.donuts.Add(selectedObject);
                                stand.checkDonuts();
                                selectedObject = null;
                                selectedStand = null;
                            }
                            else
                            {
                                _donut.Move("backToTheSocket");
                                selectedObject = null;
                                selectedStand = null;
                            }

                        }
                        else if (stand.donuts.Count == 0)
                        {
                            selectedStand.GetComponent<Stand>().changeSocket(selectedObject);
                            _donut.Move("changePos", hit.collider.gameObject, stand.giveAvailableSocket(), stand.movePosition);
                            stand.emptySocket++;
                            stand.donuts.Add(selectedObject);
                            stand.checkDonuts();
                            selectedObject = null;
                            selectedStand = null;
                        }
                        else
                        {
                            _donut.Move("backToTheSocket");
                            selectedObject = null;
                            selectedStand = null;
                        }

                    }
                    else if (selectedStand == hit.collider.gameObject)
                    {
                        _donut.Move("backToTheSocket");
                        selectedObject = null;
                        selectedStand = null;
                    }
                    else
                    {
                        Stand _stand = hit.collider.GetComponent<Stand>();
                        selectedObject = _stand.giveTopDonut();
                        _donut = selectedObject.GetComponent<Donut>();
                        isMove = true;

                        if (_donut.canMove)
                        {
                            _donut.Move("selected", null, null, _donut._belongToStand.GetComponent<Stand>().movePosition);
                            selectedStand = _donut._belongToStand;
                        }



                    }
                }

            }
        }

    }

    public void standCompleted()
    {
        completedStandCount++;
        if (completedStandCount == targetStandCount)
        {
            Debug.Log("Win"); //win panel
        }
    }

}


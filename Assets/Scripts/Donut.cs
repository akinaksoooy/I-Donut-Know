using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donut : MonoBehaviour
{
    public GameObject _belongToStand;
    public GameObject _belongToDonutSocket;
    public bool canMove;
    public string color;
    public GameManager gameManager;

    GameObject movePosition;
    GameObject whichStandToGo;

    bool selected, changePos, setSocket, backToTheSocket;

    int donutNumber;

    public void Move(string data, GameObject stand = null, GameObject socket = null, GameObject goingObject = null)
    {
        switch (data)
        {
            case "selected":
                movePosition = goingObject;
                selected = true;
                break;

            case "changePos":
                whichStandToGo = stand;
                _belongToDonutSocket = socket;
                movePosition = goingObject;
                changePos = true;
                break;

            case "backToTheSocket":
                backToTheSocket = true;
                break;
        }
    }

    void Update()
    {
        if (selected)
        {
            transform.position = Vector3.Lerp(transform.position, movePosition.transform.position, .2f);
            if (Vector3.Distance(transform.position, movePosition.transform.position) < .10f)
            {
                selected = false;
            }
        }
        if (changePos)
        {
            transform.position = Vector3.Lerp(transform.position, movePosition.transform.position, .2f);
            if (Vector3.Distance(transform.position, movePosition.transform.position) < .10f)
            {
                changePos = false;
                setSocket = true;
            }
        }
        if (setSocket)
        {
            transform.position = Vector3.Lerp(transform.position, _belongToDonutSocket.transform.position, .2f);
            if (Vector3.Distance(transform.position, _belongToDonutSocket.transform.position) < .10f)
            {
                transform.position = _belongToDonutSocket.transform.position;
                setSocket = false;

                _belongToStand = whichStandToGo;

                if (_belongToStand.GetComponent<Stand>().donuts.Count > 1)
                {
                    donutNumber = _belongToStand.GetComponent<Stand>().donuts.Count;
                    _belongToStand.GetComponent<Stand>().donuts[donutNumber - 2].GetComponent<Donut>().canMove = false;
                }

                gameManager.isMove = false;
            }
        }
        if (backToTheSocket)
        {
            transform.position = Vector3.Lerp(transform.position, _belongToDonutSocket.transform.position, .2f);
            if (Vector3.Distance(transform.position, _belongToDonutSocket.transform.position) < .10f)
            {
                transform.position = _belongToDonutSocket.transform.position;
                backToTheSocket = false;
                gameManager.isMove = false;
            }
        }
    }
}

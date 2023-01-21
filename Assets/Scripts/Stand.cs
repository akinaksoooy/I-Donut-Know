using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stand : MonoBehaviour
{
    public GameObject movePosition;
    public GameObject[] sockets;
    public int emptySocket;
    public List<GameObject> donuts;
    [SerializeField] private GameManager gameManager;

    int donutCompletedNumber;

    public GameObject giveTopDonut()
    {
        return donuts[donuts.Count - 1];
    }
    public GameObject giveAvailableSocket()
    {
        return sockets[emptySocket];
    }
    public void changeSocket(GameObject toBeDeletedObject)
    {
        donuts.Remove(toBeDeletedObject);

        if (donuts.Count != 0)
        {
            emptySocket--;
            donuts[donuts.Count - 1].GetComponent<Donut>().canMove = true;
        }
        else
        {
            emptySocket = 0;
        }
    }
    public void checkDonuts()
    {
        if (donuts.Count==4)
        {
            string color = donuts[0].GetComponent<Donut>().color;
            foreach (var item in donuts)
            {
                if (color == item.GetComponent<Donut>().color)
                {
                    donutCompletedNumber++;
                }
            }
            if (donutCompletedNumber == 4)
            {
                gameManager.standCompleted();
                completedStand();
            }
            else
            {                
                donutCompletedNumber = 0;
            }
        }
    }
    void completedStand()
    {
        foreach (var item in donuts)
        {
            item.GetComponent<Donut>().canMove = false;

            item.GetComponent<MeshRenderer>().material.color = Color.black;
            gameObject.tag = "CompletedStand";
        }
    }

}

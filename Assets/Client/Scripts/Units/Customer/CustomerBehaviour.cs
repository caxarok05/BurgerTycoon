using Client.Logic;
using Client.Services;
using Client.Units;
using Client.Units.Cashier;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerBehaviour : Unit
{
    public Transform nextCustomerPoint;
    public Transform nextCustomerLookAtPoint;

    public OrderPlace orderPlace;

    public bool reachDestination = false;

    public Transform lookAtPoint;

    public float smileFaceDuration = 1;
    public float smileFaceChance = 30;
    public GameObject smileCanvas;

    public void LookAtPoint()
    {
        gameObject.GetComponent<DestinationTracker>().OnDestinationReached -= LookAtPoint;
        gameObject.transform.LookAt(lookAtPoint.position);
        gameObject.GetComponent<WalkableAnimator>().SetIdle();
        reachDestination = true;

    }

    public void SmileFace()
    {
        if (Random.Range(0, 100) > smileFaceDuration)
        {
            StartCoroutine(MakeSmile());
        }
    }

    public IEnumerator MakeSmile()
    {
        smileCanvas.SetActive(true);
        yield return new WaitForSeconds(smileFaceDuration);
        smileCanvas.SetActive(false);

    }
    public void MakeOrder()
    {
        orderPlace.ProcessOrder();
    }
}


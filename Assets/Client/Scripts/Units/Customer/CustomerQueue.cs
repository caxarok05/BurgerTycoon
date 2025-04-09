using Client.Infrastructure.Factory;
using Client.Logic;
using Client.Services;
using Client.Units;
using Client.Units.Cashier;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerQueue : MonoBehaviour
{
    public OrderPlace orderPlace;
    public CustomersSpawner customersSpawner;

    private Queue<CustomerBehaviour> customers = new Queue<CustomerBehaviour>();

    private float updateInterval = 0.5f; 
    private float nextUpdateTime = 0;
    private IGameFactory factory;

    private void Start()
    {
        factory = AllServices.Container.Single<IGameFactory>();
    }

    private void Update()
    {
        if (Time.time >= nextUpdateTime)
        {
            if (customers.Count > 0)
            {
                foreach (var customer in customers)
                {
                    customer.agent.ResetPath();
                }
                UpdateQueuePositions();
            }
            nextUpdateTime = Time.time + updateInterval;
        }
    }
    public void CheckCustomers(CustomerBehaviour customer)
    {
        customers.Enqueue(customer);
    }

    public CustomerBehaviour GetCustomer()
    {
        if (customers.Count <= 0)
            return null;

        if (customers.Peek().reachDestination)
        {
            CustomerBehaviour customer = customers.Peek();
            customer.orderPlace = orderPlace;
            return customer;
        }

        return null;
    }

    public void CustomerGetOut()
    {
        CustomerBehaviour customer = customers.Dequeue();
        Transform randomPoint = factory.customerPoints[Random.Range(0, factory.customerPoints.Count)];
        customer.agent.SetDestination(randomPoint.position);
        customer.gameObject.GetComponent<WalkableAnimator>().StartWalking();
        customer.GetComponent<DestinationTracker>().OnDestinationReached += () => DeleteCustomer(customer);
        customer.SmileFace();
        StartCoroutine(ResetCustomerDestinations());
    }

    private void UpdateQueuePositions()
    {
        CustomerBehaviour previousCustomer = null;
        
        foreach (var customer in customers)
        {
            if (previousCustomer != null && !customer.reachDestination)
            {
                customer.agent.SetDestination(previousCustomer.nextCustomerPoint.position);
                customer.gameObject.GetComponent<WalkableAnimator>().StartWalking();

                customer.lookAtPoint = previousCustomer.nextCustomerLookAtPoint;
                customer.gameObject.GetComponent<DestinationTracker>().OnDestinationReached += customer.LookAtPoint;
                    
            }
            else if(!customer.reachDestination)
            {
                customer.agent.SetDestination(orderPlace.queueStartPoint.position);
                customer.gameObject.GetComponent<WalkableAnimator>().StartWalking();

                customer.lookAtPoint = orderPlace.CustomerLookAtPoint;
                customer.gameObject.GetComponent<DestinationTracker>().OnDestinationReached += customer.LookAtPoint;
            }

            previousCustomer = customer;
        }
        
    }
    private void DeleteCustomer(CustomerBehaviour customer)
    {
        factory.customers.Remove(customer.gameObject);
        customer.GetComponent<DestinationTracker>().OnDestinationReached -= () => DeleteCustomer(customer);
        if (customersSpawner.customersCount > 0)
            customersSpawner.customersCount--;

        Destroy(customer.gameObject);
    }

    private IEnumerator ResetCustomerDestinations()
    {
        foreach (var customer in customers)
        {
            yield return new WaitForSeconds(1f);
            customer.reachDestination = false;
        }
    }
}

using Client.Infrastructure.Factory;
using Client.Services;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomersSpawner : MonoBehaviour
{
    public CustomerQueue queue;

    public int customersCount = 0;
    public int maxCustomers = 3;

    public int minTimeDelay = 5;
    public int maxTimeDelay = 10;

    private IGameFactory _gameFactory;

    private float timer;
    private int timeDelay;

    private void Start()
    {
        _gameFactory = AllServices.Container.Single<IGameFactory>();
        timeDelay = Random.Range(minTimeDelay, maxTimeDelay);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= timeDelay)
        {
            SpawnCustomer();
            timer = 0f;
            timeDelay = Random.Range(minTimeDelay, maxTimeDelay);
        }
    }

    public void SpawnCustomer()
    {
        if (customersCount >= maxCustomers)
             return;

        GameObject customer = _gameFactory.CreateRandomCustomer();
        queue.CheckCustomers(customer.GetComponent<CustomerBehaviour>());
        customersCount++;
    }
}

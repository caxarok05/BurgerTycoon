using Client.Infrastructure.Factory;
using Client.Services;
using Client.Units;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomersSpawner : MonoBehaviour
{
    public CustomerQueue queue;

    public int customersCount = 0;
    public int maxCustomers = 3;

    public float minTimeDelay = 5;
    public float maxTimeDelay = 10;

    public float _speed = 1;

    private IGameFactory _gameFactory;

    private float timer;
    private float timeDelay;

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
    public void ChangeSpeed(float speed) => _speed = speed;
    public void BonusSpeed(float multiplier) => _speed *= multiplier;

    public void SpawnCustomer()
    {
        if (customersCount >= maxCustomers)
             return;

        GameObject customer = _gameFactory.CreateRandomCustomer();
        customer.GetComponent<Unit>().agent.speed = _speed; 
        queue.CheckCustomers(customer.GetComponent<CustomerBehaviour>());
        customersCount++;
    }
}

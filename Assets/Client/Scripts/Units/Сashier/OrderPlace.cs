using UnityEngine;
using System;
using Client.Units.Chef;
using Client.Units.Loader.StateMachine;
using System.Collections.Generic;
using Client.Units.Cashier.StateMachine;
using Client.Units.Loader;
using Client.Services.MoneyService;

namespace Client.Units.Cashier
{
    public class OrderPlace : WorkPlace
    {
        public Transform cashierPlace;
        public Transform giveOrderPoint;
        public Transform CashierlookAtPoint;
        public Transform queueStartPoint;
        public Transform CustomerLookAtPoint;

        public int BurgerPrice = 1;

        public CustomerQueue customerQueue;

        private CashierBehaviour cashier;
        private List<ChefTable> destinationTables = new List<ChefTable>();

        private CashierStateMachine stateMachine;

        private IMoneyService _moneyService;

        public void Construct(List<ChefTable> destination, CashierBehaviour cashier, IMoneyService moneyService)
        {
            destinationTables = destination;
            this.cashier = cashier;
            _moneyService = moneyService;
        }

        public void UpdateChefTables(List<ChefTable> destination)
        {
            destinationTables = destination;
        }

        private void Start()
        {
            stateMachine = new CashierStateMachine(cashier, this, cashier.gameObject.GetComponent<WalkableAnimator>());
        }

        private void Update()
        {

            if (!cashier.hasOrder && cashier.isFree)
            {
                stateMachine.Enter<WaitingForOrderState>();
                cashier.isFree = false;
            }
            else if (cashier.isFree)
            {
                ChefTable availableTable = null;

                foreach (ChefTable table in destinationTables)
                {
                    if (!table.isOccupiedbyCashier)
                    {
                        availableTable = table;
                        break;
                    }
                }

                if (availableTable != null)
                {
                    if (cashier.hasWeight)
                    {
                        stateMachine.Enter<GiveOrderState>();
                    }
                    else
                    {
                        stateMachine.Enter<TakeOrderState, ChefTable>(availableTable);
                        availableTable.isOccupiedbyCashier = true;

                    }
                    cashier.isFree = false;
                }
                else
                {
                    foreach (ChefTable table in destinationTables)
                    {
                        if (cashier.hasWeight)
                        {
                            stateMachine.Enter<GiveOrderState>();
                        }
                        else
                        {
                            stateMachine.Enter<TakeOrderState, ChefTable>(table);
                            table.isOccupiedbyCashier = true;
                        }
                        cashier.isFree = false;
                        break;
                    }
                }
            }

            var customer = customerQueue.GetCustomer();
            if (customer != null && cashier.hasOrder == false)
            {
                customer.MakeOrder();
            }

        }

        public void ProcessOrder()
        {
            cashier.hasOrder = true;
            cashier.isFree = true;
        }

        public void GiveOrder()
        {
            cashier.hasOrder = false;
            _moneyService.AddMoney(BurgerPrice);
            customerQueue.CustomerGetOut();
        }

    }
}
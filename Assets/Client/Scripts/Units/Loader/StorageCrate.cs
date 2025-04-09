using Client.Units.Chef;
using Client.Units.Loader.StateMachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Client.Units.Loader
{
    public class StorageCrate : WorkPlace
    {
        public Transform loaderPlace;
        public Transform deliveryPoint;
        public Transform lookAtPoint;
        public LoaderBehaviour loader;
        public float rechargeCooldown;
        public float oneTimeIngridentsRecharge;
        public float currentIngridients;
        public float _maxIngridients;

        private bool isRecharging = false;

        private List<ChefTable> destinationTables = new List<ChefTable>();

        private LoaderStateMachine stateMachine;

        public void Construct(List<ChefTable> destination, LoaderBehaviour loader)
        {
            destinationTables = destination;
            this.loader = loader;
        }

        public void UpdateChefTables(List<ChefTable> destination)
        {
            destinationTables = destination;
        }

        public void UpdateloaderSpeed(float speed) => loader.agent.speed = speed;

        public void UpgradeMaxIngridients(int maxIngridients) => _maxIngridients = maxIngridients;


        private void Start()
        {
            stateMachine = new LoaderStateMachine(loader, this, loader.gameObject.GetComponent<WalkableAnimator>());       
        }

        private void Update()
        {

            if (loader.isFree)
            {
                ChefTable availableTable = null;

                foreach (ChefTable table in destinationTables)
                {
                    if (!table.isOccupiedbyLoader && table._ingridientsAmount < table._maxIngridients)
                    {
                        availableTable = table; 
                        break;
                    }
                }

                if (availableTable != null)
                {
                    if (loader.hasWeight)
                    {
                        stateMachine.Enter<SetIngridientsState, ChefTable>(availableTable);
                        availableTable.isOccupiedbyLoader = true;
                    }
                    else
                        stateMachine.Enter<GetIngridientsState>();
                    loader.isFree = false;
                    
                }
                else
                {
                    foreach (ChefTable table in destinationTables)
                    {
                        if (table._ingridientsAmount < table._maxIngridients)
                        {
                            if (loader.hasWeight)
                            {
                                stateMachine.Enter<SetIngridientsState, ChefTable>(table);
                                table.isOccupiedbyLoader = true;
                            }
                            else
                                stateMachine.Enter<GetIngridientsState>();

                            loader.isFree = false;
                            break;
                        }
                    }
                }



                if (!isRecharging)
                {
                    StartCoroutine(RechargeIngridients());
                    isRecharging = true;
                }
            }
        }
        

        private IEnumerator RechargeIngridients()
        {
            yield return new WaitForSeconds(rechargeCooldown);

            if (currentIngridients < _maxIngridients)
                currentIngridients += oneTimeIngridentsRecharge;

            if (currentIngridients > _maxIngridients)
                currentIngridients = _maxIngridients;

            isRecharging = false;
        }

    }
}
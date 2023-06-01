using System.Collections.Generic;
using System;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    public event EventHandler<OnProgressChangedEventArgs> OnItemPut;
    public event EventHandler<OnProgressChangedEventArgs> OnItemTaken;
    public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    public class OnProgressChangedEventArgs : EventArgs
    {
        public float progress;
    }

    [SerializeField] private CuttingRecepieSO[] cuttingRecepieSOArray;

    private Dictionary<string, CuttingRecepieSO> cuttingRecepieSOSet = new Dictionary<string, CuttingRecepieSO>();
    private int cuttingSteps;

    public void Start()
    {
        foreach (var recepie in cuttingRecepieSOArray)
        {
            cuttingRecepieSOSet.Add(recepie.input.objectName, recepie);
        }
    }

    public override void Interact(PlayerScript player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                //?????
                if (HasRecepie(player.GetKitchenObject()))
                {
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingSteps = GetRequiredSteps();

                    OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                    {
                        progress = (GetRequiredSteps() - cuttingSteps) / (float)GetRequiredSteps()
                    });
                    OnItemPut?.Invoke(this, null);
                }
                else
                {
                    Debug.Log("Can't be slised");
                }
            }
            else
            {
                Debug.Log("Player has nothing");
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                Debug.Log("Player already holding an object");
            }
            else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                OnItemTaken?.Invoke(this, null);
            }
        }
    }

    public override void InteractDifferent(PlayerScript player)
    {
        if (HasKitchenObject())
        {
            var kObjectName = GetKitchenObject().GetKitchenObjectSO().objectName;

            if (cuttingRecepieSOSet.ContainsKey(kObjectName))
            {
                cuttingSteps--;

                OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                {
                    progress = (GetRequiredSteps() - cuttingSteps) / (float) GetRequiredSteps()
                }) ;

                if (cuttingSteps <= 0)
                {
                    GetKitchenObject().DestroyKitchenObject();
                    KitchenObjectScript kitchenObject = KitchenObjectScript.SpawnKitchenObject(cuttingRecepieSOSet[kObjectName].output, this);
                    if (HasRecepie(kitchenObject))
                    {
                        cuttingSteps = GetRequiredSteps();
                        OnProgressChanged?.Invoke(this, new OnProgressChangedEventArgs
                        {
                            progress = (GetRequiredSteps() - cuttingSteps) / (float)GetRequiredSteps()
                        });
                    }
                }
            }
            else
            {
                Debug.Log("Can't be slised");
            }
        }
        else
        {
            Debug.Log("Nothing to cut");
        }
    }

    private int GetRequiredSteps()
    {
        return cuttingRecepieSOSet[GetKitchenObject().GetKitchenObjectSO().objectName].requiredSteps;
    }

    private bool HasRecepie(KitchenObjectScript kitchenObject)
    {
        return cuttingRecepieSOSet.ContainsKey(kitchenObject.GetKitchenObjectSO().objectName);
    }
}

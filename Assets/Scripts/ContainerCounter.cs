using System;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] KitchenObjectSO kitchenObject;

    public override void Interact(PlayerScript player)
    {

        if (!player.HasKitchenObject())
        {
            KitchenObjectScript.SpawnKitchenObject(kitchenObject, player);

            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Debug.Log("Player already holding an  object");
        }
    }
}

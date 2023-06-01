using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] KitchenObjectSO kitchenObject;

    public override void Interact(PlayerScript player)
    {
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
            }
            else
            {
                Debug.Log("Player has nothing");
            }
        }
        else
        {
            if (!player.HasKitchenObject())
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            else
            {
                Debug.Log("Player already holding an object");
            }
        }
    }
}

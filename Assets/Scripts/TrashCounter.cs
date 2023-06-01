using UnityEngine;

public class TrashCounter : BaseCounter
{
    public override void Interact(PlayerScript player)
    {
        if (player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroyKitchenObject();
        }
        else
        {
            Debug.Log("Player has nothing");
        }
    }
}

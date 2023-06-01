using UnityEngine;

public interface IKitchenInteractable
{
    public Transform GetKitchenObjectTopPoint();

    public void SetKitchenObject(KitchenObjectScript kitchenObjectScript);

    public KitchenObjectScript GetKitchenObject();

    public void ClearKitchenObjet();

    public bool HasKitchenObject();
}

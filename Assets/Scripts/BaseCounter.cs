using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenInteractable
{
    [SerializeField] protected Transform topPoint;

    protected KitchenObjectScript currentKitchenObject;

    public abstract void Interact(PlayerScript player);

    public virtual void InteractDifferent(PlayerScript player)
    {
        Debug.Log("Not implemented");
    }

    public Transform GetKitchenObjectTopPoint()
    {
        return topPoint;
    }

    public void SetKitchenObject(KitchenObjectScript kitchenObjectScript)
    {
        this.currentKitchenObject = kitchenObjectScript;
    }

    public KitchenObjectScript GetKitchenObject()
    {
        return currentKitchenObject;
    }

    public void ClearKitchenObjet()
    {
        currentKitchenObject = null;
    }

    public bool HasKitchenObject()
    {
        return currentKitchenObject != null;
    }
}

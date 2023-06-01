using UnityEngine;

public class KitchenObjectScript : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    private IKitchenInteractable kitchenObjectParent;

    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }

    public void SetKitchenObjectParent(IKitchenInteractable kitchenObjectParent)
    {
        if (this.kitchenObjectParent != null) this.kitchenObjectParent.ClearKitchenObjet();
        this.kitchenObjectParent = kitchenObjectParent;

        kitchenObjectParent.SetKitchenObject(this);

        transform.parent = kitchenObjectParent.GetKitchenObjectTopPoint();
        //set position relative to parent
        transform.localPosition = Vector3.zero;
    }

    public void DestroyKitchenObject()
    {
        if (this.kitchenObjectParent != null) this.kitchenObjectParent.ClearKitchenObjet();
        Destroy(this.gameObject);
    }

    public IKitchenInteractable GetKitchenobjectParent()
    {
        return kitchenObjectParent;
    }

    public static KitchenObjectScript SpawnKitchenObject(KitchenObjectSO kObjectSO, IKitchenInteractable kitchenObjectParent)
    {
        var kObjectTransform = Instantiate(kObjectSO.prefab);
        var kObject = kObjectTransform.GetComponent<KitchenObjectScript>();
        kObject.SetKitchenObjectParent(kitchenObjectParent);
        return kObject;
    }
}

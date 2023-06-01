using System;
using UnityEngine;

public class PlayerScript : MonoBehaviour, IKitchenInteractable
{
    public static PlayerScript Instance { get; private set; }

    public event EventHandler<OnSelectedCounterChangedEventArgs> OnSelectedCounterChanged;
    public class OnSelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter selectedCounter;
    }

    [SerializeField] Transform holdPoint;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;

    private float movementSpeed = 7f;
    private float rotateSpeed = 12f;
    private bool isWalking;
    private float playerRadius = 0.7f;
    private float playerHight = 2f;
    private float interactDistance = 2f;
    private float upwordsSpeed = 5f;

    private Vector3 lastInteractDir;
    private BaseCounter selectedCounter;
    private KitchenObjectScript currentKitchenObject;
    private Vector3 lastTopPoint;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("There is more than one Player");
        }

        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        gameInput.OnInteractDifferentAction += GameInput_OnInteractDifferentAction;
    }


    private void Update()
    {
        HandleMovement();
        HandleInteractionsCounter();
        MoveUpTheHill();
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (selectedCounter != null) selectedCounter.Interact(this);
    }

    private void GameInput_OnInteractDifferentAction(object sender, EventArgs e)
    {
        if (selectedCounter != null) selectedCounter.InteractDifferent(this);
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private bool CanMove(Vector3 direction)
    {
        return !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHight, playerRadius, direction, movementSpeed * Time.deltaTime, countersLayerMask);
    }

    private void MoveUpTheHill()
    {
        Vector3 bottom = transform.position - new Vector3(0, 0.8f, 0);
        Vector3 top = transform.position - new Vector3(0, 0.7f, 0);

        VisualizeCapsyle(bottom, top, playerRadius);

        if (Physics.CapsuleCast(bottom, top, playerRadius, Vector3.up, out RaycastHit raycastHit, 10f))
        {
            Debug.Log(raycastHit.point);
            lastTopPoint = raycastHit.point;
        }

        transform.position = new Vector3(transform.position.x, Vector3.Lerp(transform.position, lastTopPoint + new Vector3(0, 0.1f, 0), Time.deltaTime * upwordsSpeed).y, transform.position.z);
    }

    private void HandleMovement()
    {
        var inputVector = gameInput.GetInputVectorNormalized();

        var moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        bool canMove = CanMove(moveDir);

        var moveDirAbsolete = moveDir;

        isWalking = moveDir != Vector3.zero;

        if (!canMove)
        {
            Vector3 moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            Vector3 moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
            if (CanMove(moveDirX))
            {
                moveDir = moveDirX;
                canMove = true;
            }
            else if (CanMove(moveDirZ))
            {
                moveDir = moveDirZ;
                canMove = true;
            }
        }

        if (canMove)
        {
            transform.position += moveDir * movementSpeed * Time.deltaTime;
        }

        //transform dir by defaul is 0, 0
        transform.forward = Vector3.Slerp(transform.forward, moveDirAbsolete, Time.deltaTime * rotateSpeed);
    }

    private void HandleInteractionsCounter()
    {
        var inputVector = gameInput.GetInputVectorNormalized();

        var moveDir = new Vector3(inputVector.x, 0, inputVector.y);

        if (moveDir != Vector3.zero) lastInteractDir = moveDir;

        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;

        OnSelectedCounterChanged?.Invoke(this, new OnSelectedCounterChangedEventArgs
        {
            selectedCounter = selectedCounter
        });
    }

    public Transform GetKitchenObjectTopPoint()
    {
        return holdPoint;
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

    private void VisualizeCapsyle(Vector3 bottom, Vector3 top, float radius)
    {
        int parts = 1000;
        double angle = 2 * Math.PI / parts;

        for (int i = 0; i < parts; i++)
        {
            double newAngel = angle * i;
            double x = Math.Cos(newAngel) * radius;
            double z = Math.Sin(newAngel) * radius;
            Vector3 plus = new Vector3((float)x, 0, (float)z);
            Debug.DrawLine(bottom + plus, top + plus, Color.red);
        }
    }
}

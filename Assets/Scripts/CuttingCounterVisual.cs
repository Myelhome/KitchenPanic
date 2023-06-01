using UnityEngine;
using System;

public class CuttingCounterVisual : MonoBehaviour
{
    private const string CUT = "Cut";

    [SerializeField] private CuttingCounter cuttingCounter;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnProgressChanged += ContainerCounter_OnProgressChanged;
    }

    private void ContainerCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs args)
    {
        if(args.progress != 0f) animator.SetTrigger(CUT);
    }
}

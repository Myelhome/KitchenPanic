using UnityEngine;

public class PlayerAnimatorScript : MonoBehaviour
{
    private const string IS_WALKING = "IsWalking";

    [SerializeField] private PlayerScript playerScript;

    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetBool(IS_WALKING, playerScript.IsWalking());
    }
}

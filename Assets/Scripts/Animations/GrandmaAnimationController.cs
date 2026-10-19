
using UnityEngine;
using UnityEngine.InputSystem;

public class GrandmaAnimationController : MonoBehaviour
{
    private Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            animator.SetTrigger("Charge");
        }
    }
}

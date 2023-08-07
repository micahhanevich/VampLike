using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMoveParentController : MonoBehaviour
{

    [SerializeField]
    protected GameObject playerObject;

    private Animator animator;
    private float graceRange = 0.1f;
    private float yOffset = -5.73f;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        if (transform.position.y - yOffset < playerObject.transform.position.y - graceRange)
        {
            animator.SetBool("MoveUp", true);
        }
        else
        {
            animator.SetBool("MoveUp", false);
        }

        if (transform.position.y - yOffset > playerObject.transform.position.y + graceRange)
        {
            animator.SetBool("MoveDown", true);
        }
        else
        {
            animator.SetBool("MoveDown", false);
        }
    }
}

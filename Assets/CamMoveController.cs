using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamMoveController : MonoBehaviour
{

    [SerializeField]
    protected GameObject playerObject;

    private Animator animator;
    private float graceRange = 0.1f;

    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        try
        {
            if (transform.position.x < playerObject.transform.position.x - graceRange)
            {
                animator.SetBool("MoveRight", true);
            }
            else
            {
                animator.SetBool("MoveRight", false);
            }

            if (transform.position.x > playerObject.transform.position.x + graceRange)
            {
                animator.SetBool("MoveLeft", true);
            }
            else
            {
                animator.SetBool("MoveLeft", false);
            }
        }
        catch (MissingReferenceException)
        {
            Debug.LogWarning("Player Missing");
        }
    }
}

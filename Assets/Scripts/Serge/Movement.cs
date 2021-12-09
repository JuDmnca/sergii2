using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{
    public NavMeshAgent serge;
    public float speed = 5f;
    private bool isMoving = false;
    private Animator animator;
    void Start() {
        serge = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        Vector3 sergePosition = GameController.Instance.SergePosition();
        serge.Warp(sergePosition);
        SceneController.Instance.OnCloseScene += SavePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameController.Instance.MenuOpen()) {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            isMoving = Mathf.Abs(x + z) != 0;

            Vector3 move = transform.right * x + transform.forward * z;
            serge.Move(move * speed * Time.deltaTime);
            animator.SetFloat("Speed", move.magnitude);
        }
    }

    public bool IsMoving() {
        return isMoving;
    }

    void SavePosition (string scene) {
        SceneController.Instance.OnCloseScene -= SavePosition;
        GameController.Instance.SetSergePosition(transform.position);
    }

}

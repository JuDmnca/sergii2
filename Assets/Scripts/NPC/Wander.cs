using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using UnityEngine.Audio;
 
public class Wander : MonoBehaviour {
 
    public float wanderRadius;
    public float waitTime;
    private Animator animator;
 
    private Transform target;
    private NavMeshAgent agent;
    private float timer;
    private bool isMoving = false;
 
    // Use this for initialization
    void Start () {
        agent = GetComponent<NavMeshAgent> ();
        animator = GetComponent<Animator>();
        timer = waitTime;
    }
 
    // Update is called once per frame

    void Update () {
        if (animator.parameterCount > 1) {
            if (!GameController.Instance.IsOnBerry() && animator.GetBool("Berry")) {
                animator.SetBool("Berry", false);
            } else if (GameController.Instance.IsOnBerry() && !animator.GetBool("Berry")) {
                animator.SetBool("Berry", true);
            }
        }


        if (!isMoving) {
            timer += Time.deltaTime;
        }

        if (timer >= waitTime) {
            timer = 0;
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius, -1);
            agent.SetDestination(newPos);
            isMoving = true;
            animator.SetFloat("Speed", newPos.magnitude);
        }

        animator.SetFloat("Speed", agent.velocity.magnitude / agent.speed);

        if (!agent.pathPending)
        {
            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                {
                    animator.SetFloat("Speed", 0);
                    isMoving = false;
                }
            }
        }

    }

    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask) {
        Vector3 randDirection = Random.insideUnitSphere * dist;
 
        randDirection += origin;
 
        NavMeshHit navHit;
 
        NavMesh.SamplePosition (randDirection, out navHit, dist, layermask);
 
        return navHit.position;
    }
}
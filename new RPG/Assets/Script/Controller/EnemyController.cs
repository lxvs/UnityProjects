using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    public float lookRadius = 10f;
    public float attackRadius = 2.5f;

    Transform target;
    NavMeshAgent agent;
    Combat combat;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = attackRadius;
        target = PlayerManager.instance.currentPlayer.transform;
        combat = GetComponent<Combat>();
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, target.position);
        if (distance <= lookRadius)
        {
            agent.SetDestination(target.position);

            if (distance <= agent.stoppingDistance)
            {
                FaceTheTarget();
                CharStats targetStats = target.GetComponent<CharStats>();
                if (targetStats != null)
                    combat.Attack(targetStats);

            }
        }
    }

    private void FaceTheTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

    }

}

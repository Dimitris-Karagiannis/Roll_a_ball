using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public Transform player;

    private NavMeshAgent nav_mesh_agent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        nav_mesh_agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            nav_mesh_agent.SetDestination(player.position);
        }
    }
}

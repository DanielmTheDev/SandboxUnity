using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Idle : IAiState
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Transform _agent;
        public bool CanActivate() => true;

        public Idle(NavMeshAgent navMeshAgent, Transform agent)
        {
            _navMeshAgent = navMeshAgent;
            _agent = agent;
        }

        public void PerformUpdate() => _navMeshAgent.SetDestination(_agent.position);
    }
}
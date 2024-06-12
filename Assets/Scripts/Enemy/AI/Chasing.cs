using Extensions;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Chasing : IAiState
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly Transform _agent;
        private readonly Transform _player;

        public Chasing(Transform agent, Transform player, NavMeshAgent navMeshAgent)
        {
            _agent = agent;
            _player = player;
            _navMeshAgent = navMeshAgent;
        }

        public bool CanActivate() => _agent.IsInLineOfSight(_player) && _agent.position.IsTransformInRange(_player.position, 7f);

        public void PerformUpdate() => _navMeshAgent.SetDestination(_player.position);
    }
}
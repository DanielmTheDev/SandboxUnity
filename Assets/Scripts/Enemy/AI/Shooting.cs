using System.Collections.Generic;
using Extensions;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Shooting : IAiState
    {
        private readonly NavMeshAgent _navMeshAgent;
        private readonly List<IAttacker> _attackers;
        private readonly Transform _agent;
        private readonly Transform _player;

        public Shooting(Transform agent, Transform player, NavMeshAgent navMeshAgent, List<IAttacker> attackers)
        {
            _agent = agent;
            _player = player;
            _navMeshAgent = navMeshAgent;
            _attackers = attackers;
        }

        public bool CanActivate() =>
            _agent.IsInLineOfSight(_player) && _agent.position.IsTransformInRange(_player.transform.position, 5f);

        public void PerformUpdate()
        {
            _navMeshAgent.SetDestination(_agent.position);
            _attackers.ForEach(attacker => attacker.Attack());
        }
    }
}
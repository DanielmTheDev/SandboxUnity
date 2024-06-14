using System.Collections.Generic;
using Extensions;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class Shooting : IAiState
    {
        private const float ROTATION_SPEED = 2f;
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
            RotateTowardsPlayer();
            _attackers.ForEach(attacker => attacker.Attack());
        }

        private void RotateTowardsPlayer()
        {
            var direction = (_player.position - _agent.position).normalized;
            var lookRotation = Quaternion.LookRotation(new(direction.x, 0, direction.z));
            _agent.rotation = Quaternion.Slerp(_agent.rotation, lookRotation, Time.deltaTime * ROTATION_SPEED);
        }
    }
}
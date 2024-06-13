using UnityEngine.AI;

namespace Extensions
{
    public static class NavMeshAgentExtensions
    {
        public static void Reset(this NavMeshAgent navMeshAgent)
        {
            navMeshAgent.updateRotation = false;
            navMeshAgent.updatePosition = false;
            navMeshAgent.updateUpAxis = false;
            navMeshAgent.isStopped = true;
        }
    }
}
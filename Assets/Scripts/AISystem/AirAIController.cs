using UnityEngine;

public enum AirAIState
{
    FollowingRoute, ApproachingTarget, ReturningToRoute
}

public class AirAIController : MonoBehaviour
{
    protected AirAIState State = AirAIState.FollowingRoute;
}

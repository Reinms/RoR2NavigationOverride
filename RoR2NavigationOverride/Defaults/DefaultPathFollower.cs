namespace RoR2NavigationOverride.Defaults
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using RoR2;
    using RoR2NavigationOverride.Custom;
    using RoR2NavigationOverride.Interfaces;
    using UnityEngine;

    public sealed class DefaultPathFollower : PathFollower, IPathFollower
    {
        Vector3 IPathFollower.targetPosition
        {
            get => base.targetPosition;
            set => base.targetPosition = value; 
        }

        Single IPathFollower.CalculateJumpVelocityNeededToReachNextWaypoint( Single moveSpeed )
        {
            return base.CalculateJumpVelocityNeededToReachNextWaypoint( moveSpeed );
        }
        void IPathFollower.DebugDrawPath( Color color, Single duration )
        {
            base.DebugDrawPath( color, duration );
        }
        Vector3 IPathFollower.GetNextPosition()
        {
            return base.GetNextPosition();
        }
        void IPathFollower.SetPath( Path path )
        {
            base.SetPath( path );
        }
        void IPathFollower.UpdatePosition( Vector3 newPosition )
        {
            base.UpdatePosition( newPosition );
        }
    }
}

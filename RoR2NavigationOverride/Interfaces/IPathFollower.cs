namespace RoR2NavigationOverride.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using RoR2;
    using UnityEngine;

    internal interface IPathFollower
    {
        //TODO: Doc PathFollower.targetPosition
        Vector3 targetPosition { get; set; }

        //TODO: Doc PathFollower.CalculateJumpVelocityNeededToReachNextWaypoint
        Single CalculateJumpVelocityNeededToReachNextWaypoint( Single moveSpeed );

        //TODO: Doc PathFollower.UpdatePosition()
        void UpdatePosition( Vector3 newPosition );

        //TODO: Doc PathFollower.SetPath()
        void SetPath( Path path );

        //TODO: Doc PathFollower.GetNextPosition()
        Vector3 GetNextPosition();

        //TODO: Doc PathFollower.DebugDraePath()
        void DebugDrawPath( Color color, Single duration );
    }
}

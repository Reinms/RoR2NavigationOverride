namespace RoR2NavigationOverride.Custom
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using RoR2;
    using RoR2NavigationOverride.Interfaces;
    using UnityEngine;

    public abstract class CustomPathFollower : PathFollower, IPathFollower
    {
        //TODO: Doc PathFollower.targetPosition
        public abstract new Vector3 targetPosition { get; set; }

        //TODO: Doc PathFollower.CalculateJumpVelocityNeededToReachNextWaypoint
        public new abstract Single CalculateJumpVelocityNeededToReachNextWaypoint( Single moveSpeed );

        //TODO: Doc PathFollower.UpdatePosition()
        public new abstract void UpdatePosition( Vector3 newPosition );

        //TODO: Doc PathFollower.SetPath()
        public new abstract void SetPath( Path path );

        //TODO: Doc PathFollower.GetNextPosition()
        public new abstract Vector3 GetNextPosition();

        //TODO: Doc PathFollower.DebugDraePath()
        public new abstract void DebugDrawPath( Color color, Single duration );

    }
}

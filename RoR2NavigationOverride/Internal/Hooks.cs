namespace RoR2NavigationOverride
{
    using System;
    using System.Reflection;
    using Mono.Cecil.Cil;
    using MonoMod.Cil;
    using RoR2;
    using RoR2NavigationOverride.Interfaces;
    using UnityEngine;

    internal static class Hooks
    {
        #region PathFollower Detours
        private static void PathFollower_EntityStates_AI_Walker_Wander_OnEnter( ILContext il )
        {
            var cursor = new ILCursor( il );

            _ = cursor.GotoNext( MoveType.After,
                x => x.MatchLdarg( 0 ),
                x => x.MatchLdfld( out _ ),
                x => x.MatchStfld( PathFollower_Reflection.targetPosition )
            );
            cursor.Index--;
            _ = cursor.Remove();
            _ = cursor.EmitDelegate<Action<PathFollower, Vector3>>( SetTargetPosition );

            void SetTargetPosition( PathFollower follower, Vector3 targetPosition )
            {
                if( follower is IPathFollower casted )
                {
                    casted.targetPosition = targetPosition;
                } else
                {
                    follower.targetPosition = targetPosition;
                }
            }
        }

        private static void PathFollower_EntityStates_AI_Walker_Wander_FixedUpdate( ILContext il )
        {
            var cursor = new ILCursor( il );

            _ = cursor.GotoNext( MoveType.AfterLabel, x => x.MatchCallOrCallvirt( PathFollower_Reflection.updatePosition ) );
            _ = cursor.Remove();
            _ = cursor.EmitDelegate<Action<PathFollower, Vector3>>( UpdatePosition );

            void UpdatePosition( PathFollower follower, Vector3 newPosition )
            {
                if( follower is IPathFollower casted )
                {
                    casted.UpdatePosition( newPosition );
                } else
                {
                    follower.UpdatePosition( newPosition );
                }
            }

            _ = cursor.GotoNext( MoveType.AfterLabel, x => x.MatchCallOrCallvirt( PathFollower_Reflection.getNextPosition ) );
            _ = cursor.Remove();
            _ = cursor.EmitDelegate<Func<PathFollower, Vector3>>( GetNextPosition );

            Vector3 GetNextPosition( PathFollower follower )
            {
                return follower is IPathFollower casted
                    ? casted.GetNextPosition()
                    : follower.GetNextPosition();
            }
        }

        private static void PathFollower_EntityStates_AI_Walker_Combat_FixedUpdate( ILContext il )
        {
            var cursor = new ILCursor( il );

            _ = cursor.GotoNext( MoveType.AfterLabel, x => x.MatchCallOrCallvirt( PathFollower_Reflection.updatePosition ) );
            _ = cursor.Remove();
            _ = cursor.EmitDelegate<Action<PathFollower, Vector3>>( UpdatePosition );

            void UpdatePosition( PathFollower follower, Vector3 newPosition )
            {
                if( follower is IPathFollower casted )
                {
                    casted.UpdatePosition( newPosition );
                } else
                {
                    follower.UpdatePosition( newPosition );
                }
            }


            _ = cursor.GotoNext( MoveType.AfterLabel, x => x.MatchCallOrCallvirt( PathFollower_Reflection.getNextPosition ) );
            _ = cursor.Remove();
            _ = cursor.EmitDelegate<Func<PathFollower, Vector3>>(GetNextPosition);

            Vector3 GetNextPosition( PathFollower follower )
            {
                return follower is IPathFollower casted
                    ? casted.GetNextPosition()
                    : follower.GetNextPosition();
            }


            _ = cursor.GotoNext( MoveType.AfterLabel, x => x.MatchCallOrCallvirt( PathFollower_Reflection.calculateJumpVelocityNeededToReachNextWaypoint ) );
            _ = cursor.Remove();
            _ = cursor.EmitDelegate<Func<PathFollower, Single, Single>>( CalculateJumpVelocityNeededToReachNextWaypoint );

            Single CalculateJumpVelocityNeededToReachNextWaypoint( PathFollower follower, Single moveSpeed )
            {
                return follower is IPathFollower casted
                    ? casted.CalculateJumpVelocityNeededToReachNextWaypoint( moveSpeed )
                    : follower.CalculateJumpVelocityNeededToReachNextWaypoint( moveSpeed );
            }
        }

        private static void PathFollower_RoR2_CharacterAI_BaseAI_FixedUpdate( ILContext il )
        {
            var cursor = new ILCursor( il );

            _ = cursor.GotoNext( MoveType.AfterLabel, x => x.MatchCallOrCallvirt( PathFollower_Reflection.setPath ) );
            _ = cursor.Remove();
            _ = cursor.EmitDelegate<Action<PathFollower, Path>>(SetPath);

            void SetPath( PathFollower follower, Path path )
            {
                if( follower is IPathFollower casted )
                {
                    casted.SetPath( path );
                } else
                {
                    follower.SetPath( path );
                }
            }
        }

        private static void PathFollower_RoR2_CharacterAI_BaseAI_DebugDrawPath( ILContext il )
        {
            var cursor = new ILCursor( il );

            _ = cursor.GotoNext( MoveType.AfterLabel, x => x.MatchCallOrCallvirt( PathFollower_Reflection.debugDrawPath ) );
            _ = cursor.Remove();
            _ = cursor.EmitDelegate<Action<PathFollower, Color, Single>>( DebugDrawPath );

            void DebugDrawPath( PathFollower follower, Color color, Single duration )
            {
                if( follower is IPathFollower casted )
                {
                    casted.DebugDrawPath( color, duration );
                } else
                {
                    follower.DebugDrawPath( color, duration );
                }
            }

        }



        #endregion

        #region CachedReflection
        //Simple subclass to cache all the reflection calls for PathFollower
        private static class PathFollower_Reflection
        {
            private static readonly BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static;
            private static readonly Type pathFollower = typeof(PathFollower);

            internal static readonly FieldInfo targetPosition = pathFollower.GetField( "targetPosition", flags );
            internal static readonly MethodInfo calculateJumpVelocityNeededToReachNextWaypoint = pathFollower.GetMethod( "CalculateJumpVelocityNeededToReachNextWaypoint", flags );
            internal static readonly MethodInfo updatePosition = pathFollower.GetMethod( "UpdatePosition", flags );
            internal static readonly MethodInfo setPath = pathFollower.GetMethod( "SetPath", flags );
            // TODO: This may get inlined, will need to verify
            internal static readonly MethodInfo getNextPosition = pathFollower.GetMethod( "GetNextPosition", flags );
            // TODO: This certainly gets inlined, also will need to compensate
            internal static readonly MethodInfo debugDrawPath = pathFollower.GetMethod( "DebugDrawPath", flags );
            
        }
        #endregion
    }

}

/*
 *
 *
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 */

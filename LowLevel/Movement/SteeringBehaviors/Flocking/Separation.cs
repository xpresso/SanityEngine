//
// Copyright (C) 2010 The Sanity Engine Development Team
//
// This source code is licensed under the terms of the
// MIT License.
//
// For more information, see the file LICENSE

using UnityEngine;
using SanityEngine.Actors;

namespace SanityEngine.Movement.SteeringBehaviors.Flocking
{
    /// <summary>
    /// Separation from flock members.
    /// </summary>
    public class Separation : FlockingBehavior
    {
        /// <summary>
        /// Create an separation behavior.
        /// </summary>
        /// <param name="flock">The flock.</param>
        public Separation(Flock flock)
            : base(flock)
        {
        }

        /// <summary>
        /// Update the behavior.
        /// </summary>
        /// <param name="manager">The steering manager.</param>
        /// <param name="actor">The actor being updated.</param>
        /// <param name="dt">The time since the last update, in seconds.
        /// </param>
        /// <returns>The steering object.</returns>
        public override Steering Update(SteeringManager manager, Actor actor,
			float dt)
        {
            float threshold = base.MaxDistance;

            Vector3 accum = Vector3.zero;
            foreach (Actor f in Flock.Members)
            {
                if (base.IsAffecting(actor, f))
                {
                    Vector3 v = actor.Position - f.Position;
                    float d = v.magnitude;
                    float str = Mathf.Max(0.0f, (threshold - d) / threshold)
						* manager.MaxForce;
                    accum += (v / d) * str;
                }
            }
			
			if(manager.IsPlanar) {
				accum.y = 0f;
			}
			
            return new Steering(accum, Vector3.zero);
        }

        public override string GetDescription()
        {
        	return "Maintain a minimum distance from other visible members"
        		+ " in a flock";
        }
    }
}

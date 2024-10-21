using System.Collections.Generic;
using System.Numerics;
using SwarmSimulation.Core.Agents.Contracts;
using SwarmSimulation.Utilities;

namespace SwarmSimulation.Core.Agents.Implementation
{
    public class Agent
    {
        protected Agent(int id, Vector2 position, int perceptionRange)
        {
            Id = id;
            Position = position;
            PerceptionRange = perceptionRange;
        }

        public int Id { get; }
        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; } = Vector2.Zero;
        public int PerceptionRange { get; }
        public List<IAgent> Neighbors { get; set; } = new List<IAgent>();
        public void Move(Vector2 controlInput)
        {
            var dt = SimulationTimeManager.GetDeltaTime();
            
            Velocity = controlInput;
            Position += controlInput * dt;
        }
    }
}
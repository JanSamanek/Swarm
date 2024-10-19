using System.Collections.Generic;
using System.Numerics;
using SwarmSimulation.Settings;
using SwarmSimulation.Utilities;

namespace SwarmSimulation.Core
{
    public class Agent
    {
        public int Id  { get; }
        public Vector2 Position { get; private set; }
        public Vector2 Velocity { get; private set; } = Vector2.Zero;
        public int PerceptionRange { get; }
        public List<Agent> Neighbors { get; set; } = new List<Agent>();

        public Agent(int id, Vector2 position, int perceptionRange)
        {
            Position = position;
            Id = id;
            PerceptionRange = perceptionRange;
        }

        public void Move(Vector2 controlInput)
        {
            var dt = SimulationTimeManager.GetDeltaTime();
            
            Velocity = controlInput;
            Position += controlInput * dt;
        }
    }
}
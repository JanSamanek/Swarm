using System.Drawing;
using System.Linq;
using System.Numerics;
using SwarmSimulation.Algorithms.Agents;

namespace SwarmSimulation.Visualization
{
    public static class SwarmRenderer
    {
        public static void DrawAgents(Swarm swarm, Graphics graphics,
            bool drawPerceptionRange = false)
        {
            var agentsList = swarm.Agents.ToList();
            
            if (drawPerceptionRange)
            {
                foreach (var agent in agentsList)
                {
                    DrawPerceptionRange(agent, graphics);
                }
            }
            
            foreach (var agent in agentsList)
            {
                DrawAgent(agent, graphics, agent.Size);
            }
        }

        private static void DrawPerceptionRange(Agent agent, Graphics graphics)
        {
            var range = agent.PerceptionRange;
            DrawCircle(graphics, agent.Position, range, Brushes.Firebrick);
        }

        private static void DrawAgent(Agent agent, Graphics graphics,  float radius)
        {
            if (agent is ForagingAgent foragingAgent && foragingAgent.CarriesResource)
            {
                DrawCircle(graphics, foragingAgent.Position, radius, Brushes.Red);
            }
            else
            {
                DrawCircle(graphics, agent.Position, radius, Brushes.White);
            }
        }

        private static void DrawCircle( Graphics graphics, Vector2 center, float radius, Brush brush)
        {
            graphics.FillEllipse(brush, (int) center.X - radius, (int) center.Y - radius, 
                radius*2, radius*2);
        }
    }
}
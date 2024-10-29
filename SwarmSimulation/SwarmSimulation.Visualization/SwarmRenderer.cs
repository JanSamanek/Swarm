using System.Drawing;
using System.Linq;
using System.Numerics;
using SwarmSimulation.Algorithms;
using SwarmSimulation.Algorithms.Agents;

namespace SwarmSimulation.Visualization
{
    public static class SwarmRenderer
    {
        public static void DrawAgents(Swarm swarm, Graphics graphics,
            bool drawPerceptionRange = false, bool drawId = false)
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
                if (drawId)
                {
                    DrawId(agent, graphics, agent.Size);
                }
            }
        }

        private static void DrawPerceptionRange(IAgent agent, Graphics graphics)
        {
            var range = agent.PerceptionRange;
            DrawCircle(graphics, agent.Position, range, Brushes.Firebrick);
        }

        private static void DrawAgent(IAgent agent, Graphics graphics,  float radius)
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
        
        private static void DrawId(IAgent agent, Graphics graphics,  float agentRadius)
        {
            var font = new Font("Arial", 10);
            var stringSize = graphics.MeasureString(agent.Id.ToString(), font);

            var (width, height) = (agentRadius, agentRadius);
            var textX = agent.Position.X - (float) width / 2 + (width - stringSize.Width) / 2;
            var textY = agent.Position.Y - (float) height / 2 + (width - stringSize.Height) / 2;

            graphics.DrawString(agent.Id.ToString(), font, Brushes.Black, textX, textY);
        }

        private static void DrawCircle( Graphics graphics, Vector2 center, float radius, Brush brush)
        {
            graphics.FillEllipse(brush, (int) center.X - radius / 2, (int) center.Y - radius / 2, 
                radius, radius);
        }
    }
}
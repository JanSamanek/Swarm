using System.Drawing;
using System.Linq;

namespace SwarmSimulation.Core
{
    public static class SwarmRenderer
    {
        public static void DrawAgents(Swarm swarm, Graphics graphics, (int Width, int Height) size,
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
                DrawAgent(agent, graphics, size);
                if (drawId)
                {
                    DrawId(agent, graphics, size);
                }
            }
        }

        private static void DrawPerceptionRange(Agent agent, Graphics graphics)
        {
            var range = agent.PerceptionRange;
            graphics.FillEllipse(Brushes.Firebrick, (int) agent.Position.X - range / 2, (int) agent.Position.Y - range / 2, 
                range, range);
        }

        private static void DrawAgent(Agent agent, Graphics graphics, (int Width, int Height) size)
        {
            graphics.FillEllipse(Brushes.White, (int) agent.Position.X - size.Width / 2, (int) agent.Position.Y - size.Height / 2, 
                size.Width, size.Height);
        }
        
        private static void DrawId(Agent agent, Graphics graphics, (int Width, int Height) agentSize)
        {
            var font = new Font("Arial", 10);
            var stringSize = graphics.MeasureString(agent.Id.ToString(), font);

            var (width, height) = agentSize;
            var textX = agent.Position.X - (float) width / 2 + (width - stringSize.Width) / 2;
            var textY = agent.Position.Y - (float) height / 2 + (width - stringSize.Height) / 2;

            graphics.DrawString(agent.Id.ToString(), font, Brushes.Black, textX, textY);
        }
    }
}
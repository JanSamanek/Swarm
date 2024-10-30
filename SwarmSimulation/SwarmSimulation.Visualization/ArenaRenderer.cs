using System.Drawing;
using SwarmSimulation.Environment;
using SwarmSimulation.Environment.Obstacles;

namespace SwarmSimulation.Visualization
{
    public static class ArenaRenderer
    {
        public static void DrawArena(Arena arena, Graphics graphics, bool drawNest=false)
        {
            if (drawNest)
            {
                DrawNest(arena.Nest, graphics);
            }
            
            foreach (var obstacle in arena.Obstacles)
            {
                switch (obstacle)
                {
                    case CircularObstacle circularObstacle:
                        DrawCircularObstacle(circularObstacle, graphics);
                        break;
                    case RectangularObstacle rectangularObstacle:
                        DrawRectangularObstacle(rectangularObstacle, graphics);
                        break;
                }
            }

            foreach (var resource in arena.Resources)
            {
                DrawResource(resource, graphics);
            }
        }

        private static void DrawNest(Nest nest, Graphics graphics)
        {
            var rect = new Rectangle((int)(nest.Center.X - nest.Width / 2),
                (int)(nest.Center.Y - nest.Height / 2),(int) nest.Width,
                (int) nest.Height);
            
            graphics.FillRectangle(Brushes.YellowGreen, rect);
        }

        private static void DrawResource(Resource resource, Graphics graphics)
        {
            graphics.FillEllipse(Brushes.Firebrick, (int)resource.Position.X - 3, (int)resource.Position.Y - 3, 6, 6);
        }
        
        private static void DrawCircularObstacle(CircularObstacle obstacle, Graphics graphics)
        {
            graphics.FillEllipse(Brushes.White, (int)obstacle.Position.X - obstacle.Radius,
                (int)obstacle.Position.Y - obstacle.Radius,
                obstacle.Radius * 2, obstacle.Radius * 2);
        }

        private static void DrawRectangularObstacle(RectangularObstacle obstacle, Graphics graphics)
        {
            var rect = new Rectangle((int)(obstacle.Center.X - obstacle.Width / 2),
                (int)(obstacle.Center.Y - obstacle.Height / 2),(int) obstacle.Width,
                (int) obstacle.Height);
            
            graphics.FillRectangle(Brushes.White, rect);
        }
    }
}
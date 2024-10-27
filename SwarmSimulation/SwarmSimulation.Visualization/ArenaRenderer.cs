using System.Drawing;
using SwarmSimulation.Environment;
using SwarmSimulation.Environment.Obstacles;

namespace SwarmSimulation.Visualization
{
    public static class ArenaRenderer
    {
        public static void DrawArena(Arena arena, Graphics graphics)
        {
            DrawNest(arena.Nest, graphics);
            
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
                (int)(nest.Center.Y - nest.Width / 2),(int) nest.Width,
                (int) nest.Height);
            
            graphics.FillRectangle(Brushes.Bisque, rect);
        }

        private static void DrawResource(Resource resource, Graphics graphics)
        {
            graphics.FillEllipse(Brushes.Firebrick, (int)resource.Position.X - 2, (int)resource.Position.Y - 2, 4, 4);
        }
        
        private static void DrawCircularObstacle(CircularObstacle obstacle, Graphics graphics)
        {
            graphics.FillEllipse(Brushes.White, (int)obstacle.Center.X - obstacle.Radius / 2,
                (int)obstacle.Center.Y - obstacle.Radius / 2,
                obstacle.Radius, obstacle.Radius);
        }

        private static void DrawRectangularObstacle(RectangularObstacle obstacle, Graphics graphics)
        {
            var rect = new Rectangle((int)(obstacle.Center.X - obstacle.Width / 2),
                (int)(obstacle.Center.Y - obstacle.Width / 2),(int) obstacle.Width,
                (int) obstacle.Height);
            
            graphics.FillRectangle(Brushes.White, rect);
        }
    }
}
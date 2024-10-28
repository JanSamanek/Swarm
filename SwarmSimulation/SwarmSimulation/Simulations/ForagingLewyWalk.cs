using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using SwarmSimulation.Agents;
using SwarmSimulation.Agents.Foraging;
using SwarmSimulation.Environment;
using SwarmSimulation.Visualization;

namespace SwarmSimulation.Simulations
{
    public sealed class ForagingLewyWalk : BaseForm
    {
        private Swarm _swarm;

        public ForagingLewyWalk()
        {
            Text = @"Swarm foraging simulation";
            StartSimulation();
        }

        protected override void InitializeSimulation()
        {
            Arena.Instance.Initialize(new Vector2(500, 300), 1000, 600);
            Arena.Instance.AddNest(new Vector2(500, 500), 150, 100);
            Arena.Instance.AddCircularObstacle(new Vector2(500, 300), 50);
            Arena.Instance.AddCircularObstacle(new Vector2(600, 150), 100);
            Arena.Instance.AddCircularObstacle(new Vector2(100, 150), 80);
            Arena.Instance.AddCircularObstacle(new Vector2(800, 450), 30);
            Arena.Instance.AddCircularObstacle(new Vector2(150, 380), 80);


            _swarm = SwarmBuilder.CreateSwarmInNest<ForagingAgent>(Arena.Instance.Nest, 10, 80);
        }

        protected override void UpdateSimulation(object sender, EventArgs e)
        {
            
        }

        protected override void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            ArenaRenderer.DrawArena(Arena.Instance, e.Graphics);
            SwarmRenderer.DrawAgents(_swarm, e.Graphics, (10, 10));
        }
    }
}
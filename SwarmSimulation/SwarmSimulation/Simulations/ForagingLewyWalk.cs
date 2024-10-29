using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using SwarmSimulation.Algorithms;
using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Algorithms.Foraging.LewyWalk;
using SwarmSimulation.Algorithms.MoveToTarget;
using SwarmSimulation.Algorithms.Utilities;
using SwarmSimulation.Environment;
using SwarmSimulation.Visualization;

namespace SwarmSimulation.Simulations
{
    public sealed class ForagingLewyWalk : BaseForm
    {
        private Swarm _swarm;
        private IAlgorithm<ForagingLewyWalkAlgorithmInput> _foragingAlgorithm;

        public ForagingLewyWalk()
        {
            Text = @"Swarm foraging simulation";
            StartSimulation();
        }

        protected override void InitializeSimulation()
        {
            var settings = new ForagingLewyWalkAlgorithmSettings
            {
                LewyParameter = 1,
                MaxFlightLength = 600,
                LewyScale = 20
            };

            _foragingAlgorithm = new ForagingLewyWalkAlgorithm(settings);
            var arenaBuilder = new ArenaBuilder();
            arenaBuilder.Initialize(new Vector2(500, 300), 1000, 600)
                .AddNest(new Vector2(500, 500), 150, 100)
                .AddCircularObstacle(new Vector2(500, 300), 50)
                .AddCircularObstacle(new Vector2(600, 150), 100)
                .AddCircularObstacle(new Vector2(100, 150), 80)
                .AddCircularObstacle(new Vector2(800, 450), 30)
                .AddCircularObstacle(new Vector2(150, 380), 80)
                .GenerateResources(30)
                .Build();
            
            _swarm = SwarmBuilder.CreateSwarmInNest<ForagingAgent>(Arena.Instance.Nest, 10, 10, 80);
        }

        protected override void UpdateSimulation(object sender, EventArgs e)
        {
            var input = new ForagingLewyWalkAlgorithmInput
            {
                MoveSpeed = 20
            };
            
            SwarmController.ExecuteAlgorithm(_swarm, _foragingAlgorithm, input);
            
            PictureBox.Invalidate();
        }

        protected override void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            ArenaRenderer.DrawArena(Arena.Instance, e.Graphics, drawNest:true);
            SwarmRenderer.DrawAgents(_swarm, e.Graphics, drawId:true);
        }
    }
}
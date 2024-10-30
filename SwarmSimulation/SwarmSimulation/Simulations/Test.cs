using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using SwarmSimulation.Algorithms;
using SwarmSimulation.Algorithms.AdaptiveMoveToTarget;
using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Algorithms.MoveToTarget;
using SwarmSimulation.Algorithms.ObstacleAvoidanceAPF;
using SwarmSimulation.Algorithms.Utilities;
using SwarmSimulation.Environment;
using SwarmSimulation.Visualization;

namespace SwarmSimulation.Simulations
{
    public sealed class Test : BaseForm
    {
        private Swarm _swarm;
        private Agent _leader;
        private IAlgorithm<MoveToTargetAlgorithmInput> _moveToTargetAlgorithm;

        public Test()
        {
            Text = @"Swarm Testing simulation";
            StartSimulation();
        }
        
         protected override void InitializeSimulation()
         {
             _moveToTargetAlgorithm = new MoveToTargetAlgorithm();
             
            const float perceptionRange = 100;
            _swarm = new Swarm();
            _leader = SwarmBuilder.AddAgent<LeaderAgent>(_swarm, new Vector2(580,320), 5, perceptionRange);
            
            var arenaBuilder = new ArenaBuilder();
            arenaBuilder.Initialize(new Vector2(500, 300), 500, 500)
                // .AddRectangularObstacle(new Vector2(500, 300), 50, 60)
                .AddCircularObstacle(new Vector2(500, 300), 25)
                .GenerateResources(50000)
                .Build();
         }

        protected override void UpdateSimulation()
        {
            var input = new MoveToTargetAlgorithmInput
            {
                Speed = 15.0f,
                TargetPosition = new Vector2(430, 300)
            };
            SwarmController.ExecuteAlgorithm(_swarm, _leader, _moveToTargetAlgorithm, input);
            
            PictureBox.Invalidate();
        }
        
        protected override void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            ArenaRenderer.DrawArena(Arena.Instance, e.Graphics);
            SwarmRenderer.DrawAgents(_swarm, e.Graphics);
        }
    }
}
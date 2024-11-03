using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using SwarmSimulation.Algorithms;
using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Algorithms.Agents.Formation;
using SwarmSimulation.Algorithms.Other.MoveToTarget;
using SwarmSimulation.Algorithms.Utilities;
using SwarmSimulation.Environment;
using SwarmSimulation.Visualization;

namespace SwarmSimulation.Simulations
{
    public sealed class Test : BaseForm
    {
        private Swarm _swarm;
        private LeaderAgent _leader;
        private IAlgorithm<MoveToTargetAlgorithmInput> _moveToTargetAlgorithm;

        public Test()
        {
            Text = @"Swarm Testing simulation";
            StartSimulation();
        }
        
         protected override void InitializeSimulation()
         {
             _moveToTargetAlgorithm = new MoveToTargetAlgorithm();
             
            _leader = new LeaderAgent(new Vector2(550,300), 10, 100);
            
            var swarmBuilder = new SwarmBuilder();
            _swarm = swarmBuilder
                .AddLeaderToSwarm(_leader)
                .Build();     
            
            var arenaBuilder = new ArenaBuilder();
            arenaBuilder.Initialize(new Vector2(500, 300), 500, 500)
                // .AddRectangularObstacle(new Vector2(500, 300), 50, 60)
                .AddCircularObstacle(new Vector2(500, 300), 25)
                .Build();
         }

        protected override void UpdateSimulation()
        {
            var input = new MoveToTargetAlgorithmInput
            {
                Speed = 20.0f,
                TargetPosition = new Vector2(430, 250)
            };
            SwarmController.ExecuteAlgorithm(_swarm, _leader, _moveToTargetAlgorithm, input);
            
            PictureBox.Invalidate();
        }
        
        protected override void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            ArenaRenderer.DrawArena(Arena.Instance, e.Graphics);
            SwarmRenderer.DrawAgents(_swarm, e.Graphics, drawId:true);
        }
    }
}
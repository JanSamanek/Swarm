using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using SwarmSimulation.Agents;
using SwarmSimulation.Agents.Basic;
using SwarmSimulation.Algorithms;
using SwarmSimulation.Algorithms.AdaptiveMoveToTarget;
using SwarmSimulation.Algorithms.MoveToTarget;
using SwarmSimulation.Algorithms.ObstacleAvoidanceAPF;
using SwarmSimulation.Environment;
using SwarmSimulation.Visualization;

namespace SwarmSimulation.Simulations
{
    public sealed class ObstacleAvoidance : BaseForm
    {
        private Swarm _swarm;
        private IAgent _leader;
        private IAlgorithm<AdaptiveMoveToTargetAlgorithmInput> _moveToTargetAlgorithm;


        public ObstacleAvoidance()
        {
            Text = @"Swarm obstacle avoidance simulation";
            StartSimulation();
        }
        
         protected override void InitializeSimulation()
         {
             var moveToTargetAlgorithmSettings = new AdaptiveMoveToTargetAlgorithmSettings
             {
                 MoveToTargetAlgorithmSettings = new MoveToTargetAlgorithmSettings
                 {
                     TargetPositionTolerance = 1f
                 },
                 ObstacleAvoidanceAlgorithmSettings = new ObstacleAvoidanceAlgorithmSettings
                 {
                     ApfGain = 5f
                 }
             };
             _moveToTargetAlgorithm = new AdaptiveMoveToTargetAlgorithm(moveToTargetAlgorithmSettings);
             
            const float perceptionRange = 100;
            _swarm = new Swarm();
            _leader = SwarmBuilder.AddAgent<LeaderAgent>(_swarm, new Vector2(580,320), perceptionRange);
            
            Arena.Instance.Initialize(new Vector2(500, 300), 600, 600);
            Arena.Instance.AddCircularObstacle(new Vector2(500, 300), 50);
         }

        protected override void UpdateSimulation(object sender, EventArgs e)
        {
            var input = new AdaptiveMoveToTargetAlgorithmInput
            {
                MoveToTargetAlgorithmInput = new MoveToTargetAlgorithmInput
                {
                    Speed = 15.0f,
                    TargetPosition = new Vector2(430, 300)
                },
                ObstacleAvoidanceAlgorithmInput = new ObstacleAvoidanceAlgorithmInput
                {
                    Distance = 5f
                }
            };
            SwarmController.ExecuteAlgorithm(_swarm, _leader, _moveToTargetAlgorithm, input);
            
            PictureBox.Invalidate();
        }
        
        protected override void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            ArenaRenderer.DrawArena(Arena.Instance, e.Graphics);
            SwarmRenderer.DrawAgents(_swarm, e.Graphics, (10, 10));
        }
    }
}
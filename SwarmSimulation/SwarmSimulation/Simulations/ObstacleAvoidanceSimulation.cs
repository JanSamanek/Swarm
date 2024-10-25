using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using SwarmSimulation.Core;
using SwarmSimulation.Core.Agents.Implementation;
using SwarmSimulation.Core.Algorithms;
using SwarmSimulation.Core.Algorithms.Contracts;
using SwarmSimulation.Core.Algorithms.Implementation.AdaptiveMoveToTarget;
using SwarmSimulation.Core.Algorithms.Implementation.CustomFormation;
using SwarmSimulation.Core.Algorithms.Implementation.MoveToTarget;
using SwarmSimulation.Core.Algorithms.Implementation.ObstacleAvoidanceAPF;
using SwarmSimulation.Environment;
using SwarmSimulation.Visualization;

namespace SwarmSimulation.Simulations
{
    public sealed class ObstacleAvoidanceSimulation : BaseForm
    {
        private Swarm _swarm;
        private int _leaderId;
        private IAlgorithm<AdaptiveMoveToTargetAlgorithmInput> _moveToTargetAlgorithm;


        public ObstacleAvoidanceSimulation()
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
            _leaderId = _swarm.AddLeader(new Vector2(580,320), perceptionRange);
            
            Arena.Instance.SetSize(700, 700);
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
            AlgorithmExecutor.ExecuteAlgorithmOn(_swarm, _leaderId, _moveToTargetAlgorithm, input);
            
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
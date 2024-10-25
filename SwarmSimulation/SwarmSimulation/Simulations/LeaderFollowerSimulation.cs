using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using SwarmSimulation.Core;
using SwarmSimulation.Core.Agents.Implementation;
using SwarmSimulation.Core.Algorithms;
using SwarmSimulation.Core.Algorithms.Contracts;
using SwarmSimulation.Core.Algorithms.Implementation.MoveToTarget;
using SwarmSimulation.Core.Algorithms.Implementation.Proximity;
using SwarmSimulation.Visualization;

namespace SwarmSimulation.Simulations
{
    public sealed class LeaderFollowerSimulation : BaseForm
    {
        private Swarm _swarm;
        private int _leaderId;

        private IAlgorithm<ProximityAlgorithmInput> _leaderFollowerAlgorithm;
        private IAlgorithm<MoveToTargetAlgorithmInput> _moveToTargetAlgorithm;
        
        public LeaderFollowerSimulation()
        {
            Text = @"Swarm line stretching simulation";
            StartSimulation();
        }

        protected override void InitializeSimulation()
        {
            
            var leaderFollowerAlgorithmSettings = new ProximityAlgorithmSettings
            {
                DampingCoefficient = 4f,
                InterAgentStiffnessCoefficient = 355.2f,
                LeaderStiffnessCoefficient = 288f
            };
            _leaderFollowerAlgorithm = new ProximityAlgorithm(leaderFollowerAlgorithmSettings);

            var moveToTargetAlgorithmSettings = new MoveToTargetAlgorithmSettings
            {
                TargetPositionTolerance = 1f
            };
            _moveToTargetAlgorithm = new MoveToTargetAlgorithm(moveToTargetAlgorithmSettings);
            
            const float perceptionRange = 100;
            _swarm = new Swarm();
            _leaderId = _swarm.AddLeader(new Vector2(500, 300), perceptionRange);
            _swarm.AddAgent(new Vector2(550, 300), perceptionRange);
            _swarm.AddAgent(new Vector2(525, 343), perceptionRange);
            _swarm.AddAgent(new Vector2(475, 343), perceptionRange);
            _swarm.AddAgent(new Vector2(450, 300), perceptionRange);
            _swarm.AddAgent(new Vector2(475, 256), perceptionRange);
            _swarm.AddAgent(new Vector2(525, 256), perceptionRange);
        }

        protected override void UpdateSimulation(object sender, EventArgs e)
        {
            var proximityAlgorithmInput = new ProximityAlgorithmInput
            {
                DesiredDistance = 50,
                NeighboursToCalculateFrom = 3
            };
            AlgorithmExecutor.ExecuteAlgorithmOn<RegularAgent, ProximityAlgorithmInput>(_swarm,
                _leaderFollowerAlgorithm, proximityAlgorithmInput);

            var moveToTargetAlgorithmInput = new MoveToTargetAlgorithmInput
            {
                TargetPosition = new Vector2(300, 300),
                Speed = 35.0f
            };
            AlgorithmExecutor.ExecuteAlgorithmOn(_swarm, _leaderId, _moveToTargetAlgorithm, moveToTargetAlgorithmInput);
            
            PictureBox.Invalidate();
        }
        
        protected override void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            SwarmRenderer.DrawAgents(_swarm, e.Graphics, (10, 10));
        }
    }
}
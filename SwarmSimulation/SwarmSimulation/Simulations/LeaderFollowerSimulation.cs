using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using SwarmSimulation.Core;
using SwarmSimulation.Core.Agents.Implementation;
using SwarmSimulation.Core.Algorithms.Contracts;
using SwarmSimulation.Core.Algorithms.Implementation.Proximity;
using SwarmSimulation.Visualization;

namespace SwarmSimulation.Simulations
{
    public sealed class LeaderFollowerSimulation : BaseForm
    {
        private Swarm _swarm;
        private LeaderAgent _leader1;

        private IAlgorithm<ProximityAlgorithmSettings, ProximityAlgorithmInput> _leaderFollowerAlgorithm;
        
        public LeaderFollowerSimulation()
        {
            Text = @"Swarm line stretching simulation";
            StartSimulation();
        }

        protected override void InitializeSimulation()
        {
            
            var algorithmSettings = new ProximityAlgorithmSettings
            {
                DampingCoefficient = 4f,
                InterAgentStiffnessCoefficient = 355.2f,
                LeaderStiffnessCoefficient = 288f
            };
            _leaderFollowerAlgorithm = new ProximityAlgorithm(algorithmSettings); 
            
            const int perceptionRange = 100;
            _swarm = new Swarm();

            _leader1 = _swarm.AddLeader(new Vector2(500, 300), perceptionRange);
            _swarm.AddAgent(new Vector2(550, 300), perceptionRange);
            _swarm.AddAgent(new Vector2(525, 343), perceptionRange);
            _swarm.AddAgent(new Vector2(475, 343), perceptionRange);
            _swarm.AddAgent(new Vector2(450, 300), perceptionRange);
            _swarm.AddAgent(new Vector2(475, 256), perceptionRange);
            _swarm.AddAgent(new Vector2(525, 256), perceptionRange);
        }

        protected override void UpdateSimulation(object sender, EventArgs e)
        {
            var input = new ProximityAlgorithmInput
            {
                DesiredDistance = 50,
                NeighboursToCalculateFrom = 1
            };
            _leader1.MoveToTarget(new Vector2(300, 300), 15.0f);
            _swarm.FollowLeader(_leaderFollowerAlgorithm, input);
            
            PictureBox.Invalidate();
        }
        
        protected override void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            SwarmRenderer.DrawAgents(_swarm, e.Graphics, (10, 10));
        }
    }
}
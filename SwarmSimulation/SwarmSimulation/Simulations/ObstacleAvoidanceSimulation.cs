using System;
using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using SwarmSimulation.Core;
using SwarmSimulation.Core.Agents.Implementation;
using SwarmSimulation.Core.Algorithms.Contracts;
using SwarmSimulation.Core.Algorithms.Implementation.CustomFormation;
using SwarmSimulation.Environment;
using SwarmSimulation.Visualization;

namespace SwarmSimulation.Simulations
{
    public sealed class ObstacleAvoidanceSimulation : BaseForm
    {
        private Swarm _swarm;
        private LeaderAgent _leader;

        public ObstacleAvoidanceSimulation()
        {
            Text = @"Swarm obstacle avoidance simulation";
            StartSimulation();
        }
        
         protected override void InitializeSimulation()
         {
            const float perceptionRange = 200;
            _swarm = new Swarm();
            _leader = _swarm.AddLeader(new Vector2(650,450), perceptionRange);
            
            Arena.Instance.SetSize(700, 700);
            Arena.Instance.AddCircularObstacle(new Vector2(500, 300), 50);
         }

        protected override void UpdateSimulation(object sender, EventArgs e)
        {
            _leader.MoveToTarget(new Vector2(350,150), 15.0f);
            
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
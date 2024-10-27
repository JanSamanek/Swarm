using System;
using System.Drawing;
using System.Windows.Forms;
using SwarmSimulation.Agents;
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
            throw new NotImplementedException();
        }

        protected override void UpdateSimulation(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected override void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            SwarmRenderer.DrawAgents(_swarm, e.Graphics, (10, 10));
        }
    }
}
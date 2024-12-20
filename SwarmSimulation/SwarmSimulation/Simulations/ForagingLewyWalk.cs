using System.Drawing;
using System.Numerics;
using System.Windows.Forms;
using SwarmSimulation.Algorithms;
using SwarmSimulation.Algorithms.Agents;
using SwarmSimulation.Algorithms.Foraging.LewyWalk;
using SwarmSimulation.Algorithms.Utilities;
using SwarmSimulation.Environment;
using SwarmSimulation.Visualization;

namespace SwarmSimulation.Simulations
{
    public sealed class ForagingLewyWalk : BaseForm
    {
        private Swarm _swarm;
        private IAlgorithm<LwForagingAlgorithmInput> _foragingAlgorithm;

        public ForagingLewyWalk()
        {
            Text = @"Swarm foraging simulation";
            StartSimulation();
        }

        protected override void InitializeSimulation()
        {
            var settings = new LwForagingAlgorithmSettings
            {
                LewyParameter = 1,
                MaxFlightLength = 800,
                LewyScale = 3f,
                MaxExploringAttempts = 100
            };
            _foragingAlgorithm = new LwForagingAlgorithm(settings);
            
            var arenaBuilder = new ArenaBuilder();
            arenaBuilder.Initialize(new Vector2(500, 300), 1000, 600)
                .AddNest(new Vector2(500, 500), 150, 100)
                .AddCircularObstacle(new Vector2(500, 300), 25)
                .AddCircularObstacle(new Vector2(600, 150), 50)
                .AddCircularObstacle(new Vector2(100, 150), 40)
                .AddCircularObstacle(new Vector2(800, 450), 15)
                .AddCircularObstacle(new Vector2(150, 380), 40)
                .GenerateResources(50)
                .Build();
            
            var swarmBuilder = new SwarmBuilder();
            _swarm = swarmBuilder
                .SetPerceptionRange(40)
                .SetAgentSize(6)
                .SetAgentType(AgentsType.LWForaging)
                .SetMaxResourceCapacity(5)
                .BuildInNest(Arena.Instance.Nest, 10);
        }

        protected override void UpdateSimulation()
        {
            var input = new LwForagingAlgorithmInput
            {
                MoveSpeed = 50
            };
            
            SwarmController.ExecuteAlgorithm(_swarm, _foragingAlgorithm, input);
            
            PictureBox.Invalidate();
        }

        protected override void OnPaint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
            ArenaRenderer.DrawArena(Arena.Instance, e.Graphics, drawNest:true);
            SwarmRenderer.DrawAgents(_swarm, e.Graphics);
        }
    }
}
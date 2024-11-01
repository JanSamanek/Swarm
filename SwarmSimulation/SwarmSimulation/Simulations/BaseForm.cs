using System;
using System.Drawing;
using System.Windows.Forms;
using SwarmSimulation.Engine;
using SwarmSimulation.Environment.Utilities;
using SwarmSimulation.Settings;
using SwarmSimulation.Utilities;

namespace SwarmSimulation.Simulations
{
    public abstract class BaseForm : Form
    {
        protected readonly PictureBox PictureBox;
        private readonly Timer _timer;

        protected BaseForm()
        {
            ClientSize = new Size(1000, 600);
            PictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,    
                BackColor = Color.Black     
            };
            PictureBox.Paint += OnPaint;
            Controls.Add(PictureBox);

            SimulationTimeManager.SetFps(SimulationSettings.Fps);
            _timer = new Timer
            {
                Interval = 1000 / SimulationSettings.Fps,
            };
            _timer.Tick += Update;
        }

        private void Update(object sender, EventArgs e)
        {
            UpdateSimulation();
            GarbageCollector.ClearHarvestedResources();
        }
        protected void StartSimulation()
        {
            DoubleBuffered = true;
            InitializeSimulation();
            _timer.Start();
        }
        protected abstract void InitializeSimulation();
        protected abstract void UpdateSimulation();
        protected abstract void OnPaint(object sender, PaintEventArgs e);
    }
}
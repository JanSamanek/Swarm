using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SwarmSimulation.Core;
using SwarmSimulation.Settings;

namespace SwarmSimulation
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

            _timer = new Timer
            {
                Interval = 1000 / SimulationSettings.Fps,
            };
            _timer.Tick += UpdateSimulation;
            
            KeyPreview = true;
            KeyDown += OnKeyDown;
        }

        private static void OnKeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    break;
                case Keys.Down:
                    break;
                case Keys.Left:
                    break;
                case Keys.Right:
                    break;
            }
        }

        protected void StartSimulation()
        {
            InitializeSimulation();
            _timer.Start();
        }
        protected abstract void InitializeSimulation();
        protected abstract void UpdateSimulation(object sender, EventArgs e);
        protected abstract void OnPaint(object sender, PaintEventArgs e);
    }
}
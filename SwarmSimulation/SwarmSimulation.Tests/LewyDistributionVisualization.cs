using System;
using System.Drawing;
using System.Windows.Forms;
using ScottPlot.PathStrategies;
using ScottPlot.WinForms;
using SwarmSimulation.Tests.Utilities;
using Label = System.Windows.Forms.Label;

namespace SwarmSimulation.Tests
{
    public sealed class LewyDistributionVisualization : Form
    {
        private readonly Panel _panel =  new Panel { Dock = DockStyle.Fill };
        private readonly FormsPlot _formsPlot = new FormsPlot { Dock = DockStyle.Fill };
        private readonly TrackBar _slider = new TrackBar
        {
            Minimum = 0,
            Maximum = 200,
            Value = 100, 
            Dock = DockStyle.Bottom,
            TickFrequency = 1,
            SmallChange = 1,
            LargeChange = 2,
        };

        private readonly Label _sliderLabel = new Label
        {
            Text = @"Lewy Parameter: 1.0",
            Dock = DockStyle.Bottom,
            TextAlign = ContentAlignment.MiddleCenter,
        };
        
        public LewyDistributionVisualization()
        {
            ClientSize = new Size(800, 450);
            Text = @"Lewy Distribution Test";
            
            _panel.Controls.Add(_formsPlot);
            Controls.Add(_panel);
            Controls.Add(_slider);
            Controls.Add(_sliderLabel);
            
            
            _slider.Scroll += Slider_Scroll;
            
            UpdatePlot();

        }
        
        private void Slider_Scroll(object sender, EventArgs e)
        {
            UpdatePlot();
        }

        private void UpdatePlot()
        {
            var lewyParameter = _slider.Value / 100f;
            var lewyData = DataGenerator.GenerateLewyDistribution(lewyParameter, max: 100);
            
            var hist = new ScottPlot.Statistics.Histogram(lewyData, 500);
                
            _formsPlot.Plot.Clear();
            var scatter = _formsPlot.Plot.Add.Scatter(hist.Bins, hist.Counts);
            scatter.MarkerSize = 0;
            scatter.LineWidth = 2;
            scatter.PathStrategy = new CubicSpline();
            _formsPlot.Plot.Axes.Margins(top: 0);
            _formsPlot.Plot.Axes.SetLimits(left: -100, right: 100, bottom: 0);
            
            _sliderLabel.Text = $@"Lewy Parameter: {lewyParameter:F2}";
            _formsPlot.Refresh();
        }
    }
}
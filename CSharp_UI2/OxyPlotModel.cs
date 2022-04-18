using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary;
using OxyPlot;
using OxyPlot.Legends;
using OxyPlot.Series;

namespace CSharp_UI2
{
    public class OxyPlotModel
    {
        public PlotModel plotModel { get; private set; }
        private MeasuredData md;
        private SplinesData sd;
        public OxyPlotModel(MeasuredData md)
        {
            this.md = md;
            this.plotModel = new PlotModel { Title = "Measured Data" };
            this.DrawDots();
        }

        public OxyPlotModel(SplinesData sd)
        {
            this.sd = sd;
            this.plotModel = new PlotModel { Title = "Measured Data and Spline" };
            this.DrawDots();
            this.DrawSpline();
        }

        public void DrawDots()
        {
            if (md != null)
            {
                this.plotModel.Series.Clear();
                OxyColor color = OxyColors.DeepPink;
                LineSeries lineSeries = new LineSeries();
                for (int i = 0; i < md.nodeNum; i++)
                {
                    lineSeries.Points.Add(new DataPoint(md.nodes[i], md.measurVals[i]));
                }

                lineSeries.Title = "Reference points";
                lineSeries.Color = color;
                lineSeries.LineStyle = LineStyle.None;
                lineSeries.MarkerType = MarkerType.Circle;
                lineSeries.MarkerSize = 4;
                lineSeries.MarkerStroke = color;
                lineSeries.MarkerFill = color;

                Legend leg = new Legend();
                this.plotModel.Legends.Add(leg);
                this.plotModel.Series.Add(lineSeries);
            }
        }

        public void DrawSpline()
        {
            if (sd != null)
            {
                this.plotModel.Series.Clear();
                OxyColor color = OxyColors.DeepPink;
                LineSeries lineSeries = new LineSeries();
                for (int i = 0; i < sd.mesData.nodeNum; i++)
                {
                    lineSeries.Points.Add(new DataPoint(sd.mesData.nodes[i], sd.mesData.measurVals[i]));
                }

                lineSeries.Title = "Reference points";
                lineSeries.Color = color;
                lineSeries.LineStyle = LineStyle.None;
                lineSeries.MarkerType = MarkerType.Circle;
                lineSeries.MarkerSize = 4;
                lineSeries.MarkerStroke = color;
                lineSeries.MarkerFill = color;

                Legend leg = new Legend();
                this.plotModel.Legends.Add(leg);
                this.plotModel.Series.Add(lineSeries);

                color = OxyColors.Aqua;
                lineSeries = new LineSeries();
                for (int i = 0; i < sd.nodes.Length; i++)
                {
                    lineSeries.Points.Add(new DataPoint(sd.nodes[i], sd.splineVals[i]));
                }

                lineSeries.Title = "Spline";
                lineSeries.Color = color;
                lineSeries.MarkerSize = 4;

                this.plotModel.Series.Add(lineSeries);
            }
        }
    }
}

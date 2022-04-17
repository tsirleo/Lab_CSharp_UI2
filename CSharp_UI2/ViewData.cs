using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using ClassLibrary;

namespace CSharp_UI2
{
    public class ViewData: IDataErrorInfo
    {
        public int ndNum { get; set; } = 5;
        public double sgstart { get; set; } = 0.0;
        public double sgend { get; set; } = 1.0;
        public SPf ftype { get; set; }
        public Uniform ndtype { get; set; }
        public int spndNum { get; set; } = 9;
        public double derstart { get; set; } = 1.0;
        public double derend { get; set; } = 1.0;
        public double intstart { get; set; } = 0.0;
        public double intend { get; set; } = 1.0;
        public MeasuredData md { get; set; }
        public SplineParameters sp { get; set; }
        public SplinesData sd { get; set; }
        public ChartData graphics;
        public ObservableCollection<string> mdList { get; set; } = new ObservableCollection<string>();
        public ObservableCollection<string> splnList { get; set; } = new ObservableCollection<string>();

        public string this[string columnName]
        {
            get
            {
                string error = string.Empty;

                switch (columnName)
                {
                    case nameof(ndNum):
                        if (ndNum <= 2)
                            error = "Node number must be greater or equal 2";
                        break;
                    case nameof(sgstart):
                        if (sgstart >= sgend)
                        {
                            error = "Beginning of segment must be less than end";
                        }
                        break;
                    case nameof(sgend):
                        if (sgend <= sgstart)
                        {
                            error = "End of segment must be greater than beginning";
                        }
                        break;
                    case nameof(spndNum):
                        if (spndNum <= 2)
                        {
                            error = "Spline nodes number must be greater or equal 2";
                        }
                        break;
                    case nameof(intstart):
                        if (intstart < sgstart || intstart > sgend || intstart >= intend)
                        {
                            error = "Beginning of segment must be less than end, and don't go beyond the original segment";
                        }
                        break;
                    case nameof(intend):
                        if (intend < sgstart || intend > sgend || intend <= intstart)
                        {
                            error = "End of segment must be greater than beginning, and don't go beyond the original segment";
                        }
                        break;
                }

                return error;
            }
        }

        public string Error { get; }

        public void ApplyMeasureData()
        {
            md = new MeasuredData(ndNum, sgstart, sgend, ftype, ndtype);
            if (md != null)
            {
                for (int i = 0; i < ndNum; i++)
                {
                    mdList.Add($"{i+1}) x = {md.nodes[i].ToString("F4")}   y = {md.measurVals[i].ToString("F4")}");
                }
            }
        }

        public void ApplySplineData()
        {
            if (md == null)
            {
                System.Windows.MessageBox.Show($"Apply the grid data", "Warning message", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                sp = new SplineParameters(spndNum, derstart, derend, intstart, intend);
                sd = new SplinesData(md, sp);
                try
                {
                    sd.Interpolate();
                    splnList.Add($"left bound derivative = {sd.derives[1].ToString("F")}");
                    splnList.Add($"right bound derivative = {sd.derives[3].ToString("F")}");
                    splnList.Add($"integral value = {sd.integral.ToString("F3")}");
                    for (int i = 0; i < sp.nodeNum; i++)
                    {
                        splnList.Add($"{i + 1}) x = {sd.nodes[i].ToString("F4")}   y = {sd.splineVals[i].ToString("F4")}");
                    }
                }
                catch (Exception e)
                {
                    System.Windows.MessageBox.Show($"An error occurred during interpolation and integration",
                        "Error message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}

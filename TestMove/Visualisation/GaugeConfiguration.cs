using Syncfusion.UI.Xaml.Gauges;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestMove
{
    public class GaugeConfiguration
    {
        public void ApplyConfiguration(SfCircularGauge defaultGauge, int highPressureValue)
        {
            // initialise circular gauge
            CircularScale mainscale = new CircularScale();

            // customise gauge scale to make smaller more visible
            var gaugeScale = DetermineGaugeScale(highPressureValue);
            mainscale.StartValue = gaugeScale.Item1;
            mainscale.EndValue = gaugeScale.Item2;

            mainscale.LabelStroke = new SolidColorBrush(Colors.White);
            mainscale.RimStroke = new SolidColorBrush(Colors.White);
            mainscale.RimStrokeThickness = 3;
            mainscale.LabelOffset = 0.1;

            // customise major and minor ticks
            MajorTickSetting majorTickSetting = new MajorTickSetting();
            majorTickSetting.StrokeThickness = 1;
            majorTickSetting.Length = 10;

            mainscale.MajorTickSettings = majorTickSetting;

            MinorTickSetting minorTickSetting = new MinorTickSetting();
            minorTickSetting.StrokeThickness = 1;
            minorTickSetting.Length = 5;

            mainscale.MinorTickSettings = minorTickSetting;

            // add and customise needle pointer and set value
            CircularPointer needlePointer = new CircularPointer();
            needlePointer.PointerType = PointerType.NeedlePointer;
            needlePointer.Value = highPressureValue;
            needlePointer.NeedleLengthFactor = 0.4;
            needlePointer.NeedlePointerType = NeedlePointerType.Triangle;
            needlePointer.PointerCapDiameter = 12;
            needlePointer.NeedlePointerStroke = new SolidColorBrush(Colors.White);
            needlePointer.KnobFill = new SolidColorBrush(Colors.White);
            needlePointer.KnobStroke = new SolidColorBrush(Colors.White);
            needlePointer.NeedlePointerStrokeThickness = 7;

            mainscale.Pointers.Add(needlePointer);

            // add range graphics
            CircularPointer rangeGraphicRight = new CircularPointer();
            rangeGraphicRight.PointerType = PointerType.RangePointer;
            rangeGraphicRight.Value = 0;
            rangeGraphicRight.RangeStart = 100;
            rangeGraphicRight.RangePointerStroke = (SolidColorBrush)new BrushConverter().ConvertFrom("#28273F");         
            CircularPointer rangeGraphicLeft = new CircularPointer();
            rangeGraphicLeft.PointerType = PointerType.RangePointer;
            rangeGraphicLeft.Value = 0;
            rangeGraphicLeft.RangeStart = -100;
            rangeGraphicLeft.RangePointerStroke = (SolidColorBrush)new BrushConverter().ConvertFrom("#80334C");

            mainscale.Pointers.Add(rangeGraphicLeft);
            mainscale.Pointers.Add(rangeGraphicRight);

            // add symbol pointer
            CircularPointer symbolPointer = new CircularPointer();
            symbolPointer.PointerType = PointerType.SymbolPointer;
            symbolPointer.Value = highPressureValue;
            symbolPointer.SymbolPointerHeight = 12;
            symbolPointer.SymbolPointerWidth = 12;
            symbolPointer.Symbol = Symbol.InvertedTriangle;
            symbolPointer.SymbolPointerStroke = new SolidColorBrush(Colors.White);
            mainscale.Pointers.Add(symbolPointer);

            // apply all presets to current gauge
            defaultGauge.Scales.Add(mainscale);
        }

        public (int,int) DetermineGaugeScale(int highPressureValue)
        {
            // methods to determine end value (start value is just the negative) 

            // take the absolute value 
            highPressureValue = Math.Abs(highPressureValue);

            // if divisible by 10, end value is +5
            if (highPressureValue % 10 == 0) // Check if the number ends in 0
            {
               int highPressureRounded = highPressureValue + 5;

               int endScaleValue = highPressureRounded;
               int startScaleValue = highPressureRounded * -1;

               return (startScaleValue, endScaleValue);
            }
            // if less than 5, end value is 5
            if (highPressureValue < 5) 
            {
                int endScaleValue = 5;
                int startScaleValue = -5;

                return (startScaleValue, endScaleValue);
            }
            // otherwise, end value is rounded up to nearest 10
            else
            {
                int highPressureRounded = ((highPressureValue + 9) / 10) * 10;

                int endScaleValue = highPressureRounded;
                int startScaleValue = highPressureRounded * -1;

                return (startScaleValue, endScaleValue);
            }                 
        }

    }
}

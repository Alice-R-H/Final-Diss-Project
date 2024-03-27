using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;

namespace TestMove
{
    public class ConfigureUIElements
    {
        // methods which configure UI elements as the user interacts (labels, grids, etc)

        private MainWindow _mainWindow;

        public ConfigureUIElements(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public void ConfigureUIOnClick(UIElement activeGrid, Shape activeRectangle, string dashboardName)
        {
            UpdateGridVisibility(activeGrid);
            UpdateButtonHighlight(activeRectangle);
            UpdateTitleOnClick(_mainWindow.TitleLabel, dashboardName);
        }

        public void UpdateTitleOnClick(TextBlock textBlock, string panelVisible)
        {
            switch (panelVisible)
            {
                case "Time":
                    textBlock.Text = "I M P A C T  O F  T I M E";
                    break;
                case "Pressure":
                    textBlock.Text = "I M P A C T  O F  P R E S S U R E";
                    break;
                case "Events":
                    textBlock.Text = "I M P A C T  O F  E V E N T S";
                    break;
            }
        }

        public void UpdateGridVisibility(UIElement activeGrid)
        {
            // on-click GRID visibility handling

            // hide all analysis grids
            _mainWindow.OTGrid.Visibility = Visibility.Hidden;
            _mainWindow.PressureGrid.Visibility = Visibility.Hidden;
            _mainWindow.EventsGrid.Visibility = Visibility.Hidden;

            // set current grid as visible
            activeGrid.Visibility = Visibility.Visible;

            // close all info grids that might be open 
            _mainWindow.WinrateInfoGrid.Visibility = Visibility.Hidden;
            _mainWindow.HeadshotInfoGrid.Visibility = Visibility.Hidden;
            _mainWindow.AbilityInfoGrid.Visibility = Visibility.Hidden;
            _mainWindow.KASTInfoGrid.Visibility = Visibility.Hidden;
            _mainWindow.PMultiplierInfoGrid.Visibility = Visibility.Hidden;
            _mainWindow.IMultiplierInfoGrid.Visibility = Visibility.Hidden;
            _mainWindow.HPPMultiplierInfoGrid.Visibility = Visibility.Hidden;
            _mainWindow.HPIMultiplierInfoGrid.Visibility = Visibility.Hidden;
            _mainWindow.HPIndexInfoGrid.Visibility = Visibility.Hidden;
            _mainWindow.EPMultiplierInfoGrid.Visibility = Visibility.Hidden;
            _mainWindow.EIMultiplierInfoGrid.Visibility = Visibility.Hidden;
            _mainWindow.EventsTrendsInfoGrid.Visibility = Visibility.Hidden;
        }

        public void UpdateButtonHighlight(Shape activeRectangle) 
        {
            // BUTTON colour handling

            // define colour for highlighted button
            var hexColorHighlight = "#15141E";
            var converterHighlight = new BrushConverter();
            var brushHighlight = (Brush)converterHighlight.ConvertFromString(hexColorHighlight);
            var highlightButtonBackground = brushHighlight;

            // set all buttons to default
            _mainWindow.TimeAnalysisHighlight.Fill = null;
            _mainWindow.PressureAnalysisHighlight.Fill = null;
            _mainWindow.EventsAnalysisHighlight.Fill = null;

            // set active button to highlighted colour
            activeRectangle.Fill = highlightButtonBackground;
        }
 
        public void ShowInfoOnClick(UIElement activeGrid, bool makeVisible)
        {
            // if makeVisible is true, we want to make activeGrid grid visible, otherwise, hide activeGrid grid.

            if (makeVisible)
            {              
                        activeGrid.Visibility = Visibility.Visible;
            }
            else
            {
                        activeGrid.Visibility = Visibility.Hidden;
            }
        }
    }
}

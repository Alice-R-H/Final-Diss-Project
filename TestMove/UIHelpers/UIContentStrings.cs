using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMove
{
    class UIContentStrings
    {

        private MainWindow _mainWindow;

        public void SetMainWindow(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        public string OverTimeImpactMultiText = "This is your Impact Multiplier, it demonstrates how your in-game impact is effected per consecutive game you play." +
            " A value less than 1 means your impact decreases, a value greater than 1 means your impact increases. The greater your impact, the more help you give to your team," +
            " so the higher the better!";
           

        public void SetAllInformationTileText()
        {
            _mainWindow.IMultiplierInfoText.Text = OverTimeImpactMultiText;
        }


    }
}

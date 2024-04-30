using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeGamePlatform
{
    public class TextLabel
    {
        private Label lbl;
        public Label LabelControl
        {
            get { return this.lbl; }
        }
        public TextLabel(string str, Position pos)
        {
            lbl = new Label();
            lbl.Top = pos.X * Board.resolutionFactor; 
            lbl.Left = pos.Y * Board.resolutionFactor; 
            lbl.Text = str;
            lbl.AutoSize = true;
        }
        public void SetFont(string name, int size)
        {
            lbl.Font = new System.Drawing.Font(name, size);
        }
        public void SetText(string str)
        {
            lbl.Text = str;
        }

        public string GetText()
        {
            return lbl.Text;
        }

        public void SetPosition(Position p)
        {
            lbl.Top = p.X * Board.resolutionFactor; 
            lbl.Left = p.Y * Board.resolutionFactor; 
        }

        public Position GetPosition()
        {
            return new Position(lbl.Top / Board.resolutionFactor, lbl.Left / Board.resolutionFactor);
        }

    }
}

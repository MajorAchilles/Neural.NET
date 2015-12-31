using System;
using System.Drawing;

namespace Neural.NET.Visualization
{
    internal class VisualNeuron : VisualizationComponent
    {
        public override Bitmap Draw()
        {
            throw new NotImplementedException();
        }

        public override void Draw(Graphics g, Color primaryColor, Color secondaryColor)
        {
            Brush primary = new SolidBrush(primaryColor);
            Pen secondary = new Pen(secondaryColor, 3F);
            g.FillEllipse(primary, new RectangleF(Left, Size));
            g.DrawEllipse(secondary, new RectangleF(Left, Size));
        }
    }
}
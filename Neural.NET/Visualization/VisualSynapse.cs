using System;
using System.Drawing;

namespace Neural.NET.Visualization
{
    internal class VisualSynapse : VisualizationComponent
    {
        /// <summary>
        /// The weight
        /// </summary>
        private float weight;

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        /// <value>
        /// The weight.
        /// </value>
        public float Weight
        {
            get
            {
                return weight;
            }

            set
            {
                weight = value;
            }
        }

        public override Bitmap Draw()
        {
            throw new NotImplementedException();
        }

        public override void Draw(Graphics g, Color primaryColor, Color secondaryColor)
        {
            Pen pen = new Pen(primaryColor, weight);
            g.DrawLine(pen, Left, Right);
        }
    }
}
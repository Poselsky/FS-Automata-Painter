using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiniteStateMachine
{
    public static class Extensions
    {
        public static Bitmap ResizeBitmap(this Bitmap input, int width, int height)
        {
            if (width <= 0 || height <= 0)
            {
                throw new ArgumentException("Resizing applies only for positive values");
            }

            Bitmap bitmap = new Bitmap(width,height);

            
            using (Graphics img = Graphics.FromImage(bitmap))
            {
                img.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
        
                img.DrawImage(input, 0,0,width,height);
            };
            
            
            return bitmap;
        }
    }
}

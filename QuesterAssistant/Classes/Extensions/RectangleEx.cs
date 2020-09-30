using System.Drawing;

namespace QuesterAssistant.Classes.Extensions
{
    public static class RectangleEx
    {
        public static int Area(this Rectangle rectangle) => 
            rectangle.Height * rectangle.Width;
    }
}
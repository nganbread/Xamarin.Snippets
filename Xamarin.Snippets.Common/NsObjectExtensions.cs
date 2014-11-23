using System.Drawing;
using MonoTouch.Foundation;

namespace Xamarin.Snippets.Common
{
    public static class NSObjectExtensions
    {
        public static RectangleF AsRectangleF(this NSObject o)
        {
            return new NSValue(o.Handle).RectangleFValue;
        }

        public static double AsDouble(this NSObject o)
        {
            return new NSNumber(o.Handle).DoubleValue;
        }

        public static int AsInt(this NSObject o)
        {
            return new NSNumber(o.Handle).IntValue;
        }
    }
}

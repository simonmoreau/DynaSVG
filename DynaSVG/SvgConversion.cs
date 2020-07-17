using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Svg;
using SVG = Svg;
using Autodesk.DesignScript.Geometry;
using Svg.Pathing;

namespace DynaSVG
{
    static class SvgConversion
    {
        public static SvgVisualElement ConvertCurve(Curve curve)
        {
            SvgVisualElement svgVisualElement = null;
            if (curve is Arc)
            {
                svgVisualElement = ConvertArc((Arc)curve);
            }
            else if (curve is Line)
            {
                svgVisualElement = ConvertLine((Line)curve);
            }
            else if (curve is Circle)
            {
                svgVisualElement = ConvertCircle((Circle)curve);
            }

            svgVisualElement.Stroke = new SvgColourServer(System.Drawing.Color.Black);
            svgVisualElement.StrokeWidth = new SvgUnit((float)0.2);
            svgVisualElement.Fill = SvgPaintServer.None;

            return svgVisualElement;

        }

        private static SvgPath ConvertArc(Arc arc)
        {

            SvgPath svgPath = new SvgPath();

            SvgPathSegmentList svgPathSegmentList = new SvgPathSegmentList();
            svgPathSegmentList.Add(new SvgMoveToSegment(arc.StartPoint.ConvertToPointF()));

            Vector a = Vector.ByTwoPoints(arc.CenterPoint, arc.StartPoint);
            Vector b = Vector.ByTwoPoints(arc.CenterPoint, arc.EndPoint);

            double angleAboutAxis = a.AngleAboutAxis(b, Vector.ZAxis());

            a.Dispose();
            b.Dispose();

            
            SvgArcSweep svgArcSweep = SvgArcSweep.Positive;

            if (angleAboutAxis >= 180) { svgArcSweep = SvgArcSweep.Negative; }

            SvgArcSize svgArcSize = SvgArcSize.Small;
            if (arc.SweepAngle > 180 ) { svgArcSize = SvgArcSize.Large; }

            SvgArcSegment svgArcSegment = new SvgArcSegment(svgPathSegmentList.Last.End, (float)arc.Radius, (float)arc.Radius, (float)arc.SweepAngle, svgArcSize, svgArcSweep, arc.EndPoint.ConvertToPointF());

            svgPathSegmentList.Add(svgArcSegment);
            svgPath.PathData = svgPathSegmentList;

            return svgPath;

        }

        private static SvgPath ConvertLine(Line line)
        {

            SvgPath svgPath = new SvgPath();

            SvgPathSegmentList svgPathSegmentList = new SvgPathSegmentList();

            svgPathSegmentList.Add(new SvgMoveToSegment(line.StartPoint.ConvertToPointF()));

            SvgLineSegment svgLineSegment = new SvgLineSegment(svgPathSegmentList.Last.End, line.EndPoint.ConvertToPointF());

            svgPathSegmentList.Add(svgLineSegment);
            svgPath.PathData = svgPathSegmentList;


            return svgPath;

        }

        private static SvgCircle ConvertCircle(Circle circle)
        {
            return new SvgCircle
            {
                Radius = new SvgUnit(SvgUnitType.Pixel, (float)circle.Radius),
                CenterX = new SvgUnit(SvgUnitType.Pixel,(float)circle.CenterPoint.X),
                CenterY = new SvgUnit(SvgUnitType.Pixel, -(float)circle.CenterPoint.Y),
            };

        }

        private static System.Drawing.PointF ConvertToPointF(this Point point)
        {
            System.Drawing.PointF pointF = new System.Drawing.PointF((float)point.X, -(float)point.Y);

            return pointF;
        }

    }
}

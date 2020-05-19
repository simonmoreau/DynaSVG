using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Svg;
using System.Drawing;
using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;

namespace DynaSVG
{
    /// <summary>
    /// A SVG document is used to agreate geometry and style to create a SVG file.
    /// </summary>
    public class Svg
    {
        /// <summary>
        /// This is the wrapped SvgDocument.
        /// </summary>
        internal SvgDocument InnerDocument;

        /// <summary>
        /// Get the aspect of the viewport.
        /// </summary>
        public string AspectRatio { get => InnerDocument.AspectRatio.ToString(); set => InnerDocument.AspectRatio = new SvgAspectRatio(); }

        /// <summary>
        /// Save a SVG document as a new SVG file.
        /// </summary>
        /// <param name="svg">
        /// The SVG document to be saved.
        /// </param>
        /// <param name="path">
        /// The path where to the newly created SVG file.
        /// </param>
        /// <param name="option">
        /// The option when saving the SVG document.
        /// </param>
        /// <returns>A boolean indicating if the document have been saved.</returns>
        /// <search>
        /// save
        /// </search>
        public static bool SaveAs(Svg svg, string path, [DefaultArgument("DynaSVG.Svg.GetNull()")]Option option)
        {
            svg.InnerDocument.Write(path);
            return true;
        }

        /// <summary>
        /// Create a new SVG document from geometry
        /// </summary>
        /// <param name="geometry">
        /// A list containing geometry to be added to the SVG document.
        /// </param>
        /// <returns>The SVG document</returns>
        /// <search>
        /// geometry, open
        /// </search>
        public static Svg ByGeometry([DefaultArgument("DynaSVG.Svg.GetNull()")]Geometry geometry)
        {
            SvgDocument svgDoc = new SvgDocument
            {
                Width = 500,
                Height = 500,
                ViewBox = new SvgViewBox(-250, -250, 500, 500),
            };

            var group = new SvgGroup();
            svgDoc.Children.Add(group);

            group.Children.Add(new SvgCircle
            {
                Radius = 100,
                Fill = new SvgColourServer(Color.Red),
                Stroke = new SvgColourServer(Color.Black),
                StrokeWidth = 2
            });

            return new Svg(svgDoc);
        }

        /// <summary>
        /// Create a new SVG document from a svg file.
        /// </summary>
        /// <param name="path">
        /// The path to the svg file.
        /// </param>
        /// <returns>The SVG document</returns>
        /// <search>
        /// file, open
        /// </search>
        public static Svg ByFile(string path)
        {
            SvgDocument doc = SvgDocument.Open(path);
            return new Svg(doc);
        }

        /// <summary>
        /// A series of options for saving the SVG document.
        /// </summary>
        /// <param name="viewbox">
        /// A rectangle defining the extend of the SVG document
        /// </param>
        /// <param name="scale">
        /// The scale between Dynamo units and SVG units
        /// </param>
        /// <returns>The options to be used for saving the SVG document.</returns>
        /// <search>
        /// options, option, save
        /// </search>
        public static Option Option(
            [DefaultArgument("DynaSVG.Svg.GetNull()")]Autodesk.DesignScript.Geometry.Rectangle viewbox,
            [DefaultArgument("DynaSVG.Svg.GetNull()")]double? scale)
        {
            return new Option(viewbox,scale);
        }

        private Svg(SvgDocument svgDocument)
        {
            InnerDocument = svgDocument;
        }

        [IsVisibleInDynamoLibrary(false)]
        public static object GetNull()
        {
            return null;
        }
    }

    [IsVisibleInDynamoLibrary(false)]
    public class Option
    {
        public Option(Autodesk.DesignScript.Geometry.Rectangle rectangle, double? scale)
        {
            double innerScale = scale ?? 1;

            if (rectangle != null)
            {
                BoundingBox boundingBox = rectangle.BoundingBox;
                viewbox = new SvgViewBox(
                    Convert.ToSingle(boundingBox.MinPoint.X * innerScale),
                    Convert.ToSingle(boundingBox.MinPoint.Y * innerScale),
                    Convert.ToSingle(rectangle.Width * innerScale),
                    Convert.ToSingle(rectangle.Height * innerScale));
            }
        }
        public SvgViewBox viewbox { get; set; }
    }
}

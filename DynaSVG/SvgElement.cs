using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.DesignScript.Runtime;
using Autodesk.DesignScript.Geometry;
// using System.Drawing;
using DSCore;

namespace DynaSVG
{
    public class SvgElement
    {
        /// <summary>
        /// Create a new SVG element from geometry
        /// </summary>
        /// <param name="geometry">
        /// Geometry to be converted to a SVG element.
        /// </param>
        /// <returns>The SVG element</returns>
        /// <search>
        /// geometry, svg, svgelement
        /// </search>
        public static SvgElement ByGeometry(Geometry geometry,
            [DefaultArgument("DynaSVG.Svg.GetNull()")]Style style,
            [DefaultArgument("DynaSVG.Svg.GetNull()")]Group group)
        {
            return new SvgElement();
        }

        /// <summary>
        /// Create a group of SVG element
        /// </summary>
        /// <param name="style">
        /// The style to be applied to all elements of the group
        /// </param>
        /// <returns>The new group.</returns>
        /// <search>
        /// group
        /// </search>
        public static Group Group([DefaultArgument("DynaSVG.Svg.GetNull()")]Style style)
        {
            return new Group(style);
        }

        /// <summary>
        /// Create a style for a SVG element.
        /// </summary>
        /// <param name="fill">Defines the color of the shape.</param>
        /// <param name="stroke">Defines the color used to paint the outline of the shape.</param>
        /// <param name="fillOpacity">Defines the opacity of the color applied to a shape.</param>
        /// <param name="strokeOpacity">Defines the opacity of the color applied to the stroke of a shape.</param>
        /// <param name="strokeWidth">Defines the width of the stroke to be applied to the shape.</param>
        /// <param name="strokeLinecap">Defines the shape to be used at the end of open subpaths when they are stroked.</param>
        /// <param name="strokeLinejoin">Defines the shape to be used at the corners of paths when they are stroked.</param>
        /// <param name="strokeDasharray">Defines the pattern of dashes and gaps used to paint the outline of the shape.</param>
        /// <returns>The new style.</returns>
        /// <search>
        /// style
        /// </search>
        public static Style Style(
            [DefaultArgument("DynaSVG.Svg.GetNull()")]Color fill,
[DefaultArgument("DynaSVG.Svg.GetNull()")]Color stroke,
[DefaultArgument("DynaSVG.Svg.GetNull()")]double? fillOpacity,
[DefaultArgument("DynaSVG.Svg.GetNull()")]double? strokeOpacity,
[DefaultArgument("DynaSVG.Svg.GetNull()")]double? strokeWidth,
[DefaultArgument("DynaSVG.Svg.GetNull()")]Linecap? strokeLinecap,
[DefaultArgument("DynaSVG.Svg.GetNull()")]Linejoin? strokeLinejoin,
[DefaultArgument("DynaSVG.Svg.GetNull()")]string strokeDasharray
)
        {
            return new Style();
        }

    }

    // [IsVisibleInDynamoLibrary(false)]
    public class Style
    {
        /// <summary>Defines the color of the shape.</summary>
        public Color Fill { get; set; }
        /// <summary>Defines the color used to paint the outline of the shape.</summary>
        public Color Stroke { get; set; }
        /// <summary>Defines the opacity of the color applied to a shape.</summary>
        public double FillOpacity { get; set; }
        /// <summary>Defines the opacity of the color applied to the stroke of a shape.</summary>
        public double StrokeOpacity { get; set; }
        /// <summary>Defines the width of the stroke to be applied to the shape.</summary>
        public string StrokeWidth { get; set; }
        /// <summary>Defines the shape to be used at the end of open subpaths when they are stroked.</summary>
        public Linecap StrokeLinecap { get; set; }
        /// <summary>Defines the shape to be used at the corners of paths when they are stroked.</summary>
        public Linejoin StrokeLinejoin { get; set; }
        /// <summary>Defines the pattern of dashes and gaps used to paint the outline of the shape.</summary>
        public string StrokeDasharray { get; set; }

    }

    [IsVisibleInDynamoLibrary(false)]
    public class Group
    {
        internal Guid id;
        internal Style Style;
        public Group(Style style)
        {
            id = Guid.NewGuid();
            Style = style;
        }
    }

    [IsVisibleInDynamoLibrary(false)]
    public enum Linecap { butt, round, square }

    [IsVisibleInDynamoLibrary(false)]
    public enum Linejoin { miter, round, bevel, miterclip, arcs }
}

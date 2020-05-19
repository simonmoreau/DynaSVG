﻿using System;
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
        /// <returns>A boolean indicating if the document have been saved.</returns>
        public static bool SaveAs(Svg svg, string path)
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
        public static Svg FromGeometry(Geometry[] geometry)
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
        public static Svg FromFile(string path)
        {
            SvgDocument doc = SvgDocument.Open(path);
            return new Svg(doc);
        }

        private Svg(SvgDocument svgDocument)
        {
            InnerDocument = svgDocument;
        }

    }
}

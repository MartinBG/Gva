using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OMR
{
    public class OMRReader : IDisposable
    {
        public OMRConfiguration ОMRConfig { get; private set; }

        private Bitmap leftBoundingImg;
        private Bitmap rightBoundingImg;

        public OMRReader(OMRConfiguration config)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using (var rc = assembly.GetManifestResourceStream("OMR.Resources.rc.jpg"))
            {
                this.rightBoundingImg = new Bitmap(rc);
            }

            using (var lc = assembly.GetManifestResourceStream("OMR.Resources.lc.jpg"))
            {
                this.leftBoundingImg = new Bitmap(lc);
            }

            this.ОMRConfig = config;
        }

        public Dictionary<string, List<List<bool>>> Read(Bitmap sourceImage)
        {
            Dictionary<string, List<List<bool>>> answers = new Dictionary<string, List<List<bool>>>();

            using (Bitmap extractedImg = FlattenAndExtractImage(sourceImage, 0, 0))
            {
                if (extractedImg == null)
                {
                    return null;
                }

                double ctrlDarkness;
                double averageInks;

                Rectangle linePattern = new Rectangle(ОMRConfig.AdjustmentBlock.X, ОMRConfig.AdjustmentBlock.Y, ОMRConfig.AdjustmentBlock.Width, ОMRConfig.AdjustmentBlock.Height);
                Rectangle whitePattern = new Rectangle(ОMRConfig.WhiteBlock.X, ОMRConfig.WhiteBlock.Y, ОMRConfig.WhiteBlock.Width, ОMRConfig.WhiteBlock.Height);
                using (Bitmap linePatternBmp = GetBmpFromRectangle(extractedImg, linePattern))
                using (Bitmap whitePatternBmp = GetBmpFromRectangle(extractedImg, whitePattern))
                {
                    ctrlDarkness = GetWhitePatternDarkness(whitePatternBmp, ОMRConfig.DarkFactor);
                    averageInks = GetSquareDarknesses(linePatternBmp, 4, ctrlDarkness).Average();
                }

                foreach (var item in ОMRConfig.Blocks)
                {
                    List<List<bool>> answersBlock = new List<List<bool>>();
                    answers.Add(item.Name, answersBlock);

                    Rectangle block = new Rectangle(item.X, item.Y, item.Width, item.Height);
                    foreach (var bmp in GetLineBitmaps(extractedImg, block, item.Slices))
                        using (bmp)
                        {
                            answersBlock.Add(RateSlice(bmp, 4, ctrlDarkness, averageInks, ОMRConfig.FillFactor));
                        }
                }
            }

            return answers;
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            try
            {
                if (disposing && this.leftBoundingImg != null && this.rightBoundingImg != null)
                {
                    using (this.leftBoundingImg)
                    using (this.rightBoundingImg)
                    {
                    }
                }
            }
            finally
            {
                this.leftBoundingImg = null;
                this.rightBoundingImg = null;
            }
        }

        /// <summary>
        /// Get a list of answers from a line block
        /// </summary>
        /// <param name="slice">Line block</param>
        /// <param name="blocks">Number of square blocks</param>
        /// <param name="ctrlDarkness">Control darkness to compare</param>
        /// <param name="averageInks">Average ink darkness</param>
        /// <returns>List of selected answers</returns>
        private List<bool> RateSlice(Bitmap slice, int blocks, double ctrlDarkness, double averageInks, double fillFactor)
        {
            List<bool> answers = new List<bool>();
            long[] squareDarknesses = GetSquareDarknesses(slice, blocks, ctrlDarkness);

            for (int i = 0; i < blocks; i++)
            {
                if ((squareDarknesses[i] > fillFactor * averageInks))
                {
                    answers.Add(true);
                }
                else
                {
                    answers.Add(false);
                }
            }

            return answers;
        }

        /// <summary>
        /// Get bitmap square blocks from image
        /// </summary>
        /// <param name="source">Source image</param>
        /// <param name="blocks">Number of square blocks</param>
        /// <returns>Bitmap square blocks</returns>
        private long[] GetSquareDarknesses(Bitmap source, int blocks, double ctrlDarkness)
        {
            Rectangle[] cropRects = new Rectangle[blocks];
            long[] squareDarknesses = new long[blocks];

            for (int i = 0; i < blocks; i++)
            {
                cropRects[i] = new Rectangle(i * source.Width / blocks, 0, source.Width / blocks, source.Height);
            }

            int crsr = 0;
            foreach (Rectangle cropRect in cropRects)
            {
                using (Bitmap squareBlock = GetBmpFromRectangle(source, cropRect))
                {
                    squareDarknesses[crsr] = SquareDarkness(squareBlock, ctrlDarkness);
                    crsr++;
                }
            }

            return squareDarknesses;
        }

        /// <summary>
        /// Get bitmap image from rectangle coords
        /// </summary>
        /// <param name="source">Source image</param>
        /// <param name="rect">Rectangle coords</param>
        /// <returns>Bitmap image</returns>
        private Bitmap GetBmpFromRectangle(Bitmap source, Rectangle rect)
        {
            Bitmap bmp = new Bitmap(rect.Width, rect.Height);
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.DrawImage(source, new Rectangle(0, 0, bmp.Width, bmp.Height), rect, GraphicsUnit.Pixel);
            }

            return bmp;
        }

        /// <summary>
        /// Get square block ink darkness
        /// </summary>
        /// <param name="square">Source bitmap</param>
        /// <param name="ctrlDarkness">Control darkness to compare</param>
        /// <returns>Square block ink darkness</returns>
        private long SquareDarkness(Bitmap square, double ctrlDarkness)
        {
            long dc = 0;
            for (int y = 0; y < square.Height; y++)
            {
                for (int x = 0; x < square.Width; x++)
                {
                    Color c = square.GetPixel(x, y);

                    if (((c.R + c.G + c.B) / 3) < ctrlDarkness)
                    {
                        dc += 255;
                    }
                }
            }

            return dc;
        }

        /// <summary>
        /// Get white square block darkest pixel with factor
        /// </summary>
        /// <param name="pattern">Source bitmap</param>
        /// <param name="factor">Factor to apply to darkest pixel</param>
        /// <returns>White square's block darkest pixel</returns>
        private double GetWhitePatternDarkness(Bitmap pattern, double factor)
        {
            int darkest = 255;
            for (int y = 0; y < pattern.Height; y++)
            {
                for (int x = 0; x < pattern.Width; x++)
                {
                    Color c = pattern.GetPixel(x, y);
                    if (((c.R + c.G + c.B) / 3) < darkest)
                    {
                        darkest = ((c.R + c.G + c.B) / 3);
                    }
                }
            }

            return darkest * factor;
        }

        /// <summary>
        /// Get bitmap line blocks from image
        /// </summary>
        /// <param name="fullSheet">Source image</param>
        /// <param name="slicer">Rectangle coordinates</param>
        /// <param name="slices">Slices count</param>
        /// <returns>Bitmap line blocks</returns>
        private IEnumerable<Bitmap> GetLineBitmaps(System.Drawing.Image fullSheet, Rectangle slicer, int slices)
        {
            Bitmap src = (Bitmap)fullSheet;
            Rectangle[] cropRects = new Rectangle[slices];

            for (int i = 0; i < slices; i++)
            {
                cropRects[i] = new Rectangle(slicer.X, slicer.Y + (slicer.Height / slices) * i, slicer.Width, slicer.Height / slices);
            }

            foreach (Rectangle cropRect in cropRects)
            {
                Bitmap lineBlock = GetBmpFromRectangle(src, cropRect);
                yield return lineBlock;
            }
        }

        /// <summary>
        /// Check if two images are the same
        /// </summary>
        /// <param name="img1">First image</param>
        /// <param name="img2">Second image</param>
        /// <returns></returns>
        private bool isSame(UnmanagedImage img1, Bitmap img2)
        {
            int count = 0, tcount = img2.Width * img2.Height;
            for (int y = 0; y < img1.Height; y++)
                for (int x = 0; x < img1.Width; x++)
                {
                    Color c1 = img1.GetPixel(x, y), c2 = img2.GetPixel(x, y);
                    if ((c1.R + c1.G + c1.B) / 3 > (c2.R + c2.G + c2.B) / 3 - 10 &&
                        (c1.R + c1.G + c1.B) / 3 < (c2.R + c2.G + c2.B) / 3 + 10)
                        count++;
                }
            return (count * 100) / tcount >= 54;
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            Bitmap result = new Bitmap(width, height);
            // set the resolutions the same to avoid cropping due to resolution differences
            result.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            //use a graphics object to draw the resized image into the bitmap
            using (Graphics graphics = Graphics.FromImage(result))
            {
                //set the resize quality modes to high quality
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                //draw the image into the target bitmap
                graphics.DrawImage(image, 0, 0, result.Width, result.Height);
            }

            //return the resulting bitmap
            return result;
        }

        private Bitmap Flatten(Bitmap bmp, int fillint, int contint)
        {
            ColorFiltering colorFilter = new ColorFiltering();

            colorFilter.Red = new IntRange(0, fillint);
            colorFilter.Green = new IntRange(0, fillint);
            colorFilter.Blue = new IntRange(0, fillint);
            colorFilter.FillOutsideRange = false;
            using (Bitmap filteredBmp = colorFilter.Apply(bmp))
            {
                AForge.Imaging.Filters.ContrastCorrection Contrast = new ContrastCorrection(contint);
                AForge.Imaging.Filters.Invert invert = new Invert();
                AForge.Imaging.Filters.ExtractChannel extract_channel = new ExtractChannel(RGB.B);
                AForge.Imaging.Filters.Threshold thresh_hold = new Threshold(44);

                Contrast.ApplyInPlace(filteredBmp);

                Bitmap extractedBmp = extract_channel.Apply(filteredBmp);
                thresh_hold.ApplyInPlace(extractedBmp);
                invert.ApplyInPlace(extractedBmp);

                return extractedBmp;
            }
        }

        private Bitmap FlattenAndExtractImage(Bitmap originalImg, int fillint, int contint)
        {
            Bitmap extractedImg;

            using (Bitmap flattened = Flatten(originalImg, fillint, contint))
            {
                extractedImg = ExtractImage(flattened, originalImg, fillint, contint);
            }

            if (extractedImg != null)
            {
                return extractedImg;
            }
            else
            {
                //try altering the contrast correction on both sides of numberline
                if (contint <= 60)
                {
                    if (contint >= 0)
                    {
                        contint += 5;
                        contint *= -1;
                        return FlattenAndExtractImage(originalImg, fillint, contint);
                    }
                    else
                    {
                        contint *= -1;
                        contint += 10;
                        return FlattenAndExtractImage(originalImg, fillint, contint);
                    }
                }

                return null;
            }
        }

        private Bitmap ExtractImage(Bitmap bitmap, Bitmap originalImg, int fillint, int contint)
        {
            int bmpWidth = bitmap.Width;
            int bmpHeight = bitmap.Height;

            // lock image, Bitmap itself takes much time to be processed
            BitmapData bitmapData = bitmap.LockBits(
                new Rectangle(0, 0, bmpWidth, bmpHeight),
                ImageLockMode.ReadWrite,
                bitmap.PixelFormat);

            // Store sheet corner locations (if anyone is detected)
            List<IntPoint> quad = new List<IntPoint>();
            try
            {
                // step 2 - locating objects
                BlobCounter blobCounter = new BlobCounter();
                blobCounter.FilterBlobs = true;
                blobCounter.MinHeight = 25;  // both these variables have to be given when calling the
                blobCounter.MinWidth = 25;   // method, the can also be queried from the XML reader using OMREnums

                UnmanagedImage unmanagedImage = new UnmanagedImage(bitmapData);

                blobCounter.ProcessImage(unmanagedImage);
                AForge.Imaging.Blob[] blobs = blobCounter.GetObjects(unmanagedImage, false);

                // this helps filtering out much smaller and much larger blobs depending upon the size of image.
                double minbr = 0.0001;
                double maxbr = 0.005;

                // Detect left edge.
                foreach (AForge.Imaging.Blob blob in blobs)
                {
                    // filters out very small or very larg blobs
                    if (((double)blob.Area) / ((double)bmpWidth * bmpHeight) > minbr &&
                        ((double)blob.Area) / ((double)bmpWidth * bmpHeight) < maxbr &&
                            blob.Rectangle.X < (bmpWidth) / 4)
                    {
                        // filters out blobs having insanely wrong aspect ratio
                        if ((double)blob.Rectangle.Width / blob.Rectangle.Height < 1.4 &&
                            (double)blob.Rectangle.Width / blob.Rectangle.Height > 0.6)
                        {
                            using (Bitmap leftBoundingResized = ResizeImage(this.leftBoundingImg, blob.Rectangle.Width, blob.Rectangle.Height))
                            {
                                if (isSame(blob.Image, leftBoundingResized))
                                {
                                    quad.Add(new IntPoint((int)blob.CenterOfGravity.X, (int)blob.CenterOfGravity.Y));
                                }
                            }
                        }
                    }
                }

                // Sort out the list in right sequence, UpperLeft, LowerLeft, LowerRight, UpperRight
                if (quad.Count >= 2)
                {
                    if (quad[0].Y > quad[1].Y)
                    {
                        IntPoint tp = quad[0];
                        quad[0] = quad[1];
                        quad[1] = tp;
                    }
                }

                // Detect right edge.
                foreach (AForge.Imaging.Blob blob in blobs)
                {
                    if (
                        ((double)blob.Area) / ((double)bmpWidth * bmpHeight) > minbr &&
                        ((double)blob.Area) / ((double)bmpWidth * bmpHeight) < maxbr &&
                        blob.Rectangle.X > (bmpWidth * 3) / 4)
                    {
                        if ((double)blob.Rectangle.Width / blob.Rectangle.Height < 1.4 &&
                            (double)blob.Rectangle.Width / blob.Rectangle.Height > 0.6)
                        {
                            using (Bitmap rightBoundingResized = ResizeImage(this.rightBoundingImg, blob.Rectangle.Width, blob.Rectangle.Height))
                            {
                                if (isSame(blob.Image, rightBoundingResized))
                                {
                                    quad.Add(new IntPoint((int)blob.CenterOfGravity.X, (int)blob.CenterOfGravity.Y));
                                }
                            }
                        }
                    }
                }

                if (quad.Count >= 4)
                {
                    if (quad[2].Y < quad[3].Y)
                    {
                        IntPoint tp = quad[2];
                        quad[2] = quad[3];
                        quad[3] = tp;
                    }
                }
            }
            finally
            {
                bitmap.UnlockBits(bitmapData);
            }

            //Again, filter out if wrong blobs pretended to our blobs.
            if (quad.Count == 4)
            {
                // clear if, both edges have insanely wrong lengths
                if (((double)quad[1].Y - (double)quad[0].Y) / ((double)quad[2].Y - (double)quad[3].Y) < 0.75 ||
                    ((double)quad[1].Y - (double)quad[0].Y) / ((double)quad[2].Y - (double)quad[3].Y) > 1.25)
                {
                    quad.Clear();
                }
                // clear if, sides appear to be "wrong sided"
                else if (quad[0].X > bmpWidth / 2 || quad[1].X > bmpWidth / 2 || quad[2].X < bmpWidth / 2 || quad[3].X < bmpWidth / 2)
                {
                    quad.Clear();
                }
            }

            // sheet found
            if (quad.Count == 4)
            {
                IntPoint tp2 = quad[3];
                quad[3] = quad[1];
                quad[1] = tp2;
                //sort the edges for wrap operation
                QuadrilateralTransformation wrap = new QuadrilateralTransformation(quad);
                wrap.UseInterpolation = false; //perspective wrap only, no binary.
                wrap.AutomaticSizeCalculaton = false;
                wrap.NewWidth = this.ОMRConfig.ImageWidth;
                wrap.NewHeight = this.ОMRConfig.ImageHeight;
                return wrap.Apply(originalImg);
            }
            else
            {
                return null;
            }
        }
    }
}

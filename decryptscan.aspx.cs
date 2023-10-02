using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Drawing.Imaging;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Drawing;

public partial class decryptscan : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
            Bitmap xorimg = new Bitmap(Server.MapPath("/check/FinalImage2.bmp"));
            Bitmap img4 =  new Bitmap(Server.MapPath("/check/FinalImage1.bmp"));
            Graphics g = Graphics.FromImage(img4);
            //g.Clear(SystemColors.AppWorkspace);
            //string dexorImage = @"C:/Users/Aditi/Documents/Visual Studio 2010/Projects/Imagedit/Imagedit/images/dexorimage.bmp";
            img4 = XORing(xorimg);

            img4.Save(Server.MapPath("images/FinalImage1.bmp"), System.Drawing.Imaging.ImageFormat.Bmp);
            img4.Dispose();
            xorimg.Dispose();
            g.Dispose();

           String path = "images/FinalImage1.bmp"; 
            System.Drawing.Image inputImg = System.Drawing.Image.FromFile(Server.MapPath(path));
            int imgWidth = Convert.ToInt32(inputImg.Width);
            int imgHeight = Convert.ToInt32(inputImg.Height);
            Response.Write("imagewidth:" + imgWidth + "imageheight:" + imgHeight);
            int cropWidth = (int)Math.Ceiling((imgWidth * 1.00) / (8 * 1.00));
            int cropHeight = (int)Math.Ceiling((imgHeight * 1.00) / (8 * 1.00));
            Response.Write("imagecropwidth:" + cropWidth + "imagecropheight:" + cropHeight);


            String _fileExtension = System.IO.Path.GetExtension(path);
            int widthCount = (int)Math.Ceiling((imgWidth * 1.00) / (cropWidth * 1.00));
            int heightCount = (int)Math.Ceiling((imgHeight * 1.00) / (cropHeight * 1.00));
            ArrayList areaList = new ArrayList();


            for (int iHeight = 0; iHeight < heightCount; iHeight++)
            {
                for (int iWidth = 0; iWidth < widthCount; iWidth++)
                {
                    int pointX = iWidth * cropWidth;
                    int pointY = iHeight * cropHeight;



                    Rectangle rect = new Rectangle(pointX, pointY, cropWidth, cropHeight);
                    areaList.Add(rect);

                }
            }
            long key = 5868421244169253;
            long n = 10;
            long pattern, num;
            for (int i = 0; i < 8; i++)
            {
                num = key % n;
                key = key / n;
                //n = n * 10;
                if (num> 7) { num = 7; }
                Response.Write("num" + num);
                pattern = key % n;
                key = key / n;
                //n = n *10;
                if (pattern > 7) { pattern = 7; }
                Response.Write("pattern" + pattern);
                switch (pattern)
                {
                    case 0: for (int j = 0; j < num; j++) { areaList = DeZigZag(areaList); } break;
                    case 1: for (int j = 0; j < num; j++) { areaList = DeSpiral(areaList); } break;
                    case 2: for (int j = 0; j < num; j++) { areaList = DeFour(areaList); } break;
                    case 3: for (int j = 0; j < num; j++) { areaList = DeTwo(areaList); } break;
                    case 4: for (int j = 0; j < num; j++) { areaList = DeReSpiral(areaList); } break;
                    case 5: for (int j = 0; j < num; j++) { areaList = DeRangoli(areaList); } break;
                    case 6: for (int j = 0; j < num; j++) { areaList = DeOne(areaList); } break;
                    case 7: for (int j = 0; j < num; j++) { areaList = DeThree(areaList); } break;

                }
            }
            int jloop = 10;
            for (int iLoop = 0; iLoop < areaList.Count; iLoop++)
            {
                Rectangle rect = (Rectangle)areaList[iLoop];

                  string fileName = "/check1/" + jloop.ToString() + ".bmp";
                Bitmap newBmp = new Bitmap(rect.Width, rect.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
                Graphics newBmpGraphics = Graphics.FromImage(newBmp);
                newBmpGraphics.DrawImage(inputImg, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);
                newBmpGraphics.Save();
                switch (_fileExtension.ToLower())
                {
                    case ".jpg":
                    case ".jpeg":
                        newBmp.Save(Server.MapPath(fileName), System.Drawing.Imaging.ImageFormat.Jpeg);
                        break;
                    case "gif":
                        newBmp.Save(Server.MapPath(fileName), System.Drawing.Imaging.ImageFormat.Gif);
                        break;
                    case ".bmp":
                        newBmp.Save(Server.MapPath(fileName), System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
                }
                jloop++;

            }
             DirectoryInfo directory = new DirectoryInfo(Server.MapPath("/check1/"));
            if (directory != null)
            {
                FileInfo[] files = directory.GetFiles();
                CombineImages(files);
            }
        }
        public Bitmap XORing(Bitmap bmp)
        {
              Bitmap bitmap = new Bitmap(Server.MapPath("images/imagedit1.bmp"));
            //e.Graphics.DrawImage(bitmap, 60, 10);
            BitmapData bmpData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData bmpData2 = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            int width = bmpData2.Width;
            int height = bmpData2.Height;
            /*
            if (bmpData2.Width > width)
                width = bmpData2.Width;
            if (bmpData2.Height > height)
                height = bmpData2.Height;
            */
            bitmap.UnlockBits(bmpData);
            bmp.UnlockBits(bmpData2);

            Bitmap bit1 = new Bitmap(bitmap, width, height);
            Bitmap bit2 = new Bitmap(bmp, width, height);

            Bitmap bmpresult = new Bitmap(width, height);

            BitmapData data1 = bit1.LockBits(new Rectangle(0, 0, bit1.Width, bit1.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData data2 = bit2.LockBits(new Rectangle(0, 0, bit2.Width, bit2.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            BitmapData data3 = bmpresult.LockBits(new Rectangle(0, 0, bmpresult.Width, bmpresult.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            unsafe
            {
                int remain1 = data1.Stride - data1.Width * 3;
                int remain2 = data2.Stride - data2.Width * 3;
                int remain3 = data3.Stride - data3.Width * 3;


                byte* ptr1 = (byte*)data1.Scan0;
                byte* ptr2 = (byte*)data2.Scan0;
                byte* ptr3 = (byte*)data3.Scan0;

                for (int i = 0; i < height; i++)
                {
                    for (int j = 0; j < width * 3; j++)
                    {
                        ptr3[0] = (byte)(XOR_Operator(ptr1[0], ptr2[0]));
                        ptr1++;
                        ptr2++;
                        ptr3++;
                    }

                    ptr1 += remain1;
                    ptr2 += remain2;
                    ptr3 += remain3;
                }


            }

            bit1.UnlockBits(data1);
            bit2.UnlockBits(data2);
            bmpresult.UnlockBits(data3);

            return bmpresult;
        }

        public byte XOR_Operator(byte a, byte b)
        {

            byte A = (byte)(255 - a);
            byte B = (byte)(255 - b);

            return (byte)((a & B) | (A & b));
        }
        private void CombineImages(FileInfo[] files)
        {
            //change the location to store the final image.
             string finalImage = "/check1/FinalImage1.bmp";
            //List<int> imageHeights = new List<int>();
            //List<int> imageWidth = new List<int>();
            int nWidth = 0;
            int nHeight = 0;
            int height = 0;
            int width = 0;
            foreach (FileInfo file in files)
            {
                System.Drawing.Image img = System.Drawing.Image.FromFile(file.FullName);
                //imageHeights.Add(Convert.ToInt32(img.Height));
                //imageWidth.Add(Convert.ToInt32(img.Width));

                height = Convert.ToInt32(img.Height);
                width = Convert.ToInt32(img.Width);

                //img.Dispose();
            }
            Response.Write("width=" + width + " Hwight=" + height + "   ");
            //Response.Write(width * 8 + " ");
            //Response.Write(height * 8);
            //imageHeights.Sort();
            //imageWidth.Sort();
            //height = imageHeights[imageHeights.Count - 1];
            //width = imageWidth[imageWidth.Count - 1];
            Response.Write("        width=" + width + " Hwight=" + height + "   ");
            width = width * 8;
            int heightnew = height * 8;
            Response.Write("        width=" + width + " Hwight=" + height + "   ");
            try
            {
                Bitmap img3 = new Bitmap(width, heightnew);
                Graphics g = Graphics.FromImage(img3);
                g.Clear(SystemColors.AppWorkspace);



                int i = 0;
                foreach (FileInfo file in files)
                {
                    System.Drawing.Image img = System.Drawing.Image.FromFile(file.FullName);
                    i++;
                    g.DrawImage(img, new Point(nWidth, nHeight));

                    nWidth = nWidth + img.Width;
                    if (i == 8)
                    {
                        nWidth = 0;
                        nHeight = nHeight + img.Height;
                        i = 0;
                    }
                    //img.Dispose();
                }




                g.Dispose();
                img3.Save(Server.MapPath(finalImage), System.Drawing.Imaging.ImageFormat.Bmp);
                img3.Dispose();
                //imageLocation.Image = SystemImage.FromFile(finalImage);
            }
            catch (Exception e) { Response.Write(e);}
        }
    private ArrayList DeZigZag(ArrayList givenArray)
    {
        //throw new NotImplementedException();
        ArrayList result = new ArrayList(64);
        // Rectangle rect = (Rectangle)givenArray[0];
        result.Add(givenArray[0]);
        result.Add(givenArray[1]);
        result.Add(givenArray[8]);
        result.Add(givenArray[16]);
        result.Add(givenArray[9]);
        result.Add(givenArray[2]);
        result.Add(givenArray[3]);
        result.Add(givenArray[10]);
        result.Add(givenArray[17]);
        result.Add(givenArray[24]);
        result.Add(givenArray[32]);
        result.Add(givenArray[25]);
        result.Add(givenArray[18]);
        result.Add(givenArray[11]);
        result.Add(givenArray[4]);
        result.Add(givenArray[5]);
        result.Add(givenArray[12]);
        result.Add(givenArray[19]);
        result.Add(givenArray[26]);
        result.Add(givenArray[33]);
        result.Add(givenArray[40]);
        result.Add(givenArray[48]);
        result.Add(givenArray[41]);
        result.Add(givenArray[34]);
        result.Add(givenArray[27]);
        result.Add(givenArray[20]);
        result.Add(givenArray[13]);
        result.Add(givenArray[6]);
        result.Add(givenArray[7]);
        result.Add(givenArray[14]);
        result.Add(givenArray[21]);
        result.Add(givenArray[28]);
        result.Add(givenArray[35]);
        result.Add(givenArray[42]);
        result.Add(givenArray[49]);
        result.Add(givenArray[56]);
        result.Add(givenArray[57]);
        result.Add(givenArray[50]);
        result.Add(givenArray[43]);
        result.Add(givenArray[36]);
        result.Add(givenArray[29]);
        result.Add(givenArray[22]);
        result.Add(givenArray[15]);
        result.Add(givenArray[23]);
        result.Add(givenArray[30]);
        result.Add(givenArray[37]);
        result.Add(givenArray[44]);
        result.Add(givenArray[51]);
        result.Add(givenArray[58]);
        result.Add(givenArray[59]);
        result.Add(givenArray[52]);
        result.Add(givenArray[45]);
        result.Add(givenArray[38]);
        result.Add(givenArray[31]);
        result.Add(givenArray[39]);
        result.Add(givenArray[46]);
        result.Add(givenArray[53]);
        result.Add(givenArray[60]);
        result.Add(givenArray[61]);
        result.Add(givenArray[54]);
        result.Add(givenArray[47]);
        result.Add(givenArray[55]);
        result.Add(givenArray[62]);
        result.Add(givenArray[63]);
        return result;
    }
    private ArrayList DeSpiral(ArrayList givenArray)
    {
        //throw new NotImplementedException();
        ArrayList result = new ArrayList(64);
        // Rectangle rect = (Rectangle)givenArray[0];
        result.Add(givenArray[0]);
        result.Add(givenArray[1]);
        result.Add(givenArray[2]);
        result.Add(givenArray[3]);
        result.Add(givenArray[4]);
        result.Add(givenArray[5]);
        result.Add(givenArray[6]);
        result.Add(givenArray[7]);
        result.Add(givenArray[15]);
        result.Add(givenArray[23]);
        result.Add(givenArray[31]);
        result.Add(givenArray[39]);
        result.Add(givenArray[47]);
        result.Add(givenArray[55]);
        result.Add(givenArray[63]);
        result.Add(givenArray[62]);
        result.Add(givenArray[61]);
        result.Add(givenArray[60]);
        result.Add(givenArray[59]);
        result.Add(givenArray[58]);
        result.Add(givenArray[57]);
        result.Add(givenArray[56]);
        result.Add(givenArray[48]);
        result.Add(givenArray[40]);
        result.Add(givenArray[32]);
        result.Add(givenArray[24]);
        result.Add(givenArray[16]);
        result.Add(givenArray[8]);
        result.Add(givenArray[9]);
        result.Add(givenArray[10]);
        result.Add(givenArray[11]);
        result.Add(givenArray[12]);
        result.Add(givenArray[13]);
        result.Add(givenArray[14]);
        result.Add(givenArray[22]);
        result.Add(givenArray[30]);
        result.Add(givenArray[38]);
        result.Add(givenArray[46]);
        result.Add(givenArray[54]);
        result.Add(givenArray[53]);
        result.Add(givenArray[52]);
        result.Add(givenArray[51]);
        result.Add(givenArray[50]);
        result.Add(givenArray[49]);
        result.Add(givenArray[41]);
        result.Add(givenArray[33]);
        result.Add(givenArray[25]);
        result.Add(givenArray[17]);
        result.Add(givenArray[18]);
        result.Add(givenArray[19]);
        result.Add(givenArray[20]);
        result.Add(givenArray[21]);
        result.Add(givenArray[29]);
        result.Add(givenArray[37]);
        result.Add(givenArray[45]);
        result.Add(givenArray[44]);
        result.Add(givenArray[43]);
        result.Add(givenArray[42]);
        result.Add(givenArray[34]);
        result.Add(givenArray[26]);
        result.Add(givenArray[27]);
        result.Add(givenArray[28]);
        result.Add(givenArray[36]);
        result.Add(givenArray[35]);
        return result;
    }
    private ArrayList DeFour(ArrayList givenArray)
    {
        //throw new NotImplementedException();
        ArrayList result = new ArrayList(64);
        // Rectangle rect = (Rectangle)givenArray[0];
        result.Add(givenArray[0]);
        result.Add(givenArray[1]);
        result.Add(givenArray[9]);
        result.Add(givenArray[8]);
        result.Add(givenArray[16]);
        result.Add(givenArray[17]);
        result.Add(givenArray[18]);
        result.Add(givenArray[10]);
        result.Add(givenArray[2]);
        result.Add(givenArray[3]);
        result.Add(givenArray[11]);
        result.Add(givenArray[19]);
        result.Add(givenArray[27]);
        result.Add(givenArray[26]);
        result.Add(givenArray[25]);
        result.Add(givenArray[24]);
        result.Add(givenArray[32]);
        result.Add(givenArray[33]);
        result.Add(givenArray[34]);
        result.Add(givenArray[35]);
        result.Add(givenArray[36]);
        result.Add(givenArray[28]);
        result.Add(givenArray[20]);
        result.Add(givenArray[12]);
        result.Add(givenArray[4]);
        result.Add(givenArray[5]);
        result.Add(givenArray[13]);
        result.Add(givenArray[21]);
        result.Add(givenArray[29]);
        result.Add(givenArray[37]);
        result.Add(givenArray[45]);
        result.Add(givenArray[44]);
        result.Add(givenArray[43]);
        result.Add(givenArray[42]);
        result.Add(givenArray[41]);
        result.Add(givenArray[40]);
        result.Add(givenArray[48]);
        result.Add(givenArray[49]);
        result.Add(givenArray[50]);
        result.Add(givenArray[51]);
        result.Add(givenArray[52]);
        result.Add(givenArray[53]);
        result.Add(givenArray[54]);
        result.Add(givenArray[46]);
        result.Add(givenArray[38]);
        result.Add(givenArray[30]);
        result.Add(givenArray[22]);
        result.Add(givenArray[14]);
        result.Add(givenArray[6]);
        result.Add(givenArray[7]);
        result.Add(givenArray[15]);
        result.Add(givenArray[23]);
        result.Add(givenArray[31]);
        result.Add(givenArray[39]);
        result.Add(givenArray[47]);
        result.Add(givenArray[55]);
        result.Add(givenArray[63]);
        result.Add(givenArray[62]);
        result.Add(givenArray[61]);
        result.Add(givenArray[60]);
        result.Add(givenArray[59]);
        result.Add(givenArray[58]);
        result.Add(givenArray[57]);
        result.Add(givenArray[56]);
        return result;
    }
    private ArrayList DeTwo(ArrayList givenArray)
    {
        //throw new NotImplementedException();
        ArrayList result = new ArrayList(64);
        // Rectangle rect = (Rectangle)givenArray[0];
        result.Add(givenArray[7]);
        result.Add(givenArray[6]);
        result.Add(givenArray[5]);
        result.Add(givenArray[4]);
        result.Add(givenArray[3]);
        result.Add(givenArray[2]);
        result.Add(givenArray[1]);
        result.Add(givenArray[0]);
        result.Add(givenArray[15]);
        result.Add(givenArray[14]);
        result.Add(givenArray[13]);
        result.Add(givenArray[12]);
        result.Add(givenArray[11]);
        result.Add(givenArray[10]);
        result.Add(givenArray[9]);
        result.Add(givenArray[8]);
        result.Add(givenArray[23]);
        result.Add(givenArray[22]);
        result.Add(givenArray[21]);
        result.Add(givenArray[20]);
        result.Add(givenArray[19]);
        result.Add(givenArray[18]);
        result.Add(givenArray[17]);
        result.Add(givenArray[16]);
        result.Add(givenArray[31]);
        result.Add(givenArray[30]);
        result.Add(givenArray[29]);
        result.Add(givenArray[28]);
        result.Add(givenArray[27]);
        result.Add(givenArray[26]);
        result.Add(givenArray[25]);
        result.Add(givenArray[24]);
        result.Add(givenArray[39]);
        result.Add(givenArray[38]);
        result.Add(givenArray[37]);
        result.Add(givenArray[36]);
        result.Add(givenArray[35]);
        result.Add(givenArray[34]);
        result.Add(givenArray[33]);
        result.Add(givenArray[32]);
        result.Add(givenArray[47]);
        result.Add(givenArray[46]);
        result.Add(givenArray[45]);
        result.Add(givenArray[44]);
        result.Add(givenArray[43]);
        result.Add(givenArray[42]);
        result.Add(givenArray[41]);
        result.Add(givenArray[40]);
        result.Add(givenArray[55]);
        result.Add(givenArray[54]);
        result.Add(givenArray[53]);
        result.Add(givenArray[52]);
        result.Add(givenArray[51]);
        result.Add(givenArray[50]);
        result.Add(givenArray[49]);
        result.Add(givenArray[48]);
        result.Add(givenArray[63]);
        result.Add(givenArray[62]);
        result.Add(givenArray[61]);
        result.Add(givenArray[60]);
        result.Add(givenArray[59]);
        result.Add(givenArray[58]);
        result.Add(givenArray[57]);
        result.Add(givenArray[56]);
        return result;
    }
    private ArrayList DeReSpiral(ArrayList givenArray)
    {
        //throw new NotImplementedException();
        ArrayList result = new ArrayList(64);
        // Rectangle rect = (Rectangle)givenArray[0];
        result.Add(givenArray[35]);
        result.Add(givenArray[36]);
        result.Add(givenArray[28]);
        result.Add(givenArray[27]);
        result.Add(givenArray[26]);
        result.Add(givenArray[34]);
        result.Add(givenArray[42]);
        result.Add(givenArray[43]);
        result.Add(givenArray[44]);
        result.Add(givenArray[45]);
        result.Add(givenArray[37]);
        result.Add(givenArray[29]);
        result.Add(givenArray[21]);
        result.Add(givenArray[20]);
        result.Add(givenArray[19]);
        result.Add(givenArray[18]);
        result.Add(givenArray[17]);
        result.Add(givenArray[25]);
        result.Add(givenArray[33]);
        result.Add(givenArray[41]);
        result.Add(givenArray[49]);
        result.Add(givenArray[50]);
        result.Add(givenArray[51]);
        result.Add(givenArray[52]);
        result.Add(givenArray[53]);
        result.Add(givenArray[54]);
        result.Add(givenArray[46]);
        result.Add(givenArray[38]);
        result.Add(givenArray[30]);
        result.Add(givenArray[22]);
        result.Add(givenArray[14]);
        result.Add(givenArray[13]);
        result.Add(givenArray[12]);
        result.Add(givenArray[11]);
        result.Add(givenArray[10]);
        result.Add(givenArray[9]);
        result.Add(givenArray[8]);
        result.Add(givenArray[16]);
        result.Add(givenArray[24]);
        result.Add(givenArray[32]);
        result.Add(givenArray[40]);
        result.Add(givenArray[48]);
        result.Add(givenArray[56]);
        result.Add(givenArray[57]);
        result.Add(givenArray[58]);
        result.Add(givenArray[59]);
        result.Add(givenArray[60]);
        result.Add(givenArray[61]);
        result.Add(givenArray[62]);
        result.Add(givenArray[63]);
        result.Add(givenArray[55]);
        result.Add(givenArray[47]);
        result.Add(givenArray[39]);
        result.Add(givenArray[31]);
        result.Add(givenArray[23]);
        result.Add(givenArray[15]);
        result.Add(givenArray[7]);
        result.Add(givenArray[6]);
        result.Add(givenArray[5]);
        result.Add(givenArray[4]);
        result.Add(givenArray[3]);
        result.Add(givenArray[2]);
        result.Add(givenArray[1]);
        result.Add(givenArray[0]);
        return result;
    }
    private ArrayList DeRangoli(ArrayList givenArray)
    {
        //throw new NotImplementedException();
        ArrayList result = new ArrayList(64);
        // Rectangle rect = (Rectangle)givenArray[0];
        result.Add(givenArray[0]);
        result.Add(givenArray[8]);
        result.Add(givenArray[1]);
        result.Add(givenArray[9]);
        result.Add(givenArray[2]);
        result.Add(givenArray[10]);
        result.Add(givenArray[3]);
        result.Add(givenArray[11]);
        result.Add(givenArray[4]);
        result.Add(givenArray[12]);
        result.Add(givenArray[5]);
        result.Add(givenArray[13]);
        result.Add(givenArray[6]);
        result.Add(givenArray[14]);
        result.Add(givenArray[7]);
        result.Add(givenArray[15]);
        result.Add(givenArray[23]);
        result.Add(givenArray[31]);
        result.Add(givenArray[22]);
        result.Add(givenArray[30]);
        result.Add(givenArray[21]);
        result.Add(givenArray[29]);
        result.Add(givenArray[20]);
        result.Add(givenArray[28]);
        result.Add(givenArray[19]);
        result.Add(givenArray[27]);
        result.Add(givenArray[18]);
        result.Add(givenArray[26]);
        result.Add(givenArray[17]);
        result.Add(givenArray[25]);
        result.Add(givenArray[16]);
        result.Add(givenArray[24]);
        result.Add(givenArray[32]);
        result.Add(givenArray[40]);
        result.Add(givenArray[33]);
        result.Add(givenArray[41]);
        result.Add(givenArray[34]);
        result.Add(givenArray[42]);
        result.Add(givenArray[35]);
        result.Add(givenArray[43]);
        result.Add(givenArray[36]);
        result.Add(givenArray[44]);
        result.Add(givenArray[37]);
        result.Add(givenArray[45]);
        result.Add(givenArray[38]);
        result.Add(givenArray[46]);
        result.Add(givenArray[39]);
        result.Add(givenArray[47]);
        result.Add(givenArray[55]);
        result.Add(givenArray[63]);
        result.Add(givenArray[54]);
        result.Add(givenArray[62]);
        result.Add(givenArray[53]);
        result.Add(givenArray[61]);
        result.Add(givenArray[52]);
        result.Add(givenArray[60]);
        result.Add(givenArray[51]);
        result.Add(givenArray[59]);
        result.Add(givenArray[50]);
        result.Add(givenArray[58]);
        result.Add(givenArray[49]);
        result.Add(givenArray[57]);
        result.Add(givenArray[48]);
        result.Add(givenArray[56]);
        return result;
    }
    private ArrayList DeOne(ArrayList givenArray)
    {
        //throw new NotImplementedException();
        ArrayList result = new ArrayList(64);
        // Rectangle rect = (Rectangle)givenArray[0];
        result.Add(givenArray[0]);
        result.Add(givenArray[1]);
        result.Add(givenArray[9]);
        result.Add(givenArray[8]);
        result.Add(givenArray[2]);
        result.Add(givenArray[10]);
        result.Add(givenArray[18]);
        result.Add(givenArray[17]);
        result.Add(givenArray[16]);
        result.Add(givenArray[3]);
        result.Add(givenArray[11]);
        result.Add(givenArray[19]);
        result.Add(givenArray[27]);
        result.Add(givenArray[26]);
        result.Add(givenArray[25]);
        result.Add(givenArray[24]);
        result.Add(givenArray[4]);
        result.Add(givenArray[12]);
        result.Add(givenArray[20]);
        result.Add(givenArray[28]);
        result.Add(givenArray[36]);
        result.Add(givenArray[35]);
        result.Add(givenArray[34]);
        result.Add(givenArray[33]);
        result.Add(givenArray[32]);
        result.Add(givenArray[5]);
        result.Add(givenArray[13]);
        result.Add(givenArray[21]);
        result.Add(givenArray[29]);
        result.Add(givenArray[37]);
        result.Add(givenArray[45]);
        result.Add(givenArray[44]);
        result.Add(givenArray[43]);
        result.Add(givenArray[42]);
        result.Add(givenArray[41]);
        result.Add(givenArray[40]);
        result.Add(givenArray[6]);
        result.Add(givenArray[14]);
        result.Add(givenArray[22]);
        result.Add(givenArray[30]);
        result.Add(givenArray[38]);
        result.Add(givenArray[46]);
        result.Add(givenArray[54]);
        result.Add(givenArray[53]);
        result.Add(givenArray[52]);
        result.Add(givenArray[51]);
        result.Add(givenArray[50]);
        result.Add(givenArray[49]);
        result.Add(givenArray[48]);
        result.Add(givenArray[7]);
        result.Add(givenArray[15]);
        result.Add(givenArray[23]);
        result.Add(givenArray[31]);
        result.Add(givenArray[39]);
        result.Add(givenArray[47]);
        result.Add(givenArray[55]);
        result.Add(givenArray[63]);
        result.Add(givenArray[62]);
        result.Add(givenArray[61]);
        result.Add(givenArray[60]);
        result.Add(givenArray[59]);
        result.Add(givenArray[58]);
        result.Add(givenArray[57]);
        result.Add(givenArray[56]);
        return result;
    }
    private ArrayList DeThree(ArrayList givenArray)
    {
        //throw new NotImplementedException();
        ArrayList result = new ArrayList(64);
        // Rectangle rect = (Rectangle)givenArray[0];
        result.Add(givenArray[0]);
        result.Add(givenArray[1]);
        result.Add(givenArray[2]);
        result.Add(givenArray[3]);
        result.Add(givenArray[4]);
        result.Add(givenArray[5]);
        result.Add(givenArray[6]);
        result.Add(givenArray[7]);
        result.Add(givenArray[15]);
        result.Add(givenArray[14]);
        result.Add(givenArray[13]);
        result.Add(givenArray[12]);
        result.Add(givenArray[11]);
        result.Add(givenArray[10]);
        result.Add(givenArray[9]);
        result.Add(givenArray[8]);
        result.Add(givenArray[16]);
        result.Add(givenArray[17]);
        result.Add(givenArray[18]);
        result.Add(givenArray[19]);
        result.Add(givenArray[20]);
        result.Add(givenArray[21]);
        result.Add(givenArray[22]);
        result.Add(givenArray[23]);
        result.Add(givenArray[31]);
        result.Add(givenArray[30]);
        result.Add(givenArray[29]);
        result.Add(givenArray[28]);
        result.Add(givenArray[27]);
        result.Add(givenArray[26]);
        result.Add(givenArray[25]);
        result.Add(givenArray[24]);
        result.Add(givenArray[32]);
        result.Add(givenArray[33]);
        result.Add(givenArray[34]);
        result.Add(givenArray[35]);
        result.Add(givenArray[36]);
        result.Add(givenArray[37]);
        result.Add(givenArray[38]);
        result.Add(givenArray[39]);
        result.Add(givenArray[47]);
        result.Add(givenArray[46]);
        result.Add(givenArray[45]);
        result.Add(givenArray[44]);
        result.Add(givenArray[43]);
        result.Add(givenArray[42]);
        result.Add(givenArray[41]);
        result.Add(givenArray[40]);
        result.Add(givenArray[48]);
        result.Add(givenArray[49]);
        result.Add(givenArray[50]);
        result.Add(givenArray[51]);
        result.Add(givenArray[52]);
        result.Add(givenArray[53]);
        result.Add(givenArray[54]);
        result.Add(givenArray[55]);
        result.Add(givenArray[63]);
        result.Add(givenArray[62]);
        result.Add(givenArray[61]);
        result.Add(givenArray[60]);
        result.Add(givenArray[59]);
        result.Add(givenArray[58]);
        result.Add(givenArray[57]);
        result.Add(givenArray[56]);
        return result;
    }

    }

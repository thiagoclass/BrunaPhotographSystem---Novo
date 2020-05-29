using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;

namespace BrunaPhotographSystem.DomainService
{
    public static class EditorDeImagem
    {
        public static byte[] AddWaterMark(byte[] imageOriginal)
        {
           
            var watermarkedStream = new MemoryStream();
            byte[] imageContent;
            using (var img = Image.FromStream(new MemoryStream(imageOriginal)))
            {
                using (var graphic = Graphics.FromImage(img))
                {
                    var font = new Font(new FontFamily("Arial"),100, FontStyle.Bold, GraphicsUnit.Pixel);
                    var font2 = new Font(new FontFamily("Arial"), 100, FontStyle.Underline, GraphicsUnit.Pixel);
                    var color = Color.FromName("Black");
                    var brush = new SolidBrush(color);
                    int tamanho= img.Width;
                    while (tamanho>0) {

                        var point = new Point(tamanho - 600, (int)(img.Height * 0.9));
                        graphic.DrawString("Bruna Tahita", font2, brush, point);
                        point = new Point(tamanho - 600, (int)(img.Height * 0.7));
                        graphic.DrawString("Amostra", font, brush, point);
                        point = new Point(tamanho - 600, (int)(img.Height * 0.5));
                        graphic.DrawString("Bruna Tahita", font2, brush, point);
                        point = new Point(tamanho - 600, (int)(img.Height * 0.3));
                        graphic.DrawString("Amostra", font, brush, point);
                        point = new Point(tamanho - 600, (int)(img.Height * 0.1));
                        graphic.DrawString("Bruna Tahita", font2, brush, point);
                        tamanho = tamanho - 600;
                    }

                    img.Save(watermarkedStream, ImageFormat.Jpeg);


                    imageContent = new byte[watermarkedStream.Length];
                    watermarkedStream.Position = 0;
                    watermarkedStream.Read(imageContent, 0, (int)watermarkedStream.Length);
                    
                }
            }
                return imageContent;
         
        }
        
        public static byte[] DrawSize(byte[] imageOriginal, int widhtapproximate)
        {
            try
            {


                var imagemBitmap = Image.FromStream(new MemoryStream(imageOriginal));
                double Perc = ((widhtapproximate * 100) / imagemBitmap.Size.Width) + 1;
                Size newSize = new Size((int)(imagemBitmap.Size.Width * (Perc / 100)), (int)(imagemBitmap.Size.Height * (Perc / 100)));

                //newSize.Width = widht;
                //var grafico = Graphics.FromImage(imagemBitmap);
                //grafico.DrawImage(imagemBitmap, widht, 0);
                Bitmap bitmap = new Bitmap(imagemBitmap, newSize);

                MemoryStream memoryStream = new MemoryStream();
                bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] imageContent = new byte[memoryStream.Length];
                memoryStream.Position = 0;
                memoryStream.Read(imageContent, 0, (int)memoryStream.Length);

                bitmap.Dispose();
                memoryStream.Dispose();


                return imageContent;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using CellSimul;
using CellSimul.CellObjects;
using System.Drawing;

namespace CellSimul
{
    public class Environment
    {
        Extent wordSize;
        Extent resize;

        List<Objects> objects = new List<Objects>();

        Bitmap buffer;

        DateTime preInvalideTime = DateTime.MinValue;

        PictureBox mPb;

        public Environment(Extent ws)
        {
            resize = wordSize = ws;
            buffer = new Bitmap(ws.Width, ws.Height);
            copyInvoke = new ImageCopyInvoke(ImageCopy);
        }

        ~Environment()
        {
            buffer.Dispose();
            objects.Clear();
        }

        public void SendTime(PictureBox pb)
        {
            if ( objects != null)
            {
                try
                {
                    if ( wordSize != resize)
                    {
                        // wordSize 변경에 의한 bitmap 재생성
                        buffer = new Bitmap(resize.Width, resize.Height);
                        wordSize = resize;
                    }

                    Graphics gp = Graphics.FromImage(buffer);

                    Rectangle rect = new Rectangle(0, 0, pb.Width - 1, pb.Height - 1);

                    // 팔렛트 셋팅...
                    gp.FillRectangle(Brushes.White, rect);
                    gp.DrawRectangle(new Pen(Color.Black), rect);

                    // movement
                    foreach (Objects ob in objects)
                    {
                        ob.Movement();

                        ob.LocationCorrection(wordSize); // 세계값을 받아서 위치 보정.
                    }

                    // render
                    foreach (Objects ob in objects)
                    {
                        ob.Render(gp);
                    }

                    mPb = pb;
                    pb.Invoke(copyInvoke);
                    //ImageCopy(pb);

                    if ((DateTime.Now - preInvalideTime).Milliseconds > 33) // 30fps
                        Invalidate(pb);
                }
                catch (Exception exp)
                {
                    Console.Write(exp.Message);
                }

            }
        }
        
        public delegate void ImageCopyInvoke();
        public ImageCopyInvoke copyInvoke;
        public void ImageCopy()
        {
            try
            {
                if(buffer != null)
                    mPb.Image = buffer;
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
            }
        }

        public delegate void MethodInvoker();

        public void Invalidate(PictureBox pb)
        {
            try { 
                if (pb.InvokeRequired)
                    pb.Invoke(new MethodInvoker(pb.Invalidate));
                else
                    pb.Invalidate(true);
            }
            catch (Exception exp)
            {
                Console.Write(exp.Message);
            }

        }

        public void AddObjects(Objects obj)
        {
            objects.Add(obj);
        }
        
        public void Resize(Extent size)
        {
            resize = size;
        }
    }
}

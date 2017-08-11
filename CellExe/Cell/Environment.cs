using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using CellExe.Element;
using CellExe.Cell.CellObjects;
using System.Drawing;

namespace CellExe.Cell
{
    public class Environment
    {
        Element.Size wordSize;

        List<Objects> objects = new List<Objects>();

        Bitmap buffer;

        DateTime preInvalideTime = DateTime.MinValue;

        public Environment(Element.Size ws)
        {
            wordSize = ws;
            buffer = new Bitmap(ws.Width, ws.Height);
        }
        

        public void SendTime(PictureBox pb)
        {
            if ( objects != null)
            {
                try
                {
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

                    pb.Image = buffer;

                    if ((DateTime.Now - preInvalideTime).Milliseconds > 33) // 30fps
                        Invalidate(pb);
                }
                catch (Exception exp)
                {
                    Console.Write(exp.Message);
                }

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

    }
}

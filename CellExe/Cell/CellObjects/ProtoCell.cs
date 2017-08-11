using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CellExe.Element;
using System.Windows.Forms;
using System.Drawing;
using System.Threading;

namespace CellExe.Cell.CellObjects
{
    public class ProtoCell : Objects
    {
        Random rnd;

        public ProtoCell (Element.Size size, Position posi)
        {
            mySize = size;
            myPosition = posi;
            Thread.Sleep(20);
            rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
        }

        public override void Movement()
        {
            // 방향과 속도를 랜덤으로 정함.
            myVector.Direction += (rnd.NextDouble() * rnd.Next(-1, 1) * 10) % 360;
            myVector.Direction %= 360.0;

            myVector.Speed += rnd.NextDouble() * rnd.Next(-1, 1) * rnd.Next(1, 5);
            if (myVector.Speed > 50) myVector.Speed = 50;
            if (myVector.Speed < 0) myVector.Speed = 0;
            
            // 백터만큼 이동
            MoveToVector();

            base.Movement();
        }

        public override void Render(PictureBox pb)
        {
            Graphics g = pb.CreateGraphics();

            g.DrawRectangle(new Pen(Color.Black), new Rectangle(myPosition.x, myPosition.y, 1, 1));

            base.Render(pb);
        }
    }
}

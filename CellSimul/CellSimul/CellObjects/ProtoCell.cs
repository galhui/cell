using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace CellSimul.CellObjects
{
    public class ProtoCell : WorldObjects
    {
        Random rnd;
        bool evasionSteering = false;

        public ProtoCell (Extent size, Position posi)
        {
            mySize = size;
            myPosition = posi;
            Thread.Sleep(10);
            rnd = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);

            myVector.Direction = rnd.NextDouble() * 365.0;
            myColor = Color.Black; //Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256));
        }

        public void RandomPosition(int width, int height)
        {
            myPosition.dx = rnd.NextDouble() * width/2 + width / 4;
            myPosition.dy = rnd.NextDouble() * height/2 + height / 4;
            myPosition.DoubleToInt();
        }

        private void RandomVector()
        {
            myVector.Direction += (rnd.NextDouble() * rnd.Next(-1, 1) * 90) % 360;

            if (rnd.Next(1, 100) > 50)
                myVector.Direction += 180.0;

            myVector.Direction %= 360.0;

            myVector.Speed += rnd.NextDouble() * rnd.Next(-10, 10);
            if (myVector.Speed > 100) myVector.Speed = 100;
            if (myVector.Speed < 0) myVector.Speed = 0;
        }

        public override void Movement(Extent worldSize, List<WorldObjects> closeObject)
        {
            // 방향과 속도를 랜덤으로 정함.
            RandomVector();

            while (AvailableDestination(closeObject))
            {
                myVector.Direction = (myVector.Direction + rnd.NextDouble()*360.0) % 360.0;
                myVector.Speed = 100;
                evasionSteering = true;
            }

            if (evasionSteering && AvailableDestination(closeObject) == false)
            {
                evasionSteering = false;
                myVector.Speed = 20;
            }

            // 백터만큼 이동
            MoveToVector();

            base.Movement(worldSize, closeObject);
        }

        public override void Render(Graphics gp)
        {

            gp.DrawEllipse(new Pen(myColor), new RectangleF(myPosition.x - (mySize.Width / 2), myPosition.y - (mySize.Height / 2), mySize.Width, mySize.Height));
            //gp.DrawRectangle(new Pen(myColor), new Rectangle(myPosition.x - (mySize.Width/2), myPosition.y - (mySize.Height/2), mySize.Width, mySize.Height));
            //gp.FillRectangle(Brushes.Black, new Rectangle(myPosition.x - (mySize.Width / 2), myPosition.y - (mySize.Height / 2), mySize.Width, mySize.Height));

            base.Render(gp);
        }
    }
}

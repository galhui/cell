using System;
using System.Collections.Generic;
using System.Drawing;

namespace CellSimul.CellObjects
{
    public class WorldObjects
    {
        public Extent mySize;
        public Position myPosition;
        public Vector myVector = new Vector();
        protected Color myColor;
        
        virtual public void Movement(Extent worldSize, List<WorldObjects> closeObject)
        {
            LocationCorrection(worldSize);
        }

        virtual public void Render(Graphics gp)
        {

        }

        protected bool AvailableDestination(List<WorldObjects> closeObject)
        {
            // 목적지가 유효하지 않으면, 근처 가능한 위치로 벡터를 조정한다. (direction을 변경?)
            Position dist = myVector.GetNextPosition(myPosition);

            foreach(WorldObjects ob in closeObject)
            {
                if (dist.GetDistance(ob.myPosition) <= this.mySize.Radius + ob.mySize.Radius)
                {   
                    return false;
                }
            }

            return true;
        }

        protected bool IsOverlap(WorldObjects ob)
        {
            return this.myPosition.GetDistance(ob.myPosition) <= this.mySize.Radius + ob.mySize.Radius;
        }

        protected double MoveToVector()
        {
            myPosition = myVector.GetNextPosition(myPosition);

            return myVector.Speed / Constants.TimeBalance;
        }

        private void LocationCorrection(Extent wordSize)
        {
            // PictureBox에 그리기 때문에 세상 밖으로 나가는 보정은 쉽다.

            if (myPosition.dx < 0)
                myPosition.dx = 0;
            if (myPosition.dx > wordSize.Width)
                myPosition.dx = wordSize.Width;
            if (myPosition.dy < 0)
                myPosition.dy = 0;
            if (myPosition.dy > wordSize.Height)
                myPosition.dy = wordSize.Height;

            myPosition.DoubleToInt();            
        }
    }
}

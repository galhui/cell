using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CellExe.Element;
using System.Windows.Forms;

namespace CellExe.Cell.CellObjects
{
    public class Objects
    {
        public Size mySize;
        public Position myPosition;
        public Vector myVector = new Vector();

        const double TimeBalance = 1000;

        virtual public void Movement()
        {

        }

        virtual public void Render(PictureBox pb)
        {

        }

        protected void MoveToVector()
        {
            myPosition.dx += (Math.Sin(myVector.Direction / 180.0 * Math.PI) * myVector.Direction / TimeBalance);
            myPosition.dy += (Math.Cos(myVector.Direction / 180.0 * Math.PI) * myVector.Direction / TimeBalance);

            myPosition.DoubleToInt();
        }

        public void LocationCorrection(Size wordSize)
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

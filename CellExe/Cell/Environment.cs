using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using CellExe.Element;
using CellExe.Cell.CellObjects;


namespace CellExe.Cell
{
    public class Environment
    {
        Size wordSize;

        List<Objects> objects = new List<Objects>();

        public Environment(Size ws)
        {
            wordSize = ws;
        }

        public void SendTime(PictureBox pb)
        {
            if ( objects != null)
            {
                // movement
                foreach (Objects ob in objects)
                {
                    ob.Movement();

                    ob.LocationCorrection(wordSize); // 세계값을 받아서 위치 보정.
                }

                // 혹시모를 보정 작업을 위해 분리

                // render
                foreach (Objects ob in objects)
                {
                    ob.Render(pb);
                }
            }
        }

        public void AddObjects(Objects obj)
        {
            objects.Add(obj);
        }

    }
}

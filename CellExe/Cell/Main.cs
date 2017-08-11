using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Threading;

using CellExe.Element;
using CellExe.Cell.CellObjects;

namespace CellExe.Cell
{
    public class Main
    {
        // 외부 연결 통로
        PictureBox pictureBox;
        Label timeLabel;

        // 내부 오브젝트
        Thread timeThread;
        Environment env;
        DateTime time;

        bool isAlive = false;

        public Main(PictureBox pb, Label lab)
        {
            pictureBox = pb;
            timeLabel = lab;
        }

        public void Start()
        {
            isAlive = true;
            if ( timeThread == null)
            { 
                timeThread = new Thread(Render);
                time = new DateTime(0);
                
                // 환경을 초기화
                env = new Environment(new Size(pictureBox.Height, pictureBox.Width));

                for (int i = 0; i < 30; i++)
                {
                    Size s = new Size(15, 15);
                    Position p = new Position(pictureBox.Width / 2, pictureBox.Height / 2);

                    ProtoCell cell = new ProtoCell(s, p);
                    cell.RandomPosition(pictureBox.Width, pictureBox.Height);

                    env.AddObjects(cell);
                }

                timeThread.Start();
            }
            else
            {
                if(timeThread.IsAlive)
                    timeThread.Resume();
            }
        }

        public void Resize(Element.Size size)
        {
            env.Resize(size);
        }

        public void Stop()
        {
            if ( timeThread.IsAlive)
                timeThread.Suspend();

            isAlive = false;
        }

        public void Reset()
        {
            if (timeThread != null)
            {
                timeThread.Suspend();
                timeThread.DisableComObjectEagerCleanup();
            }
            

            timeThread = null;
            env = null;
            isAlive = false;

            pictureBox.Invalidate();
        }

        private void Render()
        {
            while(isAlive) {
                try { 
                    time = time.AddMilliseconds(1);
                    SetTime(time.ToString("yyyy-MM-dd hh:mm:ss.fff"));

                    env.SendTime(pictureBox);
                    Thread.Sleep(0);
                }
                catch(Exception exp)
                {
                    Console.Write(exp.Message);
                }
            }
        }

        delegate void SetTextCallback(string text);
        
        private void SetTime(string text)
        {
            if (timeLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetTime);
                timeLabel.Invoke(d, new object[] { text });
            }
            else
            {
                timeLabel.Text = text;
            }
        }

    }
}

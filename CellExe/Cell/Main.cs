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

                // 기본 Cell 생성 중간에 한마리 나둬보자..
                env.AddObjects(new ProtoCell(new Size(1, 1), new Position(pictureBox.Height/2, pictureBox.Width/2)));
                env.AddObjects(new ProtoCell(new Size(1, 1), new Position(0, 0)));
                env.AddObjects(new ProtoCell(new Size(1, 1), new Position(100, 20)));
                env.AddObjects(new ProtoCell(new Size(1, 1), new Position(100, 200)));
                env.AddObjects(new ProtoCell(new Size(1, 1), new Position(150, 240)));

                timeThread.Start();
            }
            else
            {
                timeThread.Resume();
            }
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
                time = time.AddMilliseconds(1);
                SetTime(time.ToString("yyyy-MM-dd hh:mm:ss.fff"));

                env.SendTime(pictureBox);
                Thread.Sleep(1);
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

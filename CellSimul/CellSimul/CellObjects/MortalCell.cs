using System.Collections.Generic;
using System.Drawing;
using System.Threading;

namespace CellSimul.CellObjects
{
    public class MortalCell : WorldObjects
    {
        #region const values
        private const double DefaultMaxEnergy = 100.0;
        private const double DefaultCurrentEnergy = 50.0;

        private const double DefaultConsumeEnergy = 0.1;
        #endregion

        protected Energy myEnergy;


        public MortalCell ()
        {
            myEnergy = new Energy(this)
            {
                Max = DefaultMaxEnergy,
                Currnet = DefaultCurrentEnergy,
                ConsumePerSec = DefaultConsumeEnergy
            };
        }

        ~MortalCell()
        {
        }

        public override void Movement(Extent worldSize, List<WorldObjects> closeObject)
        {
            double dist = MoveToVector();

            base.Movement(worldSize, closeObject);
        }

        public override void Render(Graphics gp)
        {
            gp.DrawRectangle(new Pen(myColor), new Rectangle(myPosition.x - (mySize.Width / 2), myPosition.y - (mySize.Height / 2), mySize.Width, mySize.Height));

            base.Render(gp);
        }

        public void Dieing()
        {

        }

    }

    public class Energy
    {
        private MortalCell ParentObject;
        public Energy(MortalCell parentObject)
        {
            ParentObject = parentObject;
        }

        public double Max { get; set; }
        public double Currnet { get; set; }

        public double ConsumePerSec { get; set; }

        private void BasalMetabolism()
        {
            Currnet -= ConsumePerSec;

            if ( Currnet < 0.0 && ParentObject != null)
            {
                // 에너지가 0보다 작으면... 죽겠지..? ㅠㅠ
                ParentObject.Dieing();
            }
        }

        public void MoveConsume(double dist)
        {
            Currnet -= dist / 10;
            BasalMetabolism();
        }
    }
}

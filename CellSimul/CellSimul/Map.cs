using System;
using System.Collections.Generic;

using CellSimul.CellObjects;

namespace CellSimul
{
    public class Map
    {
        private const int divisionWidth = 40;
        private const int divisionHeight = 30;

        private List<WorldObjects>[] maps;

        public void InitMap(List<WorldObjects> objects)
        {
            int divisonSize = divisionWidth * divisionWidth;

            maps = new List<WorldObjects>[divisonSize];

            for (int i = 0; i < divisonSize; i++)
                maps[i] = new List<WorldObjects>();

            foreach(WorldObjects wo in objects)
            {
                maps[CalcIndex(wo.myPosition)].Add(wo);
            }
        }

        private int CalcIndex(Position p)
        {
            p.DoubleToInt();
            return CalcIndex(p.x, p.y);
        }

        private int CalcIndex(int x, int y)
        {
            return (x / divisionWidth) + ((y / divisionHeight) * divisionWidth);
        }

        public List<WorldObjects> CloseObjects(WorldObjects wo)
        {
            return CloseObjects(wo.myPosition);
        }

        public List<WorldObjects> CloseObjects(Position ps)
        {
            if (ps == null)
                return null;

            List<WorldObjects> response = new List<WorldObjects>();
            int index = CalcIndex(ps);

            // 가장 가까운 9개 방면에 있는 object들을 반환.. (충돌방지를 위해)
            
            // -1, -1

            // -1, 0
                
            // -1, +1

            // 0, -1
            
            // 0, 0
            foreach (WorldObjects wo in maps[index])
                response.Add(wo);

            // 0, +1

            // +1, -1

            // +1, 0

            // +1, 1

            return response;
        }

    }
}

using System;
using System.Collections.Generic;
using System.Text;


namespace CustomLib
{
    public class Conversion
    {
        public int BoolToInt(bool x)
        {
            return (x == true) ? 1 : 0;
        }

        public float AngleToRad(float angle)
        {
            return angle * ((float)Math.PI / 180);
        }
    }
    
}

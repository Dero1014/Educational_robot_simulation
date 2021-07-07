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

        // This function turns a local angle into a global by adding on 
        // all of the angles up to the current one
        public float GlobalAngle(int max, float[] localAngles)
        {
            int index = 0;
            float angle = 0;

            while (index <= max)
            {
                angle += localAngles[index];
                index++;
            }

            return angle;
        }
    }

}

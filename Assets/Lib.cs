using System;
using System.Collections.Generic;
using System.Text;


namespace CustomLib
{
    public class Conversion
    {
        public int Bool2Int(bool x)
        {
            return (x == true) ? 1 : 0;
        }

        public float Angle2Rad(float angle)
        {
            return angle * ((float)Math.PI / 180);
        }
        public float Rad2Angle(float rad)
        {
            return rad * (180/(float)Math.PI);
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

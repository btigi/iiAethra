namespace ii.Aethra
{
    public static class Utils
    {
        public static double ConvertFromReal48(byte[] real48)
        {
            // real48[0] == 0 is represents the value 0.
            if (real48[0] == 0)
                return 0.0;

            Double exponentbase = 129d;
            Double exponent = real48[0] - exponentbase; // The exponent is offset so deduct the base.

            // Now Calculate the mantissa
            Double mantissa = 0.0;
            Double value = 1.0;
            // For Each Byte.
            for (int i = 5; i >= 1; i--)
            {
                int startbit = 7;
                if (i == 5)
                { startbit = 6; } //skip the sign bit.

                //For Each Bit
                for (int j = startbit; j >= 0; j--)
                {
                    value = value / 2;// Each bit is worth half the next bit but we're going backwards.
                    if (((real48[i] >> j) & 1) == 1) //if this bit is set.
                    {
                        mantissa += value; // add the value.
                    }
                }
            }

            // The significand ia 1 + mantissa.
            // This must come before applying the sign.
            mantissa = 1 + mantissa;

            if ((real48[5] & 0x80) != 0) // Sign bit check
                mantissa = -mantissa;

            return (mantissa) * Math.Pow(2.0, exponent);
        }

        public static byte[] ConvertToReal48(double value)
        {
            // Handle zero case  
            if (value == 0.0)
                return new byte[6];

            byte[] real48 = new byte[6];

            // Determine the sign and make the value positive if negative  
            bool isNegative = value < 0;
            if (isNegative)
                value = -value;

            // Extract the exponent and normalize the value  
            int exponent = (int)Math.Floor(Math.Log(value, 2));
            double mantissa = value / Math.Pow(2.0, exponent);

            // Adjust the exponent to the offset base (129)  
            exponent += 129;
            real48[0] = (byte)exponent;

            // Remove the implicit leading 1 from the mantissa  
            mantissa -= 1.0;

            // Encode the mantissa into the remaining bytes  
            for (int i = 1; i <= 5; i++)
            {
                int startbit = (i == 5) ? 6 : 7;
                for (int j = startbit; j >= 0; j--)
                {
                    mantissa *= 2;
                    if (mantissa >= 1.0)
                    {
                        real48[i] |= (byte)(1 << j);
                        mantissa -= 1.0;
                    }
                }
            }

            // Set the sign bit if the value is negative  
            if (isNegative)
                real48[5] |= 0x80;

            return real48;
        }

    }
}
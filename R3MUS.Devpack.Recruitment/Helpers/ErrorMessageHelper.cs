using R3MUS.Devpack.Recruitment.Properties;
using System;

namespace R3MUS.Devpack.Recruitment.Helpers
{
    public class ErrorMessageHelper
    {
        public static string GetNonErrorMessage()
        {
            var rand = new Random().Next(1, 7);
            switch (rand)
            {
                case 2:
                    return Resources.NotAnError2;
                case 3:
                    return Resources.NotAnError3;
                case 4:
                    return Resources.NotAnError4;
                case 5:
                    return Resources.NotAnError5;
                case 6:
                    return Resources.NotAnError6;
                default:
                    return Resources.NotAnError1;
            }
        }
    }
}
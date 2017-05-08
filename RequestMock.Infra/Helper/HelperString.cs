using System;
using System.Collections.Generic;
using System.Text;

namespace RequestMock.Infra.Helper
{
    public class HelperString
    {
        public static string Base64Encode(string plainText, Encode encode)
        {
            byte[] plainTextBytes;

            switch (encode)
            {
                case Encode.iso88591:
                    plainTextBytes = Encoding.ASCII.GetBytes(plainText);
                    break;
                case Encode.utf16:
                    plainTextBytes = Encoding.Unicode.GetBytes(plainText);
                    break;
                default:
                    plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                    break;
            }

            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(string base64EncodedData, Encode encode)
        {
            byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedData);

            switch (encode)
            {
                case Encode.iso88591:
                    return Encoding.ASCII.GetString(base64EncodedBytes);                    
                case Encode.utf16:
                    return Encoding.Unicode.GetString(base64EncodedBytes);                    
                default:
                    return Encoding.UTF8.GetString(base64EncodedBytes);                    
            }            
        }
    }
}

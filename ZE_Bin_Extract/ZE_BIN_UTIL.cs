using System.Text;

namespace ZE_BIN
{
    internal class BIN_Util
    {
        internal static int Read_Int32(Stream stream)
        {
            byte[] buff = new byte[4];
            stream.Read(buff, 0, buff.Length);
            return BitConverter.ToInt32(buff, 0);
        }
        internal static ushort Read_UInt16(Stream stream)
        {
            byte[] buff = new byte[2];
            stream.Read(buff, 0, buff.Length);
            return BitConverter.ToUInt16(buff, 0);
        }
        internal static short Read_Int16(Stream stream)
        {
            byte[] buff = new byte[2];
            stream.Read(buff, 0, buff.Length);
            return BitConverter.ToInt16(buff, 0);
        }
        internal static byte[] Read_ByteArray(Stream stream, uint length)
        {
            byte[] bytes = new byte[length];
            stream.Read(bytes);
            return bytes;
        }

        public static string Read_Bin_String(Stream stream)
        {
            int stringLen = Read_Int32(stream);
            byte[] stringBuffer = new byte[stringLen];
            stream.Read(stringBuffer, 0, stringLen);
            /*for (int i = 0; i < stringBuffer.Length; i++)
            {
                if (stringBuffer[i] == 226 || stringBuffer[i] == 128 || stringBuffer[i] == 147)
                    stringBuffer[i] = (byte)(char)stringBuffer[i];
            }*/
            return Encoding.UTF8.GetString(stringBuffer);
        }
        public static void Write_Bin_String(Stream stream, string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            stream.Write(BitConverter.GetBytes(bytes.Length));
            stream.Write(bytes);
        }
    }
}

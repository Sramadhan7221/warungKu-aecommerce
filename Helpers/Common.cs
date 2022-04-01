namespace WarungKuApp.Helpers;

public static class Common
{
     public static byte[] StreamToByte(Stream StreamContent)
     {
          MemoryStream ms = new MemoryStream();
          StreamContent.CopyTo(ms);
          return ms.ToArray();
     }

     public static string ToEmpty(this string content)
     {
          return "";
     }

     public static int ToInt(this string content)
     {
          if (int.TryParse(content, out int result))
          {
               return result;
          }

          throw new InvalidOperationException("Belum login euy");
     }

     public static byte[] ToBytes(this Stream streamContent)
     {
          MemoryStream ms = new MemoryStream();
          streamContent.CopyTo(ms);
          return ms.ToArray();
     }
}
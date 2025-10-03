
using System;
using System.Text;

public static class EncryptionData
{
    private static readonly byte[] SECRET_KEY = Guid.NewGuid().ToByteArray();

    // Mã hóa chuỗi bằng thuật toán XOR
    public static string Encrypt(string data)
    {
        if (string.IsNullOrEmpty(data))
        {
            return string.Empty;
        }

        byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        byte[] encryptedBytes = new byte[dataBytes.Length];

        for (int i = 0; i < dataBytes.Length; i++)
        {
            // XOR từng byte của dữ liệu với từng byte của khóa
            encryptedBytes[i] = (byte)(dataBytes[i] ^ SECRET_KEY[i % SECRET_KEY.Length]);
        }

        // Chuyển kết quả đã mã hóa thành chuỗi Base64 để lưu trữ an toàn
        return System.Convert.ToBase64String(encryptedBytes);
    }

    // Giải mã chuỗi
    public static string Decrypt(string encryptedData)
    {
        if (string.IsNullOrEmpty(encryptedData))
        {
            return string.Empty;
        }

        // Chuyển chuỗi Base64 về lại mảng byte
        byte[] encryptedBytes = Convert.FromBase64String(encryptedData);
        byte[] decryptedBytes = new byte[encryptedBytes.Length];

        for (int i = 0; i < encryptedBytes.Length; i++)
        {
            // Thực hiện phép XOR tương tự như khi mã hóa để giải mã
            decryptedBytes[i] = (byte)(encryptedBytes[i] ^ SECRET_KEY[i % SECRET_KEY.Length]);
        }

        // Chuyển mảng byte đã giải mã về chuỗi ban đầu
        return System.Text.Encoding.UTF8.GetString(decryptedBytes);
    }
}

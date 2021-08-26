using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NewBot.Models.Controller;
using NewBot.Models.Model;
using NewBot.Security.Extensions;

namespace NewBot.Security
{
    /// <summary>
    /// Encryption Method :RSA
    /// </summary>
    public class Encryption
    {
        private UnicodeEncoding ByteConverter = new UnicodeEncoding();
        private static byte[] Encrypt(byte[] DataToEncrypt, RSAParameters keyDetail, bool DoOAEPPadding)
        {
            try
            {
                byte[] EncryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(keyDetail);
                    EncryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                return EncryptedData;
            }
            catch (CryptographicException)
            {
                return null;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }
        private static byte[] Decrypt(byte[] DataToDecrypt, RSAParameters keyDetail, bool DoOAEPPadding)
        {
            try
            {
                byte[] DecryptedData;
                using (var RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(keyDetail);
                    DecryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return DecryptedData;
            }
            catch (CryptographicException)
            {
                return null;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }
        /// <summary>
        /// This Funcation Will Return Bytes In Encryption And String In Decryption
        /// </summary>
        /// <param name="encryptionModel"> Need This Parameters To Running</param>
        /// <returns></returns>
        public string Run(EncryptionModel encryptionModel)
        {
            using (var Rsa = new RSACryptoServiceProvider())
            {
                switch (encryptionModel.type)
                {
                    case Type.Encrypt:
                        byte[] BytedData = ByteConverter.GetBytes(encryptionModel.DataToEnCrypt);
                        byte[] EncryptedBytes = Encrypt(BytedData, Rsa.ExportParameters(false), false);
                        string FinalKey = "";
                        foreach (var Byte in EncryptedBytes)
                        {
                            FinalKey += Byte;
                        }

                        string s = BytedData.RsaEncryption(Rsa.ExportParameters(false), false).ByteToString();
                        return FinalKey;
                    case Type.Decrypt:
                        byte[] deCryptedData = Decrypt(encryptionModel.DataToDecrypt, Rsa.ExportParameters(true), true);
                        return ByteConverter.GetString(deCryptedData);
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }

}

namespace NewBot.Security.Extensions
{
    public static class Cryption
    {
        private static UnicodeEncoding ByteConverter = new UnicodeEncoding();
        public static byte[] RsaEncryption(this byte[] DataToEncrypt, RSAParameters keyDetail, bool DoOAEPPadding)
        {
            try
            {
                byte[] EncryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(keyDetail);
                    EncryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                return EncryptedData;
            }
            catch (CryptographicException)
            {
                return null;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }
        private static byte[] RsaDecryption(this byte[] DataToDecrypt, RSAParameters keyDetail, bool DoOAEPPadding)
        {
            try
            {
                byte[] DecryptedData;
                using (var RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(keyDetail);
                    DecryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return DecryptedData;
            }
            catch (CryptographicException)
            {
                return null;
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }


    }

    public static class CryptionExtensions
    {
        private static UnicodeEncoding ByteConverter = new UnicodeEncoding();
        public static string ByteToString(this byte[] BytesToConvert)
        {
            return ByteConverter.GetString(BytesToConvert);
        }

        public static string KeyShorter(this string LongKey)
        {
            return null;
        }
    }
}

namespace NewBot.Security.Extensions.CallBacks
{
    public static class CallBackExtensions
    {
        private static DbController _db = new DbController();

        public static async Task<string> CallBackHandlerAsync(this string CallBackText)
        {
            string[] getIdentifierByStar = CallBackText.Split('*');
            await _db.AddCallBackAsync(new CallBack() { Identifier = getIdentifierByStar[3], CallBack1 = CallBackText });
            return $"$={getIdentifierByStar[3]}";
        }

        public static async Task<string> CallBackReaderAsync(this string Identifier)
        {
            var getCallBack = await _db.GetCallBackAsync(new CallBack() { Identifier = Identifier.Remove(0, 2) });
            return getCallBack.CallBack1;
        }
    }
}
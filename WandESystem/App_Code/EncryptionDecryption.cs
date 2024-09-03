using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

/// <summary>
/// This class is used to define common method to encrypt and decryptstrings.
/// </summary>
/// <CreatedBy>Darpan Khandhar</CreatedBy>
/// <CreatedDate> 25-Jun-2013 </CreatedDate>
/// <ModifiedBy>Darpan Khandhar</ModifiedBy>
/// <ModifiedDate> 26-Jun-2013 </ModifiedDate>
public class EncryptionDecryption
{
    #region Variable Declaration

    private static string keyString = "272F79D3-1F09-48ab-834C-4629C2274D43";
    
    #endregion

    #region Constructor

    public EncryptionDecryption()
	{
		//
		// TODO: Add constructor logic here
		//
    }

    #endregion

    #region Methods/Functions

    /// <summary>
    /// Get Encrpted Value of Passed value
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static string GetEncrypt(string value)
    {
        return HttpUtility.UrlEncode(Encrypt(keyString, value));
    }

    /// <summary>
    /// Get Decrypted value of passed encrypted string
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static string GetDecrypt(string value)
    {
        return Decrypt(keyString, value);
    }

    /// <summary>
    /// Encrypt value
    /// </summary>
    /// <param name="strKey"></param>
    /// <param name="strData"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    private static string Encrypt(string Passphrase, string Message)
    {
        byte[] Results;
        System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

        // Step 1. We hash the passphrase using MD5
        // We use the MD5 hash generator as the result is a 128 bit byte array
        // which is a valid length for the TripleDES encoder we use below

        MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
        byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

        // Step 2. Create a new TripleDESCryptoServiceProvider object
        TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

        // Step 3. Setup the encoder
        TDESAlgorithm.Key = TDESKey;
        TDESAlgorithm.Mode = CipherMode.ECB;
        TDESAlgorithm.Padding = PaddingMode.PKCS7;

        // Step 4. Convert the input string to a byte[]
        byte[] DataToEncrypt = UTF8.GetBytes(Message);

        // Step 5. Attempt to encrypt the string
        try
        {
            ICryptoTransform Encryptor = TDESAlgorithm.CreateEncryptor();
            Results = Encryptor.TransformFinalBlock(DataToEncrypt, 0, DataToEncrypt.Length);
        }
        finally
        {
            // Clear the TripleDes and Hashprovider services of any sensitive information
            TDESAlgorithm.Clear();
            HashProvider.Clear();
        }

        // Step 6. Return the encrypted string as a base64 encoded string
        return Convert.ToBase64String(Results);
    }

    /// <summary>
    /// decrypt value
    /// </summary>
    /// <param name="strKey"></param>
    /// <param name="strData"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    private static string Decrypt(string Passphrase, string Message)
    {
        byte[] Results;
        System.Text.UTF8Encoding UTF8 = new System.Text.UTF8Encoding();

        // Step 1. We hash the passphrase using MD5
        // We use the MD5 hash generator as the result is a 128 bit byte array
        // which is a valid length for the TripleDES encoder we use below

        MD5CryptoServiceProvider HashProvider = new MD5CryptoServiceProvider();
        byte[] TDESKey = HashProvider.ComputeHash(UTF8.GetBytes(Passphrase));

        // Step 2. Create a new TripleDESCryptoServiceProvider object
        TripleDESCryptoServiceProvider TDESAlgorithm = new TripleDESCryptoServiceProvider();

        // Step 3. Setup the decoder
        TDESAlgorithm.Key = TDESKey;
        TDESAlgorithm.Mode = CipherMode.ECB;
        TDESAlgorithm.Padding = PaddingMode.PKCS7;

        // Step 4. Convert the input string to a byte[]
        byte[] DataToDecrypt = Convert.FromBase64String(Message);

        // Step 5. Attempt to decrypt the string
        try
        {
            ICryptoTransform Decryptor = TDESAlgorithm.CreateDecryptor();
            Results = Decryptor.TransformFinalBlock(DataToDecrypt, 0, DataToDecrypt.Length);
        }
        catch
        {
            return "";
        }
        finally
        {
            // Clear the TripleDes and Hashprovider services of any sensitive information
            TDESAlgorithm.Clear();
            HashProvider.Clear();
        }

        // Step 6. Return the decrypted string in UTF8 format
        return UTF8.GetString(Results);
    }

    #endregion
}
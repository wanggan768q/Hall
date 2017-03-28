using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

internal class RSA : MonoBehaviour
{
	private RSACryptoServiceProvider rsa;

	private string key = "ktznqmxlgzppazsc";

	public RSA()
	{
		this.rsa = new RSACryptoServiceProvider();
	}

	public string GetPublicKey()
	{
		return this.rsa.ToXmlString(false);
	}

	public string GetPrivateKey()
	{
		return this.rsa.ToXmlString(true);  
	}

	public string Encrypt(string Source, string PublicKey)
	{
		this.rsa.FromXmlString(PublicKey);
		byte[] inArray = this.rsa.Encrypt(Convert.FromBase64String(Source), false);
		return Convert.ToBase64String(inArray);
	}

	public byte[] Encrypt(byte[] Source, string PublicKey)
	{
		this.rsa.FromXmlString(PublicKey);
		return this.rsa.Encrypt(Source, false);
	}

	public void Encrypt(string inFileName, string outFileName, string PublicKey)
	{
		string xmlString = this.rsa.ToXmlString(false);
		this.rsa.FromXmlString(xmlString);
		string text = File.ReadAllText(inFileName);
		byte[] bytes = Encoding.Default.GetBytes(text);
		byte[] bytes2 = this.rsa.Encrypt(bytes, false);
		File.WriteAllBytes(outFileName, bytes2);
		try
		{
		}
		catch (Exception ex)
		{
			throw new Exception("在文件加密的时候出现错误！错误提示： \n" + ex.Message);
		}
	}

	public string Decrypt(string Source, string PrivateKey)
	{
		string xmlString = this.rsa.ToXmlString(true);
		this.rsa.FromXmlString(xmlString);
		byte[] inArray = this.rsa.Decrypt(Encoding.Default.GetBytes(Source), false);
		return Convert.ToBase64String(inArray);
	}

	public byte[] Decrypt(byte[] Source, string PrivateKey)
	{
		this.rsa.FromXmlString(PrivateKey);
		return this.rsa.Decrypt(Source, false);
	}

	public void Decrypt(string inFileName, string outFileName, string PrivateKey)
	{
		string text = this.rsa.ToXmlString(true);		
		string text2 = File.ReadAllText(inFileName, Encoding.UTF8);
		byte[] bytes = Encoding.Default.GetBytes(text2);
		byte[] bytes2 = this.rsa.Decrypt(bytes, true);
		File.WriteAllBytes(outFileName, bytes2);
		try
		{
		}
		catch (Exception ex)
		{
			throw new Exception("在文件解密的时候出现错误！错误提示： \n" + ex.Message);
		}
	}

	public string Encrypt(string toEncrypt)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(this.key);
		byte[] bytes2 = Encoding.UTF8.GetBytes(toEncrypt);
		ICryptoTransform cryptoTransform = new RijndaelManaged
		{
			Key = bytes,
			IV = Encoding.UTF8.GetBytes("abcdefghijklmnop"),
			Mode = CipherMode.CBC,
			Padding = PaddingMode.PKCS7
		}.CreateEncryptor();
		byte[] array = cryptoTransform.TransformFinalBlock(bytes2, 0, bytes2.Length);
		return Convert.ToBase64String(array, 0, array.Length);
	}

	public string Decrypt(string toDecrypt)
	{
		byte[] bytes = Encoding.UTF8.GetBytes(this.key);
		byte[] array = Convert.FromBase64String(toDecrypt);
		ICryptoTransform cryptoTransform = new RijndaelManaged
		{
			Key = bytes,
			IV = Encoding.UTF8.GetBytes("abcdefghijklmnop"),
			Mode = CipherMode.CBC,
			Padding = PaddingMode.PKCS7
		}.CreateDecryptor();
		byte[] bytes2 = cryptoTransform.TransformFinalBlock(array, 0, array.Length);
		return Encoding.UTF8.GetString(bytes2);
	}

	public void EncryptData(string inName, string outName)
	{
		string s = "pe4y3txd";
		byte[] bytes = Encoding.UTF8.GetBytes(s);
		byte[] bytes2 = Encoding.UTF8.GetBytes("abcdefghijklmnop");
		FileStream fileStream = new FileStream(inName, FileMode.Open, FileAccess.Read);
		FileStream fileStream2 = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
		fileStream2.SetLength(0L);
		byte[] array = new byte[100];
		long num = 0L;
		long length = fileStream.Length;
		DES dES = new DESCryptoServiceProvider();
		CryptoStream cryptoStream = new CryptoStream(fileStream2, dES.CreateEncryptor(bytes, bytes2), CryptoStreamMode.Write);
		while (num < length)
		{
			int num2 = fileStream.Read(array, 0, 100);
			cryptoStream.Write(array, 0, num2);
			num += (long)num2;
		}
		cryptoStream.Close();
		fileStream2.Close();
		fileStream.Close();
	}

	public void DecryptData(string inName, string outName)
	{
		string s = "pe4y3txd";
		byte[] bytes = Encoding.UTF8.GetBytes(s);
		byte[] bytes2 = Encoding.UTF8.GetBytes("abcdefghijklmnop");
		FileStream fileStream = new FileStream(inName, FileMode.Open, FileAccess.Read);
		FileStream fileStream2 = new FileStream(outName, FileMode.OpenOrCreate, FileAccess.Write);
		fileStream2.SetLength(0L);
		byte[] array = new byte[100];
		long num = 0L;
		long length = fileStream.Length;
		DES dES = new DESCryptoServiceProvider();
		CryptoStream cryptoStream = new CryptoStream(fileStream2, dES.CreateDecryptor(bytes, bytes2), CryptoStreamMode.Write);
		while (num < length)
		{
			int num2 = fileStream.Read(array, 0, 100);
			cryptoStream.Write(array, 0, num2);
			num += (long)num2;
		}
		cryptoStream.Close();
		fileStream2.Close();
		fileStream.Close();
	}
}

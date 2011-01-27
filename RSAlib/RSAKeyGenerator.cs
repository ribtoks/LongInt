using System;
using LongInt;
using LongInt.Math;
using LongInt.Math.Special;

namespace RSAlib
{
	public enum BitLength { Length128Bit = 64, Length256Bit = 128, 
		Length512Bit = 256, Length1024Bit = 512, Length2048Bit = 1024 }
	
	public sealed class RSAKeyGenerator
	{
		#region Private data
		
		RandomLong rand;
		PrimeGenerator prGen;
		
		ULongIntB p = null;
		ULongIntB q = null;
		SLongIntB eilerNumber = null;
		
		bool generated = false;
		RSAKey key;
		
		#endregion
		
		public RSAKeyGenerator (ulong seed)
		{
			rand = new RandomLong(seed);
			prGen = new PrimeGenerator(rand);
		}
		
		/// <summary>
		/// Calculates long random prime numbers
		/// </summary>
		/// <param name="bitLength">Length of prime numbers</param>
		private void CalculatePQ(BitLength bitLength)
		{
			ULongIntB min = (ULongIntB)0;
			min.SetBit((uint)bitLength);
			
			ULongIntB max = (ULongIntB)0;
			max.SetBit((uint)bitLength + 1);
			
			p = prGen.Next(min, max);
			
			//min.ShR(1);
			//max.ShR(1);
			
			q = prGen.Next(min, max);
		}
		
		/// <summary>
		/// Provides all operations needed to
		/// get public and private RSA keys. Must 
		/// be called before other methods of this class
		/// </summary>
		/// <param name="bitLength">Length of key</param>
		public void GenerateRSAKey(BitLength bitLength)
		{
			key = new RSAKey();
			// find prime numbers p and q
			CalculatePQ(bitLength);			
			
			// n is product of found numbers
		    key.N = (SLongIntB)(p * q);
			
			--p;
			--q;
			// eiler number equals (p - 1)*(q - 1)
			eilerNumber = (SLongIntB) (p * q);
			++p;
			++q;
			
			SLongIntB tempEiler = eilerNumber - 1;
			
			SLongIntB e = new SLongIntB(rand.NextPQ(p, q), ConstructorMode.Assign);
			while ((CryptoMath.GCD(e, eilerNumber) != 1) || (e > tempEiler))
			{
				e = new SLongIntB(rand.NextPQ(p, q), ConstructorMode.Assign);
			}
			
			key.E = e;
			
			SLongIntB d = null;
			CryptoMath.eXGCDInvertedSafe(e, eilerNumber, out d);
			
			key.D = d;
			
			generated = true;
		}
		
		/// <summary>
		/// Creates public key from calculated data
		/// </summary>
		/// <returns>Public RSA key pair</returns>
		public RSAPublicKey GetPublicKey()
		{
			if (!generated)
				throw new InvalidOperationException("Generate RSA key first!");
			
			return new RSAPublicKey(key.E, key.N);
		}
		
		/// <summary>
		/// Creates private key from calculated data
		/// </summary>
		/// <returns>Private RSA key pair</returns>
		public RSAPrivateKey GetPrivateKey()
		{
			if (!generated)
				throw new InvalidOperationException("Generate RSA key first!");
			
			return new RSAPrivateKey(key.D, key.N);
		}
	}
}
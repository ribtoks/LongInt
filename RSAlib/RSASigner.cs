using System;

using LongInt;
using LongInt.Math;
using LongInt.Math.Special;

namespace RSAlib
{
	public static class RSASigner
	{
		public static ULongIntB Sign(RSAPrivateKey key, ULongIntB message)
		{
			return CryptoMath.ExpMod5(message, (ULongIntB)key.D, (ULongIntB)key.N);
		}
		
		public static bool IsSignValid(RSAPublicKey key, ULongIntB message, ULongIntB checkNumber)
		{
			return (CryptoMath.ExpMod5(checkNumber, (ULongIntB)key.E, (ULongIntB)key.N) == message);
		}
	}
}

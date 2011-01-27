using System;
using LongInt;
using LongInt.Math;
using LongInt.Math.Special;

namespace RSAlib
{
	public static class RSAEncoder
	{
		public static ULongIntB Encode(RSAPublicKey key, ULongIntB number)
		{
			return CryptoMath.ExpMod5(number, (ULongIntB)key.E, (ULongIntB)key.N);
		}
	}
}

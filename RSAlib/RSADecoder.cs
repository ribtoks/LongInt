using System;
using LongInt;
using LongInt.Math;
using LongInt.Math.Special;

namespace RSAlib
{
	public static class RSADecoder
	{
		public static ULongIntB Decode(RSAPrivateKey key, ULongIntB number)
		{
			return CryptoMath.ExpMod5(number, (ULongIntB)key.D, (ULongIntB)key.N);
		}
	}
}
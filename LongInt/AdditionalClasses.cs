using System;
using System.Text;

namespace LongInt
{
	internal static class SignTransformer
	{
		public static bool IsPositive(LSign sign)
		{
			return (sign == LSign.Positive);
		}
		
		public static LSign GetLSign(bool isPositive)
		{
			if (isPositive)
				return LSign.Positive;
			else
				return LSign.Negative;
		}
		
		public static LSign GetLSign(int a)
		{
			if (a >= 0)
				return LSign.Positive;
			else
				return LSign.Negative;
		}
	}
}
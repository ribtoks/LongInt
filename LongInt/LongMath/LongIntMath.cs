using System;
using System.Text;

namespace LongInt.Math
{
	public static class LongIntHelper
	{
		public static ulong Power(uint a, ulong n)
		{
			uint x = a;
			ulong r = 1;
			
			while (n != 0)
			{
				if((n & 1) != 0)
					r *= x;
				n >>= 1;
				x *= x;
			}
			return r;
		}
		
		public static uint CountBits(int a)
		{
			uint k = 0;

			while (a != 0)
			{
				++k;
				a &= a - 1;
			}
			
			return k;
		}
		
		public static void TwoFact(int a, out int k, out int u)
		{
			k = 0;
			u = a;
			
			while ((u & 1) == 0)
			{
				u >>= 1;
				++k;
			}
		}
	}
	
	public static partial class LongMath
	{
		#region Internal operations
		
		internal static LongIntBase BuildFromInt(ulong number, uint Base, uint MaxDigits)
		{
			LongIntBase lNumber = new LongIntBase(MaxDigits);
			
			int numberLength = IntLength(number);
			int baseLength = IntLength(Base);
			
			lNumber.Size = (numberLength / baseLength) + Convert.ToInt32(numberLength % baseLength != 0);
			
			ulong tmp = number;
			for (int i = 0; i < lNumber.Size; ++i)
			{
				lNumber[i] = (ushort) (tmp % (ulong)Base);
				tmp /= (ulong)Base;
			}
			
			return lNumber;
		}
		
		internal static int GetNthDigit(LongIntBase lNumber, uint n)
		{
			int k;
	  		if (n <= 4)
		  		k = 0;
	  		else
		  		k = (int)((n - 1) >> 2);
			ushort number = lNumber[k];
			int tmp = (int)(n - (k << 2));
			for (ushort i = 0; i < tmp - 1; ++i)
				number /= 10;
			return number % 10;
		}
		
		internal static LongIntBase InnerSqr(LongIntBase A)
		{
            if (A.Size == 1)
                if (A[0] == 1 || A[0] == 0)
                    return new LongIntBase(A, A.CoefLength);
			
			uint Base = A.Base;

			LongIntBase Result = null;
			
			if (A.Size * A.Size < A.CoefLength)
				Result = new LongIntBase(A.Base, A.CoefLength);
			else
				Result = new LongIntBase(A.Base, (uint)(A.Size * (A.Size + 1)));
			
			int i = 0, j;
			uint temp;
			uint carry = 0;
			
			// product of cells with not equal indices
			for (i = 0; i < A.Size - 1; ++i)
			{
				carry = 0;
				for (j = i + 1; j < A.Size; ++j)
				{
					temp = Result[i + j] + (uint)A[i] * (uint)A[j] + carry;
					carry = temp / Base;
					Result[i + j] = (ushort)(temp - carry * Base);
				}
				Result[i + A.Size] = (ushort)carry;
			}
			
			// double inner products
			carry = 0;
			for (i = 1; i < 2*A.Size - 1; ++i)
			{
				temp = 2*(uint)Result[i] + carry;
				carry = temp / Base;
				Result[i] = (ushort)(temp - carry * Base);
			}
			Result[2 * A.Size - 1] = (ushort)carry;
			
			carry = 0;
			// sum inner squares
			for (i = 0; i < A.Size; ++i)
			{
				temp = Result[2 * i] + (uint)A[i] * (uint)A[i] + carry;
				carry = temp / Base;
				Result[2 * i] = (ushort)(temp - carry * Base);
				
				temp = Result[2 * i + 1] + carry;
				carry = temp / Base;
				Result[2*i + 1] = (ushort)(temp - carry * Base);
			}
			
			Result[2 * A.Size - 1] += (ushort)carry;

            i = 2 * A.Size + 2;

            while (Result[i] == 0)
			{
                --i;
				if (i == 0)
					break;
			}
			
            Result.Size = i + 1;
			return Result;
		}
		
		internal static int IntLength(ulong a)
		{
			if (a == 0)
				return 1;
			
			int i = 0;
			while (a != 0)
			{
				a /= 10;
				++i;
			}
			
			return i;
		}
				
		#endregion
	
		#region Divide region
		
		public static void Divide(ULongIntD A, ULongIntD B, out ULongIntD Q, out ULongIntD R)
		{
			LongIntBase QNumber = new LongIntBase(A.Base);
			LongIntBase RNumber = new LongIntBase(B.Base);
			
			LongMath.Divide(A, B, out QNumber, out RNumber, true, true);
			Q = new ULongIntD(QNumber, ConstructorMode.Assign);
			R = new ULongIntD(RNumber, ConstructorMode.Assign);
		}
		
		public static void Divide(SLongIntD A, SLongIntD B, out SLongIntD Q, out SLongIntD R)
		{
			LongIntBase QNumber = new LongIntBase(A.Base);
			LongIntBase RNumber = new LongIntBase(B.Base);
			
			LSign resultSign = SignTransformer.GetLSign(A.Sign * B.Sign);
			
			LongMath.Divide(A, B, out QNumber, out RNumber, true, true);
			Q = new SLongIntD(QNumber, resultSign, ConstructorMode.Assign);
			R = new SLongIntD(RNumber, resultSign, ConstructorMode.Assign);
		}
		
		public static void Divide(ULongIntB A, ULongIntB B, out ULongIntB Q, out ULongIntB R)
		{
			LongIntBase QNumber = new LongIntBase(A.Base);
			LongIntBase RNumber = new LongIntBase(B.Base);
			
			LongMath.Divide(A, B, out QNumber, out RNumber, true, true);
			Q = new ULongIntB(QNumber, ConstructorMode.Assign);
			R = new ULongIntB(RNumber, ConstructorMode.Assign);
		}
		
		public static void Divide(SLongIntB A, SLongIntB B, out SLongIntB Q, out SLongIntB R)
		{
			LongIntBase QNumber = new LongIntBase(A.Base);
			LongIntBase RNumber = new LongIntBase(B.Base);
			
			LSign resultSign = SignTransformer.GetLSign(A.Sign * B.Sign);
			
			LongMath.Divide(A, B, out QNumber, out RNumber, true, true);
			Q = new SLongIntB(QNumber, resultSign, ConstructorMode.Assign);
			R = new SLongIntB(RNumber, resultSign, ConstructorMode.Assign);
		}
		
		#endregion
				
		public static void SelfAbs(ILongSigned number)
		{
			if (number.LongSign == LSign.Negative)
				number.LongSign = LSign.Positive;
		}
		
		/// <summary>
		/// Finds absolute value of input number
		/// </summary>
		/// <param name="A">Input number</param>
		/// <returns>Absolute number of A</returns>
		public static SLongIntB Abs(SLongIntB A)
		{
			SLongIntB temp = new SLongIntB(A);
			SelfAbs(temp);
			return temp;
		}
		
		/// <summary>
		/// Finds absolute value of input number
		/// </summary>
		/// <param name="A">Input number</param>
		/// <returns>Absolute number of A</returns>
		public static SLongIntD Abs(SLongIntD A)
		{
			SLongIntD temp = new SLongIntD(A);
			SelfAbs(temp);
			return temp;
		}
		
		#region SQR-region
		
		/// <summary>
		/// Calculates A*A
		/// </summary>
		/// <param name="A">Long Number</param>
		/// <returns>Long number that is result of self-product of A</returns>
		public static ULongIntD Sqr(ULongIntD A)
		{
			return new ULongIntD(LongMath.InnerSqr(A), ConstructorMode.Assign);
		}
		
		/// <summary>
		/// Calculates A*A
		/// </summary>
		/// <param name="A">Long Number</param>
		/// <returns>Long number that is result of self-product of A</returns>
		public static ULongIntB Sqr(ULongIntB A)
		{
			return new ULongIntB(LongMath.InnerSqr(A), ConstructorMode.Assign);
		}
		
		/// <summary>
		/// Calculates A*A
		/// </summary>
		/// <param name="A">Long Number</param>
		/// <returns>Long number that is result of self-product of A</returns>
		public static SLongIntD Sqr(SLongIntD A)
		{
			return new SLongIntD(LongMath.InnerSqr(A), LSign.Positive, ConstructorMode.Assign);
		}
		
		/// <summary>
		/// Calculates A*A
		/// </summary>
		/// <param name="A">Long Number</param>
		/// <returns>Long number that is result of self-product of A</returns>
		public static SLongIntB Sqr(SLongIntB A)
		{
			return new SLongIntB(LongMath.InnerSqr(A), LSign.Positive, ConstructorMode.Assign);
		}
		
		#endregion
		
		public static ULongIntD Sqrt(ULongIntD A)
		{
			throw new NotImplementedException("Sqrt() is not implemented yet!");
			/*
			ULongIntD currentResult = null;
			
			return currentResult;
			*/
		}
		
		#region Exp region
		
		public static ULongIntD Exp(ULongIntD A, ulong n)
		{
			return new ULongIntD(InnerPower(A, n), ConstructorMode.Assign);
		}
		
		public static SLongIntD Exp(SLongIntD A, ulong n)
		{
			return new SLongIntD(InnerPower(A, n), 
			                     SignTransformer.GetLSign(A.IsPositive || (n % 2 == 0)), ConstructorMode.Assign);
		}
		
		public static ULongIntB Exp(ULongIntB A, ulong n)
		{
			return new ULongIntB(InnerPower(A, n), ConstructorMode.Assign);
		}
		
		public static SLongIntB Exp(SLongIntB A, ulong n)
		{
			return new SLongIntB(InnerPower(A, n), 
			                     SignTransformer.GetLSign(A.IsPositive || (n % 2 == 0)), ConstructorMode.Assign);
		}
		
		#endregion
		
		#region Params region
		
		public static ULongIntB Product(params ULongIntB[] numbers)
		{
			ULongIntB res = (ULongIntB)1;
			
			foreach (ULongIntB number in numbers)
				res *= number;
			
			return res;
		}
		
		public static SLongIntB Product(params SLongIntB[] numbers)
		{
			SLongIntB res = (SLongIntB)1;
			
			foreach (SLongIntB number in numbers)
				res *= number;
			
			return res;
		}
		
		public static ULongIntB Sum(params ULongIntB[] numbers)
		{
			ULongIntB res = (ULongIntB)1;
			
			foreach (ULongIntB number in numbers)
				res += number;
			
			return res;
		}
		
		public static SLongIntB Sum(params SLongIntB[] numbers)
		{
			SLongIntB res = (SLongIntB)1;
			
			foreach (SLongIntB number in numbers)
				res += number;
			
			return res;
		}
		
		public static ULongIntB Max(params ULongIntB[] numbers)
		{
			ULongIntB max = numbers[0];
			
			for (int i = 1; i < numbers.Length; ++i)
				if (numbers[i] > max)
					max = numbers[i];
			
			return max;
		}
		
		public static SLongIntB Max(params SLongIntB[] numbers)
		{
			SLongIntB max = numbers[0];
			
			for (int i = 1; i < numbers.Length; ++i)
				if (numbers[i] > max)
					max = numbers[i];
			
			return max;
		}
		
		public static ULongIntB Min(params ULongIntB[] numbers)
		{
			ULongIntB min = numbers[0];
			
			for (int i = 1; i < numbers.Length; ++i)
				if (numbers[i] < min)
					min = numbers[i];
			
			return min;
		}
		
		public static SLongIntB Min(params SLongIntB[] numbers)
		{
			SLongIntB min = numbers[0];
			
			for (int i = 1; i < numbers.Length; ++i)
				if (numbers[i] < min)
					min = numbers[i];
			
			return min;
		}
		
		#endregion
		
		public static bool IsEven(LongIntBase A)
		{
			return ((A[0] & 1) == 0);
		}
		
		public static bool IsOdd(LongIntBase A)
		{
			return ((A[0] & 1) == 1);
		}
	}
}

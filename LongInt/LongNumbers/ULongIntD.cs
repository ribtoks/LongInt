using System;
using LongInt.Math;

namespace LongInt
{
	/// <summary>
	/// Class, that represents long numbers
	/// with BASE 10^n
	/// 
	/// class Unsigned Long integer decimal based
	public sealed class ULongIntD : LongIntDecimal, ILongUnsigned
	{
		#region Constructors
		
		public ULongIntD ()
			:base()
		{
		}
		
		public ULongIntD (uint MaxSize)
			:base(MaxSize)
		{
		}
		
		public ULongIntD (ulong number, uint MaxSize)
			:base(number, MaxSize)
		{
		}
		
		public ULongIntD (ULongIntD From)
			:base((LongIntDecimal)From)
		{
		}
		
		public ULongIntD (LongIntBase FromLongNumber, ConstructorMode mode)
			:base(FromLongNumber, mode)
		{
		}
		
		public ULongIntD (LongIntDecimal From, ConstructorMode mode)
			:base(From, mode)
		{
		}
		
		public ULongIntD (string s)
			:base(s)
		{
		}
		
		public ULongIntD (char[] array)
			:base(array)
		{
		}
		
		public ULongIntD (char[] array, uint MaxSize)
			:base(array, MaxSize)
		{
		}
		
		#endregion
		
		#region Overriden functions
		
		/// <summary>
		/// Checks if current long number 
		/// is equal to input parameter
		/// </summary>
		/// <param name="obj">Input long number</param>
		/// <returns>True, if numbers are equal, otherwise false</returns>
		public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            ULongIntD lint = (ULongIntD)obj;
			
			//convert to object, because 
			//operator == can be called with 'null'
            if ((object)lint == null)
                return false;
			
			return LongMath.Equal(this, lint);
        }
		
		public override int GetHashCode ()
		{
			return base.GetHashCode();
		}
		
		// <summary>
		/// Transforms long number from inner representation
		/// to string with decimal number
		/// </summary>
		/// <returns>System.String with long number</returns>
		public override string ToString ()
		{
			return base.ToString();
		}
		
		#endregion
		
		public ILongSigned GetSignedClone()
		{
			return new SLongIntD(this, ConstructorMode.Copy);
		}
		
		#region Bool operators
		
		// checks if two long numbers are equal
		public static bool operator ==(ULongIntD A, ULongIntD B)
		{
			return LongMath.Equal(A, B);
		}
		
		// checks if two long numbers are not equal
		public static bool operator !=(ULongIntD A, ULongIntD B)
		{
			return !LongMath.Equal(A, B);
		}
		
		// checks if long number is equal to short number
		public static bool operator ==(ULongIntD A, ushort b)
		{
			return LongMath.Equal(A, b);
		}
		
		// checks if long number is not equal to short number
		public static bool operator !=(ULongIntD A, ushort b)
		{
			return !LongMath.Equal(A, b);
		}
		
		// checks if long number is less than other long number
		public static bool operator < (ULongIntD A, ULongIntD B)
		{
			return LongMath.Less(A, B);
		}
		
		// checks if long number is less than short number
		public static bool operator < (ULongIntD A, ushort b)
		{
			return LongMath.Less(A, b);
		}
		
		// checks if long number is greater than other long number
		public static bool operator > (ULongIntD A, ULongIntD B)
		{
			return LongMath.Greater(A, B);
		}
		
		// checks if long number is greater, than short number
		public static bool operator > (ULongIntD A, ushort b)
		{
			return LongMath.Greater(A, b);
		}
		
		#endregion
		
		#region Math Operators
		
		// calculates sum of two unsigned long numbers
		public static ULongIntD operator + (ULongIntD A, ULongIntD B)
		{
			return new ULongIntD(LongMath.Add(A, B), ConstructorMode.Assign);
		}
		
		// calculates sum of unsigned long number and short number
		public static ULongIntD operator + (ULongIntD A, ushort b)
		{
			return new ULongIntD(LongMath.Add(A, b), ConstructorMode.Assign);
		}
		
		// calculates sum of unsigned long number and short number
		public static ULongIntD operator + (ushort b, ULongIntD A)
		{
			return new ULongIntD(LongMath.Add(A, b), ConstructorMode.Assign);
		}
		
		// calculates difference between two long numbers
		public static ULongIntD operator - (ULongIntD A, ULongIntD B)
		{
			if (A.Size < B.Size)
				throw new NegativeResultException("Class ULongIntD does not support negative numbers!");
			
			return new ULongIntD(LongMath.SubstractSafe(A, B), ConstructorMode.Assign);
		}
		
		// calculates product of unsigned long number and unsigned short number
		public static ULongIntD operator * (ushort b, ULongIntD A)
		{
			if (b == 1)
				return new ULongIntD(A);
			
			return new ULongIntD(LongMath.UShortMul(A, b), ConstructorMode.Assign);
		}
		
		// calculates product of unsigned long number and unsigned short number
		public static ULongIntD operator * (ULongIntD A, ushort b)
		{
			if (b == 1)
				return new ULongIntD(A);
			
			return new ULongIntD(LongMath.UShortMul(A, b), ConstructorMode.Assign);
		}
		
		// calculates product of two long numbers
		public static ULongIntD operator * (ULongIntD A, ULongIntD B)
		{
			return new ULongIntD(LongMath.Multiply(A, B), ConstructorMode.Assign);
		}
		
		// calculates result of division of long number on short number
		public static ULongIntD operator / (ULongIntD A, ushort b)
		{
			if (b == 1)
				return new ULongIntD(A);
			
			ushort r = 0;
			LongIntBase Q = new LongIntBase(A.Base);
			
			LongMath.UShortDiv(A, b, out Q, out r);
			return new ULongIntD(Q, ConstructorMode.Assign);
		}
		
		// calculates result of division of two long numbers
		public static ULongIntD operator / (ULongIntD A, ULongIntD B)
		{
			LongIntBase Q = new LongIntBase(A.Base);
			LongIntBase R = new LongIntBase(B.Base);
			
			LongMath.Divide(A, B, out Q, out R, true, false);
			return new ULongIntD(Q, ConstructorMode.Assign);
		}
		
		// calculates remainder of division of long number on short number
		public static ushort operator % (ULongIntD A, ushort b)
		{
			ushort r = 0;
			LongIntBase Q = new LongIntBase(A.Base);
			
			LongMath.UShortDiv(A, b, out Q, out r);
			return r;
		}
		
		// calculates remainder of division of two long numbers
		public static ULongIntD operator % (ULongIntD A, ULongIntD B)
		{
			LongIntBase Q = new LongIntBase(A.Base);
			LongIntBase R = new LongIntBase(B.Base);
			
			LongMath.Divide(A, B, out Q, out R, false, true);
			return new ULongIntD(R, ConstructorMode.Assign);
		}
		
		public static ULongIntD operator ++(ULongIntD A)
		{
			LongIntBase tempA = A;
			LongMath.Increment(ref tempA, 1);
			return A;
		}
		
		public static ULongIntD operator --(ULongIntD A)
		{
			if (A > 0)
			{
				LongIntBase tempA = A;
				LongMath.DecrementSafe(ref tempA, 1);
			}
			else
				throw new NegativeResultException("Unsigned value can not be less, than zero!");
			return A;
		}
		
		#endregion
		
		#region Other operators
		
		// converts Unsigned Long Number to Signed long number
		// Explicit convertation
		public static explicit operator SLongIntD (ULongIntD From)
		{
			return new SLongIntD(From, LSign.Positive, ConstructorMode.Assign);
		}
		
		// converts Unsigned short Number to Unsigned long number
		// Explicit convertation
		public static explicit operator ULongIntD (int From)
		{
			return new ULongIntD((ulong)From, LongIntBase.MaxRange);
		}
		
		public static explicit operator ULongIntB (ULongIntD From)
		{
			ULongIntB result = new ULongIntB(From[0], LongIntBase.MaxRange);			
			ULongIntB oldBase = new ULongIntB(From.Base, LongIntBase.MaxRange);
			ULongIntB oldBaseTemp = new ULongIntB((ulong)From.Base, LongIntBase.MaxRange);
			
			for (int i = 1; i < From.Size; ++i)
			{
				result += oldBase*From[i];				
				oldBase *= oldBaseTemp;
			}
			
			return result;
		}
		
		#endregion
	}
}

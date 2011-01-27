using System;
using LongInt.Math;

namespace LongInt
{
	public class ULongIntB : LongIntBinary, ILongUnsigned
	{
		#region Constructors
		
		public ULongIntB ()
			:base()
		{
		}
		
		public ULongIntB (uint MaxSize)
			:base(MaxSize)
		{
		}
		
		public ULongIntB (ulong number, uint MaxSize)
			:base(number, MaxSize)
		{
		}
		
		public ULongIntB (ULongIntB From)
			:base((LongIntBinary)From)
		{
		}
		
		public ULongIntB (LongIntBase FromLongNumber, ConstructorMode mode)
			:base(FromLongNumber, mode)
		{
		}
		
		public ULongIntB (LongIntBinary From, ConstructorMode mode)
			:base(From, mode)
		{
		}
		
		public ULongIntB (string s)
			:base(s)
		{
		}
		
		public ULongIntB (char[] array)
			:base(array)
		{
		}
		
		public ULongIntB (char[] array, uint MaxSize)
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

            ULongIntB lint = (ULongIntB)obj;
			
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
		
		public override string ToString ()
		{
			return base.ToString();
		}
		
		#endregion		
		
		public ILongSigned GetSignedClone()
		{
			return new SLongIntB(this, ConstructorMode.Copy);
		}

				
		#region Bool operators
		
		// checks if two long numbers are equal
		public static bool operator ==(ULongIntB A, ULongIntB B)
		{
			return LongMath.Equal(A, B);
		}
		
		// checks if two long numbers are not equal
		public static bool operator !=(ULongIntB A, ULongIntB B)
		{
			return !LongMath.Equal(A, B);
		}
		
		// checks if long number is equal to short number
		public static bool operator ==(ULongIntB A, ushort b)
		{
			return LongMath.Equal(A, b);
		}
		
		// checks if long number is not equal to short number
		public static bool operator !=(ULongIntB A, ushort b)
		{
			return !LongMath.Equal(A, b);
		}
		
		// checks if long number is less than other long number
		public static bool operator < (ULongIntB A, ULongIntB B)
		{
			return LongMath.Less(A, B);
		}
		
		// checks if long number is less than short number
		public static bool operator < (ULongIntB A, ushort b)
		{
			return LongMath.Less(A, b);
		}
		
		// checks if long number is greater than other long number
		public static bool operator > (ULongIntB A, ULongIntB B)
		{
			return LongMath.Greater(A, B);
		}
		
		// checks if long number is greater, than short number
		public static bool operator > (ULongIntB A, ushort b)
		{
			return LongMath.Greater(A, b);
		}
		
		#endregion
		
		#region Math Operators
		
		// calculates sum of two unsigned long numbers
		public static ULongIntB operator + (ULongIntB A, ULongIntB B)
		{
			return new ULongIntB(LongMath.Add(A, B), ConstructorMode.Assign);
		}
		
		// calculates sum of unsigned long number and short number
		public static ULongIntB operator + (ULongIntB A, ushort b)
		{
			return new ULongIntB(LongMath.Add(A, b), ConstructorMode.Assign);
		}
		
		// calculates sum of unsigned long number and short number
		public static ULongIntB operator + (ushort b, ULongIntB A)
		{
			return new ULongIntB(LongMath.Add(A, b), ConstructorMode.Assign);
		}
		
		// calculates difference between two long numbers
		public static ULongIntB operator - (ULongIntB A, ULongIntB B)
		{
			if (A.Size < B.Size)
				throw new NegativeResultException("Class ULongIntD does not support negative numbers!");
			
			return new ULongIntB(LongMath.SubstractSafe(A, B), ConstructorMode.Assign);
		}
		
		// calculates product of unsigned long number and unsigned short number
		public static ULongIntB operator * (ushort b, ULongIntB A)
		{
			if (b == 1)
				return new ULongIntB(A);
			
			return new ULongIntB(LongMath.UShortMul(A, b), ConstructorMode.Assign);
		}
		
		// calculates product of unsigned long number and unsigned short number
		public static ULongIntB operator * (ULongIntB A, ushort b)
		{
			if (b == 1)
				return new ULongIntB(A);
			
			return new ULongIntB(LongMath.UShortMul(A, b), ConstructorMode.Assign);
		}
		
		// calculates product of two long numbers
		public static ULongIntB operator * (ULongIntB A, ULongIntB B)
		{
			return new ULongIntB(LongMath.Multiply(A, B), ConstructorMode.Assign);
		}
		
		// calculates result of division of long number on short number
		public static ULongIntB operator / (ULongIntB A, ushort b)
		{
			if (b == 1)
				return new ULongIntB(A);
			
			ushort r = 0;
			LongIntBase Q = new LongIntBase(A.Base);
			
			LongMath.UShortDiv(A, b, out Q, out r);
			return new ULongIntB(Q, ConstructorMode.Assign);
		}
		
		// calculates result of division of two long numbers
		public static ULongIntB operator / (ULongIntB A, ULongIntB B)
		{
			LongIntBase Q = new LongIntBase(A.Base);
			LongIntBase R = new LongIntBase(B.Base);
			
			LongMath.Divide(A, B, out Q, out R, true, false);
			return new ULongIntB(Q, ConstructorMode.Assign);
		}
		
		// calculates remainder of division of long number on short number
		public static ushort operator % (ULongIntB A, ushort b)
		{
			ushort r = 0;
			LongIntBase Q = new LongIntBase(A.Base);
			
			LongMath.UShortDiv(A, b, out Q, out r);
			return r;
		}
		
		// calculates remainder of division of two long numbers
		public static ULongIntB operator % (ULongIntB A, ULongIntB B)
		{
			LongIntBase Q = new LongIntBase(A.Base);
			LongIntBase R = new LongIntBase(B.Base);
			
			LongMath.Divide(A, B, out Q, out R, false, true);
			return new ULongIntB(R, ConstructorMode.Assign);
		}
		
		public static ULongIntB operator ++(ULongIntB A)
		{
			LongIntBase tempA = A;
			LongMath.Increment(ref tempA, 1);
			return A;
		}
		
		public static ULongIntB operator --(ULongIntB A)
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
		
		// converts Unsigned Long decimal Number to Unsigned long binary number
		// Explicit convertation
		public static explicit operator ULongIntD (ULongIntB From)
		{
			ULongIntB temp = new ULongIntB(From);
			
			LongIntBase tempInner = null;
			
			ULongIntD number = (ULongIntD)0;
			//remain
			ushort r = 0;
			int index = 0;
			
			while (temp != 0)
			{
				tempInner = temp;
				LongMath.UShortDivSafe(temp, (ushort)number.Base, ref tempInner, ref r);
				number[index] = r;
				++index;
			}
			
			number.Size = index;
			
			return number;
		}
		
		public static explicit operator ULongIntB (int From)
		{
			return new ULongIntB((ulong)From, LongIntBase.MaxRange);
		}
		
		public static explicit operator SLongIntB(ULongIntB From)
		{
			return new SLongIntB(From, ConstructorMode.Assign);
		}
		
		#endregion
	}
}

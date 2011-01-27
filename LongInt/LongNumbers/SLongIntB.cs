using System;
using LongInt.Math;

namespace LongInt
{
	public sealed class SLongIntB : LongIntBinary, ILongSigned
	{
		private LSign sign = LSign.Positive;
		
		#region Constructors
		
		public SLongIntB ()
			:base()
		{
		}
		
		public SLongIntB (uint MaxSize)
			:base(MaxSize)
		{
		}
		
		public SLongIntB (long number, uint MaxSize)
			:base( (ulong)((number < 0) ? (-number) : number), MaxSize)
		{
			if (number < 0)
				sign = LSign.Negative;
		}
		
		public SLongIntB (SLongIntB From)
			:base((LongIntBinary)From)
		{
			sign = From.sign;
		}
		
		public SLongIntB (string s)
			:base(s)
		{
			if (s[0] == '-')
				sign = LSign.Negative;
		}
		
		public SLongIntB (uint MaxSize, LSign lSign)
			: this(MaxSize)
		{
			sign = lSign;
		}
		
		public SLongIntB (LongIntBase FromLongNumber, ConstructorMode mode)
			:base(FromLongNumber, mode)
		{
		}
		
		public SLongIntB (LongIntBase FromLongNumber, LSign lSign, ConstructorMode mode)
			:this(FromLongNumber, mode)
		{
			sign = lSign;
		}
		
		public SLongIntB (LongIntBinary From, LSign lSign, ConstructorMode mode)
			:base(From, mode)
		{
			sign = lSign;
		}

		public SLongIntB (LongIntBinary From, ConstructorMode mode)
			:this(From, LSign.Positive, mode)
		{
		}
		
		public SLongIntB (char[] array)
			:base(array)
		{
		}
		
		public SLongIntB (char[] array, uint MaxSize)
			:base(array, MaxSize)
		{
		}
		
		#endregion
		
		#region Properties
		
		/// <summary>
		/// Sign of long number
		/// </summary>
		public int Sign
		{
			get 
			{ 
				if (size == 0)
					return 0;
				return (int)sign;
			}
		}
		
		public LSign LongSign
		{
			get { return sign; }
			set { sign = value; }
		}
		
		public bool IsPositive
		{
			get { return sign == LSign.Positive; }
		}
		
		#endregion
		
		public void ChangeSign()
		{
			if (sign == LSign.Negative)
				sign = LSign.Positive;
			else
				sign = LSign.Negative;
		}
		
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

            SLongIntB lint = (SLongIntB)obj;
			
			//convert to object, because 
			//operator == can be called with 'null'
            if ((object)lint == null)
                return false;
			
			if (this.LongSign != lint.LongSign)
				return false;
			
			return LongMath.Equal(this, lint);
        }
		
		public override int GetHashCode ()
		{
			return base.GetHashCode();
		}
		
		/// <summary>
		/// Transforms long number from inner representation
		/// to string with decimal number
		/// </summary>
		/// <returns>System.String with long number</returns>
		public override string ToString ()
		{
			return (sign == LSign.Negative ? "-" : "") + base.ToString();
		}		
		
		public override int Shr ()
		{
			int result = base.Shr ();
			
			if (IsZero())
				sign = LSign.Positive;
			
			return result;
		}
		
		public override void Mod2n (uint n)
		{
			base.Mod2n (n);
			if (sign == LSign.Negative)
				sign = LSign.Positive;
		}
		
		public ILongUnsigned GetUnsignedClone()
		{
			return new ULongIntB(this, ConstructorMode.Copy);
		}
		
		public void Assign(SLongIntB What)
		{
			if (What.size <= 0)
				return;
			
			int i = 0;
			
			for (i = 0; i < What.size; ++i)
				coef[i] = What.coef[i];
			
			for (; i < size; ++i)
				coef[i] = 0;
			
			size = What.size;			
			sign = What.sign;
		}
		
		#endregion
		
		#region Bool Operators
		
		// checks if two signed long numbers are equal
		public static bool operator ==(SLongIntB A, SLongIntB B)
		{
			return LongMath.Equal(A, A.IsPositive, B, B.IsPositive);
		}
		
		// checks if two signed long numbers are not equal
		public static bool operator !=(SLongIntB A, SLongIntB B)
		{
			return !LongMath.Equal(A, A.IsPositive, B, B.IsPositive);
		}
		
		// checks if long number is equal to short number
		public static bool operator ==(SLongIntB A, short b)
		{
			return LongMath.Equal(A, A.IsPositive, b);
		}
		
		// checks if signed long number is not equal to short number
		public static bool operator !=(SLongIntB A, short b)
		{
			return !LongMath.Equal(A, A.IsPositive, b);
		}
		
		// checks if signed long number is less than other long number
		public static bool operator < (SLongIntB A, SLongIntB B)
		{
			return LongMath.Less(A, A.IsPositive, B, B.IsPositive);
		}
		
		// checks if signed long number is less than short number
		public static bool operator < (SLongIntB A, short b)
		{
			return LongMath.Less(A, A.IsPositive, b);
		}
		
		// checks if signed long number is greater than other long number
		public static bool operator > (SLongIntB A, SLongIntB B)
		{
			return LongMath.Greater(A, A.IsPositive, B, B.IsPositive);
		}
		
		// checks if long number is greater, than short number
		public static bool operator > (SLongIntB A, short b)
		{
			return LongMath.Greater(A, A.IsPositive, b);
		}
		
		#endregion
		
		#region Math Operators
		
		// calculates sum of two signed long numbers
		public static SLongIntB operator + (SLongIntB A, SLongIntB B)
		{
			bool sign = false;
			LongIntBase temp = LongMath.Add(A, A.IsPositive, B, B.IsPositive, out sign);
			
			return new SLongIntB(temp, SignTransformer.GetLSign(sign), ConstructorMode.Assign);
		}

		public static SLongIntB operator + (SLongIntB A, short b)
		{
			bool sign = false;
			LongIntBase temp = LongMath.Add(A, A.IsPositive, b, out sign);
			
			return new SLongIntB(temp, SignTransformer.GetLSign(sign), ConstructorMode.Assign);
		}
		
		public static SLongIntB operator + (short b, SLongIntB A)
		{
			bool sign = false;
			LongIntBase temp = LongMath.Add(A, A.IsPositive, b, out sign);
			
			return new SLongIntB(temp, SignTransformer.GetLSign(sign), ConstructorMode.Assign);
		}
		
		// calculates difference between two signed long numbers
		public static SLongIntB operator - (SLongIntB A, SLongIntB B)
		{
			bool sign = false;
			LongIntBase temp = LongMath.Substract(A, A.IsPositive, B, B.IsPositive, out sign);
			
			return new SLongIntB(temp, SignTransformer.GetLSign(sign), ConstructorMode.Assign);
		}
		
		public static SLongIntB operator - (SLongIntB A, short b)
		{
			bool sign = false;
			LongIntBase temp = LongMath.Substract(A, A.IsPositive, b, out sign);
			
			return new SLongIntB(temp, SignTransformer.GetLSign(sign), ConstructorMode.Assign);
		}
		
		// calculates product of signed long number and short number
		public static SLongIntB operator * (SLongIntB A, short b)
		{
			bool sign = false;
			LongIntBase temp = LongMath.Multiply(A, A.IsPositive, b, out sign);
			
			return new SLongIntB(temp, SignTransformer.GetLSign(sign), ConstructorMode.Assign);
		}
		
		// calculates product of signed long number and short number
		public static SLongIntB operator * (short b, SLongIntB A)
		{
			bool sign = false;
			LongIntBase temp = LongMath.Multiply(A, A.IsPositive, b, out sign);
			
			return new SLongIntB(temp, SignTransformer.GetLSign(sign), ConstructorMode.Assign);
		}
		
		// calculates product of two signed long numbers
		public static SLongIntB operator * (SLongIntB A, SLongIntB B)
		{
			bool sign = false;
			LongIntBase temp = LongMath.Multiply(A, A.IsPositive, B, B.IsPositive, out sign);
			
			return new SLongIntB(temp, SignTransformer.GetLSign(sign), ConstructorMode.Assign);
		}
		
		// calculates result of division of signed long number on short number
		public static SLongIntB operator / (SLongIntB A, short b)
		{
			if (System.Math.Abs(b) == 1)
			{
				SLongIntB res = new SLongIntB(A);
				
				if (b < 0)
					res.ChangeSign();
				
				return res;
			}
			
			bool sign = false;
			LongIntBase temp = LongMath.Divide(A, A.IsPositive, b, out sign);
			
			return new SLongIntB(temp, SignTransformer.GetLSign(sign), ConstructorMode.Assign);
		}
		
		// calculates result of division of two signed long numbers
		public static SLongIntB operator / (SLongIntB A, SLongIntB B)
		{
			bool sign = false;
			LongIntBase temp = LongMath.Divide(A, A.IsPositive, B, B.IsPositive, out sign);
			
			return new SLongIntB(temp, SignTransformer.GetLSign(sign), ConstructorMode.Assign);
		}
		
		// calculates remainder of division of signed long number on short number
		public static ushort operator % (SLongIntB A, ushort b)
		{
			return LongMath.Mod(A, b);
		}
		
		// calculates remainder of division of two signed long numbers
		public static SLongIntB operator % (SLongIntB A, SLongIntB B)
		{
			//return new SLongIntB(LongMath.Mod(A, B), SignTransformer.GetLSign(!(A.IsPositive ^ B.IsPositive)), ConstructorMode.Assign);
			return new SLongIntB(LongMath.Mod(A, B), ConstructorMode.Assign);
		}
		
		public static SLongIntB operator ++(SLongIntB A)
		{
			LongIntBase tempA = A;
			
			if (A < 0)
			{
				LongMath.DecrementSafe(ref tempA, 1);
				
				if (tempA.IsZero())
					A.sign = LSign.Positive;
			}
			else
				LongMath.Increment(ref tempA, 1);
			return A;
		}
		
		public static SLongIntB operator --(SLongIntB A)
		{
			LongIntBase tempA = A;
			if (A > 0)
				LongMath.DecrementSafe(ref tempA, 1);
			else
			{
				if (A.IsPositive)
					A.sign = LSign.Negative;
				
				LongMath.Increment(ref tempA, 1);
			}
			return A;
		}
		
		#endregion
		
		#region Other operators
		
		// converts Unsigned Long decimal Number to Unsigned long binary number
		// Explicit convertation
		public static explicit operator SLongIntD (SLongIntB From)
		{
			SLongIntB temp = new SLongIntB(From);
			
			LongIntBase tempInner = null;
			
			SLongIntD number = (SLongIntD)0;
			//remain
			ushort r = 0;
			int index = 0;
			
			while (!LongMath.Equal(temp, 0))
			{
				tempInner = temp;
				LongMath.UShortDivSafe(temp, (ushort)number.Base, ref tempInner, ref r);
								
				number[index] = r;
				++index;
			}
			
			number.Size = index;
			
			if (number.LongSign != From.sign)
				number.ChangeSign();
			
			return number;
		}
		
		public static explicit operator SLongIntB (int From)
		{
			return new SLongIntB(From, LongIntBase.MaxRange);
		}
		
		public static explicit operator ULongIntB(SLongIntB From)
		{
			return new ULongIntB(From, ConstructorMode.Assign);
		}
		
		#endregion
	}
}

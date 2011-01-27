using System;
using LongInt.Math;

namespace LongInt
{
	/// <summary>
	/// Class, that represents 
	/// Singed Long Integer Numbers
	/// </summary>
	public sealed class SLongIntD : LongIntDecimal, ILongSigned
	{
		private LSign sign = LSign.Positive;
		
		#region Constructors
		
		public SLongIntD ()
			:base()
		{
		}
		
		public SLongIntD (uint MaxSize)
			:base(MaxSize)
		{
		}
		
		public SLongIntD (long number, uint MaxSize)
			:base( (ulong)((number < 0) ? (-number) : number), MaxSize)
		{
			if (number < 0)
				sign = LSign.Negative;
		}
		
		public SLongIntD (SLongIntD From)
			:base((LongIntDecimal)From)
		{
			sign = From.sign;
		}
		
		public SLongIntD (string s)
			:base(s)
		{
			if (s[0] == '-')
				sign = LSign.Negative;
		}
		
		public SLongIntD (uint MaxSize, LSign lSign)
			: this(MaxSize)
		{
			sign = lSign;
		}
		
		public SLongIntD (LongIntBase FromLongNumber, ConstructorMode mode)
			:base(FromLongNumber, mode)
		{
		}
		
		public SLongIntD (LongIntBase FromLongNumber, LSign lSign, ConstructorMode mode)
			:this(FromLongNumber, mode)
		{
			sign = lSign;
		}
		
		public SLongIntD (LongIntDecimal From, ConstructorMode mode)
			:this(From, LSign.Positive, mode)
		{
		}

		public SLongIntD (LongIntDecimal From, LSign lSign, ConstructorMode mode)
			:base(From, mode)
		{
			sign = lSign;
		}
		
		public SLongIntD (char[] array)
			:base (array)
		{
		}
		
		public SLongIntD (char[] array, uint MaxSize)
			:base (array, MaxSize)
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
		
		public ILongUnsigned GetUnsignedClone()
		{
			return new ULongIntD(this, ConstructorMode.Copy);
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

            SLongIntD lint = (SLongIntD)obj;
			
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
			/*
			if (size <= 0)
				return "0";
			
			System.Text.StringBuilder sb = new System.Text.StringBuilder(size * 4);
			
			if (sign == LSign.Negative)
				sb.Append('-');
			
			
			sb.Append(coef[size - 1].ToString());
			
			for (int i = size - 2; i >= 0; --i)
			{
				//length of current number
				int length = 1;
				length = LongMath.IntLength(coef[i]);
				
				//if some zeros are missing
				if (length < 4)
					sb.Append('0', 4 - length);
				
				sb.Append(coef[i]);
			}	
			
			return sb.ToString();
			*/
		}
		
		#endregion
		
		#region Bool Operators
		
		// checks if two signed long numbers are equal
		public static bool operator ==(SLongIntD A, SLongIntD B)
		{
			return LongMath.Equal(A, A.IsPositive, B, B.IsPositive);
		}
		
		// checks if two signed long numbers are not equal
		public static bool operator !=(SLongIntD A, SLongIntD B)
		{
			return !LongMath.Equal(A, A.IsPositive, B, B.IsPositive);
		}
		
		// checks if long number is equal to short number
		public static bool operator ==(SLongIntD A, short b)
		{
			return LongMath.Equal(A, A.IsPositive, b);
		}
		
		// checks if signed long number is not equal to short number
		public static bool operator !=(SLongIntD A, short b)
		{
			return !LongMath.Equal(A, A.IsPositive, b);
		}
		
		// checks if signed long number is less than other long number
		public static bool operator < (SLongIntD A, SLongIntD B)
		{
			return LongMath.Less(A, A.IsPositive, B, B.IsPositive);
		}
		
		// checks if signed long number is less than short number
		public static bool operator < (SLongIntD A, short b)
		{
			return LongMath.Less(A, A.IsPositive, b);
		}
		
		// checks if signed long number is greater than other long number
		public static bool operator > (SLongIntD A, SLongIntD B)
		{
			return LongMath.Greater(A, A.IsPositive, B, B.IsPositive);
		}
		
		// checks if long number is greater, than short number
		public static bool operator > (SLongIntD A, short b)
		{
			return LongMath.Greater(A, A.IsPositive, b);
		}
		
		#endregion
		
		#region Math Operators
		
		// calculates sum of two signed long numbers
		public static SLongIntD operator + (SLongIntD A, SLongIntD B)
		{
			bool sign = false;
			LongIntBase temp = LongMath.Add(A, A.IsPositive, B, B.IsPositive, out sign);
			
			return new SLongIntD(temp, SignTransformer.GetLSign(sign), ConstructorMode.Assign);
		}
		
		public static SLongIntD operator + (SLongIntD A, short b)
		{
			bool sign = false;
			LongIntBase temp = LongMath.Add(A, A.IsPositive, b, out sign);
			
			return new SLongIntD(temp, SignTransformer.GetLSign(sign), ConstructorMode.Assign);
		}
		
		public static SLongIntD operator + (short b, SLongIntD A)
		{
			bool sign = false;
			LongIntBase temp = LongMath.Add(A, A.IsPositive, b, out sign);
			
			return new SLongIntD(temp, SignTransformer.GetLSign(sign), ConstructorMode.Assign);
		}
		
		// calculates difference between two signed long numbers
		public static SLongIntD operator - (SLongIntD A, SLongIntD B)
		{
			bool sign = false;
			LongIntBase temp = LongMath.Substract(A, A.IsPositive, B, B.IsPositive, out sign);
			
			return new SLongIntD(temp, SignTransformer.GetLSign(sign), ConstructorMode.Assign);
		}
		
		public static SLongIntD operator - (SLongIntD A, short b)
		{
			bool sign = false;
			LongIntBase temp = LongMath.Substract(A, A.IsPositive, b, out sign);
			
			return new SLongIntD(temp, SignTransformer.GetLSign(sign), ConstructorMode.Assign);
		}
		
		// calculates product of signed long number and short number
		public static SLongIntD operator * (SLongIntD A, short b)
		{
			bool sign = false;
			LongIntBase temp = LongMath.Multiply(A, A.IsPositive, b, out sign);
			
			return new SLongIntD(temp, SignTransformer.GetLSign(sign), ConstructorMode.Assign);
		}
		
		// calculates product of signed long number and short number
		public static SLongIntD operator * (short b, SLongIntD A)
		{
			bool sign = false;
			LongIntBase temp = LongMath.Multiply(A, A.IsPositive, b, out sign);
			
			return new SLongIntD(temp, SignTransformer.GetLSign(sign), ConstructorMode.Assign);
		}
		
		// calculates product of two signed long numbers
		public static SLongIntD operator * (SLongIntD A, SLongIntD B)
		{
			bool sign = false;
			LongIntBase temp = LongMath.Multiply(A, A.IsPositive, B, B.IsPositive, out sign);
			
			return new SLongIntD(temp, SignTransformer.GetLSign(sign), ConstructorMode.Assign);
		}
		
		// calculates result of division of signed long number on short number
		public static SLongIntD operator / (SLongIntD A, short b)
		{
			if (System.Math.Abs(b) == 1)
			{
				SLongIntD res = new SLongIntD(A);
				
				if (b < 0)
					res.ChangeSign();
				
				return res;
			}
			
			bool sign = false;
			LongIntBase temp = LongMath.Divide(A, A.IsPositive, b, out sign);
			
			return new SLongIntD(temp, SignTransformer.GetLSign(sign), ConstructorMode.Assign);
		}
		
		// calculates result of division of two signed long numbers
		public static SLongIntD operator / (SLongIntD A, SLongIntD B)
		{
			bool sign = false;
			LongIntBase temp = LongMath.Divide(A, A.IsPositive, B, B.IsPositive, out sign);
			
			return new SLongIntD(temp, SignTransformer.GetLSign(sign), ConstructorMode.Assign);
		}
		
		// calculates remainder of division of signed long number on short number
		public static ushort operator % (SLongIntD A, ushort b)
		{
			return LongMath.Mod(A, b);
		}
		
		// calculates remainder of division of two signed long numbers
		public static SLongIntD operator % (SLongIntD A, SLongIntD B)
		{
			return new SLongIntD(LongMath.Mod(A, B), ConstructorMode.Assign);
		}
		
		public static SLongIntD operator ++(SLongIntD A)
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
		
		public static SLongIntD operator --(SLongIntD A)
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
		
		// converts Signed Long number to Unsigned Long number
		// Explicit convertation
		public static explicit operator ULongIntD (SLongIntD From)
		{
			return new ULongIntD(From, ConstructorMode.Assign);
		}
		
		// converts signed short number to Signed Long number
		// Explicit convertation
		public static explicit operator SLongIntD (int From)
		{
			return new SLongIntD((long)From, LongIntBase.MaxRange);
		}
		
		public static explicit operator SLongIntB (SLongIntD From)
		{
			SLongIntB result = new SLongIntB((long)From[0], LongIntBase.MaxRange);			
			SLongIntB oldBase = new SLongIntB((long)From.Base, LongIntBase.MaxRange);
			SLongIntB oldBaseTemp = new SLongIntB((long)From.Base, LongIntBase.MaxRange);
			
			for (int i = 1; i < From.Size; ++i)
			{
				result += oldBase*(short)From[i];
				oldBase *= oldBaseTemp;
			}
			
			result.LongSign = From.LongSign;
			
			return result;
		}
		
		#endregion
	}
}

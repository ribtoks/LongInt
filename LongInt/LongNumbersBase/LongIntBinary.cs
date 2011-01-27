using System;

namespace LongInt
{
	public class LongIntBinary : LongIntBase
	{
		public static readonly uint NBase = (uint)NumberBase.Binary;
		
		#region Constructors
		
		public LongIntBinary ()
			: base(LongIntBinary.NBase)
		{
		}
		
		public LongIntBinary (uint MaxSize)
			: base(LongIntBinary.NBase, MaxSize)
		{
		}
		
		public LongIntBinary (ulong number, uint MaxSize)
			: base(number, LongIntBinary.NBase, MaxSize)
		{
		}
		
		public LongIntBinary (LongIntBinary From)
			: base(From)
		{
		}
		
		public LongIntBinary (LongIntBinary From, ConstructorMode mode)
			: base(From, mode)
		{
		}
		
		public LongIntBinary (LongIntBase FromLongNumber, ConstructorMode mode)
			:base(FromLongNumber, mode)
		{
		}
		
		public LongIntBinary (string s)
			:base(s, LongIntBinary.NBase, LongIntBinary.MaxRange)
		{
		}
		
		public LongIntBinary (char[] array, uint MaxSize)
			:base(array, LongIntBinary.NBase, MaxSize)
		{
		}
		
		public LongIntBinary (char[] array)
			:this(array, LongIntBase.MaxRange)
		{
		}
		
		#endregion
		
		#region ILongBinary interface implementation
		
		/// <summary>
		/// Shifts left long number on one bit
		/// </summary>
		public virtual void Shl()
		{
			//  a0 a1 a2 a3 a4 a5 a6 a7
			//  0 a0 a1 a2 a3 a4 a5 a6 >a7<
			//  		a7 - carry
			
			int bits = LIntConstant.BitsPerDigit;
			
			uint lastCarry = 0, currCarry = 0;
			int i = 0;
			
			for (; i < size; ++i)
			{
				currCarry = (uint)(coef[i] << 1);
				coef[i] = (ushort)(currCarry | (lastCarry >> bits));
				lastCarry = currCarry;
			}
			
			if ((lastCarry >> bits) == 1)
			{
				if (size >= coef.Length)
					throw new NotEnoughSpaceException("Cannot multiply 2. Not enough space in array.");
				
				coef[i] = 1;
				++size;
			}
		}
		
		/// <summary>
		/// Shifts right long number on one bit
		/// </summary>
		/// <returns>Bit, which is remaining after shift</returns>
		public virtual int Shr()
		{
			//       a0 a1 a2 a3 a4 a5 a6 a7
			//  >a0< a1 a2 a3 a4 a5 a6 a7
			//  		a0 - result
			
			int bits = LIntConstant.BitsPerDigit;
			ushort carry = 0;
			ushort temp;
			
			if (size < 1)
				return 0;
			
			for (int i = size - 1; i >= 0; --i)
			{
				temp = (ushort)((carry << (bits - 1)) | (int)(coef[i] >> 1));
				carry = (ushort)(coef[i] & 1);
				coef[i] = temp;
			}
			
			if (size > 1)
				if (coef[size - 1] == 0)
					--size;
			
			return (int)carry;
		}
		
		/// <summary>
		/// Shifts left long number on specified 
		/// number of bits
		/// </summary>
		/// <param name="count">Number of bits</param>
		public virtual void ShL(uint count)
		{
			int bits = LIntConstant.BitsPerDigit;
			
			int mulBaseCount = (int)(count / bits);
			int mulCount = (int)(count % bits);
			
			if (mulBaseCount > 0)
				MulBase(mulBaseCount);
			
			
			uint lastCarry = 0, currCarry = 0;
			int i = 0;
			
			for (; i < size; ++i)
			{
				currCarry = (uint)(coef[i] << mulCount);
				coef[i] = (ushort)(currCarry | (lastCarry >> bits));
				lastCarry = currCarry;
			}
			
			if ((lastCarry >> bits) != 0)
			{
				if (size >= coef.Length)
					throw new NotEnoughSpaceException("Cannot multiply 2. Not enough space in array.");
				
				coef[i] = (ushort)(lastCarry >> bits);
				++size;
			}
		}
		
		/// <summary>
		/// Shifts right long number 
		/// on specified number of bits
		/// </summary>
		/// <param name="count">Number of bits</param>
		/// <returns>Bit, which are remaining after shift</returns>
		public virtual int ShR(uint count)
		{
			int bits = LIntConstant.BitsPerDigit;
			
			int divBaseCount = (int)(count / bits);
			int divCount = (int)(count % bits);
			
			if (divBaseCount > 0)
				DivBase(divBaseCount);			
			
			ushort carry = 0;
			uint temp;
			
			ushort andValue = (ushort)(ushort.MaxValue >> (bits - divCount));
			
			for (int i = size - 1; i >= 0; --i)
			{
				temp = carry;
				temp <<= (bits - divCount);
				
				temp |= (uint)(coef[i] >> divCount);
				
				carry = (ushort)(coef[i] & andValue);
				coef[i] = (ushort)temp;
			}
			
			if (coef[size - 1] == 0)
				if (size > 1)
					--size;
			
			return carry;
		}
		
		/// <summary>
		/// Sets specified bit as 1
		/// </summary>
		/// <param name="bitNumber">Number of bit</param>
		public virtual void SetBit(uint bitNumber)
		{
			int bits = LIntConstant.BitsPerDigit;
			
			int coefIndex = (int)(bitNumber / bits);
			int bitIndex = (int)(bitNumber % bits);
			
			coef[coefIndex] |= (ushort)(1 << bitIndex);
			
		 	if (size < coefIndex + 1)
				size = coefIndex + 1;
		}
		
		/// <summary>
		/// Gets specified bit
		/// </summary>
		/// <param name="bitNumber">Number of bit</param>
		/// <returns>Value of bit - 0/1</returns>
		public virtual int GetBit(uint bitNumber)
		{
			int bits = LIntConstant.BitsPerDigit;
			
			int coefIndex = (int)(bitNumber / bits);
			int bitIndex = (int)(bitNumber % bits);
			
			 return Convert.ToInt32((coef[coefIndex] & (1 << bitIndex)) != 0);
		}
		
		/// <summary>
		/// Sets specified bit as 0 and 
		/// doesn't checks if size of number changed
		/// </summary>
		/// <param name="bitNumber">Number of bit to unset</param>
		public virtual void UnSetBitSafe(uint bitNumber)
		{
			int bits = LIntConstant.BitsPerDigit;
			
			int coefIndex = (int)(bitNumber / bits);
			int bitIndex = (int)(bitNumber % bits);
			
			ushort andValue = (ushort)(1 << bitIndex);
			andValue = (ushort)(~andValue);
			
			coef[coefIndex] &= andValue;
		}
		
		/// <summary>
		/// Unsets bit and check if number 
		/// length changed
		/// </summary>
		/// <param name="bitNumber">Number of bit to unset</param>
		public virtual void UnSetBit(uint bitNumber)
		{
			UnSetBitSafe(bitNumber);
			
			if ((int)(bitNumber / LIntConstant.BitsPerDigit) == size - 1)
				Normalize();
		}
		
		/// <summary>
		/// Assigns current long number reamining
		/// from division on 2^n
		/// </summary>
		/// <param name="n">Power of 2</param>
		public virtual void Mod2n(uint n)
		{
			int bits = LIntConstant.BitsPerDigit;
			
			if (n > coef.Length * LIntConstant.BitsPerDigit)
				return;
			
			int lastCoefIndex = (int)(n / bits);
			int lastBitIndex = (int)(n % bits);
			int i = lastCoefIndex + 1;
			
			for (; i < size; ++i)
				coef[i] = 0;
			
			coef[lastCoefIndex] &= (ushort)((1 << lastBitIndex) - 1);
			
			
			size = lastCoefIndex + 1;
			i = size - 1;
			if (i > 0)
					while (coef[i] == 0)
					{
						--i;
						
						if (i == 0)
							break;
					}
			size = i + 1;
		}
		
		#endregion				
	}
}

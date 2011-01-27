using System;
using LongInt.Math;

namespace LongInt
{
	public class LongIntDecimal : LongIntBase
	{		
		public static readonly uint NBase = (uint)NumberBase.Decimal;
		
		#region Constructors
		
		public LongIntDecimal ()
			: base(LongIntDecimal.NBase)
		{
		}
		
		public LongIntDecimal (uint MaxSize)
			: base(LongIntDecimal.NBase, MaxSize)
		{
		}
		
		public LongIntDecimal (ulong number, uint MaxSize)
			: base(number, LongIntDecimal.NBase, MaxSize)
		{
		}
		
		public LongIntDecimal (LongIntDecimal From)
			: base(From)
		{
		}
		
		public LongIntDecimal (LongIntDecimal From, ConstructorMode mode)
			: base(From, mode)
		{
		}
		
		public LongIntDecimal (LongIntBase FromLongNumber, ConstructorMode mode)
			:base(FromLongNumber, mode)
		{
		}
		
		public LongIntDecimal (char[] array, uint MaxSize)
			:base(array, LongIntDecimal.NBase, MaxSize)
		{
		}
		
		public LongIntDecimal (char[] array)
			:this(array, LongIntBase.MaxRange)
		{
		}
		
		public LongIntDecimal (string line)
			:base(LongIntDecimal.NBase, LongIntDecimal.MaxRange)
		{
			string s = "";
			if (line[0] == '-')
				s = line.Substring(1);
			else
				s = line.Substring(0);
			
			int baseLength = LongMath.IntLength(numberBase - 1);
			
			size = (s.Length / baseLength) + Convert.ToInt32((s.Length % baseLength) != 0);
			
			int index = 0;
			while (index < size - 1)
			{
				coef[index] = Convert.ToUInt16(s.Substring(s.Length - baseLength*(index + 1), baseLength));
				++index;
			}
			//first part of number can be smaller, than <baseLength> digits
			coef[index] = Convert.ToUInt16(s.Substring(0, s.Length - index*baseLength));
		}
		
		#endregion
		
		/// <summary>
		/// Builds ULongInt from usual integer
		/// </summary>
		/// <param name="number">Unsigned Integer number, from which long
		/// number would be builded
		/// </param>
		public void BuildFromInt(ulong number)
		{
			int nBase = (int)LongIntDecimal.NBase;
			
			int numberLength = LongMath.IntLength(number);
			int baseLength =  LongMath.IntLength((ulong)(nBase - 1));
			
			size = (numberLength / baseLength) + Convert.ToInt32(numberLength % baseLength != 0);
			coef = new ushort[LongIntDecimal.MaxRange];
			
			ulong tmp = number;
			for (int i = 0; i < size; ++i)
			{
				coef[i] = (ushort) (tmp % (ulong)nBase);
				tmp /= (ulong)nBase;
			}
		}
		
		/// <summary>
		/// Gets decimal digit, that stands
		/// on specified position
		/// </summary>
		/// <param name="n">Position of digit</param>
		/// <returns>Decimal digit</returns>
		public int GetNthDigit(uint n)
		{
			return LongMath.GetNthDigit(this, n);
		}
		
		public override string ToString ()
		{
			if (size <= 0)
				return "0";
			
			System.Text.StringBuilder sb = new System.Text.StringBuilder(size * 4);
			
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
		}
	}
}

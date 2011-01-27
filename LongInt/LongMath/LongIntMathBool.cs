using System;
using System.Text;

namespace LongInt.Math
{
	public static partial class LongMath
	{		
		#region Bool Operations
		
		internal static bool Equal(LongIntBase A, LongIntBase B)
		{
			if (A.Base != B.Base)
				return false;
			
			if (A.Size != B.Size)
				return false;
			
			int i = 0;
			while (A[i] == B[i])
			{
				++i;
				if (i >= A.Size)
					break;
			}
			return (i == A.Size);
		}
		
		internal static bool Equal(LongIntBase A, ushort b)
		{
			if (b == 0)
				return A.IsZero();
			
			if (A.Size > 2 || A.Size < 1)
				return false;
			
			int index = 0;
			uint Base = A.Base;
			do
			{
				if (A[index] != (ushort)(b % Base))
					return false;
				
				b = (ushort)(b / Base);
				
				++index;
			} while (b != 0);
			
			return true;
		}
		
		internal static bool Greater(LongIntBase A, LongIntBase B)
		{
			if (A.Base != B.Base)
				throw new DifferentBasesException("Attempt to compare two numbers with different bases!");
			
			if (A.Size < B.Size) 
				return false;
			
			if (A.Size > B.Size) 
				return true;
			
			int i = A.Size - 1;
			
			while (A[i] == B[i])
			{
				--i;
				if (i < 0)
					break;
			}
			
			if (i < 0)
				return false;
			
			return (A[i] > B[i]);
		}
		
		internal static bool Greater(LongIntBase A, ushort b)
		{
			LongIntBase temp = new LongIntBase(b, A.Base, 2);
			
			return Greater(A, temp);
		}
		
		internal static bool Less(LongIntBase A, LongIntBase B)
		{
			if (A.Base != B.Base)
				throw new DifferentBasesException("Attempt to compare two numbers with different bases!");
			
			if (A.Size < B.Size) 
				return true;
			
			if (A.Size > B.Size) 
				return false;
			
			int i = A.Size - 1;
			
			while (A[i] == B[i])
			{
				--i;
				if (i < 0)
					break;
			}
			
			if (i < 0)
				return false;
			
			return (A[i] < B[i]);
		}
		
		internal static bool Less(LongIntBase A, ushort b)
		{
			LongIntBase temp = new LongIntBase(b, A.Base, 2);
			
			return Less(A, temp);
		}
		
		#endregion
		
		#region Bool Signed Operations
		
		internal static bool Equal(LongIntBase A, bool AisPositive,
		                           LongIntBase B, bool BisPositive)
		{
			if (A.IsZero() && B.IsZero())
				return true;
			
			if (AisPositive != BisPositive)
				return false;
			
			return LongMath.Equal(A, B);
		}
		
		internal static bool Equal(LongIntBase A, bool AisPositive,
		                           short b)
		{
			if (b == 0)
				return A.IsZero();
			
			bool bSign = (b >= 0);
			if (AisPositive != bSign)
				return false;
			
			if (b < 0)
				b *= -1;
			
			return LongMath.Equal(A, (ushort)b);
		}
		
		internal static bool Greater(LongIntBase A, bool AisPositive,
		                             LongIntBase B, bool BisPositive)
		{
			if (AisPositive == BisPositive)
			{
				if (AisPositive)
					return LongMath.Greater(A, B);
				else
					return LongMath.Less(A, B);
			}
			
			if (AisPositive)
				return true;
			
			return false;
		}
		
		internal static bool Greater(LongIntBase A, bool AisPositive,
		                             short b)
		{
			bool bSign = (b >= 0);
			
			if (b < 0)
				b *= -1;
			
			if (AisPositive == bSign)
			{
				if (AisPositive)
					return LongMath.Greater(A, (ushort)b);
				else
					return LongMath.Less(A, (ushort)b);
			}
			
			if (AisPositive)
				return true;
			
			return false;
		}
		
		internal static bool Less(LongIntBase A, bool AisPositive,
		                          LongIntBase B, bool BisPositive)
		{
			if (AisPositive == BisPositive)
			{
				if (AisPositive)
					return LongMath.Less(A, B);
				else
					return LongMath.Greater(A, B);
			}
			
			if (AisPositive)
				return false;
			
			return true;
		}
		
		internal static bool Less(LongIntBase A, bool AisPositive,
		                          short b)
		{
			bool bSign = (b >= 0);
			
			if (b < 0)
				b *= -1;
			
			if (AisPositive == bSign)
			{
				if (AisPositive)
					return LongMath.Less(A, (ushort)b);
				else
					return LongMath.Greater(A, (ushort)b);
			}
			
			if (AisPositive)
				return false;
			
			return true;
		}
		
		#endregion		
	}
}
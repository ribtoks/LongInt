using System;
using System.Text;

namespace LongInt.Math
{
	public static partial class LongMath
	{
		#region Math Operations
		
		internal static LongIntBase Add(LongIntBase A, LongIntBase B)
		{
			LongIntBase What = A, To = B;
			
			if (A.Size < B.Size)
			{
				What = B;
				To = A;
			}
			
			LongIntBase C = new LongIntBase(What.Base, System.Math.Max(What.CoefLength, To.CoefLength));
			int Base = (int)What.Base;
			
			int carry = 0;
			int temp;
			int i;
			
			for (i = 0; i < To.Size; ++i)
			{
				temp = (int)What[i] + (int)To[i] + carry;
				if (temp >= Base)
				{
					C[i] = (ushort)(temp - Base);
					carry = 1;
				}
				else
				{
					C[i] = (ushort)temp;
					carry = 0;
				}
			}
			
			for (; i < What.Size; ++i)
			{
				temp = What[i] + carry;
				if (temp >= Base)
				{
					C[i] = (ushort)(temp - Base);
					carry = 1;
				}
				else
				{
					C[i] = (ushort)temp;
					carry = 0;
				}
			}
			
			if (carry != 0)
			{
				C[i] = (ushort)carry;
				C.Size = What.Size + 1;
			}
			else
				C.Size = What.Size;
			
			return C;
		}
		
		internal static LongIntBase Add(LongIntBase A, ushort b)
		{
			LongIntBase C = new LongIntBase(A);
			uint Base = A.Base;
			
			int index = 0;
			uint carry = 0;
			
			uint temp = (uint)C[0] + (uint)b;
			if (temp >= Base)
			{
				C[0] = (ushort)(temp - Base);
				carry = 1;
			}
			else
			{
				C[0] = (ushort)temp;
				carry = 0;
			}
				
			++index;
			
			while (carry != 0)
			{
				temp = C[index] + carry;
				if (temp >= Base)
				{
					C[index] = (ushort)(temp - Base);
					carry = 1;
				}
				else
				{
					C[index] = (ushort)temp;
					carry = 0;
				}
				
				++index;
			}
			
			if (index > A.Size)
				C.Size = index;
			else
				C.Size = A.Size;
			
			return C;
		}
		
		internal static LongIntBase SubstractSafe(LongIntBase From, LongIntBase What)
		{
			LongIntBase C = new LongIntBase(From.Base, System.Math.Max(From.CoefLength, What.CoefLength));
			uint Base = From.Base;
			
			int i;
			int temp;
			int carry = 0;
			
			for (i = 0; i < What.Size; ++i)
			{
				temp = From[i] - What[i] + carry;
				if (temp < 0)
				{
					C[i] = (ushort)(temp + Base);
					carry = -1;
				}
				else
				{
					C[i] = (ushort)temp;
					carry = 0;
				}
			}
			
			for (; i < From.Size; ++i)
			{
				temp = From[i] + carry;
				if (temp < 0)
				{
					C[i] = (ushort)(temp + Base);
					carry = -1;
				}
				else
				{
					C[i] = (ushort)temp;
					carry = 0;
				}
			}
			
			i = From.Size - 1;
			if (i > 0)
				while (C[i] == 0) 
				{
					--i;
					if (i == 0)
						break;
				}
			
			C.Size = i + 1;
			return C;
		}
		
		internal static LongIntBase SubstractSafe(LongIntBase From, ushort What)
		{
			LongIntBase C = new LongIntBase(From);
			int Base = (int)From.Base;
			
			int i = 0;
			int carry = 0;
			
			int temp = From[0] - (int)What;
			
			if (temp < 0)
			{
				C[0] = (ushort)(temp + Base);
				carry = -1;
			}
			else
			{
				C[0] = (ushort)temp;
				carry = 0;
			}
			++i;
			
			while (carry != 0)
			{
				temp = From[i] + carry;
				if (temp < 0)
				{
					C[i] = (ushort)(temp + Base);
					carry = -1;
				}
				else
				{
					C[i] = (ushort)temp;
					carry = 0;
				}
				
				++i;
			}
			
			i = From.Size - 1;
			if (i > 0)
				while (C[i] == 0) 
				{
					--i;
					if (i == 0)
						break;
				}
			
			C.Size = i + 1;
			
			return C;
		}
		
		internal static LongIntBase SubstractSafe(ushort From, LongIntBase What)
		{
			LongIntBase C = new LongIntBase(What);
			
			int temp = From - C[0];
			C[0] = (ushort)temp;
			
			return C;
		}		
		
		internal static LongIntBase UShortMul(LongIntBase A, ushort B)
		{
			LongIntBase C = new LongIntBase(A.Base, A.CoefLength + 1);
			uint Base = A.Base;
			
			int i = 0;
			long temp, carry = 0;
			long b = B;
			
			for (i = 0; i < A.Size; ++i)
			{
				temp = A[i]*b + carry;				
				carry = temp / Base;
				
				C[i] = (ushort)(temp - (carry*Base));
			}
			
			if (carry != 0)
			{
				C[i] = (ushort)carry;
				C.Size = A.Size + 1;
			}
			else
				C.Size = A.Size;
			
			return C;
		}
		
		internal static void UShortMulSafe(ref LongIntBase A, ushort B)
		{
			uint Base = A.Base;
			
			int i = 0;
			long temp, carry = 0;
			long b = B;
			
			for (i = 0; i < A.Size; ++i)
			{
				temp = A[i]*b + carry;				
				carry = temp / Base;
				
				A[i] = (ushort)(temp - (carry*Base));
			}
			
			if (carry != 0)
			{
				A[i] = (ushort)carry;
				A.Size++;
			}
		}
		
		internal static LongIntBase Multiply(LongIntBase A, LongIntBase B)
		{
			LongIntBase C = new LongIntBase(A.Base, System.Math.Max(A.CoefLength, B.CoefLength) + 1);
			uint Base = A.Base;
			
			int i, j;
			long temp, carry;
			
			for (i = 0; i < A.Size; ++i)
			{
				carry = 0;
				for (j = 0; j < B.Size; ++j)
				{
					temp = A[i];
					temp *= B[j];
					temp += C[i + j] + carry;
					
					carry = temp / Base;
					C[i + j] = (ushort)(temp - (carry * Base));
				}
				C[i + j] = (ushort)carry;
			}
			
			i = A.Size + B.Size - 1;
			if (i > 0)
				while (C[i] == 0)
				{
					--i;
					if (i == 0)
						break;
				}
			
			C.Size = i + 1;
			
			return C;
		}
		
		internal static void UShortDiv(LongIntBase A, ushort B, out LongIntBase Q, out ushort R)
		{
			// division with new result
			uint Base = A.Base;
			Q = new LongIntBase(A.Base, A.CoefLength);
			
			long r = 0;
			int i;
			long temp, b = B;
			
			for (i = A.Size - 1; i >= 0; --i)
			{
				temp = (r * Base) + A[i];
				Q[i] = (ushort)(temp / b);
				r = temp - Q[i]*b;
			}
			
			//r - remaining from division
			R = (ushort)r;
			
			i = A.Size - 1;
			if (i > 0)
				while (Q[i] == 0)
				{
					--i;
					if (i == 0)
						break;
				}
			
			Q.Size = i + 1;
		}
		
		internal static void UShortDivSafe(LongIntBase A, ushort B, ref LongIntBase Q, ref ushort R)
		{
			// division in place
			uint Base = A.Base;
			
			long r = 0;
			int i;
			long temp, b = B;
			
			for (i = A.Size - 1; i >= 0; --i)
			{
				temp = (r * Base) + A[i];
				Q[i] = (ushort)(temp / b);
				r = temp - Q[i]*b;
			}
			
			//r - remaining from division
			R = (ushort)r;
			
			i = A.Size - 1;
			if (i > 0)
				while (Q[i] == 0)
				{
					--i;
					if (i == 0)
						break;
				}
			
			Q.Size = i + 1;
		}
		
		internal static void Divide(LongIntBase A, LongIntBase B, out LongIntBase Q, out LongIntBase R, 
		                            bool useResultMaxSize, bool useRemainingMaxSize)
		{
			if (B.IsZero())
				throw new DivideByZeroException("Cannot divide by zero!");
			
			int Base = (int)A.Base;
			
			if (LongMath.Less(A, B))
			{
				Q = new LongIntBase(A.Base, A.CoefLength);
				R = new LongIntBase(A);
				return;
			}
			
			if (B.Size == 1)
			{
				ushort tempR = 0;
				R = new LongIntBase(A.Base, B.CoefLength);
				UShortDiv(A, B[0], out Q, out tempR);
				R[0] = tempR;
				R.Size = 1;
				
				return;
			}
			
		 	//LongIntBase A = new LongIntBase(inputA, (uint)inputA.Size + 1);
			//LongIntBase B = new LongIntBase(inputB, (uint)inputB.Size + 1);
			
			Q = new LongIntBase(A.Base, (useResultMaxSize ? A.CoefLength : (uint)A.Size + 1) );			
			R = new LongIntBase(B.Base, (useRemainingMaxSize ? B.CoefLength : (uint)B.Size + 1) );
			
			
			/*
			 * create temporaty LongNUmber U, that is 
			 * equivalent to A
			 * Maximum U size is greater than A size,
			 * if we will advance A while normalizing
			*/
			LongIntBase U = new LongIntBase(A, (uint)A.Size + 1);
			//U[A.Size] = 0;
			
			int n = B.Size;
			int m = U.Size - B.Size;
			
			int uJ, vJ, i;
			long temp1, temp2, temp;
			
			//normalization coeficient
			int scale;
			
			//guessed number for remaining
			long qGuess, r;
			
			//carries
			int borrow, carry;
			
			scale = (Base / ((int)B[n - 1] + 1));
			
			if (scale > 1)
			{
				UShortMulSafe(ref U, (ushort)scale);
				UShortMulSafe(ref B, (ushort)scale);
			}
			
			/*
			 * Main loop in dividing
			 * Every iteration gets next remaining digit
			 * vJ - current shift from U, that is used for substraction
			 *  (index of next digit in result)
			 * uJ - index of current U digit
			*/

			for (vJ = m, uJ = n + vJ; vJ >= 0; --vJ, --uJ) 
			{
				long numerator = U[uJ]*(long)Base + (long)U[uJ - 1];
				
				qGuess = numerator / B[n - 1];
				r = numerator - qGuess*B[n - 1];
		
				while (r < Base)
				{	
					temp2 = B[n - 2]*qGuess;					
					temp1 = r*Base + U[uJ - 2];
		
					if ((temp2 > temp1) || (qGuess == Base)) 
					{
						--qGuess;
						r += B[n - 1];
					} 
					else 
						break;
				}
				
				/*
				 * Now qGuess is correct part or is advanced with 1 to q
				 * Calculate divisor B, multiplied on qGuess from U
				 * from position vJ + i
				*/
				carry = 0; borrow = 0; 
				//LongIntBase uShift = U;
				
				//unsigned short *uShift = u + vJ;
		
				//looping on B digits
				for (i = 0; i < n; ++i) 
				{
					//try to get digit of product B*qGuess
					temp1 = B[i]*qGuess + carry;
					
					carry = (int)(temp1 / Base);
					
					temp1 -= ((long)carry*Base);
					
					//substract it from U
					temp2 = U[i + vJ] - temp1 + borrow;	
					if (temp2 < 0) 
					{
						U[i + vJ] = (ushort)(temp2 + Base);
						borrow = -1;
					} 
					else 
					{
						U[i + vJ] = (ushort)temp2;
						borrow = 0;
					}
				}
				
				/*
				 * maybe, B that is multiplied on qQuess became bigger
				 * if yes, after multiplying carry was unused
				 * it must be substracted also
				*/
				temp2 = U[i + vJ] - carry + borrow;
				if (temp2 < 0) 
				{
					U[i + vJ] = (ushort)(temp2 + Base);
					borrow = -1;
				} 
				else 
				{
					U[i + vJ] = (ushort)temp2;
					borrow = 0;
				}
				
				//if division was ok, than
				if (borrow == 0) 
				{
					Q[vJ] = (ushort)qGuess;
				} 
				else 
				{
					Q[vJ] = (ushort)(qGuess - 1);
					//add one substraction
					carry = 0;
					for (i = 0; i < n; ++i) 
					{
						temp = U[i + vJ] + B[i] + carry;
						if (temp >= Base) 
						{
							U[i + vJ] = (ushort)(temp - Base);
							carry = 1;
						} 
						else 
						{
							U[i + vJ] = (ushort)temp;
							carry = 0;
						}
					}
					U[i + vJ] = (ushort)(U[i + vJ] + carry - Base);
				}
				i = U.Size - 1;
				if (i > 0)
					while (U[i] == 0) 
					{
						--i;
						if (i == 0)
							break;
					}
				U.Size = i + 1;
		
			}
			if (m > 0)
				while (Q[m] == 0)
				{
					--m;
					if (m == 0)
						break;
				}
			Q.Size = m + 1;
			
			if (scale > 1) 
			{
				ushort junk = 0;
				UShortDivSafe(B, (ushort)scale, ref B, ref junk);
				
				LongIntBase tempR = null;
				UShortDiv(U, (ushort)scale, out tempR, out junk);
				
				R = new LongIntBase(tempR, (useRemainingMaxSize ? LongIntBase.MaxRange : (uint)tempR.Size + 1));
			}
			else
				R.AssignBase(U);// = U;
		
		}
		
		internal static void Increment(ref LongIntBase A, ushort incrementValue)
		{
			int index = 0;
			int carry = 0;
			uint Base = A.Base;
			
			int temp = A[0] + incrementValue + carry;
			if (temp >= Base)
			{
				A[0] = (ushort)(temp - Base);
				carry = 1;
			}
			else
			{
				A[0] = (ushort)temp;
				carry = 0;
			}
				
			++index;
			
			while (carry != 0)
			{
				temp = A[index] + carry;
				if (temp >= Base)
				{
					A[index] = (ushort)(temp - Base);
					carry = 1;
				}
				else
				{
					A[index] = (ushort)temp;
					carry = 0;
				}
				
				++index;
			}
			
			if (index > A.Size)
				A.Size = index;
		}
		
		internal static void DecrementSafe(ref LongIntBase A, ushort decrementValue)
		{
			int i = 0;
			short carry = 0;
			uint Base = A.Base;
			
			int temp = A[0] - decrementValue;
			
			if (temp < 0)
			{
				A[0] = (ushort)(temp + Base);
				carry = -1;
			}
			else
			{
				A[0] = (ushort)temp;
				carry = 0;
			}
			++i;
			
			while (carry != 0)
			{
				temp = A[i] + carry;
				if (temp < 0)
				{
					A[i] = (ushort)(temp + Base);
					carry = -1;
				}
				else
				{
					A[i] = (ushort)temp;
					carry = 0;
				}
				
				++i;
			}
			
			i = A.Size - 1;
			if (i > 0)
				while (A[i] == 0) 
				{
					--i;
					if (i == 0)
						break;
				}
			
			A.Size = i + 1;
		}
		
		internal static LongIntBase InnerPower(LongIntBase A, ulong n)
		{
			LongIntBase X = new LongIntBase(A);
			LongIntBase R = new LongIntBase(A.Base, X.CoefLength);
			R[0] = 1;
			
			while (n != 0)
			{
				if ((n & 1) == 1)
					R = Multiply(R, X);
				
				n >>= 1;
				
				X = InnerSqr(X);
			}
			
			return R;
		}
		
		#endregion
		
		#region Math Signed Operatons
		
		internal static LongIntBase Add(LongIntBase A, bool AisPositive,
		                                LongIntBase B, bool BisPositive, out bool resultSign)
		{
			if (AisPositive == BisPositive)
			{
				resultSign = AisPositive;
				// (A + B) or ((-A) + (-B)) == -(A + B)
				return new LongIntBase(LongMath.Add(A, B), ConstructorMode.Assign);
			}
			
			LongIntBase tempFrom = A;
			LongIntBase tempWhat = B;

            resultSign = true;

            if (!AisPositive)
            {
                // B - A
                if (Greater(A, B))
                {
                    // -(A - B)
                    resultSign = false;
                }
                else
                {
                    // B - A
                    tempFrom = B;
                    tempWhat = A;
                }
            }
            else
            {
                // A - B
                if (Less(A, B))
                {
                    // -(B - A)
                    tempFrom = B;
                    tempWhat = A;
                    resultSign = false;
                }
                // else A - B
            }
			
			return new LongIntBase(LongMath.SubstractSafe(tempFrom, tempWhat), ConstructorMode.Assign);
		}
		
		
		internal static LongIntBase Add(LongIntBase A, bool AisPositive, short b, out bool resultSign)
		{
			bool BisPositive = (b >= 0);
			
			if (b < 0)
				b *= -1;
			
			if (AisPositive == BisPositive)
			{
				resultSign = AisPositive;
				// (A + B) or ((-A) + (-B)) == -(A + B)
				return new LongIntBase(LongMath.Add(A, (ushort)b), ConstructorMode.Assign);
			}
			
			LongIntBase tempFrom = A;
			ushort tempWhat = (ushort)b;

            resultSign = true;

            if (!AisPositive)
            {
                // B - A
                if (Greater(A, (ushort)b))
                {
                    // -(A - B)
                    resultSign = false;
                }
                else
                {
                    // B - A
                    return new LongIntBase(LongMath.SubstractSafe(tempWhat, tempFrom), ConstructorMode.Assign);
                }
            }
            else
            {
                // A - B
                if (Less(A, (ushort)b))
                {
                    // -(B - A)
                    resultSign = false;
					return new LongIntBase(LongMath.SubstractSafe(tempWhat, tempFrom), ConstructorMode.Assign);
                }
                // else A - B
            }
			
			return new LongIntBase(LongMath.SubstractSafe(tempFrom, tempWhat), ConstructorMode.Assign);
		}
		
		internal static LongIntBase Substract(LongIntBase A, bool AisPositive, 
		                                      LongIntBase B, bool BisPositive,
		                                      out bool resultSign)
		{
			if (AisPositive != BisPositive)
			{
				// (A + B) or (-A - B) == -(A + B)
				resultSign = AisPositive;	
				return new LongIntBase(LongMath.Add(A, B), ConstructorMode.Assign);
			}
			else
			{
				/*
				 * Possible variants:
				 * (A - B)  ==
				 * 
				 * A > B  ->  A - B
				 * A == B ->  0
				 * A < B  ->  -(B - A)
				*/
				
				resultSign = true;
				
				if (LongMath.Equal(A, B))
					return new LongIntBase(0, A.Base, LongIntBase.MaxRange);
				
				resultSign = AisPositive;
				
				if (Greater(A, B))
					return new LongIntBase(LongMath.SubstractSafe(A, B), ConstructorMode.Assign);
				
				resultSign = !AisPositive;
				
				return new LongIntBase(LongMath.SubstractSafe(B, A), ConstructorMode.Assign);
			}
		}
		
		internal static LongIntBase Substract(LongIntBase A, bool AisPositive,
		                                      short b, out bool resultSign)
		{
			bool BisPositive = (b >= 0);
			
			if (b < 0)
				b *= -1;
			
			if (AisPositive != BisPositive)
			{
				// (A + B) or (-A - B) == -(A + B)
				resultSign = AisPositive;
				return new LongIntBase(LongMath.Add(A, (ushort)b), ConstructorMode.Assign);
			}
			else
			{
				/*
				 * Possible variants:
				 * (A - B)  ==
				 * 
				 * A > B  ->  A - B
				 * A == B ->  0
				 * A < B  ->  -(B - A)
				*/
				resultSign = true;
				
				if (LongMath.Equal(A, (ushort)b))
					return new LongIntBase(0, A.Base, LongIntBase.MaxRange);
				
				if (AisPositive == false)
					resultSign = !resultSign;
				
				if (Greater(A, (ushort)b))
					return new LongIntBase(LongMath.SubstractSafe(A, (ushort)b), ConstructorMode.Assign);
				
				resultSign = false;
				
				if (AisPositive == false)
					resultSign = !resultSign;
				
				return new LongIntBase(LongMath.SubstractSafe((ushort)b, A), ConstructorMode.Assign);
			}
		}
		
		internal static LongIntBase Multiply(LongIntBase A, bool AisPositive,
		                                     LongIntBase B, bool BisPositive, out bool resultSign)
		{
			if (A.IsZero() || B.IsZero())
			{
				resultSign = true;
				return new LongIntBase(0, A.Base, A.CoefLength);
			}
			
			resultSign = !(AisPositive ^ BisPositive);
			return new LongIntBase(LongMath.Multiply(A, B), ConstructorMode.Assign);
		}
		
		internal static LongIntBase Multiply(LongIntBase A, bool AisPositive, short b, out bool resultSign)
		{
			if (A.IsZero() || (b == 0))
			{
				resultSign = true;
				return new LongIntBase(0, A.Base, A.CoefLength);
			}
			
			resultSign = !(AisPositive ^ (b >= 0));
			
			if (b < 0)
				b *= -1;
			
			if (b == 1)
				return new LongIntBase(A, ConstructorMode.Copy);
			
			return new LongIntBase(LongMath.UShortMul(A, (ushort)b), ConstructorMode.Assign);
		}
		
		internal static LongIntBase Divide(LongIntBase A, bool AisPositive,
		                                   LongIntBase B, bool BisPositive,
		                                   out bool resultSign)
		{
			if (A.IsZero())
			{
				resultSign = true;
				return new LongIntBase(A);
			}
			
			LongIntBase Q = new LongIntBase(A.Base);
			LongIntBase R = new LongIntBase(B.Base);
			
			LongMath.Divide(A, B, out Q, out R, true, false);
			
			resultSign = !(AisPositive ^ BisPositive);
			return new LongIntBase(Q, ConstructorMode.Assign);
		}
		
		internal static LongIntBase Divide(LongIntBase A, bool AisPositive, short b, out bool resultSign)
		{
			if (A.IsZero())
			{
				resultSign = true;
				return new LongIntBase(A);
			}
			
			resultSign = !((b >= 0) ^ AisPositive);
			
			if (b < 0)
				b *= -1;
			
			if (b == 1)
				return new LongIntBase(A, ConstructorMode.Copy);
			
			ushort r = 0;
			LongIntBase Q = new LongIntBase(A.Base);
			
			LongMath.UShortDiv(A, (ushort)b, out Q, out r);
			
			return new LongIntBase(Q, ConstructorMode.Assign);
		}
		
		internal static LongIntBase Mod(LongIntBase A, LongIntBase B)
		{
			LongIntBase Q = new LongIntBase(A.Base);
			LongIntBase R = new LongIntBase(B.Base);
			
			LongMath.Divide(A, B, out Q, out R, false, true);
			
			return new LongIntBase(R, ConstructorMode.Assign);
		}
		
		internal static ushort Mod(LongIntBase A, ushort b)
		{
			ushort r = 0;
			LongIntBase Q = new LongIntBase(A.Base);
			
			LongMath.UShortDiv(A, b, out Q, out r);
			
			return r;
		}
		
		#endregion		
	}
}
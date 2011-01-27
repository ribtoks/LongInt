using System;

using LongInt;
using LongInt.Math;
using LongInt.Math.Special;

namespace LongInt
{
	/// <summary>
	/// Linear generator of 64-bit 
	/// random numbers
	/// </summary>
	internal class RandomUInt64
	{
		private ulong a = 6364136223846793005;
		private ulong currX = 0;
		
		internal ulong CurrentValue
		{
			get { return currX; }
		}
		
		public RandomUInt64 (ulong seed)
		{
			currX = seed;
		}
		
		/// <summary>
		/// Finds next random 64-bit number
		/// </summary>
		/// <returns>
		/// A <see cref="System.UInt64"/>
		/// </returns>
		public ulong Next()
		{
			unchecked
			{
				currX *= a;
				++currX;
			}
			
			return currX;
		}
		
		/// <summary>
		/// Finds next 64-bit number
		/// </summary>
		/// <returns>16 older bits of found number</returns>
		public ushort NextUShort()
		{
			ulong next = Next();
			next >>= (LIntConstant.BitsPerDigit * 2);
			return (ushort)next;
		}
	}
	
	/// <summary>
	/// Class, that generates long random numbers
	/// </summary>
	public class RandomLong
	{
		RandomUInt64 innerRand;
		
		public RandomLong(ulong seed)
		{
			innerRand = new RandomUInt64(seed);
		}
		
		public RandomLong(RandomLong from)
		{
			innerRand = new RandomUInt64(from.innerRand.CurrentValue);
		}
		
		/// <summary>
		/// Gets next random long number, that 
		/// is less than specified number
		/// </summary>
		/// <param name="N">Greatest possible number</param>
		/// <returns>Random long number</returns>
		public LongIntBinary Next(LongIntBinary N)
		{
			LongIntBase A = new LongIntBase(N.Base, LongIntBase.MaxRange);
			
			int i = 0;
			int shrValue = (N.Size % 5) + 9;
			
			int mod = innerRand.NextUShort() >> shrValue;
			while (mod == 0)
				mod = innerRand.NextUShort() >> shrValue;
			
			int sign = (int)(innerRand.Next() % 3);
			sign -= 1;
			
			int newSize = System.Math.Abs(N.Size + 1 + sign * (innerRand.NextUShort() % mod));
						
			for (i = 0; i <= newSize; ++i)
				A[i] = innerRand.NextUShort();
			
			if (i > 0)
				while (A[i] == 0)
				{
					--i;
					if (i == 0)
						break;
				}
			
			A.Size = i + 1;
			
			return new LongIntBinary(LongMath.Mod(A, N), ConstructorMode.Assign);
		}
		
		/// <summary>
		/// Gets next random number, that is 
		/// greater equal to Min and less, than Max
		/// </summary>
		/// <param name="Min">Lower bound of interval</param>
		/// <param name="Max">Upper bound of interval</param>
		/// <returns>Random long number</returns>
		public LongIntBinary Next(LongIntBinary Min, LongIntBinary Max)
		{
			if (LongMath.Greater(Min, Max))
				throw new ArgumentException("Lower bound is less, that upper bound!");
			
			return new LongIntBinary(
			             LongMath.Add(Min, 
			             Next(new LongIntBinary(LongMath.SubstractSafe(Max, Min), ConstructorMode.Assign))), 
			                         ConstructorMode.Assign);
		}
		
		internal LongIntBinary NextNotZero(LongIntBinary N)
		{
			LongIntBinary temp = Next(N);
			while (temp.IsZero())
				temp = Next(N);
			return temp;
		}
		
		internal LongIntBinary NextNotOne(LongIntBinary N)
		{
			LongIntBinary temp = Next(N);
			while (temp.IsZero() || LongMath.Equal(temp, 1))
				temp = Next(N);
			return temp;
		}
		
		/// <summary>
		/// Finds next long number using 
		/// coprime numbers P and Q
		/// </summary>
		/// <param name="P"></param>
		/// <param name="Q"></param>
		/// <returns>Random long number in Z(p*q)</returns>
		public LongIntBinary NextPQ(LongIntBinary P, LongIntBinary Q)
		{
			SLongIntB temp = InnerNexpPQ(P, Q);
			
			while (temp < 0)
				temp = InnerNexpPQ(P, Q);
			
			return temp;
		}
		
		private SLongIntB InnerNexpPQ(LongIntBinary P, LongIntBinary Q)
		{
			SLongIntB x1 = new SLongIntB(NextNotZero(P), ConstructorMode.Assign);
			SLongIntB x2 = new SLongIntB(NextNotZero(Q), ConstructorMode.Assign);	
			
			SLongIntB gcd = CryptoMath.GCD(x1, x2);
			x1 /= gcd;
			x2 /= gcd;
			
			while (x1 < 2 || x2 < 2)
			{
				x1 = new SLongIntB(NextNotZero(P), ConstructorMode.Assign);
				x2 = new SLongIntB(NextNotZero(Q), ConstructorMode.Assign);
				
				gcd = CryptoMath.GCD(x1, x2);
				x1 /= gcd;
				x2 /= gcd;
			}			
			
			SLongIntB p = new SLongIntB(P, ConstructorMode.Assign);
			SLongIntB q = new SLongIntB(Q, ConstructorMode.Assign);
			
			return CryptoMath.ChineRem(new SLongIntB[]{x1, x2}, new SLongIntB[]{p, q});
		}
	}
}
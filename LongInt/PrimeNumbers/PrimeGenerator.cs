using System;
using LongInt.Math;
using LongInt.Math.Special;

namespace LongInt
{
	public class PrimeGenerator
	{
		RandomLong rand;
		
		public PrimeGenerator (RandomLong randGenerator)
		{
			rand = new RandomLong(randGenerator);
		}
		
		/// <summary>
		/// Gets next prime number that is greater 
		/// equal than Min, and less, than Max
		/// </summary>
		/// <param name="Min">Lower bound of interval</param>
		/// <param name="Max">Upper bound of interval</param>
		/// <returns>Prime long number</returns>
		public ULongIntB Next(ULongIntB Min, ULongIntB Max)
		{
			if (Min > Max)
				throw new ArgumentException("Lower bound is less, that upper bound!");
			
			ULongIntB p = new ULongIntB(rand.Next(Min, Max), ConstructorMode.Assign);
			ULongIntB t = Max - Min;
			
			if (LongMath.IsEven(p))
				++p;
			
			if (p > Max)
				p = Min + (p % (t + 1));
			
			while (CryptoMath.TestPrimeMillerRabin(p, RoundsNumber.Rounds25) == PrimeTestResult.Composite)
			{
				++p;
				++p;
				
				while (p > Max)
				{
					p = Min + (p % (t + 1));
					if (LongMath.IsEven(p))
						++p;
				}
			}
			
			return p;
		}
		
		// also checks if GCD(result - 1, f) == 1
		/// <summary>
		/// Gets next prime number that is greater 
		/// equal than Min, and less, than Max and
		/// GCD(number - 1, f) = 1
		/// </summary>
		/// <param name="Min">Lower bound of interval</param>
		/// <param name="Max">Upper bound of interval</param>
		/// <param name="f">Number f</param>
		/// <returns>Prime long number</returns>
		public ULongIntB Next(ULongIntB Min, ULongIntB Max, SLongIntB f)
		{
			if (Min > Max)
				throw new ArgumentException("Lower bound is less, that upper bound!");
			
			if (LongMath.IsEven(f))
				throw new ArgumentException("Argument 'f' cannot be even!");
			
			ULongIntB p = new ULongIntB(rand.Next(Min, Max), ConstructorMode.Assign);
			ULongIntB t = Max - Min;
			
			if (LongMath.IsEven(p))
				++p;
			
			if (p > Max)
				p = Min + (p % (t + 1));
			
			while (CryptoMath.TestPrimeMillerRabin(p, RoundsNumber.Rounds25) == PrimeTestResult.Composite
			       || (CryptoMath.GCD((SLongIntB)p - 1, f) == 1))
			{
				++p;
				++p;
				
				while (p > Max)
				{
					p = Min + (p % (t + 1));
					if (LongMath.IsEven(p))
						++p;
				}
			}
			
			return p;
		}
	}
}

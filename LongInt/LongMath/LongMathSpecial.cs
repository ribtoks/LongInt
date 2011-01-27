using System;

using LongInt.Math;

namespace LongInt.Math.Special
{
	public static partial class CryptoMath
	{
		#region GCD-LCM region
		
		/// <summary>
		/// Extended GCD algorithm. Finds U and V, that A*U + B*V = GCD(A, B)
		/// </summary>
		/// <param name="inputA">Number A</param>
		/// <param name="inputB">Number B</param>
		/// <param name="U">U</param>
		/// <param name="V">V</param>
		/// <returns>GCD of A and B</returns>
		public static SLongIntB eXtendedGCD(SLongIntB inputA, SLongIntB inputB, out SLongIntB U, out SLongIntB V)
		{
			// GCD == A*U + B*V
			
			SLongIntB A = inputA;
			SLongIntB B = inputB;
			
			U = (SLongIntB)1;
			SLongIntB D = new SLongIntB(A);
			
			if (B.IsZero())
			{
				V = (SLongIntB)0;
				
				return D;
			}
			
			SLongIntB v1 = (SLongIntB)0;
			SLongIntB v3 = new SLongIntB(B);
			
			SLongIntB Q = null;
			SLongIntB t3 = null;
			SLongIntB t1 = null;
			
			while (!v3.IsZero())
			{
				LongMath.Divide(D, v3, out Q, out t3);
				
				t1 = U - (Q*v1);
				
				U.Assign(v1);
				D.Assign(v3);
				
				v1.Assign(t1);
				v3.Assign(t3);
			}
			
			V = D - (U*A);
			V /= B;
			
			return D;
		}
		
		/// <summary>
		/// Uses extended GCD Algorithm to find inverted by modulo element
		/// </summary>
		/// <param name="A">Number</param>
		/// <param name="N">Modulo</param>
		/// <param name="U">Inverted element. Can be less, than zero. 
		/// To get always positive number, use eXGCDInvernedSafe instead.</param>
		/// <returns>GCD of A and N</returns>
		public static SLongIntB eXGCDInverted(SLongIntB A, SLongIntB N, out SLongIntB U)
		{
			// 1 == A*U + N*V
						
			if (A.IsZero())
			{
				U = new SLongIntB();
				
				if (A.IsZero())
					return new SLongIntB(N);
				else
					return new SLongIntB(A);
			}
			
			
			U = (SLongIntB)1;
			SLongIntB G = new SLongIntB(A);
			
			SLongIntB v1 = (SLongIntB)0;
			SLongIntB v3 = new SLongIntB(N);
			
			SLongIntB Q = null;
			SLongIntB t3 = null;
			SLongIntB t1 = null;
			
			while (!v3.IsZero())
			{
				LongMath.Divide(G, v3, out Q, out t3);
				
				t1 = U - (Q*v1);
				
				U.Assign(v1);
				G.Assign(v3);
				
				v1.Assign(t1);
				v3.Assign(t3);
			}
			
			return G;
		}
		
		/// <summary>
		/// Uses extended GCD Algorithm to find inverted by modulo element
		/// </summary>
		/// <param name="A">Number</param>
		/// <param name="N">Modulo</param>
		/// <param name="U">Inverted element, that is always greater zero</param>
		/// <returns>GCD of A and N</returns>
		public static SLongIntB eXGCDInvertedSafe(SLongIntB A, SLongIntB N, out SLongIntB U)
		{
			SLongIntB tempRes = eXGCDInverted(A, N, out U);
			
			if (U < 0)
				U += N;
			
			return tempRes;
		}
		
		/// <summary>
		/// Binary GCD algorithm
		/// </summary>
		/// <param name="inputA">A</param>
		/// <param name="inputB">B</param>
		/// <returns>GCD of A and B</returns>
		public static SLongIntB GCD(SLongIntB inputA, SLongIntB inputB)
		{
			SLongIntB A = LongMath.Abs(inputA);
			SLongIntB B = LongMath.Abs(inputB);
			
			if (A < B)
			{
				SLongIntB temp = A;
				A = B;
				B = temp;
			}
			
			if (B == 0)
				return A;
			
			SLongIntB r = A % B;
			A.Assign(B);
			B.Assign(r);
			
			if (B == 0)
				return A;
			
			int k = 0;
			
			while (LongMath.IsEven(A) && LongMath.IsEven(B))
			{
				++k;
				A.Shr();
				B.Shr();
			}
			
			while (LongMath.IsEven(A))
				A.Shr();
			
			while (LongMath.IsEven(B))
				B.Shr();
			
			do
			{
				SLongIntB t = A - B;
				t.Shr();
				
				if (t.IsZero())
					break;
				
				while (LongMath.IsEven(t))
					t.Shr();
				
				if (t < 0)
				{
					B.Assign(t);
					LongMath.SelfAbs(B);
				}
				else
					A.Assign(t);
				
			} while (true);
			
			A.ShL((uint)k);
			return A;
		}
		
		public static SLongIntB LCM(SLongIntB A, SLongIntB B)
		{
			return (A*B) / GCD(A, B);
		}
		
		#endregion
		
		#region Binary functions
		
		public static ULongIntB BinaryAnd(ULongIntB a, ULongIntB b)
		{
			ULongIntB c = new ULongIntB(a.CoefLength);
			
			for (int i = 0; i < a.Size; ++i)
				c[i] = (ushort)(a[i] & b[i]);
			
			c.Size = System.Math.Max(a.Size, b.Size);
			
			int s = c.Size - 1;
			if (s > 0)
				while (c[s] == 0) 
				{
					--s;
					if (s == 0)
						break;
				}
			
			c.Size = s + 1;
			
			return c;
		}
		
		public static ULongIntB BinaryOr(ULongIntB a, ULongIntB b)
		{
			ULongIntB c = new ULongIntB(a.CoefLength);
			
			for (int i = 0; i < a.Size; ++i)
				c[i] = (ushort)(a[i] | b[i]);
			
			c.Size = System.Math.Max(a.Size, b.Size);
			return c;
		}
		
		public static ULongIntB BinaryXor(ULongIntB a, ULongIntB b)
		{
			ULongIntB c = new ULongIntB(a.CoefLength);
			
			for (int i = 0; i < a.Size; ++i)
				c[i] = (ushort)(a[i] ^ b[i]);
			
			c.Size = System.Math.Max(a.Size, b.Size);
			
			int s = c.Size - 1;
			if (s > 0)
				while (c[s] == 0) 
				{
					--s;
					if (s == 0)
						break;
				}
			
			c.Size = s + 1;
			
			return c;
		}
		
		#endregion
		
		public static SLongIntB ChineRem(SLongIntB[] coefs, SLongIntB[] modules)
		{
			if (coefs.Length != modules.Length)
				throw new ArgumentException("Input parameters have different sizes.");
			
			int i = 0;
			SLongIntB m = modules[i];
			SLongIntB x = coefs[i];
			
			SLongIntB U = null;
			SLongIntB V = null;
			
			while (i < coefs.Length - 1)
			{
				++i;
				SLongIntB gcd = eXtendedGCD(m, modules[i], out U, out V);
				
				if (gcd != 1)
					throw new WrondResultException("Modules are not coprime!");
				
				SLongIntB temp = U*m*coefs[i] + V*modules[i]*x;
				m = m*modules[i];
				
				x = temp % m;
				
				x.LongSign = SignTransformer.GetLSign(temp.Sign*m.Sign);
			}
			
			return x;
		}
		
		public static void TwoFact(SLongIntB A, out SLongIntB u, out int k)
		{
			k = 0;
			u = new SLongIntB(A);
			
			if (A == 0)
				return;
			
			while (LongMath.IsEven(u))
			{
				++k;
				u.Shr();
			}
		}
		
		public static void TwoFact(ULongIntB A, out ULongIntB u, out int k)
		{
			k = 0;
			u = new ULongIntB(A);
			
			if (A == 0)
				return;
			
			while (LongMath.IsEven(u))
			{
				++k;
				u.Shr();
			}
		}
		
		/// <summary>
		/// Divides input number by 2 while can
		/// </summary>
		/// <param name="A">Input number</param>
		/// <param name="k">Number of times, that value is diveded by 2</param>
		public static void SelfTwoFact(SLongIntB A, out int k)
		{
			k = 0;
			
			if (A == 0)
				return;
			
			while (LongMath.IsEven(A))
			{
				A.Shr();
				++k;
			}
		}
		
		/// <summary>
		/// Calculates square of input signed number by modulo
		/// </summary>
		/// <param name="a">Input number</param>
		/// <param name="m">Modulo</param>
		/// <returns>(a*a) % m</returns>
		public static SLongIntB SquareMod(SLongIntB a, SLongIntB m)
		{
			return LongMath.Sqr(a) % m;
		}
		
		/// <summary>
		/// Calculates square of input signed number N times by modulo
		/// </summary>
		/// <param name="a">Input number</param>
		/// <param name="n">Number of times to calculate square</param>
		/// <param name="m">Modulo</param>
		/// <returns>[ a ^ (2*n) ] % m</returns>
		public static SLongIntB NSquareMod(SLongIntB a, uint n, SLongIntB m)
		{
			SLongIntB res = a % m;
			for (int i = 0; i < n; ++i)
				res = LongMath.Sqr(res) % m;
			return res;
		}
		
		//// <summary>
		/// Calculates square of input unsigned number by modulo
		/// </summary>
		/// <param name="a">Input number</param>
		/// <param name="m">Modulo</param>
		/// <returns>(a*a) % m</returns>
		public static ULongIntB SquareMod(ULongIntB a, ULongIntB m)
		{
			return LongMath.Sqr(a) % m;
		}
		
		/// <summary>
		/// Calculates square of input unsigned number N times by modulo
		/// </summary>
		/// <param name="a">Input number</param>
		/// <param name="n">Number of times to calculate square</param>
		/// <param name="m">Modulo</param>
		/// <returns>[ a ^ (2*n) ] % m</returns>
		public static ULongIntB NSquareMod(ULongIntB a, uint n, ULongIntB m)
		{
			ULongIntB res = a % m;
			for (int i = 0; i < n; ++i)
				res = LongMath.Sqr(res) % m;
			return res;
		}
		
		/// <summary>
		/// Calculates Jacobi symbol for long numbers A and B -  (A/B)
		/// </summary>
		/// <param name="A"></param>
		/// <param name="B"></param>
		/// <returns></returns>
		public static int JacobiSymbol(SLongIntB a, SLongIntB b)
		{
			if (b.IsZero())
			{
				if (a == 1 || a == -1)
					return 1;
				else
					return 0;
			}
			
			if (LongMath.IsEven(a) && LongMath.IsEven(b))
				return 0;
			
			SLongIntB A = new SLongIntB(a);
			SLongIntB B = new SLongIntB(b);
			
			sbyte[] mods = new sbyte[] { 0, 1, 0, -1, 0, -1, 0, 1 };
			
			int v = 0;
			SelfTwoFact(B, out v);
			
			int k = 1;
			
			if (v % 2 == 1)
				k = mods[A[0] & 7];
			
			LongMath.SelfAbs(B);
			if (A < 0)
				k = -k;
			
			while (A > 0)
			{
				SelfTwoFact(A, out v);
				
				if (v % 2 == 1)
					k = mods[B[0] & 7] * k;
				
				if ((A[0] & B[0] & 2) != 0)
					k = -k;
				
				SLongIntB r = LongMath.Abs(A);
				A.Assign(B % r);
				B.Assign(r);
			}
			
			if (B > 1)
				return 0;
			else
				return k;
		}
		
		/// <summary>
		/// Calculates binary logarithm of input number
		/// </summary>
		/// <param name="a">Input number</param>
		/// <returns>Binary logarithm</returns>
		public static uint Log2(ULongIntB a)
		{
			if (a.Size <= 0)
				return 0;
			
			int temp = a[a.Size - 1];
			int bitsCount = 0;
			while (temp != 0)
			{
				++bitsCount;
				temp >>= 1;
			}
			
			return (uint)(bitsCount + (a.Size - 1)*LIntConstant.BitsPerDigit - 1);
		}
		
		public static ULongIntB ExpMod5(ULongIntB a, ULongIntB n, ULongIntB m)
		{
			if (a == 0)
				return (ULongIntB)0;
			
			if (a == 1)
				return (ULongIntB)1;
			
			if (n == 0)
				return (ULongIntB)1;
			
			if (m == 1)
				return (ULongIntB)0;
			
			int k = 5;
			int M = (1 << k);
			
			ULongIntB[] mods = new ULongIntB[(M >> 1) + 1];
			ULongIntB tempSquare = SquareMod(a, m);
			mods[0] = a % m;
			
			// generate table of numbers  [  (a ^ k) mod m, where k == (2*j + 1) ]
			int index = 1;
			int i = 3;
			while (i <= M - 1)
			{
				mods[index] = (mods[index - 1] * tempSquare) % m;
				
				++index;
				i += 2;
			}
			
			int bitsCount = LIntConstant.BitsPerDigit;
			
			
			int log = (int)Log2((ULongIntB)n) + 1;
			
			// length of power in 2^k number system
			int N = log / k;
			if ((log % k) != 0)
			    ++N;
			
			i = N - 1;
			
			int s = (k*i) / bitsCount;
			int d = (k*i) % bitsCount;
			
			int eN = (((int)n[s] | ((int)n[s + 1] << bitsCount)) >> d) & (M - 1);
			
			int t, u;
			
			ULongIntB p = null;
			
			if (eN == 0)
				p = (ULongIntB)1;
			else
			{
				LongIntHelper.TwoFact(eN, out t, out u);
				
				index = ((u - 1) / 2);
				p = new ULongIntB(mods[index]);				
				p = NSquareMod(p, (uint)t, m);
			}
			
			i = N - 2;
			
			while (i >= 0)
			{
				s = (k*i) / bitsCount;
				d = (k*i) % bitsCount;
				
				int e = (((int)n[s] | ((int)n[s + 1] << bitsCount)) >> d) & (M - 1);
				
				if (e == 0)
					p = NSquareMod(p, (uint)k, m);
				else
				{
					LongIntHelper.TwoFact(e, out t, out u);
					p = NSquareMod(p, (uint)(k - t), m);
					
					index = ((u - 1) / 2);
					p = (p * mods[index]) % m;
					p = NSquareMod(p, (uint)t, m);					
				}
				
				--i;
			}
			
			return p;
		}
		
		
		internal static int TestSmallPrimes(ULongIntB n, uint ladgestSmallPrime)
		{
			if (ladgestSmallPrime > LIntConstant.smallPrimes[LIntConstant.smallPrimes.Length - 1])
				ladgestSmallPrime = LIntConstant.smallPrimes[LIntConstant.smallPrimes.Length - 1];
			
			// by testing small numbers
			// we can avoid testing on 85% other natural numbers
			for (int i = 0; LIntConstant.smallPrimes[i] < ladgestSmallPrime; ++i)
			{
				ushort r = n % LIntConstant.smallPrimes[i];
				if (r == 0)
					return r;
			}
			
			return 0;
		}
		
		/// <summary>
		/// Test if input number is prime
		/// </summary>
		/// <param name="n">Input number</param>
		/// <param name="rounds">Number of times to provide test</param>
		/// <returns>Exact answer if input number is composite, and 
		/// a probable answer if input number can be prime. Probability of mistake
		/// answer is (0.25)^(Rounds number)</returns>
		public static PrimeTestResult TestPrimeMillerRabin(ULongIntB n, RoundsNumber rounds)
		{
			int r = TestSmallPrimes(n, 2000);
			if (r != 0)
				return PrimeTestResult.Composite;
			
			ULongIntB Nm1 = new ULongIntB(n);
			--Nm1;
			
			int t = 0;
			ULongIntB q = null;
			TwoFact(Nm1, out q, out t);
			
			RandomLong rand = new RandomLong((ulong)DateTime.Now.Millisecond);
			
			for (int i = 0; i < (int)rounds; ++i)
			{
				ULongIntB a = new ULongIntB(rand.NextNotOne(Nm1), ConstructorMode.Assign);
				ULongIntB b = ExpMod5(a, q, n);
				
				if ((b == 1) || b == Nm1)
					continue;
				
				bool next = false;
				
				for (int j = 0; j < t - 1; ++j)
				{
					b = SquareMod(b, n);
					if (b == 1)
						return PrimeTestResult.Composite;
						
					if (b == Nm1)
					{
						next = true;
						break;
					}
				}
				
				if (!next)
					return PrimeTestResult.Composite;
			}
			
			return PrimeTestResult.DontKnow;
		}
	}
}
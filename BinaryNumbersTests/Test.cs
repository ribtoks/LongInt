using System;
using NUnit.Framework;

using LongInt;
using LongInt.Math;
using LongInt.Math.Special;

namespace BinaryNumbersTests
{
	[TestFixture()]
	public class Test
	{		
		[Test()]
		public void TestAdditionCommutativity()
		{
			SLongIntB A = (SLongIntB)1236154;
			SLongIntB B = (SLongIntB)894876154;
			
			Assert.AreEqual(A + B, B + A);
		}
		
		[Test()]
		public void TestAddition1()
		{
			SLongIntB A = new SLongIntB("-98276598235872687523562056");
			SLongIntB B = (SLongIntB)0;
			short k = 120;
			
			for (int i = 0; i < (int)k; ++i)
				B += A;
			
			Assert.AreEqual(k*A, B);
			
			A *= -1;
			B = (SLongIntB)0;
			
			for (int i = 0; i < (int)k; ++i)
				B += A;
			
			Assert.AreEqual(k*A, B);
		}
		
		[Test()]
		public void TestAddition2()
		{
			SLongIntB A = new SLongIntB("-982");
			SLongIntB B = new SLongIntB(A);
			
			for (int i = 1; i < 1000; ++i)
			{
				A++;
				Assert.AreEqual(A, B + (short)i);
			}
		}
		
		[Test()]
		public void TestSubstraction1()
		{
			SLongIntB N = new SLongIntB("2945729752935981200000005151659293467923476293623");
			RandomLong rand = new RandomLong((ulong)DateTime.Now.Millisecond);
			
			for (int i = 0; i < 1000; ++i)
			{
				SLongIntB A = new SLongIntB(rand.Next(N), ConstructorMode.Assign);
				SLongIntB B = new SLongIntB(rand.Next(N), ConstructorMode.Assign);
				
				SLongIntB C = A + B;
				
				Assert.AreEqual(C - B, A, C.ToString() + " " + B.ToString());
				Assert.AreEqual(C - A, B, C.ToString() + " " + A.ToString());
				
				Assert.AreEqual(C - 0, C);
				Assert.AreEqual(C - C, (SLongIntB)0);
			}
		}
		
		[Test()]
		public void TestSubstraction2()
		{
			SLongIntB A = new SLongIntB("239");
			SLongIntB B = new SLongIntB(A);
			
			for (int i = 1; i < 1000; ++i)
			{
				--A;
				Assert.AreEqual(A, B - (short)i);
			}
		}
		
		[Test()]
		public void TestSubstraction3()
		{
			SLongIntB N = new SLongIntB("2945729752935981200000005151659293467923476293623");
			RandomLong rand = new RandomLong((ulong)DateTime.Now.Millisecond);
			
			for (int i = 0; i < 1000; ++i)
			{
				SLongIntB A = new SLongIntB(rand.Next(N), ConstructorMode.Assign);
				SLongIntB B = new SLongIntB(rand.Next(N), ConstructorMode.Assign);
				
				SLongIntB C = A - B;
				
				Assert.AreEqual(C + B, A);
				Assert.AreEqual(LongMath.Abs(C - A), B);
				
				Assert.AreEqual(C - 0, C);
				Assert.AreEqual(C - C, (SLongIntB)0);
			}
		}
		
		[Test()]
		public void TestDistributivity()
		{
			SLongIntB A = (SLongIntB)1236154;
			SLongIntB B = (SLongIntB)894876154;
			SLongIntB C = (SLongIntB)(-23462767);
			
			Assert.AreEqual((A + B)*C, C*B + C*A);
			Assert.AreEqual((A - C)*B, A*B - C*B);
		}
		
		[Test()]
		public void TestSquare()
		{
			SLongIntB A = (SLongIntB)265727;
			SLongIntB B = (SLongIntB)894876154;
			
			Console.WriteLine(LongMath.Sqr(A + B).ToString());
			
			Assert.AreEqual(LongMath.Sqr(A + B), A*A + (A*B) + (A*B) + B*B);
		}
		
		[Test()]
		public void TestDivision1()
		{
			SLongIntB N = new SLongIntB("2945729752935981200000005151659293467923476293623");
			RandomLong rand = new RandomLong((ulong)DateTime.Now.Millisecond);
			
			for (int i = 0; i < 1000; ++i)
			{
				SLongIntB A = new SLongIntB(rand.Next(N), ConstructorMode.Assign);
				SLongIntB B = new SLongIntB(rand.Next(N), ConstructorMode.Assign);
				
				SLongIntB C = A*B;
				
				if (C == 0)
					continue;
			
				Assert.AreEqual(C / B, A);
				Assert.AreEqual(C / A, B);
			}
		}
		
		[Test()]
		public void TestDivision2()
		{
			SLongIntB N = new SLongIntB("2945729752935981200000005151659293467923476293623");
			RandomLong rand = new RandomLong((ulong)DateTime.Now.Millisecond);
			
			for (int i = 0; i < 1000; ++i)
			{
				SLongIntB A = new SLongIntB(rand.Next(N), ConstructorMode.Assign);
				SLongIntB B = new SLongIntB(rand.Next(N), ConstructorMode.Assign);
				
				if (B == 0)
					continue;
				
				if ((B * (A / B)) + (A % B) != A)
				{
					Console.WriteLine(A);
					Console.WriteLine(B);
					
					Console.WriteLine((A/B).ToString());
					Console.WriteLine((A%B).ToString());
				}
				
				Assert.AreEqual((B * (A / B)) + (A % B), A, string.Join(" ", 
				        new string[]{ A.ToString(), B.ToString(), (A/B).ToString(), (A%B).ToString() }));
			}
		}
		
		[Test()]
		public void TestMul1()
		{
			SLongIntB N = new SLongIntB("2945729752935981200000005151659293467923476293623");
			RandomLong rand = new RandomLong((ulong)DateTime.Now.Millisecond);
			
			for (int i = 0; i < 1000; ++i)
			{
				SLongIntB temp1 = new SLongIntB(rand.Next(N), ConstructorMode.Assign);
				SLongIntB temp2 = new SLongIntB(rand.Next(N), ConstructorMode.Assign);
				SLongIntB temp3 = new SLongIntB(rand.Next(N), ConstructorMode.Assign);
				
				Assert.AreEqual((temp1*temp2)*temp3, temp1*(temp2*temp3));
			}
		}
		
		[Test()]
		public void PowerTest1()
		{
			SLongIntB A = new SLongIntB("-98276598235872");
			SLongIntB B = (SLongIntB)1;
			short k = 120;
			
			for (int i = 0; i < (int)k; ++i)
				B *= A;
			
			Assert.AreEqual(LongMath.Exp(A, (ulong)k), B);
		}
		
		[Test()]
		public void PowerTest2()
		{
			SLongIntB A = new SLongIntB("-98276598235872");
			short r = 12, s = 31;
			
			Assert.AreEqual(LongMath.Exp(A, (ulong)(r*s)), LongMath.Exp(LongMath.Exp(A, (ulong)s), (ulong)r));
		}
		
		[Test()]
		public void PowerTest3()
		{
			SLongIntB A = new SLongIntB("-98276598235872");
			short r = 12, s = 31;
			
			Assert.AreEqual(LongMath.Exp(A, (ulong)(r + s)), LongMath.Exp(A, (ulong)r) * LongMath.Exp(A, (ulong)s));
		}
		
		[Test()]
		public void TestShifts1()
		{
			SLongIntB A = new SLongIntB("-98276598235872");
			SLongIntB B = new SLongIntB(A);
			
			for (int i = 0; i < 10; ++i)
			{
				A.Shr();
				B /= 2;
				Assert.AreEqual(A, B);
			}
			
			for (int i = 0; i < 10; ++i)
			{
				A.Shl();
				B *= 2;
				Assert.AreEqual(A, B);
			}
		}
		
		[Test()]
		public void TestShifts2()
		{
			SLongIntB A = new SLongIntB("-98276598235872");
			SLongIntB B = new SLongIntB(A);
			
			for (int i = 1; i < 11; ++i)
			{
				SLongIntB C = new SLongIntB(A);
				
				C.ShR((uint)i);
				B /= 2;
				Assert.AreEqual(C, B);
			}
		}
		
		[Test()]
		public void TestShifts3()
		{
			SLongIntB A = (SLongIntB)0;
			SLongIntB B = (SLongIntB)1;
			
			for (int i = 1; i < 111; ++i)
			{
				A.SetBit((uint)i);
				B.ShL((uint)i);
				
				Assert.AreEqual(A, B);
				
				B.ShR((uint)i);
				A.UnSetBit((uint)i);
			}
		}
		
		[Test()]
		public void TestShifts4()
		{
			SLongIntB A = new SLongIntB("-98276598235872");
			SLongIntB B = new SLongIntB(A);
			
			A.Mod2n(10);
			ushort b = B % 1024;
			
			Assert.AreEqual(A, (SLongIntB)b);
		}
		
		[Test()]
		public void TestConvertFromInt()
		{
			int a = -123515136;
			SLongIntB A = (SLongIntB)a;
			Assert.AreEqual(a.ToString(), A.ToString());
		}
		
		[Test()]
		public void TestBinaryDecimalCast()
		{
			SLongIntB A = new SLongIntB("-20976982698769825692774362798672976276668754000000000000000765700000000");
			SLongIntD B = (SLongIntD)A;
			
			Assert.AreEqual(A.ToString(), B.ToString());
			Assert.AreEqual((SLongIntD)A, B);
			Assert.AreEqual(A, (SLongIntB)B);
		}
		
		[Test()]
		public void TestShiftRight()
		{
			SLongIntB N = new SLongIntB("2945729752935981200000005151659293467923476293623");
			RandomLong rand = new RandomLong((ulong)DateTime.Now.Millisecond);
			
			for (int i = 0; i < 1000; ++i)
			{
				SLongIntB temp1 = new SLongIntB(rand.Next(N), ConstructorMode.Assign);
				SLongIntB temp3 = new SLongIntB(temp1);
				
				temp1.Shr();
				
				SLongIntB temp2 = new SLongIntB(temp1);
				temp2.Shl();
				
				Assert.AreEqual(temp1 * 2, temp2);
				Assert.AreEqual(temp1, temp3 / 2);
			}
		}
		
		//[Test()]
		public void TestExtendedGCD()
		{
			SLongIntB N = new SLongIntB("2945729752935981200000005151659293467923476293623");
			RandomLong rand = new RandomLong((ulong)DateTime.Now.Millisecond);
			
			SLongIntB U = null, V = null;
			
			for (int i = 0; i < 1000; ++i)
			{
				SLongIntB temp1 = new SLongIntB(rand.Next(N), ConstructorMode.Assign);
				SLongIntB temp2 = new SLongIntB(rand.Next(N), ConstructorMode.Assign);
				
				SLongIntB gcd = CryptoMath.eXtendedGCD(temp1, temp2, out U, out V);
				SLongIntB gcdB = CryptoMath.GCD(temp1, temp2);
				
				Assert.AreEqual(gcd, temp1*U + temp2*V);				
				Assert.AreEqual(gcd, gcdB, temp1.ToString() + " " + temp2.ToString() + " " + gcd.ToString() + " " + gcdB.ToString());
			}
		}
		
		[Test()]
		public void TestSquare2()
		{
			SLongIntB N = new SLongIntB("2945729752935981200000005151659293467923476293623");
			RandomLong rand = new RandomLong((ulong)DateTime.Now.Millisecond);
			
			for (int i = 0; i < 1000; ++i)
			{
				SLongIntB temp = new SLongIntB(rand.Next(N), ConstructorMode.Assign);
				Assert.AreEqual(temp*temp, LongMath.Sqr(temp));
			}
		}
		
		[Test()]
		public void TestPower()
		{
			SLongIntB N = new SLongIntB("2945");
			RandomLong rand = new RandomLong((ulong)DateTime.Now.Millisecond);
			Random rand32 = new Random(DateTime.Now.Millisecond);
			
			SLongIntB m = new SLongIntB("98696766574783");
			
			for (int i = 0; i < 100; ++i)
			{
				SLongIntB temp = new SLongIntB(rand.Next(N), ConstructorMode.Assign);
				int power = rand32.Next(ushort.MaxValue) % 10000;
				
				Assert.AreEqual(CryptoMath.ExpMod5((ULongIntB)temp, (ULongIntB)power, (ULongIntB)m), LongMath.Exp((ULongIntB)temp, (ulong)power) % (ULongIntB)m,
				                temp.ToString() + "^" + power.ToString());
			}
		}
	}
}
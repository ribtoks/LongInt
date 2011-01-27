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
			SLongIntD A = (SLongIntD)1236154;
			SLongIntD B = (SLongIntD)894876154;
			
			Assert.AreEqual(A + B, B + A);
		}
		
		[Test()]
		public void TestAddition1()
		{
			SLongIntD A = new SLongIntD("-98276598235872687523562056");
			SLongIntD B = (SLongIntD)0;
			short k = 120;
			
			for (int i = 0; i < (int)k; ++i)
				B += A;
			
			Assert.AreEqual(k*A, B);
		}
		
		[Test()]
		public void TestAddition2()
		{
			SLongIntD A = new SLongIntD("-982");
			SLongIntD B = new SLongIntD(A);
			
			for (int i = 1; i < 1000; ++i)
			{
				A++;
				Assert.AreEqual(A, B + (short)i);
			}
		}
		
		[Test()]
		public void TestSubstraction1()
		{
			SLongIntD A = new SLongIntD("-2945729752935981200000005151659293467923476293623");
			SLongIntD B = new SLongIntD("2398762987692620872000000");
			
			SLongIntD C = A + B;
			
			Console.WriteLine(C);
			
			
			Assert.AreEqual(C - A, B);
			Assert.AreEqual(C - B, A);
			
			Assert.AreEqual(C - 0, C);
			
			Assert.AreEqual(C - C, (SLongIntD)0);
		}
		
		[Test()]
		public void TestSubstraction2()
		{
			SLongIntD A = new SLongIntD("239");
			SLongIntD B = new SLongIntD(A);
			
			for (int i = 1; i < 1000; ++i)
			{
				--A;
				Assert.AreEqual(A, B - (short)i);
			}
		}
		
		[Test()]
		public void TestDistributivity()
		{
			SLongIntD A = (SLongIntD)1236154;
			SLongIntD B = (SLongIntD)894876154;
			SLongIntD C = (SLongIntD)(-23462767);
			
			Assert.AreEqual((A + B)*C, C*B + C*A);
		}
		
		[Test()]
		public void TestSquare()
		{
			SLongIntD A = (SLongIntD)265727;
			SLongIntD B = (SLongIntD)894876154;
			
			Console.WriteLine(LongMath.Sqr(A + B).ToString());
			
			Assert.AreEqual(LongMath.Sqr(A + B), A*A + (A*B) + (A*B) + B*B);
		}
		
		[Test()]
		public void TestDivision1()
		{
			SLongIntD A = new SLongIntD("-2945729752935981200000005151659293467923476293623");
			SLongIntD B = new SLongIntD("2398762987692620872000000");
			SLongIntD C = A*B;
			
			Assert.AreEqual(C / B, A);
			Assert.AreEqual(C / A, B);
		}
		
		[Test()]
		public void TestDivision2()
		{
			SLongIntD A = new SLongIntD("-2945729752935981200000005151659293467923476293623");
			SLongIntD B = new SLongIntD("2398762987692620872000000");
			
			Console.WriteLine((A % B).ToString());
			
			Assert.AreEqual(LongMath.Abs((B * (A / B))) + (A % B), LongMath.Abs(A));
		}
		
		[Test()]
		public void PowerTest1()
		{
			SLongIntD A = new SLongIntD("-98276598235872");
			SLongIntD B = (SLongIntD)1;
			short k = 120;
			
			for (int i = 0; i < (int)k; ++i)
				B *= A;
			
			Assert.AreEqual(LongMath.Exp(A, (ulong)k), B);
		}
		
		[Test()]
		public void PowerTest2()
		{
			SLongIntD A = new SLongIntD("-98276598235872");
			short r = 12, s = 31;
			
			Assert.AreEqual(LongMath.Exp(A, (ulong)(r*s)), LongMath.Exp(LongMath.Exp(A, (ulong)s), (ulong)r));
		}
		
		[Test()]
		public void PowerTest3()
		{
			SLongIntD A = new SLongIntD("-98276598235872");
			short r = 12, s = 31;
			
			Assert.AreEqual(LongMath.Exp(A, (ulong)(r + s)), LongMath.Exp(A, (ulong)r) * LongMath.Exp(A, (ulong)s));
		}
		
		[Test()]
		public void TestConvertFromInt()
		{
			int a = -123515136;
			SLongIntD A = (SLongIntD)a;
			Assert.AreEqual(a.ToString(), A.ToString());
		}
	}
}
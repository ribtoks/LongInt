using System;

using LongInt;
using LongInt.Math;
using LongInt.Math.Special;

using RSAlib;

namespace LongIntTesting
{
	
	class MainClass
	{
		public static void Main (string[] args)
		{
			/*
			RandomLong rand = new RandomLong((ulong)DateTime.Now.Millisecond);
			
			SLongIntB p = new SLongIntB("359334085968622831041960188598043661065388726959079837");
			SLongIntB q = new SLongIntB("35742549198872617291353508656626642567");
			
			for (int i = 0; i < 100; ++i)
			{
				//Console.WriteLine(rand.Next(p, q).ToString());
				Console.WriteLine(rand.Next(p).ToString());
			}
			*/
			/*
			ULongIntB I = (ULongIntB)1;
			
			for (int i = 1; i < 10000000; ++i, ++I)
			{
				uint log = (uint)Math.Floor(Math.Log((double)i) / Math.Log(2.0));
				if (log != CryptoMath.Log2(I))
				{
					Console.WriteLine("{0} {1} {2} {3}", i, I.ToString(), CryptoMath.Log2(I), log);
					throw new Exception();
				}
			}
			
			Console.WriteLine("Done");
			
		
			
			
			SLongIntB N = new SLongIntB("2945");
			RandomLong rand = new RandomLong((ulong)DateTime.Now.Millisecond);
			Random rand32 = new Random(DateTime.Now.Millisecond);
			
			SLongIntB m = new SLongIntB("986219628765865723478523");
			
			for (int i = 0; i < 100; ++i)
			{
				SLongIntB temp = new SLongIntB(rand.Next(N), ConstructorMode.Assign);
				int power = rand32.Next(ushort.MaxValue);
				
				if (CryptoMath.PowerMod(temp, (SLongIntB)power, m) != (LongMath.Power(temp, (ulong)power) % m))
				{
					Console.WriteLine("{0} - {1}", temp.ToString(), power);
					throw new Exception();
				}
			}			
			
			SLongIntB N = new SLongIntB("294");//5729752935981200000005151659293467923476293623");
			RandomLong rand = new RandomLong((ulong)DateTime.Now.Millisecond);
			
			SLongIntB U = null, V = null;
			
			for (int i = 0; i < 100000; ++i)
			{
				SLongIntB temp1 = new SLongIntB(rand.Next(N), ConstructorMode.Assign);
				SLongIntB temp2 = new SLongIntB(rand.Next(N), ConstructorMode.Assign);
				
				SLongIntB gcd = CryptoMath.eXtendedGCD(temp1, temp2, out U, out V);
				
				//Assert.AreEqual(gcd, temp1*U + temp2*V);
				
				SLongIntB cGCD = CryptoMath.GCD(temp1, temp2);
				
				if (gcd != cGCD)
				{
					Console.WriteLine("{0} {1} {2} {3}", temp1.ToString(), temp2.ToString(), gcd.ToString(), cGCD);
					throw new Exception();
				}
			}
			
			
			SLongIntB temp1 = new SLongIntB("11654994443698947172");
			SLongIntB temp2 = new SLongIntB("1807189884270654913554787050078756234804427523018");
			
			SLongIntB U = null, V = null;
			
			Console.WriteLine(CryptoMath.GCD(temp1, temp2).ToString());
			Console.WriteLine(CryptoMath.eXtendedGCD(temp1, temp2, out U, out V).ToString());
			
			Console.WriteLine((temp1*U + temp2*V).ToString());
			
			Console.WriteLine(StupidGCD(temp1, temp2).ToString());
			
			//SLongIntB N = new SLongIntB("2945");
			//RandomLong rand = new RandomLong((ulong)DateTime.Now.Millisecond);
			//Random rand32 = new Random(DateTime.Now.Millisecond);
			
			ULongIntB a = new ULongIntB("359334085968622831041960188598043661065388726959079621");
			
			if (CryptoMath.TestPrimeMillerRabin(a, RoundsNumber.High) == PrimeTestResult.DontKnow)
				Console.WriteLine("Maybe prime!");
			else
				Console.WriteLine("Composite");
			
			Console.WriteLine("Done");
			*/
			
			
			RSAKeyGenerator keyGen = new RSAKeyGenerator((ulong)DateTime.Now.Millisecond);
			
			for (int i = 0; i < 10; ++i)
			{
				keyGen.GenerateRSAKey(BitLength.Length256Bit);
			
				ULongIntB message = (ULongIntB)356891919;
			
				ULongIntB encoded = RSAEncoder.Encode(keyGen.GetPublicKey(), message);
			
				ULongIntB decoded = RSADecoder.Decode(keyGen.GetPrivateKey(), encoded);
			
				//Console.WriteLine("Found decoded: {0}", decoded.ToString());
			
				if (message == decoded)
					Console.WriteLine("It works!!! - {0}", i);
				else
					throw new Exception("Not equal!!!");
			}
			
			Console.WriteLine("Done...");
			
			Console.ReadLine();
		}
		
		public static SLongIntB StupidGCD(SLongIntB a, SLongIntB b)
		{
			while (b != 0)
			{
				SLongIntB r = a % b;
				a.Assign(b);
				b.Assign(r);
			}
			return a;
		}
	}
}

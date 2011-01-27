using System;
using LongInt;
using LongInt.Math;
using LongInt.Math.Special;

namespace RSAlib
{
	internal class RSAKey
	{
		SLongIntB e;
		SLongIntB d;
		SLongIntB n;
		
		#region Properties
		
		public SLongIntB N {
			get {
				return n;
			}
			set {
				n = value;
			}
		}
		
		
		public SLongIntB E {
			get {
				return e;
			}
			set {
				e = value;
			}
		}
		
		
		public SLongIntB D {
			get {
				return d;
			}
			set {
				d = value;
			}
		}
		
		#endregion
				
		public RSAKey ()
		{
		}
	}
	
	/// <summary>
	/// Class, that represents pair of values,
	/// that are public key for RSA algorithm
	/// </summary>
	public class RSAPublicKey
	{
		SLongIntB e;
		SLongIntB n;
		
		public SLongIntB N 
		{
			get { return n; }
		}
		
		public SLongIntB E 
		{
			get {return e;}
		}
		
		public RSAPublicKey(SLongIntB eKey, SLongIntB nKey)
		{
			e = new SLongIntB(eKey);
			n = new SLongIntB(nKey);
		}
		
		public RSAPublicKey(RSAPublicKey from)
			: this(from.e, from.n)
		{
		}
	}
	
	/// <summary>
	/// Class, that represents pair of values,
	/// that are private key for RSA algorithm
	/// </summary>
	public class RSAPrivateKey
	{
		SLongIntB d;
		SLongIntB n;
		
		public SLongIntB N 
		{
			get { return n; }
		}
		
		public SLongIntB D
		{
			get {return d;}
		}
		
		public RSAPrivateKey(SLongIntB dKey, SLongIntB nKey)
		{
			d = new SLongIntB(dKey);
			n = new SLongIntB(nKey);
		}
		
		public RSAPrivateKey(RSAPrivateKey from)
			: this(from.d, from.n)
		{
		}
	}
}

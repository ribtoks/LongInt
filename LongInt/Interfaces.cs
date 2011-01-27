using System;

namespace LongInt
{
	public interface ILongSigned
	{
		/// <summary>
		/// Sign of long number
		/// </summary>
		int Sign
		{
			get;
		}
		
		LSign LongSign
		{
			get;
			set;
		}
		
		bool IsPositive
		{
			get;
		}
		
		void ChangeSign();
		
		ILongUnsigned GetUnsignedClone();
	}
	
	public interface ILongUnsigned
	{
		ILongSigned GetSignedClone();
	}
}

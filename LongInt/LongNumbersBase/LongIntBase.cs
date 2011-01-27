using System;
using System.Text;
using LongInt.Math;

namespace LongInt
{
	/// <summary>
	/// Basic representation of long numbers
	/// </summary>
	public class LongIntBase
	{		
		//maximum number of digits (also can be 2097152*2)
		public static readonly uint MaxRange = 10000;
		
		#region Data
		
		protected ushort[] coef;
		protected int size;
		protected uint numberBase;
		
		#endregion
		
		#region Properties
		
		internal ushort this[int index]
		{
			get { return coef[index]; }
			set { coef[index] = value; }
		}
		
		public int Size
		{
			get { return size; }
			internal set { size = value; }
		}
		
		public uint CoefLength
		{
			get { return (uint)coef.Length; }
		}
		
		public uint Base
		{
			get { return numberBase; }
		}
		
		public NumberBase NumBase
		{
			get { return (NumberBase)numberBase; }
		}
		
		#endregion
		
		#region Constructors
		
		/// <summary>
		/// Constructs emply long number
		/// </summary>
		/// <param name="Base">Сoeficients notation base</param>
		public LongIntBase (uint Base)
		{
			numberBase = Base;
			coef = new ushort[0];
			size = 0;
		}
		
		/// <summary>
		/// Constructs zero long number from 
		/// coeficients of specified length
		/// </summary>
		/// <param name="Base">Coeficients notatino</param>
		/// <param name="coefLength">Length of coeficients array</param>
		public LongIntBase (uint Base, uint coefLength)
		{
			numberBase = Base;
			coef = new ushort[coefLength];
			
			if (coefLength == 0)
				size = 0;
			else
				size = 1;
		}
		
		/// <summary>
		/// Copy contrtuctor
		/// </summary>
		/// <param name="From">Number to copy from</param>
		public LongIntBase (LongIntBase From)
		{
			numberBase = From.numberBase;
			coef = (ushort[])From.coef.Clone();
			size = From.size;
		}
		
		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="From">Number to copy from</param>
		/// <param name="mode">Mode of copying (Full and References copying)</param>
		public LongIntBase (LongIntBase From, ConstructorMode mode)
		{
			numberBase = From.numberBase;
			size = From.size;
			if (mode == ConstructorMode.Copy)
				coef = (ushort[])From.coef.Clone();
			else
				if (mode == ConstructorMode.Assign)
					coef = From.coef;
		}
		
		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="From">Number to copy from</param>
		/// <param name="MaxSize">Maximum size cells to copy</param>
		public LongIntBase (LongIntBase From, uint MaxSize)
		{
			size = From.size;
			coef = new ushort[MaxSize];
			numberBase = From.numberBase;
			Buffer.BlockCopy(From.coef, 0, coef, 0, size*sizeof(ushort));
		}
		
		/// <summary>
		/// Constructor from usual numeric type
		/// </summary>
		/// <param name="number">Number to create from</param>
		/// <param name="Base">Coeficients notation base</param>
		/// <param name="coefLength">Length of array with coeficients</param>
		public LongIntBase (ulong number, uint Base, uint coefLength)
		{
			numberBase = Base;
			
			coef = new ushort[coefLength];
			
			int index = 0;
			do
			{
				coef[index] = (ushort)(number % Base);
				++index;
				number /= Base;
				
				if (index >= coefLength)
				{
					size = index;
					throw new NotEnoughSpaceException("coefLength parameter is too short!");
				}					
			}
			while (number != 0);
			
			size = index;
		}
		
		/// <summary>
		/// Constructs long number from characters array
		/// </summary>
		/// <param name="array">Array of chars</param>
		/// <param name="Base">Coeficients notation base</param>
		public LongIntBase (char[] array, uint Base)
			:this(array, Base, System.Math.Max((uint)array.Length, LongIntBase.MaxRange))
		{
		}
		
		/// <summary>
		/// Constructs long number from 
		/// characters array with specified length
		/// </summary>
		/// <param name="array">Array of chars</param>
		/// <param name="Base">Coeficients notation base</param>
		/// <param name="coefLength">Length of number coeficients</param>
		public LongIntBase (char[] array, uint Base, uint coefLength)
		{
			numberBase = Base;
			coef = new ushort[coefLength];
			
			int maxIndex = array.Length;
			// if coefLength is less, that chars array
			// length, that copy as many chars
			// as our coeficients array can store
			if (coefLength < array.Length)
				maxIndex = (int)coefLength;
			
			for (int i = 0; i < maxIndex; ++i)
				coef[i] = (ushort)array[i];
			
			size = array.Length;
			
			Normalize();
		}
		
		/// <summary>
		/// Constructs long number from number
		/// in string representation
		/// </summary>
		/// <param name="line">String with long number</param>
		/// <param name="Base">Coeficients notation base</param>
		/// <param name="coefLength">Length of coeficients array</param>
		public LongIntBase (string line, uint Base, uint coefLength)
		{
			numberBase = Base;
			string s = "";
			
			if (line[0] == '-')
				s = line.Substring(1);
			else
				s = line.Substring(0);
			
			LongIntBase temp = new LongIntBase(Base, coefLength);
			temp.size = 1;
			
			int index = 0;
			int charsRead = 0;
			ushort mulFull = 10000;
			ushort mulValue;
			
			ushort currValue = 0;
			while (charsRead < s.Length)
			{
				if (s.Length - (index + 1)*4 > 0)
				{
					currValue = Convert.ToUInt16(s.Substring(index * 4, 4));
					mulValue = mulFull;
					
					charsRead += 4;
				}
				else
				{
					string lastString = s.Substring(index * 4);
					currValue = Convert.ToUInt16(lastString);
					mulValue = (ushort)System.Math.Pow(10, lastString.Length);
					charsRead += lastString.Length;
				}
				
				LongMath.UShortMulSafe(ref temp, mulValue);
				LongMath.Increment(ref temp, currValue);
				++index;
			}
			
			coef = (ushort[])temp.coef.Clone();
			size = temp.size;
		}
		
		#endregion
		
		#region Interface implementation
		
		/// <summary>
		/// Shifts long number on specified
		/// number of digits left
		/// </summary>
		/// <param name="mulCount">Number of times to multiply on Base</param>
		public void MulBase(int mulCount)
		{
			// if number is negative, than divide base
			if (mulCount < 0)
				DivBase(-mulCount);
			
			if (!IsZero() && mulCount > 0)
			{
				if (size + mulCount >= this.CoefLength)
					throw new NotEnoughSpaceException("Number length is too small.");
				
				for (int i = size + mulCount - 1; i >= mulCount; --i)
					coef[i] = coef[i - mulCount];
				
				for (int i = 0; i < mulCount; ++i)
					coef[i] = 0;
				
				size += mulCount;
			}
		}
		
		/// <summary>
		/// Shifts long number on specified
		/// number of digits right
		/// </summary>
		/// <param name="divCount">Number of times to divide on Base</param>
		public void DivBase(int divCount)
		{
			// if number is negative, than multiply base
			if (divCount < 0)
				MulBase(-divCount);
			
			if (!IsZero() && divCount > 0)
			{
				if (size <= divCount)
				{
					for (int i = 0; i < size; ++i)
						coef[i] = 0;
					
					size = 1;
					
					return;
				}
				
				for (int i = 0; i < size - divCount; ++i)
					coef[i] = coef[i + divCount];
				
				for (int i = size - divCount; i < size; ++i)
					coef[i] = 0;
				
				size -= divCount;
			}
		}		
		
		/// <summary>
		/// Sets current number as zero
		/// </summary>
		public void Zero()
		{
			for (int i = 0; i < size; ++i)
				coef[i] = 0;
			size = 1;
		}
		
		/// <summary>
		/// Checks if current number is zero
		/// </summary>
		/// <returns>True, if long number is zero, otherwise false</returns>
		public bool IsZero()
		{
			return (size == 1 && coef[0] == 0);
		}
		
		/// <summary>
		/// Resizes coeficients array to new size
		/// </summary>
		/// <param name="newSize">New size of coeficients array</param>
		public void ResizeNumber(uint newSize)
		{
			if (newSize < size)
				size = (int)newSize;
			Array.Resize(ref coef, (int)newSize);
		}
		
		#endregion
		
		/// <summary>
		/// Assignes coeficients to other long
		/// number without recreating array
		/// </summary>
		/// <param name="What">Number to assign</param>
		public void AssignBase(LongIntBase What)
		{
			int i = 0;
			
			for (; i < What.size; ++i)
				coef[i] = What.coef[i];
			
			for (; i < size; ++i)
				coef[i] = 0;
			
			numberBase = What.numberBase;
			size = What.size;
		}
		
		/// <summary>
		/// Founds real size of number
		/// </summary>
		public void Normalize()
		{
			int i = size - 1;
			if (i > 0)
				while (coef[i] == 0)
				{
					--i;
					if (i == 0)
						break;
				}
			
			size = i + 1;
		}
		
		public char[] ToCharArray()
		{
			char[] array = new char[size];
			for (int i = 0; i < size; ++i)
				array[i] = (char)coef[i];
			return array;
		}
		
		public ushort[] ToArray()
		{
			return (ushort[])coef.Clone();
		}
		
		#region Override region
		
		public override int GetHashCode ()
		{
			return coef.GetHashCode();
		}
		
		public override bool Equals (object obj)
		{
			if (obj == null)
                return false;

            LongIntBase lint = (LongIntBase)obj;
			
			// convert to object, because 
			// operator == can be called with 'null'
            if ((object)lint == null)
                return false;
			
			return LongMath.Equal(this, lint);
		}

		/// <summary>
		/// Converts long number to decimal number in string
		/// </summary>
		/// <returns>System.String with long number coeficients</returns>
		public override string ToString()
		{
			if (size <= 0 || IsZero())
				return "0";
			
			StringBuilder sb = new StringBuilder();			
			LongIntBase thisTemp = new LongIntBase(this);
			
			ushort currNumber = 0;			
			bool notZero = (thisTemp.size > 1 || thisTemp.coef[0] != 0);
			
			while (notZero)
			{
				LongMath.UShortDivSafe(thisTemp, 10000, ref thisTemp, ref currNumber);
				
				int numberLength = LongMath.IntLength(currNumber);
				sb.Insert(0, currNumber);
				
				notZero = (thisTemp.size > 1 || thisTemp.coef[0] != 0);
				
				if (notZero)
					if (numberLength < 4)
						sb.Insert(0, "0", 4 - numberLength);
			}
			
			return sb.ToString();
		}
		
		#endregion
	}
}
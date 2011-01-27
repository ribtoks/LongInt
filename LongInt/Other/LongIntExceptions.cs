using System;

namespace LongInt
{
	// occurs, when unsigned long number is going to take negative value
	public class NegativeResultException : ApplicationException
	{
		public NegativeResultException()
			: base ()
		{
		}
		
		public NegativeResultException(string message)
			: base (message)
		{
		}
		
		public NegativeResultException(string message, Exception innerException)
			: base (message, innerException)
		{
		}
	}
	
	// occurs, when someone tries to assign Sign property zero value
	public class ZeroSignException : ApplicationException
	{
		public ZeroSignException()
			: base ()
		{
		}
		
		public ZeroSignException(string message)
			: base (message)
		{
		}
		
		public ZeroSignException(string message, Exception innerException)
			: base (message, innerException)
		{
		}
	}
	
	// occurs, when while adding, multyplying, etc numbers they are filled
	public class NotEnoughSpaceException : ApplicationException
	{
		public NotEnoughSpaceException()
			: base ()
		{
		}
		
		public NotEnoughSpaceException(string message)
			: base (message)
		{
		}
		
		public NotEnoughSpaceException(string message, Exception innerException)
			: base (message, innerException)
		{
		}
	}
	
	// occurs, when user tries to resize number to less, that zero, size
	public class NewSizeLessZeroException : ApplicationException
	{
		public NewSizeLessZeroException()
			: base ()
		{
		}
		
		public NewSizeLessZeroException(string message)
			: base (message)
		{
		}
		
		public NewSizeLessZeroException(string message, Exception innerException)
			: base (message, innerException)
		{
		}
	}
	
	// occurs, when user tries to compare or to provide operations 
	// with numbers of different BASE
	public class DifferentBasesException : ApplicationException
	{
		public DifferentBasesException()
			: base ()
		{
		}
		
		public DifferentBasesException(string message)
			: base (message)
		{
		}
		
		public DifferentBasesException(string message, Exception innerException)
			: base (message, innerException)
		{
		}
	}
	
	public class WrondResultException : ApplicationException
	{
		public WrondResultException()
			: base ()
		{
		}
		
		public WrondResultException(string message)
			: base (message)
		{
		}
		
		public WrondResultException(string message, Exception innerException)
			: base (message, innerException)
		{
		}
	}
}
using System;
using System.IO;

namespace PriFormat;

internal class SubStream : Stream
{
	private Stream baseStream;

	private long subStreamPosition;

	private long subStreamLength;

	public long SubStreamPosition => subStreamPosition;

	public override bool CanRead => baseStream.CanRead;

	public override bool CanSeek => baseStream.CanSeek;

	public override bool CanWrite => false;

	public override long Length => subStreamLength;

	public override long Position
	{
		get
		{
			return baseStream.Position - subStreamPosition;
		}
		set
		{
			baseStream.Position = subStreamPosition + value;
		}
	}

	public SubStream(Stream baseStream, long subStreamPosition, long subStreamLength)
	{
		this.baseStream = baseStream;
		this.subStreamPosition = subStreamPosition;
		this.subStreamLength = subStreamLength;
	}

	public override void Flush()
	{
	}

	public override int Read(byte[] buffer, int offset, int count)
	{
		if (Position < 0)
		{
			throw new InvalidOperationException("Cannot read when position is negative.");
		}
		if (Position + count > subStreamLength)
		{
			count = (int)(subStreamLength - Position);
		}
		return baseStream.Read(buffer, offset, count);
	}

	public override long Seek(long offset, SeekOrigin origin)
	{
		return origin switch
		{
			SeekOrigin.Begin => baseStream.Seek(subStreamPosition + offset, SeekOrigin.Begin) - subStreamPosition, 
			SeekOrigin.Current => baseStream.Seek(offset, SeekOrigin.Current) - subStreamPosition, 
			SeekOrigin.End => baseStream.Seek(subStreamPosition + subStreamLength + offset, SeekOrigin.Begin) - subStreamPosition, 
			_ => throw new ArgumentException("Invalid origin.", "origin"), 
		};
	}

	public override void SetLength(long value)
	{
		throw new NotSupportedException();
	}

	public override void Write(byte[] buffer, int offset, int count)
	{
		throw new NotSupportedException();
	}
}

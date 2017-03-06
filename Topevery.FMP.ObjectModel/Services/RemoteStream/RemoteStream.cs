using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Topevery.FMP.ObjectModel.Exceptions;
using Topevery.Framework.Data.Collections;
using Topevery.Framework.ServiceModel.Proxy;
using Topevery.FMP.ObjectModel.Proxy;
using System.Diagnostics;

namespace Topevery.FMP.ObjectModel
{
    public class RemoteStream : Stream
    {
        #region Fields
        private const long InitWritePos = -1;
        private static readonly int _isOpen = BitVector32.CreateMask();
        private static readonly int _isClose = BitVector32.CreateMask(_isOpen);
        private static readonly int _needWrite = BitVector32.CreateMask(_isClose);
        private BitVector32 _state = new BitVector32();
        private RemoteStreamContextWrapper _contextWrapper;
        
        private long _origin;
        private long _lenght;
        private byte[] _buffer;
        private int _bufferCapacity;
        private int _bufferOffset;
        private int _bufferCount;
        private long _bufferWritePos = InitWritePos;
        private int _bufferWriteCount;
        [ThreadStatic]
        private static byte[] _cacheBuffer;
        #endregion


        #region Constructor
        public RemoteStream(RemoteStreamContext context)
        {
            this.IsClose = true;
            _contextWrapper = this.CreateWrapper();
            this._contextWrapper.Initialize(context);
            _lenght = _contextWrapper._fileLength;
            _origin = _contextWrapper._origin;
            _bufferCapacity = context.BufferSize;
            this.IsClose = false;
            this.NeededWrite = false;
        }
        #endregion

        #region Methods
        #region Public
        public override int Read(byte[] buffer, int offset, int count)
        {
            //Stopwatch w = new Stopwatch();
            //w.Start();
            if (this.IsClose)
            {
                __Error.FileIsClosed();
            }
            if (!CanRead)
            {
                throw new NotSupportedException("NotSupported_UnReadableStream");
            }
            CheckReadWriteParameter(buffer, offset, count);
            if (count == 0)
                return 0;
            long pos = this.Position;
            long maxReadCount =_lenght - pos;
            if (maxReadCount > count)
            {
                maxReadCount = count;
            }
            if (maxReadCount < 0)
            {
                return 0;
            }
            long readCount = maxReadCount;
            byte[] readBuffer = BufferInternal;
            while (readCount > 0)
            {                
                if (_bufferCount == 0)
                {
                    this.FlushRead();
                    if (_bufferCount == 0)
                    {
                        maxReadCount = 0;
                        break;
                    }
                }
                long currentReadCount = readCount;
                if (readCount > _bufferCount - _bufferOffset)
                {
                    currentReadCount = _bufferCount - _bufferOffset;
                }
                if (_bufferCount > 0)
                {
                    Buffer.BlockCopy(readBuffer, _bufferOffset, buffer, offset, (int)currentReadCount);
                    offset += (int)currentReadCount;
                    _bufferOffset += (int)currentReadCount;
                    if (_bufferOffset >= _bufferCount && readCount - currentReadCount > 0)
                    {
                        _origin = pos + currentReadCount;
                        pos = _origin;
                        this.ClearBufferState();
                        readBuffer = BufferInternal;
                    }
                }
                readCount -= currentReadCount;
            }
            //w.Stop();
            //Console.WriteLine("Read Execute times:" + w.ElapsedMilliseconds);
            return (int)maxReadCount;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            if (this.IsClose)
            {
                __Error.FileIsClosed();
            }
            if (!CanWrite)
            {
                throw new NotSupportedException("NotSupported_UnWriteableStream");
            }
            CheckReadWriteParameter(buffer, offset, count);
            if (count == 0)
                return;
            long pos = this.Position;
            int maxWriteCount = count;
            if (_lenght < pos + maxWriteCount)
            {
                _lenght = pos + maxWriteCount;
            }
            int writeCount = maxWriteCount;
            byte[] writeBuffer = BufferInternal;
            while (writeCount > 0)
            {
                int currentWriteCount = writeCount;
                if (_bufferCount <= 0)
                {
                    currentWriteCount = Math.Min(currentWriteCount, _bufferCapacity);
                    _bufferCount = _bufferCapacity;                    
                }
                else if (_bufferCount - _bufferOffset < writeCount)
                {
                    currentWriteCount = _bufferCount - _bufferOffset;
                }
                if (_bufferCount > 0)
                {
                    NeededWrite = true;
                    if (_bufferWritePos == InitWritePos)
                    {
                        _bufferWritePos = _origin + _bufferOffset;
                    }
                    Buffer.BlockCopy(buffer, offset, writeBuffer, _bufferOffset, currentWriteCount);
                    offset += currentWriteCount;
                    
                    _bufferOffset += currentWriteCount;                    
                    _bufferWriteCount += currentWriteCount;
                    if (_bufferOffset  >= _bufferCount)
                    {                       
                        FlushWrite();
                        writeBuffer = BufferInternal;
                        _origin = pos + currentWriteCount;
                        pos = _origin;
                    }
                }
                writeCount -= currentWriteCount;
            }
        }

        private static void CheckReadWriteParameter(byte[] buffer, int offset, int count)
        {
            __Error.CheckNullReference(buffer, "buffer");
            if (offset < 0)
                throw new ArgumentOutOfRangeException("offset");
            if (count < 0)
                throw new ArgumentOutOfRangeException("count");
            if (buffer.Length - offset < count)
            {
                throw new ArgumentOutOfRangeException("count");
            }
        }

        public override long Seek(long offset, SeekOrigin origin)
        {            
            if ((origin < SeekOrigin.Begin) || (origin > SeekOrigin.End))
            {
                throw new ArgumentException("Argument_InvalidSeekOrigin");
            }
            if (this.IsClose)
            {
                __Error.FileIsClosed();
            }
            if (!CanSeek)
            {
                throw new NotSupportedException("NotSupported_UnseekableStream");
            }

            long oldPosition = Position;
            long position = 0;
            switch (origin)
            {
                case SeekOrigin.Begin:
                    position = offset;
                    break;
                case SeekOrigin.End:
                    position = _lenght + offset;                    
                    break;
                case SeekOrigin.Current:
                    position = oldPosition + offset;
                    break;
            }  
           
            long seekOffset = position - oldPosition;
            if (_bufferOffset + seekOffset < 0 || _bufferOffset + seekOffset > _bufferCount)
            {
                this.FlushWrite();
            }
            else
            {
                _bufferOffset += (int)seekOffset;
            }
            Position = position;
            return Position;
        }

        public override void SetLength(long value)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public override void Flush()
        {
            
        }
        #endregion

        #region Protected
        protected override void Dispose(bool disposing)
        {
            if (!this.IsClose)
            {
                this.FlushWrite();
                this.ContextWrapper.Close();
                base.Dispose(disposing);
                this.IsClose = true;
            }
        }        

        protected virtual void WriteCore(long pos, byte[] buffer)
        {
            WriteFileItemData item = new WriteFileItemData();

            if (LogicFileInfo != null)
            {
                item.ClientFileName = LogicFileInfo.LogicFileName;
            }
            if (ContextWrapper.OpenFileResult != null)
            {
                item.LockID = ContextWrapper.OpenFileResult.LockedID;
            }

            if (ContextWrapper.PhysicalFileInfo != null)
            {
                item.PhysicalFileID = ContextWrapper.PhysicalFileInfo.ID;
            }

            item.Position = pos;
            item.WriteData = buffer;
            
            FileManager.WriteFile(item, this.ContextWrapper.ServiceProxy);
        }

        protected virtual byte[] ReadCore(long pos, int count)
        {
            byte[] result = null;
            ReadFileItemData item = new ReadFileItemData();
            if (LogicFileInfo != null)
            {
                item.ClientFileName = LogicFileInfo.LogicFileName ;
            }
            if (ContextWrapper.OpenFileResult != null)
            {
                item.LockID = ContextWrapper.OpenFileResult.LockedID;
            }

            if (ContextWrapper.PhysicalFileInfo != null)
            {
                item.PhysicalFileID = ContextWrapper.PhysicalFileInfo.ID;
            }

            item.Position = pos;
            item.ReadCount = count;
            ReadFileResultItemData itemResult = FileManager.ReadFile(item, this.ContextWrapper.ServiceProxy);
            if (itemResult != null && itemResult.ReadDataLength > 0)
            {
                result = itemResult.ReadData;
            }
            return result;
        }
        #endregion

        #region Private
        private RemoteStreamContextWrapper CreateWrapper()
        {
            RemoteStreamContextWrapper wrapper = new RemoteStreamContextWrapper();
            return wrapper;
        }

        private void ClearBufferState()
        {
            _buffer = null;
            _bufferCount = 0;
            _bufferOffset = 0;
            _bufferWritePos = InitWritePos;
            _bufferWriteCount = 0;
            this.NeededWrite = false;
        }

        private void FlushWrite()
        {
            if (this.NeededWrite && _buffer != null && _bufferWriteCount > 0 && _bufferCount > 0 && _bufferCount >= _bufferWriteCount)
            {
                int writeCount = _bufferWriteCount;
                byte[] buffer = new byte[writeCount];
                Buffer.BlockCopy(_buffer, 0, buffer, 0, writeCount);
                long pos = _bufferWritePos;
                WriteCore(pos, buffer);
                ClearBufferState();
            }
        }

        private void FlushRead()
        {
            int readCount = _bufferCapacity;
            long pos = this.Position;
            if (readCount > 0 && pos < Length)
            {
                this.FlushWrite();
                byte[] buffer = ReadCore(pos, readCount);
                if (buffer != null)
                {
                    _bufferCount = buffer.Length;
                    Buffer.BlockCopy(buffer, 0, BufferInternal, 0, _bufferCount);
                }
                else
                {
                    _bufferCount = 0;                    
                }
            }
        }

        private static byte[] GetCacheBuffer(int bufferSize)
        {
            if (_cacheBuffer != null && _cacheBuffer.Length != bufferSize)
            {
                _cacheBuffer = null;
            }
            if (_cacheBuffer == null)
            {
                _cacheBuffer = new byte[bufferSize];
            }
            return _cacheBuffer;
        }
        #endregion
        #endregion

        #region Properties
        public override bool CanRead
        {
            get
            {
                return this.ContextWrapper.CanRead;
            }
        }

        public override bool CanSeek
        {
            get
            {
                return this.ContextWrapper.CanSeek;
            }
        }

        public override bool CanWrite
        {
            get
            {
                return ContextWrapper.CanWrite;
            }
        }        

        public override long Length
        {
            get
            {
                return _lenght;
            }
        }

        public override long Position
        {
            get
            {
                if (this.IsClose)
                {
                    __Error.FileIsClosed();
                }
                return _origin + _bufferOffset;
            }
            set
            {
                if (this.IsClose)
                {
                    __Error.FileIsClosed();
                }
                if (value >= 0 && value < _lenght)
                {
                    if (value >= _origin && value < _origin + _bufferCount)
                    {
                        _bufferOffset = (int)(value - _origin);
                    }
                    else
                    {
                        _origin = value;
                        this.FlushWrite();
                        this.ClearBufferState();
                    }
                }
            }
        }

        public LogicFileInfoData LogicFileInfo
        {
            get
            {
                return ContextWrapper.LogicFileInfo;
            }
        }

        private RemoteStreamContextWrapper ContextWrapper
        {
            get
            {
                return this._contextWrapper;
            }
        }

        protected bool IsClose
        {
            get{
                return this._state[_isClose];
            }
            set{
                this._state[_isClose]= value;
            }
        }

        protected bool NeededWrite
        {
            get
            {
                return this._state[_needWrite];
            }
            set
            {
                this._state[_needWrite] = value;
            }
        }

        private byte[] BufferInternal
        {
            get
            {
                if (_buffer == null)
                {
                    _buffer = GetCacheBuffer(_bufferCapacity);// new byte[_bufferSize];
                }
                return _buffer;
            }
        }
        #endregion

        #region Class
        private sealed class RemoteStreamContextWrapper
        {
            #region Fields
            private RemoteStreamContext _context;
            private OpenFileResultItemData _itemResult;
            private bool _canRead;
            private bool _canWrite;
            private bool _canSeek;
            internal long _fileLength;
            internal long _origin;
            private IRemoteFileStorage _proxy;
            #endregion

            #region Constrcutor
            public RemoteStreamContextWrapper()
            {                
                _canRead = false;
                _canWrite = false;
                _canSeek = true;
            }
            #endregion

            #region Methods
            internal void Initialize(RemoteStreamContext context)
            {
                __Error.CheckNullReference(context, "context");
                if (context.FileID == Guid.Empty)
                {
                    __Error.InvalidateFileID();
                }
                _context = context;
                _proxy = CreateServiceProxy(context);
                //Stopwatch w = new Stopwatch();
                //w.Start();
                _itemResult = FileManager.OpenFile(context, _proxy);
                //w.Stop();
                //Console.WriteLine("Open File execute times:" + w.ElapsedMilliseconds);
                //_logicFileInfo = FileManager.GetFileInfo(context.FileID, context.Version);
                if (context.FileMode == FileMode.None)
                {
                    __Error.InvalidateFileMode();
                }

                FileAccess access = context.FileAccess;

                if (FileMode.Truncate == context.FileMode)
                {
                    access = FileAccess.Write;
                }

                LogicFileInfoData logicFileInfo = LogicFileInfo;

                if (logicFileInfo == null)
                {
                    if (context.FileMode == FileMode.Open
                        || context.FileMode == FileMode.Append
                        || context.FileMode == FileMode.Truncate)
                    {
                        __Error.NotExistsFileID(context.FileID);
                    }
                }

                if (logicFileInfo != null)
                {
                    if (logicFileInfo.ReadOnly && this._canWrite)
                    {
                        __Error.FileReadOnly(context.FileID);
                    }

                    if (context.FileMode == FileMode.CreateNew)
                    {
                        __Error.ExistsFileID(context.FileID);
                    }                    
                }

                this._canRead = (access & FileAccess.Read) != 0;
                this._canWrite = (access & FileAccess.Write) != 0;

                PhysicalFileInfoData physicalFileInfo = this.PhysicalFileInfo;
                if (physicalFileInfo != null)
                {
                    this._fileLength = physicalFileInfo.FileLength;
                }

                if (context.FileMode == FileMode.Append)
                {
                    _origin = _fileLength;
                }
            }

            internal void Close()
            {
                if(this.OpenFileResult != null && this.OpenFileResult.LockedID != Guid.Empty)
                {
                    FileManager.CloseFileByLockID(this.OpenFileResult.LockedID, _proxy);
                }
            }

            private IRemoteFileStorage CreateServiceProxy(RemoteStreamContext context)
            {
                IRemoteFileStorage result = null;
                if (!string.IsNullOrEmpty(context.ServiceGroup))
                {
                    result = ServiceProxyProviderFactory.GetProvider(context.ServiceGroup, 
                        FMPServiceProxyProviderManager.RemoteFileStorageServiceName) as IRemoteFileStorage;
                }
                return result;
            }
            #endregion

            #region Properties
            public RemoteStreamContext Context
            {
                get
                {
                    return this._context;
                }
            }

            public LogicFileInfoData LogicFileInfo
            {
                get
                {
                    if (this._itemResult != null)
                    {
                        return this._itemResult.LogicFileInfo;
                    }
                    return null;
                }
            }
            

            public PhysicalFileInfoData PhysicalFileInfo
            {
                get
                {
                    PhysicalFileInfoData result = null;
                    LogicFileInfoData logicInfo = LogicFileInfo;
                    if (logicInfo != null)
                    {
                        int count = logicInfo.PhysicalFileInfos.Count;
                        if (count > 0)
                        {
                            result = logicInfo.PhysicalFileInfos[count - 1];
                        }
                    }
                    return result;
                }
            }

            public OpenFileResultItemData OpenFileResult
            {
                get
                {
                    return this._itemResult;
                }
            }

            public bool CanRead
            {
                get
                {
                    return this._canRead;
                }
            }

            public bool CanWrite
            {
                get
                {
                    return this._canWrite;
                }
            }

            public bool CanSeek
            {
                get
                {
                    return this._canSeek;
                }
            }

            public IRemoteFileStorage ServiceProxy
            {
                get
                {
                    return _proxy;
                }
            }
            #endregion
        }
        #endregion
    }
}

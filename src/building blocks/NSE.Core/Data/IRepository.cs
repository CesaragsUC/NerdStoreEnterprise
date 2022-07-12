﻿using NSE.Core.DomanObjects;
using System;

namespace NSE.Core.Data
{
    public interface IRepository<T>: IDisposable where  T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}

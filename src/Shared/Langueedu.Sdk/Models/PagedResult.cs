﻿namespace Langueedu.Sdk.Models
{
    public class PagedResult<T> : Result<T>
    {
        public PagedResult(PagedInfo pagedInfo, T value) : base(value)
        {
            PagedInfo = pagedInfo;
        }

        public PagedInfo PagedInfo { get; }
    }
}
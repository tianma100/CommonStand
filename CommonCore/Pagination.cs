using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    /// <summary>
    /// 分页条信息
    /// </summary>
    public class Pagination
    {

        /// <summary>
        /// 划过 个数
        /// </summary>
        public int Offset { get; set; }
        /// <summary>
        /// 页数据量
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 总数据条数
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 空构造器
        /// </summary>
        public Pagination() { }
        /// <summary>
        /// 常用构造函数
        /// </summary>
        /// <param name="Offset">划过数量</param>
        /// <param name="PageSize">获取数量</param>
        public Pagination(int Offset, int PageSize)
        {
            this.Offset = Offset;
            this.PageSize = PageSize;
        }

        /// <summary>
        /// 当前页索引
        /// </summary>
        public int PageIndex
        {
            get
            {
                return (int)Math.Ceiling(this.StartIndex / (double)PageSize);
            }
        }
        /// <summary>
        /// 总页数 
        /// </summary>
        public int PageCount
        {
            get
            {
                return (int)Math.Ceiling(this.Total / (double)PageSize);
            }
        }
        /// <summary>
        /// 开始位置 
        /// </summary>
        public int StartIndex
        {
            get
            {
                return this.Offset + 1;
            }
        }
        /// <summary>
        /// 当前页结束位置 
        /// </summary>
        public int EndIndex
        {
            get
            {
                return this.StartIndex + PageSize;
            }
        }
        /// <summary>
        /// 划过数量
        /// </summary>
        public int SkipCount
        {
            get
            {
                return this.StartIndex - 1;
            }
        }

    }
}

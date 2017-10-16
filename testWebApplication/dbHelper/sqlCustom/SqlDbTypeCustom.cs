﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace System.Data
{
    public class SqlDbTypeCustom : IDbTypeCustom
    {

        public SqlDbType dbType
        {
            get;
            set;
        }

        /// <summary>
        /// 长度
        /// </summary>
        public long length
        {
            get;
            set;
        }

        /// <summary>
        /// 精度 如果是小数就是 小数点左边的位数+小数点右边的位数
        /// </summary>
        public short precison
        {
            get;
            set;
        }

        /// <summary>
        /// 规模 如果是小数就是 小数点右边的位数
        /// </summary>
        public short scale
        {
            get;
            set;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data
{
    public class SqlDefaultPropertyName
    {
        private string mUserId;
        /// <summary>
        /// 人员编码
        /// </summary>
        public string userId
        {
            set
            {
                mUserId = value;
            }
            get
            {
                return mUserId;
            }
        }

        private string mUpdaterId;
        /// <summary>
        /// 更新人员字段
        /// </summary>
        public string updaterId
        {
            set
            {
                mUpdaterId = value;
            }
            get
            {
                if (string.IsNullOrEmpty(mUpdaterId))
                {
                    mUpdaterId = "updaterId";
                }
                return mUpdaterId;
            }
        }

        private string mUpdateTime;
        /// <summary>
        /// 更新时间字段
        /// </summary>
        public string updateTime
        {
            set
            {
                mUpdateTime = value;
            }
            get
            {
                if (string.IsNullOrEmpty(mUpdateTime))
                {
                    mUpdateTime = "updateTime";
                }
                return mUpdateTime;
            }
        }

        private string mCreaterId;
        /// <summary>
        /// 创建人字段
        /// </summary>
        public string createrId
        {
            set
            {
                mCreaterId = value;
            }
            get
            {
                if (string.IsNullOrEmpty(mCreaterId))
                {
                    mCreaterId = "createrId";
                }
                return mCreaterId;
            }
        }

        private string mCreateTime;
        /// <summary>
        /// 创建时间字段
        /// </summary>
        public string createTime
        {
            set
            {
                mCreateTime = value;
            }
            get
            {
                if (string.IsNullOrEmpty(mCreateTime))
                {
                    mCreateTime = "createTime";
                }
                return mCreateTime;
            }
        }

        private string mDeletePropertyName;
        /// <summary>
        /// 删除字段
        /// </summary>
        public string deletePropertyName
        {
            set
            {
                mDeletePropertyName = value;
            }
            get
            {
                if (string.IsNullOrEmpty(mDeletePropertyName))
                {
                    mDeletePropertyName = "disState";
                }
                return mDeletePropertyName;
            }
        }

        private string mIdPropertyName;
        /// <summary>
        /// 主键编码字段
        /// </summary>
        public string idPropertyName
        {
            set
            {
                mIdPropertyName = value;
            }
            get
            {
                return mIdPropertyName;
            }
        }

        /// <summary>
        /// 是否更新
        /// </summary>
        public bool whetherUpdate { get; set; }
    }
}

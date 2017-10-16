using System.Data;
using System.Runtime.Serialization;

namespace System.Data
{
    [DataContract, Serializable]
    public class DbResultCustom : IDbResultCustom
    {
        /// <summary>
        /// 状态
        /// </summary>
        [DataMember]
        public EnumDbResultStatus dbResultStatus
        {
            set { _dbResultStatus = value; }
            get { return _dbResultStatus; }
        }
        private EnumDbResultStatus _dbResultStatus = EnumDbResultStatus.Failure;

        /// <summary>
        /// 行为
        /// </summary>
        [DataMember]
        public EnumDbAction dbAction
        {
            set { _dbAction = value; }
            get { return _dbAction; }
        }
        private EnumDbAction _dbAction = EnumDbAction.Inherit;
    }
}
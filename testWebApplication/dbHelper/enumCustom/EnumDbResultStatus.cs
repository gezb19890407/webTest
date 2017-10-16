namespace System.Data
{
    /// <summary>
    /// 执行结果
    /// </summary>
    public enum EnumDbResultStatus
    {
        /// <summary>
        /// 出错
        /// </summary>
        Error = -3,

        /// <summary>
        /// 失败
        /// 包括：
        ///       条件不足
        /// </summary>
        Failure = 0,

        /// <summary>
        /// 成功
        /// </summary>
        Success = 1,
    }
}
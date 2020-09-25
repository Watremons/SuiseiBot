namespace AuctionBot.Code.Resource.TypeEnum
{
    internal enum LenType : int
    {
        /// <summary>
        /// 不合法长度
        /// </summary>
        Illegal = 1,

        /// <summary>
        /// 合法长度
        /// </summary>
        Legitimate = 2,

        /// <summary>
        /// 合法长度且允许额外参数
        /// </summary>
        Extra = 3
    }
}
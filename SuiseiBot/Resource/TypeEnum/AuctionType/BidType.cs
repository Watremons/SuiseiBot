namespace AuctionBot.Code.Resource.TypeEnum.AuctionType
{
    internal enum BidType
    {
        /// <summary>
        /// 非法出价
        /// </summary>
        Illeage = -1,

        /// <summary>
        /// 普通出价
        /// </summary>
        Normal = 0,

        /// <summary>
        /// 成功拍得出价
        /// </summary>
        Last = 1
    }
}
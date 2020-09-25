using Native.Sdk.Cqp;
using Native.Sdk.Cqp.EventArgs;

namespace AuctionBot.Code.DatabaseUtils
{
    internal class DBMsgUtils
    {
        /// <summary>
        /// 数据库发生错误时的消息提示
        /// </summary>
        public static void DatabaseFailedTips(CQGroupMessageEventArgs groupEventArgs)
        {
            groupEventArgs.FromGroup.SendGroupMessage(CQApi.CQCode_At(groupEventArgs.FromQQ.Id),
                                            "\r\nERROR",
                                            "\r\n数据库错误");
        }
    }
}
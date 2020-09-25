using Native.Sdk.Cqp;
using System.Collections.Generic;
using AuctionBot.Code.Resource.TypeEnum.CmdType;

namespace AuctionBot.Code.Resource.Commands
{
    /// <summary>
    /// 存放需要参数的关键词
    /// </summary>
    internal class KeywordCmd
    {
        public static Dictionary<string, KeywordCmdType> Keywords = new Dictionary<string, KeywordCmdType>();

        /// <summary>
        /// 初始化特殊关键词
        /// </summary>
        public static void SpecialKeywordsInit(CQApi cqApi)
        {
            Keywords.Add("查询排名", KeywordCmdType.PCRTools_GetGuildRank);
            Keywords.Add("来点切噜", KeywordCmdType.Cheru_Encode);
            Keywords.Add("切噜~", KeywordCmdType.Cheru_Decode);
            Keywords.Add("切噜～", KeywordCmdType.Cheru_Decode);

            Keywords.Add("echo", KeywordCmdType.Debug_Echo);
            Keywords.Add(CQApi.CQCode_At(cqApi.GetLoginQQ().Id).ToString(), KeywordCmdType.At_Bot);
        }

        public static KeywordCmdType TryGetKeywordType(string message)
        {
            string keyword = message.Split(' ')[0];
            Keywords.TryGetValue(keyword, out KeywordCmdType keywordType);
            return keywordType;
        }
    }
}
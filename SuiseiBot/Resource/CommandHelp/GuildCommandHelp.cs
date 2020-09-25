using AuctionBot.Code.Resource.TypeEnum.CmdType;
using System.Collections.Generic;

namespace AuctionBot.Code.Resource.CommandHelp
{
    /// <summary>
    /// 大佬快教教我这个指令怎么用.jpg
    /// </summary>
    internal static class GuildCommandHelp
    {
        public static Dictionary<PCRGuildCmdType, string> HelpText = new Dictionary<PCRGuildCmdType, string>();

        public static void InitHelpText()
        {
            HelpText.Add(PCRGuildCmdType.CreateGuild, "#建会 [区域(cn/tw/jp)] [公会名]");
            HelpText.Add(PCRGuildCmdType.JoinGuild, "#入会 [@成员(或@自己)]");
        }
    }
}
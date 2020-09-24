using System.Collections.Generic;
using SuiseiBot.Code.Resource.TypeEnum.CmdType;

namespace SuiseiBot.Code.Resource.Commands
{
    internal static class AuctionCmd
    {
        public static Dictionary<string, AuctionCmdType> AuctionCommands = new Dictionary<string, AuctionCmdType>();
        /// <summary>
        /// 初始化公会相关的指令
        /// </summary>
        public static void AuctionCommandInit()
        {
            //拍卖会管理模块的指令
            AuctionCommands.Add("准备拍卖", AuctionCmdType.CreateAuction);
            AuctionCommands.Add("创建小组", AuctionCmdType.CreateTeam);
            AuctionCommands.Add("查看成员", AuctionCmdType.ListMember);
            AuctionCommands.Add("退出小组", AuctionCmdType.QuitTeam);
            AuctionCommands.Add("清空成员", AuctionCmdType.QuitAll);
            AuctionCommands.Add("删除小组", AuctionCmdType.DeleteGroup);
            AuctionCommands.Add("删除小组", AuctionCmdType.AddLot);
            //拍卖模块的指令（在做了在做了
            AuctionCommands.Add("开始拍卖", AuctionCmdType.AuctionStart);
            AuctionCommands.Add("结束拍卖", AuctionCmdType.AuctionEnd);
            AuctionCommands.Add("申请出价", AuctionCmdType.RequestBid);
            AuctionCommands.Add("取消申请", AuctionCmdType.UndoRequestBid);
            AuctionCommands.Add("出价", AuctionCmdType.Bid);
            AuctionCommands.Add("撤销出价", AuctionCmdType.UndoBid);
            AuctionCommands.Add("删除出价", AuctionCmdType.DeleteBid);
            AuctionCommands.Add("拍卖进度", AuctionCmdType.ShowProgress);
            AuctionCommands.Add("修改进度", AuctionCmdType.ModifyProgress);
            AuctionCommands.Add("全拍卖记录", AuctionCmdType.ShowAllSellList);
            AuctionCommands.Add("组拍卖记录", AuctionCmdType.ShowSellList);
            AuctionCommands.Add("暂停", AuctionCmdType.Pause);
        }
    }
}

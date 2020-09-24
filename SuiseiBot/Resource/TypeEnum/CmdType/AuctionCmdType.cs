namespace SuiseiBot.Code.Resource.TypeEnum.CmdType
{
    internal enum AuctionCmdType : int
    {
        /// <summary>
        /// 创建一场拍卖会
        /// </summary>
        CreateAuction = 1001,
        /// <summary>
        /// 创建参与拍卖的小组
        /// </summary>
        CreateTeam = 1002,
        /// <summary>
        /// 查看成员（各个小组/全部小组）
        /// </summary>
        ListMember = 1003,
        /// <summary>
        /// 退出小组
        /// </summary>
        QuitTeam = 1004,
        /// <summary>
        /// 清空拍卖会所有成员
        /// </summary>
        QuitAll = 1005,
        /// <summary>
        /// 删除小组
        /// </summary>
        DeleteGroup = 1006,
        /// <summary>
        /// 添加拍卖会藏品
        /// </summary>
        AddLot = 1007,

        /// <summary>
        /// 拍卖开始指令
        /// </summary>
        AuctionStart = 1101,
        /// <summary>
        /// 会战结束命令
        /// </summary>
        AuctionEnd = 1102,
        /// <summary>
        /// 申请叫价命令
        /// </summary>
        RequestBid = 1103,
        /// <summary>
        /// 取消叫价申请
        /// </summary>
        UndoRequestBid = 1104,
        /// <summary>
        /// 叫价命令
        /// </summary>
        Bid = 1105,
        /// <summary>
        /// 撤销叫价命令
        /// </summary>
        UndoBid = 1106,
        /// <summary>
        /// 删除叫价命令
        /// </summary>
        DeleteBid = 1107,
        /// <summary>
        /// 查看进度命令
        /// </summary>
        ShowProgress = 1108,
        /// <summary>
        /// 全购买表命令
        /// </summary>
        ShowAllSellList = 1109,
        /// <summary>
        /// 修改进度
        /// </summary>
        ModifyProgress = 1110,
        /// <summary>
        /// 单组购买表查询
        /// </summary>
        ShowSellList = 1111,
        /// <summary>
        /// 暂停拍卖进程
        /// </summary>
        Pause = 1112
    }
}

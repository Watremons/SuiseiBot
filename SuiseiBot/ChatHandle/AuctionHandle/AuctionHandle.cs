using System;
using Native.Sdk.Cqp;
using Native.Sdk.Cqp.EventArgs;
using SuiseiBot.Code.OrderManager.AuctionManager;
using SuiseiBot.Code.Resource.CommandHelp;
using SuiseiBot.Code.Resource.Commands;
using SuiseiBot.Code.Resource.TypeEnum.CmdType;
using SuiseiBot.Code.Tool.LogUtils;

namespace SuiseiBot.Code.ChatHandle.AuctionHandle
{
    internal class AuctionHandle
    {
        #region 属性
        public object Sender { private set; get; }
        public CQGroupMessageEventArgs AuctionEventArgs { private set; get; }
        public string AuctionCommand { private get; set; }
        private AuctionCmdType CommandType { get; set; }
        #endregion

        #region 构造函数
        public AuctionHandle(object sender, CQGroupMessageEventArgs e)
        {
            this.AuctionEventArgs = e;
            this.Sender = sender;
        }
        #endregion

        #region 消息解析函数
        public void GetChat() //消息接收并判断是否响应
        {
            try
            {
                //获取第二个字符开始到空格为止的拍卖命令
                AuctionCommand = AuctionEventArgs.Message.Text.Substring(1).Split(' ')[0];
                //获取指令类型
                AuctionCmd.AuctionCommands.TryGetValue(AuctionCommand, out AuctionCmdType commandType);
                ConsoleLog.Debug("Guild Command", $"user={AuctionEventArgs.FromQQ.Id} command={commandType}");
                this.CommandType = commandType;
                //未知指令
                if (CommandType == 0)
                {
                    GetUnknowCommand(AuctionEventArgs);
                    ConsoleLog.Info("拍卖Bot", CommandType == 0 ? "解析到未知指令" : $"解析指令{CommandType}");
                    return;
                }
                //拍卖分组管理指令
                if ((int)CommandType > 1000 && (int)CommandType < 1100)
                {
                    //PCRHandler.GuildMgrResponse(Sender, AuctionEventArgs, CommandType);
                }
                //拍卖管理指令
                else if ((int)CommandType > 1100 && (int)CommandType < 1200)
                {
                    //GuildBattleManager battleManager = new GuildBattleManager(PCRGuildEventArgs, CommandType);
                    //battleManager.GuildBattleResponse();
                }
            }
            catch (Exception e)
            {
                //命令无法被正确解析
                ConsoleLog.Error("拍卖Bot", $"指令解析发生错误\n{e}");
            }
        }
        #endregion

        #region 非法指令响应
        /// <summary>
        /// 得到未知指令时的响应
        /// </summary>
        /// <param name="e">CQGroupMessageEventArgs</param>
        public static void GetUnknowCommand(CQGroupMessageEventArgs e)
        {
            ConsoleLog.Warning("拍卖Bot", "未知指令");
            e.FromGroup.SendGroupMessage(CQApi.CQCode_At(e.FromQQ.Id), "\n未知指令");
        }

        /// <summary>
        /// 存在非法参数时的响应
        /// </summary>
        /// <param name="e">CQGroupMessageEventArgs</param>
        /// <param name="commandType">指令类型</param>
        /// <param name="errDescription">错误描述</param>
        public static void GetIllegalArgs(CQGroupMessageEventArgs e, PCRGuildCmdType commandType, string errDescription)
        {
            ConsoleLog.Warning("拍卖Bot", "非法参数");
            e.FromGroup.SendGroupMessage(
                                         CQApi.CQCode_At(e.FromQQ.Id),
                                         "\n非法参数请重新输入指令" +
                                         $"\n错误：{errDescription}" +
                                         $"\n指令帮助：{GetCommandHelp(commandType)}");
        }
        #endregion

        #region 辅助函数
        /// <summary>
        /// 获取对应指令的帮助文本
        /// </summary>
        /// <returns>帮助文本</returns>
        public static string GetCommandHelp(PCRGuildCmdType commandType)
        {
            GuildCommandHelp.HelpText.TryGetValue(commandType, out string helptext);
            if (string.IsNullOrEmpty(helptext)) helptext = "该指令还在开发中，请询问机器人维护者或者开发者";
            return helptext;
        }
        #endregion
    }
}

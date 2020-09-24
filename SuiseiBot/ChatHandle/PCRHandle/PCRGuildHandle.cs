using System;
using Native.Sdk.Cqp;
using Native.Sdk.Cqp.EventArgs;
using SuiseiBot.Code.OrderManager.PCRGuildManager;
using SuiseiBot.Code.Resource.CommandHelp;
using SuiseiBot.Code.Resource.Commands;
using SuiseiBot.Code.Resource.TypeEnum.CmdType;
using SuiseiBot.Code.Tool.LogUtils;

namespace SuiseiBot.Code.ChatHandle.PCRHandle
{
    internal class PCRGuildHandle
    {
        #region 属性
        public object Sender { private set; get; }
        public CQGroupMessageEventArgs PCRGuildEventArgs { private set; get; }
        public string PCRGuildCommand { private get; set; }
        private PCRGuildCmdType CommandType { get; set; }
        #endregion

        #region 构造函数
        public PCRGuildHandle(object sender, CQGroupMessageEventArgs e)
        {
            this.PCRGuildEventArgs = e;
            this.Sender = sender;
        }
        #endregion

        #region 消息解析函数
        public void GetChat() //消息接收并判断是否响应
        {
            try
            {
                //获取第二个字符开始到空格为止的PCR命令
                PCRGuildCommand = PCRGuildEventArgs.Message.Text.Substring(1).Split(' ')[0];
                //获取指令类型
                PCRGuildCmd.PCRGuildCommands.TryGetValue(PCRGuildCommand, out PCRGuildCmdType commandType);
                ConsoleLog.Debug("Guild Command",$"user={PCRGuildEventArgs.FromQQ.Id} command={commandType}");
                this.CommandType = commandType;
                //未知指令
                if (CommandType == 0)
                {
                    GetUnknowCommand(PCRGuildEventArgs);
                    ConsoleLog.Info("PCR公会管理", CommandType == 0 ? "解析到未知指令" : $"解析指令{CommandType}");
                    return;
                }
                //公会管理指令
                if (CommandType > 0 && (int)CommandType < 100)
                {
                    PCRHandler.GuildMgrResponse(Sender, PCRGuildEventArgs, CommandType);
                }
                //出刀管理指令
                else if ((int)CommandType > 100 && (int)CommandType < 200)
                {
                    GuildBattleManager battleManager = new GuildBattleManager(PCRGuildEventArgs,CommandType);
                    battleManager.GuildBattleResponse();
                }
            }
            catch(Exception e)
            {
                //命令无法被正确解析
                ConsoleLog.Error("PCR公会管理", $"指令解析发生错误\n{e}");
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
            ConsoleLog.Warning("PCR公会管理", "未知指令");
            e.FromGroup.SendGroupMessage(CQApi.CQCode_At(e.FromQQ.Id), "\n未知指令");
        }
        #endregion
    }
}

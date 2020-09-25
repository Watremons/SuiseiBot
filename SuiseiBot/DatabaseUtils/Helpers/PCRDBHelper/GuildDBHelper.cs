using Native.Sdk.Cqp.EventArgs;
using SqlSugar;
using System;
using System.Collections.Generic;
using AuctionBot.Code.SqliteTool;
using AuctionBot.Code.Tool.LogUtils;

namespace AuctionBot.Code.DatabaseUtils.Helpers.PCRDBHelper
{
    internal class GuildDBHelper
    {
        #region 属性

        protected CQGroupMessageEventArgs GuildEventArgs { set; get; }
        protected string DBPath { get; set; }

        #endregion 属性

        #region 通用查询函数

        /// <summary>
        /// 检查公会是否存在
        /// </summary>
        /// <returns>
        /// <para><see langword="1"/> 公会存在</para>
        /// <para><see langword="0"/> 公会不存在</para>
        /// <para><see langword="-1"/> 数据库错误</para>
        /// </returns>
        public int GuildExists()
        {
            try
            {
                using SqlSugarClient dbClient = SugarUtils.CreateSqlSugarClient(DBPath);
                return dbClient.Queryable<GuildInfo>().Where(guild => guild.Gid == GuildEventArgs.FromGroup.Id).Any()
                    ? 1
                    : 0;
            }
            catch (Exception e)
            {
                ConsoleLog.Error("Database error", ConsoleLog.ErrorLogBuilder(e));
                return -1;
            }
        }

        /// <summary>
        /// 获取公会名
        /// </summary>
        /// <param name="groupid"></param>
        /// <returns>
        /// <para>公会名</para>
        /// <para><see langword="空字符串"/> 公会不存在</para>
        /// <para><see langword="null"/> 数据库错误</para>
        /// </returns>
        public string GetGuildName(long groupid)
        {
            try
            {
                using SqlSugarClient dbClient = SugarUtils.CreateSqlSugarClient(DBPath);
                var data = dbClient.Queryable<GuildInfo>().Where(i => i.Gid == groupid);
                if (data.Any())
                {
                    return data.First().GuildName;
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception e)
            {
                ConsoleLog.Error("Database error", ConsoleLog.ErrorLogBuilder(e));
                return null;
            }
        }

        /// <summary>
        /// 获取公会成员数
        /// </summary>
        /// <param name="gid">公会群号</param>
        /// <returns>
        /// <para>成员数</para>
        /// <para><see langword="-1"/> 数据库错误</para>
        /// </returns>
        public int GetMemberCount(long gid)
        {
            try
            {
                using SqlSugarClient dbClient = SugarUtils.CreateSqlSugarClient(DBPath);
                return dbClient.Queryable<MemberInfo>().Where(guild => guild.Gid == gid).Count();
            }
            catch (Exception e)
            {
                ConsoleLog.Error("Database error", ConsoleLog.ErrorLogBuilder(e));
                return -1;
            }
        }

        /// <summary>
        /// 检查公会是否有这个成员
        /// </summary>
        /// <param name="uid">QQ号</param>
        /// <returns>
        /// <para><see langword="1"/> 存在</para>
        /// <para><see langword="0"/> 不存在</para>
        /// <para><see langword="-1"/> 数据库错误</para>
        /// </returns>
        public int CheckMemberExists(long uid)
        {
            try
            {
                using SqlSugarClient dbClient = SugarUtils.CreateSqlSugarClient(DBPath);
                return dbClient.Queryable<MemberInfo>()
                               .Where(i => i.Uid == uid && i.Gid == GuildEventArgs.FromGroup.Id)
                               .Any() ? 1 : 0;
            }
            catch (Exception e)
            {
                ConsoleLog.Error("Database error", ConsoleLog.ErrorLogBuilder(e));
                return -1;
            }
        }

        /// <summary>
        /// 获取成员信息
        /// </summary>
        /// <param name="uid"></param>
        /// <returns>
        /// <para>成员信息</para>
        /// <para><see langword="null"/> 数据库错误</para>
        /// </returns>
        public MemberInfo GetMemberInfo(long uid)
        {
            try
            {
                using SqlSugarClient dbClient = SugarUtils.CreateSqlSugarClient(DBPath);
                return dbClient.Queryable<MemberInfo>()
                               .Where(i => i.Uid == uid && i.Gid == GuildEventArgs.FromGroup.Id)
                               .First();
            }
            catch (Exception e)
            {
                ConsoleLog.Error("Database error", ConsoleLog.ErrorLogBuilder(e));
                return null;
            }
        }

        /// <summary>
        /// 获取所有成员信息
        /// </summary>
        /// <param name="gid">群号</param>
        /// <returns>
        /// <para>成员信息</para>
        /// <para><see langword="null"/> 数据库错误</para>
        /// </returns>
        public List<MemberInfo> GetAllMembersInfo(long gid)
        {
            try
            {
                using SqlSugarClient dbClient = SugarUtils.CreateSqlSugarClient(DBPath);
                return dbClient.Queryable<MemberInfo>()
                               .Where(group => group.Gid == gid)
                               .ToList();
            }
            catch (Exception e)
            {
                ConsoleLog.Error("Database error", ConsoleLog.ErrorLogBuilder(e));
                return null;
            }
        }

        /// <summary>
        /// 获取公会信息
        /// </summary>
        /// <param name="gid"></param>
        /// <returns>
        /// <para>成员信息</para>
        /// <para><see langword="null"/> 数据库错误</para>
        /// </returns>
        public GuildInfo GetGuildInfo(long gid)
        {
            try
            {
                using SqlSugarClient dbClient = SugarUtils.CreateSqlSugarClient(DBPath);
                return dbClient.Queryable<GuildInfo>()
                               .InSingle(GuildEventArgs.FromGroup.Id); //单主键查询
            }
            catch (Exception e)
            {
                ConsoleLog.Error("Database error", ConsoleLog.ErrorLogBuilder(e));
                return null;
            }
        }

        #endregion 通用查询函数
    }
}
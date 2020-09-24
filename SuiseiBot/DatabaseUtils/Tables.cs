using System.Collections.Generic;
using SqlSugar;
using SuiseiBot.Code.Resource.TypeEnum;
using SuiseiBot.Code.Resource.TypeEnum.AuctionType;
using SuiseiBot.Code.Resource.TypeEnum.GuildBattleType;
using FlagType = SuiseiBot.Code.Resource.TypeEnum.GuildBattleType.FlagType;

namespace SuiseiBot.Code.DatabaseUtils
{
    #region 彗酱签到表定义
    /// <summary>
    /// 用于存放彗酱信息的表定义
    /// </summary>
    [SugarTable("suisei", "suisei data table")]
    internal class SuiseiData
    {
        //用户QQ
        [SugarColumn(ColumnName = "uid", ColumnDataType = "INTEGER", IsPrimaryKey = true)]
        public long Uid { get; set; }
        //用户所在群号
        [SugarColumn(ColumnName = "gid", ColumnDataType = "INTEGER", IsPrimaryKey = true)]
        public long Gid { get; set; }
        //好感度（大概
        [SugarColumn(ColumnName = "favor_rate", ColumnDataType = "INTEGER")]
        public int FavorRate { get; set; }
        //签到时间(使用时间戳）
        [SugarColumn(ColumnName = "use_date", ColumnDataType = "INTEGER")]
        public long ChatDate { get; set; }
    }
    #endregion

    #region 阿B订阅数据表定义
    [SugarTable("bili_subscription")]
    internal class BiliSubscription
    {
        [SugarColumn(ColumnName = "gid", ColumnDataType = "INTEGER", IsPrimaryKey = true)]
        public long Gid { set; get; }
        [SugarColumn(ColumnName = "subscription_id", ColumnDataType = "INTEGER", IsPrimaryKey = true)]
        public long SubscriptionId { set; get; }
        [SugarColumn(ColumnName = "update_time", ColumnDataType = "VARCHAR")]
        public long UpdateTime { set; get; }
    }
    #endregion

    #region 会战数据

    #region 出刀记录表定义
    [SugarTable("guildbattle")]
    internal class GuildBattle
    {
        /// <summary>
        /// 记录编号[自增]
        /// </summary>
        [SugarColumn(ColumnName = "aid" , ColumnDataType = "INTEGER",IsIdentity = true,IsPrimaryKey = true)]
        public int Aid { get; set; }
        /// <summary>
        /// 用户QQ号
        /// </summary>
        [SugarColumn(ColumnName = "uid",ColumnDataType = "INTEGER")]
        public long Uid { get; set; }
        /// <summary>
        /// 记录产生时间
        /// </summary>
        [SugarColumn(ColumnName = "time",ColumnDataType = "INTEGER")]
        public long Time { get; set; }
        /// <summary>
        /// 周目数
        /// </summary>
        [SugarColumn(ColumnName = "round",ColumnDataType = "INTEGER")]
        public int Round { get; set; }
        /// <summary>
        /// boss的序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num",ColumnDataType = "INTEGER")]
        public int Order { get; set; }
        /// <summary>
        /// 伤害数值
        /// </summary>
        [SugarColumn(ColumnName = "dmg",ColumnDataType = "INTEGER")]
        public long Damage { get; set; }
        /// <summary>
        /// 出刀类型标记
        /// </summary>
        [SugarColumn(ColumnName = "flag",ColumnDataType = "INTEGER")]
        public AttackType Attack { get; set; }
    }
    #endregion

    #region 成员表定义
    [SugarTable("member")]
    internal class MemberInfo
    {
        /// <summary>
        /// 用户所在群号，同时也是公会标识
        /// </summary>
        [SugarColumn(ColumnName = "gid",ColumnDataType = "INTEGER",IsPrimaryKey = true)]
        public long Gid { get; set; }
        /// <summary>
        /// 用户的QQ号
        /// </summary>
        [SugarColumn(ColumnName = "uid",ColumnDataType = "INTEGER",IsPrimaryKey = true)]
        public long Uid { get; set; }
        /// <summary>
        /// 用户状态修改时间
        /// </summary>
        [SugarColumn(ColumnName = "time",ColumnDataType = "INTEGER")]
        public long Time { get; set; }
        /// <summary>
        /// 用户状态标志
        /// </summary>
        [SugarColumn(ColumnName = "flag",ColumnDataType = "INTEGER")]
        public FlagType Flag { get; set; }
        /// <summary>
        /// 状态描述（可空，需按照文档进行修改）
        /// </summary>
        [SugarColumn(ColumnName = "info",ColumnDataType = "VARCHAR",IsNullable = true)]
        public string Info { get; set; }
        /// <summary>
        /// 当日SL标记,使用时间戳存储产生时间
        /// </summary>
        [SugarColumn(ColumnName = "sl",ColumnDataType = "INTEGER")]
        public long SL { get; set; }
    }
    #endregion

    #region 公会表定义
    [SugarTable("guild")]
    internal class GuildInfo
    {
        /// <summary>
        /// 公会所属群号
        /// </summary>
        [SugarColumn(ColumnName = "gid",ColumnDataType = "INTEGER",IsPrimaryKey = true)]
        public long Gid { get; set; }

        /// <summary>
        /// 公会名
        /// </summary>
        [SugarColumn(ColumnName = "name", ColumnDataType = "VARCHAR")]
        public string GuildName { get; set; }

        /// <summary>
        /// 公会所在区服
        /// </summary>
        [SugarColumn(ColumnName = "server", ColumnDataType = "INTEGER")]
        public Server ServerId { get; set; }

        /// <summary>
        /// 当前boss的血量
        /// </summary>
        [SugarColumn(ColumnName = "hp",ColumnDataType = "INTEGER")]
        public long HP { get; set; }

        /// <summary>
        /// 当前boss的总血量
        /// </summary>
        [SugarColumn(ColumnName = "total_hp",ColumnDataType = "INTEGER")]
        public long TotalHP { get; set; }

        /// <summary>
        /// 当前公会所在周目
        /// </summary>
        [SugarColumn(ColumnName = "round",ColumnDataType = "INTEGER")]
        public int Round { get; set; }

        /// <summary>
        /// 当前所在boss序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num",ColumnDataType = "INTEGER")]
        public int Order { get; set; }

        /// <summary>
        /// 当前boss阶段
        /// </summary>
        [SugarColumn(ColumnName = "boss_phase",ColumnDataType = "INTEGER")]
        public int BossPhase { get; set; }

        /// <summary>
        /// 公会是否在会战
        /// </summary>
        [SugarColumn(ColumnName = "in_battle",ColumnDataType = "INTEGER")]
        public bool InBattle { get; set; }
    }
    #endregion

    #region Boss数据
    [SugarTable("guild_battle_boss")]
    internal class GuildBattleBoss
    {
        /// <summary>
        /// boss的区服
        /// </summary>
        [SugarColumn(ColumnName = "server",ColumnDataType = "INTEGER",IsPrimaryKey = true)]
        public Server ServerId { set; get; }

        /// <summary>
        /// Boss序号
        /// </summary>
        [SugarColumn(ColumnName = "order_num",ColumnDataType = "INTEGER",IsPrimaryKey = true)]
        public int Order { set; get; }

        /// <summary>
        /// 阶段
        /// </summary>
        [SugarColumn(ColumnName = "phase",ColumnDataType = "INTEGER",IsPrimaryKey = true)]
        public int Phase { set; get; }

        /// <summary>
        /// 进入下一阶段的所需的周目数
        /// </summary>
        [SugarColumn(ColumnName = "round",ColumnDataType = "INTEGER")]
        public int Round { set; get; }

        /// <summary>
        /// boss的血量
        /// </summary>
        [SugarColumn(ColumnName = "hp",ColumnDataType = "INTEGER")]
        public long HP { set; get; }

        public static List<GuildBattleBoss> GetInitBossInfos()
        {
            List<GuildBattleBoss> initInfos = new List<GuildBattleBoss>();

            #region 一阶段
            initInfos.Add(new GuildBattleBoss
            {
                ServerId = Server.CN,
                HP       = 6000000,
                Order    = 1,
                Phase    = 1,
                Round    = 1
            });
            initInfos.Add(new GuildBattleBoss
            {
                ServerId = Server.CN,
                HP       = 8000000,
                Order    = 2,
                Phase    = 1,
                Round    = 1
            });
            initInfos.Add(new GuildBattleBoss
            {
                ServerId = Server.CN,
                HP       = 10000000,
                Order    = 3,
                Phase    = 1,
                Round    = 1
            });
            initInfos.Add(new GuildBattleBoss
            {
                ServerId = Server.CN,
                HP       = 12000000,
                Order    = 4,
                Phase    = 1,
                Round    = 1
            });
            initInfos.Add(new GuildBattleBoss
            {
                ServerId = Server.CN,
                HP       = 20000000,
                Order    = 5,
                Phase    = 1,
                Round    = 1
            });
            #endregion
            
            #region 二阶段
            initInfos.Add(new GuildBattleBoss
            {
                ServerId = Server.CN,
                HP       = 6000000,
                Order    = 1,
                Phase    = 2,
                Round    = -1
            });
            initInfos.Add(new GuildBattleBoss
            {
                ServerId = Server.CN,
                HP       = 8000000,
                Order    = 2,
                Phase    = 2,
                Round    = -1
            });
            initInfos.Add(new GuildBattleBoss
            {
                ServerId = Server.CN,
                HP       = 10000000,
                Order    = 3,
                Phase    = 2,
                Round    = -1
            });
            initInfos.Add(new GuildBattleBoss
            {
                ServerId = Server.CN,
                HP       = 12000000,
                Order    = 4,
                Phase    = 2,
                Round    = -1
            });
            initInfos.Add(new GuildBattleBoss
            {
                ServerId = Server.CN,
                HP       = 20000000,
                Order    = 5,
                Phase    = 2,
                Round    = -1
            });
            #endregion

            return initInfos;
        }
    }
    #endregion

    #endregion

    #region 拍卖游戏数据表定义

    #region 拍卖会数据表定义
    [SugarTable("auction")]
    internal class AuctionInfo
    {
        /// <summary>
        /// 拍卖会所属群号
        /// </summary>
        [SugarColumn(ColumnName = "aid", ColumnDataType = "INTEGER", IsPrimaryKey = true)]
        public long Aid { get; set; }

        /// <summary>
        /// 拍卖会名
        /// </summary>
        [SugarColumn(ColumnName = "name", ColumnDataType = "VARCHAR")]
        public string AuctionName { get; set; }

        /// <summary>
        /// 当前拍卖品的轮次
        /// </summary>
        [SugarColumn(ColumnName = "round", ColumnDataType = "INTEGER")]
        public long Round { get; set; }

        /// <summary>
        /// 当前拍卖品起拍价
        /// </summary>
        [SugarColumn(ColumnName = "curr_start_bid", ColumnDataType = "INTEGER")]
        public long CurrStartBid { get; set; }

        /// <summary>
        /// 当前拍卖品加价幅度
        /// </summary>
        [SugarColumn(ColumnName = "curr_range_bid", ColumnDataType = "INTEGER")]
        public long CurrRangeBid { get; set; }

        /// <summary>
        /// 当前拍卖品价格
        /// </summary>
        [SugarColumn(ColumnName = "curr_bid", ColumnDataType = "INTEGER")]
        public long CurrBid { get; set; }

        /// <summary>
        /// 公会是否在会战
        /// </summary>
        [SugarColumn(ColumnName = "in_auction", ColumnDataType = "INTEGER")]
        public bool InAuction { get; set; }
    }
    #endregion

    #region 拍卖会小组定义
    [SugarTable("group")]
    internal class GroupInfo
    {
        /// <summary>
        /// 拍卖小组所在群号，同时标识所在拍卖会
        /// </summary>
        [SugarColumn(ColumnName = "aid", ColumnDataType = "INTEGER", IsPrimaryKey = true)]
        public long Aid { get; set; }

        /// <summary>
        /// 拍卖小组组号
        /// </summary>
        [SugarColumn(ColumnName = "gid", ColumnDataType = "INTEGER", IsPrimaryKey = true)]
        public long Gid { get; set; }

        /// <summary>
        /// 拍卖小组组名
        /// </summary>
        [SugarColumn(ColumnName = "name", ColumnDataType = "VARCHAR")]
        public string GroupName { get; set; }

        /// <summary>
        /// 当前已获得总价值
        /// </summary>
        [SugarColumn(ColumnName = "curr_value", ColumnDataType = "INTEGER")]
        public long CurrValue { get; set; }

        /// <summary>
        /// 当前账户资金
        /// </summary>
        [SugarColumn(ColumnName = "curr_amount", ColumnDataType = "INTEGER")]
        public long CurrAmount { get; set; }
    }
    #endregion

    #region 拍卖会小组成员表定义
    [SugarTable("member")]
    internal class AuctionMemberInfo
    {
        /// <summary>
        /// 拍卖小组组号
        /// </summary>
        [SugarColumn(ColumnName = "gid", ColumnDataType = "INTEGER", IsPrimaryKey = true)]
        public long Gid { get; set; }

        /// <summary>
        /// 用户的QQ号
        /// </summary>
        [SugarColumn(ColumnName = "qid", ColumnDataType = "INTEGER", IsPrimaryKey = true)]
        public long Qid { get; set; }

        /// <summary>
        /// 用户等级标志
        /// </summary>
        [SugarColumn(ColumnName = "flag", ColumnDataType = "INTEGER")]
        public FlagType Flag { get; set; }
    }
    #endregion

    #region 拍卖品数据定义
    [SugarTable("auction_lot")]
    internal class AuctionLot
    {
        /// <summary>
        /// 拍卖品所属拍卖会，同时也标识拍卖会
        /// </summary>
        [SugarColumn(ColumnName = "aid", ColumnDataType = "INTEGER", IsPrimaryKey = true)]
        public long Aid { get; set; }

        /// <summary>
        /// 当前拍卖品的轮次
        /// </summary>
        [SugarColumn(ColumnName = "round", ColumnDataType = "INTEGER", IsPrimaryKey = true)]
        public long Round { get; set; }

        /// <summary>
        /// 当前拍卖品价值
        /// </summary>
        [SugarColumn(ColumnName = "value", ColumnDataType = "INTEGER")]
        public long Value { get; set; }

        /// <summary>
        /// 当前拍卖品起拍价
        /// </summary>
        [SugarColumn(ColumnName = "start_bid", ColumnDataType = "INTEGER")]
        public long StartBid { get; set; }

        /// <summary>
        /// 当前拍卖品加价幅度
        /// </summary>
        [SugarColumn(ColumnName = "range_bid", ColumnDataType = "INTEGER")]
        public long RangeBid { get; set; }
    }
    #endregion

    #region 出价记录表定义
    [SugarTable("auction_bid")]
    internal class AuctionBid
    {
        /// <summary>
        /// 记录编号[自增]
        /// </summary>
        [SugarColumn(ColumnName = "rid", ColumnDataType = "INTEGER", IsIdentity = true, IsPrimaryKey = true)]
        public int Rid { get; set; }

        /// <summary>
        /// 出价小组组号
        /// </summary>
        [SugarColumn(ColumnName = "gid", ColumnDataType = "INTEGER")]
        public long Gid { get; set; }

        /// <summary>
        /// 记录产生时间
        /// </summary>
        [SugarColumn(ColumnName = "time", ColumnDataType = "INTEGER")]
        public long Time { get; set; }

        /// <summary>
        /// 目标拍卖品轮次
        /// </summary>
        [SugarColumn(ColumnName = "round", ColumnDataType = "INTEGER")]
        public int Round { get; set; }

        /// <summary>
        /// 出价数值
        /// </summary>
        [SugarColumn(ColumnName = "price", ColumnDataType = "INTEGER")]
        public long Price { get; set; }

        /// <summary>
        /// 出刀类型标记
        /// </summary>
        [SugarColumn(ColumnName = "flag", ColumnDataType = "INTEGER")]
        public BidType BidFlag { get; set; }
    }
    #endregion

    #endregion
}

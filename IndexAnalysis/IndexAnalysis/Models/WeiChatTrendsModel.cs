using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndexAnalysis.Models
{
    public class WeiChatTrendsModel
    {
        public bool Success { get; set; }
        public WeiChatData Data { get; set; }
    }

    public class WeiChatData
    {
        public List<List<WeiChatTrend>> PvList { get; set; }
        public List<dynamic> InfoList { get; set; }
        public List<dynamic> TopPvDataList { get; set; }
    }
    public class WeiChatTrend
    {
        public int KwdId { get; set; }
        public int PV { get; set; }
        public int IsPeek { get; set; }
        public long Id { get; set; }
        public string Date { get; set; }
    }
}

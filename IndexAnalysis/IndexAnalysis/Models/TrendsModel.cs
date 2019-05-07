using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndexAnalysis.Models
{
    public class TrendsModel
    {
        public List<Trend> Fooww { get; set; }
        public List<Trend> FangYou { get; set; }
        public List<Trend> HaoFangTong { get; set; }
        public List<Trend> YiFangDaShi { get; set; }
        public List<Trend> FangZaiXian { get; set; }
        public List<Trend> FangWangTong { get; set; }
    }

    public class Trend
    {
        public DateTime Date { get; set; }
        public int Index { get; set; }
    }
}

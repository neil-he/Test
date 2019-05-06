using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IndexAnalysis.Controllers
{
    public class AnalysisController : Controller
    {
        public IActionResult GetTrends()
        {
            //todo:返回所有竞品数据   
            return null;
        }

        public IActionResult GetWeiChatTrends()
        {
            //todo:返回微信指数
            return null;
        }

        public IActionResult GetPersonPortraitTrends()
        {
            //todo:返回人群画像
            return null;
        }
        public IActionResult GetDemandMappingTrends()
        {
            //todo:返回需求图谱
            return null;
        }

    }
}
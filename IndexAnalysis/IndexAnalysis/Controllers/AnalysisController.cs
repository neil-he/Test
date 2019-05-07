using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using IndexAnalysis.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace IndexAnalysis.Controllers
{
    public class AnalysisController : Controller
    {
        private readonly IConfiguration m_configuration;
        public AnalysisController(IConfiguration configuration)
        {
            m_configuration = configuration;
        }
        public JsonResult GetTrends()
        {
            //todo:返回所有竞品数据
            Process p = new Process();
            string path =Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "baidu/demo.py");
            string sArguments = path;

            p.StartInfo.FileName = m_configuration["Python"];
            p.StartInfo.Arguments = sArguments;
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            StreamReader sReader = p.StandardOutput;
            string[] output = sReader.ReadToEnd().Split('\r');

            TrendsModel trendsModel = new TrendsModel();
            trendsModel.Fooww = JsonConvert.DeserializeObject<List<Trend>>(output[0]);
            trendsModel.FangYou = JsonConvert.DeserializeObject<List<Trend>>(output[1]);
            trendsModel.HaoFangTong = JsonConvert.DeserializeObject<List<Trend>>(output[2]);
            trendsModel.YiFangDaShi = JsonConvert.DeserializeObject<List<Trend>>(output[3]);
            trendsModel.FangZaiXian = JsonConvert.DeserializeObject<List<Trend>>(output[4]);
            trendsModel.FangWangTong = JsonConvert.DeserializeObject<List<Trend>>(output[5]);


            return Json(trendsModel);
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
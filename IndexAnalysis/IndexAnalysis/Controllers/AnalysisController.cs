using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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

            //TrendsModel trendsModel = new TrendsModel();
            //trendsModel.Fooww = JsonConvert.DeserializeObject<List<Trend>>(output[0]);
            //trendsModel.FangYou = JsonConvert.DeserializeObject<List<Trend>>(output[1]);
            //trendsModel.HaoFangTong = JsonConvert.DeserializeObject<List<Trend>>(output[2]);
            //trendsModel.YiFangDaShi = JsonConvert.DeserializeObject<List<Trend>>(output[3]);
            //trendsModel.FangZaiXian = JsonConvert.DeserializeObject<List<Trend>>(output[4]);
            ////trendsModel.FangWangTong = JsonConvert.DeserializeObject<List<Trend>>(output[5]);

            List<List<string[]>> model = new List<List<string[]>>();
            model.Add(JsonConvert.DeserializeObject<List<Trend>>(output[0]).Select(a => new string[] { a.Date.ToString("yyyy-MM-dd"), a.Index.ToString() }).ToList());
            model.Add(JsonConvert.DeserializeObject<List<Trend>>(output[1]).Select(a => new string[] { a.Date.ToString("yyyy-MM-dd"), a.Index.ToString() }).ToList());
            model.Add(JsonConvert.DeserializeObject<List<Trend>>(output[2]).Select(a => new string[] { a.Date.ToString("yyyy-MM-dd"), a.Index.ToString() }).ToList());
            model.Add(JsonConvert.DeserializeObject<List<Trend>>(output[3]).Select(a => new string[] { a.Date.ToString("yyyy-MM-dd"), a.Index.ToString() }).ToList());
            model.Add(JsonConvert.DeserializeObject<List<Trend>>(output[4]).Select(a => new string[] { a.Date.ToString("yyyy-MM-dd"), a.Index.ToString() }).ToList());

            return Json(model);
        }

        public JsonResult GetWeiChatTrends()
        {
            //todo:返回微信指数

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://zhishu.sogou.com/getDateData?kwdNamesStr=%E6%A2%B5%E8%AE%AF%E6%88%BF%E5%B1%8B%E7%AE%A1%E7%90%86%E7%B3%BB%E7%BB%9F,%E6%88%BF%E5%8F%8B,%E5%A5%BD%E6%88%BF%E9%80%9A,%E6%98%93%E6%88%BF%E5%A4%A7%E5%B8%88,%E6%88%BF%E5%9C%A8%E7%BA%BF&startDate=20190401&endDate=20190430&dataType=MEDIA_WECHAT&queryType=INPUT");
            request.Method = "GET";
            var response = request.GetResponse();
            var sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            string retXml = sr.ReadToEnd();
            sr.Close();

            WeiChatTrendsModel r = JsonConvert.DeserializeObject<WeiChatTrendsModel>(retXml);

            //TrendsModel trendsModel = new TrendsModel();
            //trendsModel.Fooww = r.Data.PvList[0].Select(p => new string[] { p.PV.ToString(), DateTime.ParseExact(p.Date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString() }).ToList();
            //trendsModel.FangYou = r.Data.PvList[1].Select(p => new string[] { p.PV,  DateTime.ParseExact(p.Date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture) }).ToList();
            //trendsModel.HaoFangTong = r.Data.PvList[2].Select(p => new string[] { p.PV,  DateTime.ParseExact(p.Date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture) }).ToList();
            //trendsModel.YiFangDaShi = r.Data.PvList[3].Select(p => new string[] {  p.PV, DateTime.ParseExact(p.Date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture) }).ToList();
            //trendsModel.FangZaiXian = r.Data.PvList[4].Select(p => new string[] {  p.PV, DateTime.ParseExact(p.Date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture) }).ToList();

            List<List<string[]>> model = new List<List<string[]>>();
            model.Add(r.Data.PvList[0].Select(p => new string[] { DateTime.ParseExact(p.Date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"), p.PV.ToString() }).ToList());
            model.Add(r.Data.PvList[1].Select(p => new string[] { DateTime.ParseExact(p.Date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"), p.PV.ToString() }).ToList());
            model.Add(r.Data.PvList[2].Select(p => new string[] { DateTime.ParseExact(p.Date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"), p.PV.ToString() }).ToList());
            model.Add(r.Data.PvList[3].Select(p => new string[] { DateTime.ParseExact(p.Date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"), p.PV.ToString() }).ToList());
            model.Add(r.Data.PvList[4].Select(p => new string[] { DateTime.ParseExact(p.Date, "yyyyMMdd", System.Globalization.CultureInfo.InvariantCulture).ToString("yyyy-MM-dd"), p.PV.ToString() }).ToList());

            return Json(model);
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
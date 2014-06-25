using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Gva.Api.ModelsDO;
using Ghostscript.NET;
using System.Collections.Generic;
using System.IO;
using System.Data.SqlClient;
using System.Drawing;
using Common.Blob;
using Common.Json;
using Ghostscript.NET.Rasterizer;
using OMR;
using System.Configuration;
using Newtonsoft.Json.Linq;
using Common.Api.Models;
using Common.Data;
using Gva.Api.Models;
using Common.Api.Repositories.NomRepository;
using Docs.Api.DataObjects;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/exam")]
    public class ExamsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private INomRepository nomRepository;

        public ExamsController(
            IUnitOfWork unitOfWork,
            INomRepository nomRepository)
        {
            this.unitOfWork = unitOfWork;
            this.nomRepository = nomRepository;
        }

        [Route("getAnswers")]
        [HttpGet]
        public IHttpActionResult GetAnswers(string fileKey, int id, string name)
        {
            GhostscriptVersionInfo lastInstalledVersion = GhostscriptVersionInfo.GetLastInstalledVersion(GhostscriptLicense.GPL | GhostscriptLicense.AFPL, GhostscriptLicense.GPL);
            Dictionary<string, List<List<bool>>> answers = new Dictionary<string, List<List<bool>>>();
            byte[] file;

            string[] extentions = { ".pdf", ".jpg", ".bmp", ".jpeg", ".png", ".tiff" };
            if (!extentions.Contains(Path.GetExtension(name)))
            {
                return Ok(new { err = "No PDF or IMAGE file" });
            }

            using (MemoryStream m1 = new MemoryStream())
            {
                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
                {
                    connection.Open();

                    using (var blobStream = new BlobReadStream(connection, "dbo", "Blobs", "Content", "Key", fileKey))
                    {
                        blobStream.CopyTo(m1);
                    }
                }

                Bitmap bitmapImg;
                if (Path.GetExtension(name) == ".pdf")
                {
                    using (GhostscriptRasterizer rasterizer = new GhostscriptRasterizer())
                    {
                        rasterizer.Open(m1, lastInstalledVersion, false);

                        //Copied, because rasterizer disposes itself
                        bitmapImg = new Bitmap(rasterizer.GetPage(300, 300, 1));
                    }
                }
                else
                {
                    bitmapImg = new Bitmap(m1);
                }

                OMRConfiguration conf = new OMRConfiguration();
                conf.AdjustmentBlock = new OMRAdjustmentBlock(1572, 3075, 425, 106);
                conf.WhiteBlock = new OMRAdjustmentBlock(1997, 3075, 106, 106);
                conf.DarkFactor = 0.90;
                conf.FillFactor = 1.60;
                conf.ImageWidth = 2182;
                conf.ImageHeight = 3210;
                conf.Blocks.Add(new OMRQuestionBlock("commonQuestions1", 387, 1059, 425, 530, 5));
                conf.Blocks.Add(new OMRQuestionBlock("commonQuestions2", 1312, 1059, 425, 530, 5));
                conf.Blocks.Add(new OMRQuestionBlock("specializedQuestions1", 387, 1798, 425, 1060, 10));
                conf.Blocks.Add(new OMRQuestionBlock("specializedQuestions2", 1312, 1798, 425, 1060, 10));

                using (var omr = new OMRReader(conf))
                using (bitmapImg)
                {
                    answers = omr.Read(bitmapImg);

                    using (Bitmap bitmapImgResized = omr.ResizeImage(bitmapImg, 580, 800))
                    {
                        using (MemoryStream m2 = new MemoryStream())
                        {
                            bitmapImgResized.Save(m2, System.Drawing.Imaging.ImageFormat.Jpeg);
                            file = m2.ToArray();
                        }
                    }
                }
            }

            if (answers != null)
            {
                return Ok(new { answ = answers, file = file });
            }
            else
            {
                return Ok(new { err = "Failed recognition!" });
            }
        }

        [Route("calculateGrade")]
        [HttpPost]
        public IHttpActionResult PostCalculateGrade(ASExamDO exam)
        {
            if (exam.CommonQuestion == null || exam.SpecializedQuestion == null)
            {
                return BadRequest();
            }

            List<List<bool>> commonQuestions = this.unitOfWork.DbContext.Set<ASExamVariantQuestion>()
                .Include(e => e.ASExamQuestion)
                .Where(e => e.ASExamVariantId == exam.CommonQuestion.NomValueId)
                .OrderBy(e => e.QuestionNumber)
                .SelectMany(e => new List<List<bool>>()
                {
                    new List<bool>()
                    {
                        e.ASExamQuestion.IsChecked1,
                        e.ASExamQuestion.IsChecked2,
                        e.ASExamQuestion.IsChecked3,
                        e.ASExamQuestion.IsChecked4
                    }
                }).ToList();

            List<List<bool>> specializedQuestions = this.unitOfWork.DbContext.Set<ASExamVariantQuestion>()
                .Include(e => e.ASExamQuestion)
                .Where(e => e.ASExamVariantId == exam.SpecializedQuestion.NomValueId)
                .OrderBy(e => e.QuestionNumber)
                .SelectMany(e => new List<List<bool>>()
                {
                    new List<bool>()
                    {
                        e.ASExamQuestion.IsChecked1,
                        e.ASExamQuestion.IsChecked2,
                        e.ASExamQuestion.IsChecked3,
                        e.ASExamQuestion.IsChecked4
                    }
                }).ToList();

            double result = 0;

            for (int i = 0; i < commonQuestions.Count(); i++)
            {
                double mid = 1;
                for (int j = 0; j < 4; j++)
                {
                    if (exam.CommonQuestions[i][j] != commonQuestions[i][j])
                    {
                        mid -= 0.5;
                    }
                }

                mid = mid < 0 ? 0 : mid;

                result += mid;
            }

            for (int i = 0; i < specializedQuestions.Count(); i++)
            {
                double mid = 1;
                for (int j = 0; j < 4; j++)
                {
                    if (exam.SpecializedQuestions[i][j] != specializedQuestions[i][j])
                    {
                        mid -= 0.5;
                    }
                }

                mid = mid < 0 ? 0 : mid;

                result += mid;
            }

            var yesNoNom = this.nomRepository.GetNomValues("boolean");
            NomValue passed = null;

            if (((result * 100) / 30) >= exam.SuccessThreshold)
            {
                passed = yesNoNom.FirstOrDefault(e => e.Alias == "yes");
            }
            else
            {
                passed = yesNoNom.FirstOrDefault(e => e.Alias == "no");
            }

            return Ok(new { result = result, passed = passed });
        }
    }
}


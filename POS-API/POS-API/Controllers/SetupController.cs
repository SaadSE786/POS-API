using POS_API.BusinessObjects;
using POS_API.Models;
using POS_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace POS_API.Controllers
{
    [RoutePrefix("api/setup")]
    public class SetupController : ApiController
    {
        POSEntities db = new POSEntities();
        readonly SQLService _sqlService;

        public SetupController(SQLService sqlService)
        {
            _sqlService = sqlService;
        }

        #region Level 1
        [HttpPost, Route("addLevel1")]
        public async Task<HttpResponseMessage> AddLevel1([FromBody] Level1 lvl)
        {
            if(lvl != null)
            {
                try
                {
                    tblLevel1 level1 = new tblLevel1();
                    level1.intLevel1Id = await _sqlService.GetMaxId("tblLevel1", "intLevel1Id"); 
                    level1.varLevel1Name = lvl.varLevel1Name;
                    level1.dtCreationDate = DateTime.Now;
                    level1.intCreatedBy = lvl.intCreatedBy;
                    level1.intCompanyId = lvl.intCompanyId;
                    db.tblLevel1.Add(level1);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success" });
                    //return Request.CreateResponse(HttpStatusCode.Unauthorized, new { status = 401, message = "Unauthorized" });

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, new { status = 500, message = "Internal Server Error" });
                    throw ex;
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { status = 400, message = "Bad Request" });

            }
        }

        [HttpGet, Route("getLevel1")]
        public HttpResponseMessage GetLevel1()
        {
            try
            {
                List<Level1> level1s = db.tblLevel1.Select(l => new Level1
                {
                    intLevel1Id = l.intLevel1Id,
                    varLevel1Name = l.varLevel1Name,
                    intCreatedBy = l.intCreatedBy,
                    intCompanyId = l.intCompanyId,
                    dtCreationDate = l.dtCreationDate,
                    dtUpdationDate = l.dtUpdationDate
                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success", levels = level1s });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }
        [HttpPut, Route("updateLevel1")]
        public HttpResponseMessage UpdateLevel1([FromBody] Level1 lvl)
        {
            try
            {
                tblLevel1 level1 = db.tblLevel1.Find(lvl.intLevel1Id);
                if (level1 == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = 202, message = "Not found"});
                }
                level1.varLevel1Name = lvl.varLevel1Name;
                level1.dtUpdationDate = DateTime.Now;
                level1.intUpdatedBy = lvl.intUpdatedBy;
                db.Entry(level1).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success"});
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }
        [HttpDelete, Route("deleteLevel1/{id}")]
        public HttpResponseMessage DeleteLevel1(int id)
        {
            try
            {
                
                tblLevel1 level = db.tblLevel1.Find(id);
                if (level == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = 202, message = "Not found"});
                }
                db.tblLevel1.Remove(level);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success"});
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }
        [HttpGet, Route("getLevel1ById/{id}")]
        public HttpResponseMessage GetLevel1ById(int id)
        {
            try
            {
                Level1 level = db.tblLevel1.Where(i => i.intLevel1Id == id).Select(l => new Level1
                {
                    intLevel1Id = l.intLevel1Id,
                    varLevel1Name = l.varLevel1Name,
                    intCreatedBy = l.intCreatedBy,
                    intCompanyId = l.intCompanyId,
                    dtCreationDate = l.dtCreationDate,
                    dtUpdationDate = l.dtUpdationDate
                }).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, level);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }
        #endregion
        #region Level 2
        [HttpPost, Route("addLevel2")]
        public HttpResponseMessage AddLevel2([FromBody] Level2 lvl)
        {
            if (lvl != null)
            {
                try
                {
                    tblLevel2 level2 = new tblLevel2();
                    level2.varLevel2Name = lvl.varLevel2Name;
                    level2.dtCreationDate = DateTime.Now;
                    level2.intCreatedBy = lvl.intCreatedBy;
                    level2.intLevel1Id = lvl.intLevel1Id;
                    db.tblLevel2.Add(level2);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success" });
                    //return Request.CreateResponse(HttpStatusCode.Unauthorized, new { status = 401, message = "Unauthorized" });

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, new { status = 500, message = "Internal Server Error" });
                    throw ex;
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { status = 400, message = "Bad Request" });

            }
        }

        [HttpGet, Route("getLevel2")]
        public HttpResponseMessage GetLevel2()
        {
            try
            {
                var level2s = _sqlService.GetLevel2();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success", levels = level2s });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }
        [HttpPut, Route("updateLevel2")]
        public HttpResponseMessage UpdateLevel2([FromBody] Level2 lvl)
        {
            try
            {
                tblLevel2 level2 = db.tblLevel2.Find(lvl.intLevel2Id);
                if (level2 == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = 202, message = "Not found" });
                }
                level2.varLevel2Name = lvl.varLevel2Name;
                level2.dtUpdationDate = DateTime.Now;
                level2.intUpdatedBy = lvl.intUpdatedBy;
                level2.intLevel1Id = lvl.intLevel1Id;
                db.Entry(level2).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }
        [HttpDelete, Route("deleteLevel2/{id}")]
        public HttpResponseMessage DeleteLevel2(int id)
        {
            try
            {

                tblLevel2 level = db.tblLevel2.Find(id);
                if (level == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = 202, message = "Not found" });
                }
                db.tblLevel2.Remove(level);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }
        [HttpGet, Route("getLevel2ById/{id}")]
        public HttpResponseMessage GetLevel2ById(int id)
        {
            try
            {
                Level2 level = _sqlService.GetLevel2ById(id);
                return Request.CreateResponse(HttpStatusCode.OK, level);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }
        #endregion
        #region Level3
        [HttpPost, Route("addLevel3")]
        public HttpResponseMessage AddLevel3([FromBody] Level3 lvl)
        {
            if (lvl != null)
            {
                try
                {
                    tblLevel3 level3 = new tblLevel3();
                    level3.varLevel3Name = lvl.varLevel3Name;
                    level3.dtCreationDate = DateTime.Now;
                    level3.intCreatedBy = lvl.intCreatedBy;
                    level3.intLevel2Id = lvl.intLevel2Id;
                    db.tblLevel3.Add(level3);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success" });
                    //return Request.CreateResponse(HttpStatusCode.Unauthorized, new { status = 401, message = "Unauthorized" });

                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, new { status = 500, message = "Internal Server Error" });
                    throw ex;
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { status = 400, message = "Bad Request" });

            }
        }

        [HttpGet, Route("getLevel3")]
        public HttpResponseMessage GetLevel3()
        {
            try
            {
                var level2s = _sqlService.GetLevel3();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success", levels = level2s });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }
        [HttpPut, Route("updateLevel3")]
        public HttpResponseMessage UpdateLevel3([FromBody] Level3 lvl)
        {
            try
            {
                tblLevel3 level3 = db.tblLevel3.Find(lvl.intLevel3Id);
                if (level3 == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = 202, message = "Not found" });
                }
                level3.varLevel3Name = lvl.varLevel3Name;
                level3.dtUpdationDate = DateTime.Now;
                level3.intUpdatedBy = lvl.intUpdatedBy;
                level3.intLevel2Id = lvl.intLevel2Id;
                db.Entry(level3).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }
        [HttpDelete, Route("deleteLevel3/{id}")]
        public HttpResponseMessage DeleteLevel3(int id)
        {
            try
            {

                tblLevel3 level = db.tblLevel3.Find(id);
                if (level == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = 202, message = "Not found" });
                }
                db.tblLevel3.Remove(level);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }
        [HttpGet, Route("getLevel3ById/{id}")]
        public HttpResponseMessage GetLevel3ById(int id)
        {
            try
            {
                Level3 level = _sqlService.GetLevel3ById(id);
                return Request.CreateResponse(HttpStatusCode.OK, level);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }
        #endregion
    }
}

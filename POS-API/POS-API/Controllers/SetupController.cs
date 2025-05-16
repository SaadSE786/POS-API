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

        #region Item
        [HttpPost,Route("addItem")]
        public HttpResponseMessage AddItem([FromBody] Item item)
        {
            if (item != null)
            {
                try
                {
                    tblItem tblItem = new tblItem();
                    tblItem.varItemName = item.varItemName;
                    tblItem.dcOrderLevel = item.dcOrderLevel;
                    tblItem.dcMinLevel = item.dcMinLevel;
                    tblItem.dcMaxLevel = item.dcMaxLevel;
                    tblItem.dcOpenStock = item.dcOpenStock;
                    tblItem.dtOpenDate = item.dtOpenDate;
                    tblItem.dcPurRate = item.dcPurRate;
                    tblItem.dcSellRate = item.dcSellRate;
                    tblItem.dcRetailSaleRate = item.dcRetailSaleRate;
                    tblItem.dcDistributorSaleRate = item.dcDistributorSaleRate;
                    tblItem.dcDiscount = item.dcDiscount;
                    tblItem.isActive = item.isActive;
                    tblItem.isTaxable = item.isTaxable;
                    tblItem.isExpirable = item.isExpirable;
                    tblItem.dtExpiryDate = item.dtExpiryDate;
                    tblItem.varUom = item.varUom;
                    tblItem.intCompanyId = item.intCompanyId;
                    tblItem.intCreatedBy = item.intCreatedBy;
                    tblItem.dtCreationDate = DateTime.Now;
                    db.tblItems.Add(tblItem);
                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success" });
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
        [HttpGet, Route("getItems")]
        public HttpResponseMessage GetItems()
        {
            try
            {
                var itemList = db.tblItems.Select(i => new Item
                {
                    intItemId = i.intItemId,
                    varItemName = i.varItemName,
                    dcOrderLevel = i.dcOrderLevel,
                    dcMinLevel = i.dcMinLevel,
                    dcMaxLevel = i.dcMaxLevel,
                    dcOpenStock = i.dcOpenStock,
                    dtOpenDate = i.dtOpenDate,
                    dcPurRate = i.dcPurRate,
                    dcSellRate = i.dcSellRate,
                    dcRetailSaleRate = i.dcRetailSaleRate,
                    dcDistributorSaleRate = i.dcDistributorSaleRate,
                    dcDiscount = i.dcDiscount,
                    isActive = i.isActive,
                    isTaxable = i.isTaxable,
                    isExpirable = i.isExpirable,
                    dtExpiryDate = i.dtExpiryDate,
                    varUom = i.varUom,
                    intCompanyId = i.intCompanyId,
                    intCreatedBy = i.intCreatedBy,
                    dtCreationDate = i.dtCreationDate,
                    dtUpdationDate = i.dtUpdationDate
                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success", items = itemList });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }
        [HttpPut, Route("updateItem")]
        public HttpResponseMessage UpdateItem([FromBody] Item item)
        {
            try
            {
                tblItem tblItem = db.tblItems.Find(item.intItemId);
                if (tblItem == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = 202, message = "Not found" });
                }
                tblItem.varItemName = item.varItemName;
                tblItem.dcOrderLevel = item.dcOrderLevel;
                tblItem.dcMinLevel = item.dcMinLevel;
                tblItem.dcMaxLevel = item.dcMaxLevel;
                tblItem.dcOpenStock = item.dcOpenStock;
                tblItem.dtOpenDate = item.dtOpenDate;
                tblItem.dcPurRate = item.dcPurRate;
                tblItem.dcSellRate = item.dcSellRate;
                tblItem.dcRetailSaleRate = item.dcRetailSaleRate;
                tblItem.dcDistributorSaleRate = item.dcDistributorSaleRate;
                tblItem.dcDiscount = item.dcDiscount;
                tblItem.isActive = item.isActive;
                tblItem.isTaxable = item.isTaxable;
                tblItem.isExpirable = item.isExpirable;
                tblItem.dtExpiryDate = item.dtExpiryDate;
                tblItem.varUom = item.varUom;
                tblItem.intCompanyId = item.intCompanyId;
                tblItem.intUpdatedBy = item.intUpdatedBy;
                tblItem.dtUpdationDate = DateTime.Now;
                db.Entry(tblItem).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }
        [HttpDelete, Route("deleteItem/{id}")]
        public HttpResponseMessage DeleteItem(int id)
        {
            try
            {
                tblItem item = db.tblItems.Find(id);
                if (item == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = 202, message = "Not found" });
                }
                db.tblItems.Remove(item);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }
        [HttpGet, Route("getItemById/{id}")]
        public HttpResponseMessage GetItem(int id)
        {
            try
            {
                Item item = db.tblItems.Where(i => i.intItemId == id).Select(i => new Item
                {
                    intItemId = i.intItemId,
                    varItemName = i.varItemName,
                    dcOrderLevel = i.dcOrderLevel,
                    dcMinLevel = i.dcMinLevel,
                    dcMaxLevel = i.dcMaxLevel,
                    dcOpenStock = i.dcOpenStock,
                    dtOpenDate = i.dtOpenDate,
                    dcPurRate = i.dcPurRate,
                    dcSellRate = i.dcSellRate,
                    dcRetailSaleRate = i.dcRetailSaleRate,
                    dcDistributorSaleRate = i.dcDistributorSaleRate,
                    dcDiscount = i.dcDiscount,
                    isActive = i.isActive,
                    isTaxable = i.isTaxable,
                    isExpirable = i.isExpirable,
                    dtExpiryDate = i.dtExpiryDate,
                    varUom = i.varUom,
                    intCompanyId = i.intCompanyId,
                    intCreatedBy = i.intCreatedBy,
                    dtCreationDate = i.dtCreationDate,
                    dtUpdationDate = i.dtUpdationDate
                }).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, item);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }
        #endregion
        #region Warehouse
        [HttpPost, Route("addWarehouse")]
        public HttpResponseMessage AddWarehouse([FromBody] Warehouse wrhouse)
        {
            if (wrhouse != null)
            {
                try
                {
                    tblWarehouse warehouse = new tblWarehouse();
                    warehouse.varWarehouseName = wrhouse.varWarehouseName;
                    warehouse.dtCreationDate = DateTime.Now;
                    warehouse.intCreatedBy = wrhouse.intCreatedBy;
                    warehouse.intCompanyId = wrhouse.intCompanyId;
                    db.tblWarehouses.Add(warehouse);
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

        [HttpGet, Route("getWarehouse")]
        public HttpResponseMessage GetWarehouse()
        {
            try
            {
                List<Warehouse> wrhouse = db.tblWarehouses.Select(l => new Warehouse
                {
                    intWarehouseId = l.intWarehouseId,
                    varWarehouseName = l.varWarehouseName,
                    intCreatedBy = l.intCreatedBy,
                    intCompanyId = l.intCompanyId,
                    dtCreationDate = l.dtCreationDate,
                    dtUpdationDate = l.dtUpdationDate,
                    intUpdatedBy = l.intUpdatedBy
                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success", warehouses = wrhouse });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }
        [HttpPut, Route("updateWarehouse")]
        public HttpResponseMessage UpdateWarehouse([FromBody] Warehouse wrhouse)
        {
            try
            {
                tblWarehouse warehouse = db.tblWarehouses.Find(wrhouse.intWarehouseId);
                if (warehouse == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = 202, message = "Not found" });
                }
                warehouse.varWarehouseName = wrhouse.varWarehouseName;
                warehouse.dtUpdationDate = DateTime.Now;
                warehouse.intUpdatedBy = wrhouse.intUpdatedBy;
                db.Entry(warehouse).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }
        [HttpDelete, Route("deleteWarehouse/{id}")]
        public HttpResponseMessage DeleteWarehouse(int id)
        {
            try
            {

                tblWarehouse warehouse = db.tblWarehouses.Find(id);
                if (warehouse == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = 202, message = "Not found" });
                }
                db.tblWarehouses.Remove(warehouse);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }
        [HttpGet, Route("getWarehouseById/{id}")]
        public HttpResponseMessage GetWarehouseById(int id)
        {
            try
            {
                Warehouse warehouse = db.tblWarehouses.Where(i => i.intWarehouseId == id).Select(l => new Warehouse
                {
                    intWarehouseId = l.intWarehouseId,
                    varWarehouseName = l.varWarehouseName,
                    intCreatedBy = l.intCreatedBy,
                    intCompanyId = l.intCompanyId,
                    dtCreationDate = l.dtCreationDate,
                    dtUpdationDate = l.dtUpdationDate
                }).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, warehouse);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }
        #endregion
        #region Transporter
        [HttpPost, Route("addTransporter")]
        public HttpResponseMessage AddTransporter([FromBody] Transporter transporter)
        {
            if (transporter != null)
            {
                try
                {
                    tblTransporter tblTransporter = new tblTransporter();
                    tblTransporter.varTransporterName = transporter.varTransporterName;
                    tblTransporter.varContactNo = transporter.varContactNo;
                    tblTransporter.varEmail = transporter.varEmail;
                    tblTransporter.varAddress = transporter.varAddress;
                    tblTransporter.dtCreationDate = DateTime.Now;
                    tblTransporter.intCreatedBy = transporter.intCreatedBy;
                    tblTransporter.intCompanyId = transporter.intCompanyId;
                    db.tblTransporters.Add(tblTransporter);
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

        [HttpGet, Route("getTransporter")]
        public HttpResponseMessage GetTransporter()
        {
            try
            {
                List<Transporter> transporters = db.tblTransporters.Select(l => new Transporter
                {
                    intTransporterId = l.intTransporterId,
                    varTransporterName = l.varTransporterName,
                    varContactNo = l.varContactNo,
                    varEmail = l.varEmail,
                    varAddress = l.varAddress,
                    intCreatedBy = l.intCreatedBy,
                    intCompanyId = l.intCompanyId,
                    dtCreationDate = l.dtCreationDate,
                    dtUpdationDate = l.dtUpdationDate,
                    intUpdatedBy = l.intUpdatedBy
                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success", transporter = transporters });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }
        [HttpPut, Route("updateTransporter")]
        public HttpResponseMessage UpdateTransporter([FromBody] Transporter transporter)
        {
            try
            {
                tblTransporter tblTransporter = db.tblTransporters.Find(transporter.intTransporterId);
                if (tblTransporter == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = 202, message = "Not found" });
                }
                tblTransporter.varTransporterName = transporter.varTransporterName;
                tblTransporter.varContactNo = transporter.varContactNo;
                tblTransporter.varEmail = transporter.varEmail;
                tblTransporter.varAddress= transporter.varAddress;
                tblTransporter.dtUpdationDate = DateTime.Now;
                tblTransporter.intUpdatedBy = transporter.intUpdatedBy;
                db.Entry(tblTransporter).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }
        [HttpDelete, Route("deleteTransporter/{id}")]
        public HttpResponseMessage DeleteTransporter(int id)
        {
            try
            {
                tblTransporter transporter = db.tblTransporters.Find(id);
                if (transporter == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = 202, message = "Not found" });
                }
                db.tblTransporters.Remove(transporter);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }
        [HttpGet, Route("getTransporterById/{id}")]
        public HttpResponseMessage GetTransporterById(int id)
        {
            try
            {
                Transporter transporter = db.tblTransporters.Where(i => i.intTransporterId == id).Select(l => new Transporter
                {
                    intTransporterId = l.intTransporterId,
                    varTransporterName = l.varTransporterName,
                    varContactNo = l.varContactNo,
                    varEmail = l.varEmail,
                    varAddress = l.varAddress,
                    intCreatedBy = l.intCreatedBy,
                    intCompanyId = l.intCompanyId,
                    dtCreationDate = l.dtCreationDate,
                    dtUpdationDate = l.dtUpdationDate
                }).FirstOrDefault();
                return Request.CreateResponse(HttpStatusCode.OK, transporter);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }
        #endregion
        #region User
        [HttpGet, Route("getUser")]
        public HttpResponseMessage GetUser()
        {
            try
            {
                var users = db.tblUsers.ToList() 
            .Select(u => new User
            {
                intUserId = u.intUserId,
                varName = u.varName,
                varEmail = u.varEmail,
                varAddress = u.varAddress,
                varPassword = u.varPassword,
                varCnic = u.varCnic,
                varContactNo = u.varContactNo,
                intCompanyId = u.intCompanyId,
                dtCreationDate = u.dtCreationDate,
                dtUpdationDate = u.dtUpdationDate,
                //intCreatedBy = u.intCreatedBy,
                //intUpdatedBy = u.intUpdatedBy,
                varPhoto = u.varPhoto != null ? Convert.ToBase64String(u.varPhoto) : null
            }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success",  users });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }

        [HttpGet, Route("getUserById/{id}")]
        public HttpResponseMessage GetUserById(int id)
        {
            try
            {
                var userEntity = db.tblUsers.FirstOrDefault(u => u.intUserId == id);

                if (userEntity == null)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = 202, message = "Not Found" });
                }

                var user = new User
                {
                    intUserId = userEntity.intUserId,
                    varName = userEntity.varName,
                    varEmail = userEntity.varEmail,
                    varAddress = userEntity.varAddress,
                    varPassword = userEntity.varPassword,
                    varCnic = userEntity.varCnic,
                    varContactNo = userEntity.varContactNo,
                    intCompanyId = userEntity.intCompanyId,
                    dtCreationDate = userEntity.dtCreationDate,
                    dtUpdationDate = userEntity.dtUpdationDate,
                    //intCreatedBy = userEntity.intCreatedBy,
                    //intUpdatedBy = userEntity.intUpdatedBy,
                    varPhoto = userEntity.varPhoto != null ? Convert.ToBase64String(userEntity.varPhoto) : null
                };
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success", user = user });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }

        [HttpPost, Route("addUser")]
        public HttpResponseMessage AddUser([FromBody] User user)
        {
            try
            {
                if (user == null)
                    return Request.CreateResponse(HttpStatusCode.BadRequest, new { status = 400, message = "Bad Request" });

                tblUser tblUser = new tblUser
                {
                    varName = user.varName,
                    varEmail = user.varEmail,
                    varAddress = user.varAddress,
                    varPassword = user.varPassword,
                    varCnic = user.varCnic,
                    varContactNo = user.varContactNo,
                    intCompanyId = user.intCompanyId,
                    dtCreationDate = DateTime.Now,
                    //intCreatedBy = user.intCreatedBy,
                    varPhoto = !string.IsNullOrEmpty(user.varPhoto) ? Convert.FromBase64String(user.varPhoto) : null
                };

                db.tblUsers.Add(tblUser);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }

        [HttpPut, Route("updateUser")]
        public HttpResponseMessage UpdateUser([FromBody] User user)
        {
            try
            {
                tblUser tblUser = db.tblUsers.Find(user.intUserId);
                if (tblUser == null)
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = 202, message = "Not Found" });

                tblUser.varName = user.varName;
                tblUser.varEmail = user.varEmail;
                tblUser.varAddress = user.varAddress;
                tblUser.varPassword = user.varPassword;
                tblUser.varCnic = user.varCnic;
                tblUser.varContactNo = user.varContactNo;
                tblUser.intCompanyId = user.intCompanyId;
                tblUser.dtUpdationDate = DateTime.Now;
                //tblUser.intUpdatedBy = user.intUpdatedBy;

                if (!string.IsNullOrEmpty(user.varPhoto))
                {
                    tblUser.varPhoto = Convert.FromBase64String(user.varPhoto);
                }

                db.Entry(tblUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, ex);
                throw;
            }
        }

        [HttpDelete, Route("deleteUser/{id}")]
        public HttpResponseMessage DeleteUser(int id)
        {
            try
            {
                tblUser user = db.tblUsers.Find(id);
                if (user == null)
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = 202, message = "Not Found" });

                db.tblUsers.Remove(user);
                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success" });
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

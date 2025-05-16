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
    [RoutePrefix("api/sale")]
    public class SaleController : ApiController
    {
        readonly POSEntities db = new POSEntities();
        SQLService _sqlService;
        public SaleController(SQLService sqlservice)
        {
            _sqlService = sqlservice;
        }

        #region Sale
        [HttpPost,Route("posvrnos")]
        public async Task<HttpResponseMessage> GetVrNo([FromBody] DateRequest request)
        {
            try
            {
                int[] vrnos = new int[2];
                DateTime date = request.date;
                vrnos[0] = await _sqlService.GetMaxId("tblStockMain", "intVrno", "Cash_Sale", date);
                vrnos[1] = await _sqlService.GetMaxId("tblStockMain", "intVrnoa", "Cash_Sale", date);
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Sale added successfully", data = vrnos });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { status = 500, message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpPost, Route("addSale")]
        public HttpResponseMessage AddSale([FromBody] StockMain sale)
        {
            if (sale != null)
            {
                try
                {
                    tblStockMain stockMain = new tblStockMain
                    {
                        intCompanyId = sale.intCompanyId,
                        intPartyId = sale.intPartyId,
                        intVrno = sale.intVrno,
                        intVrnoA = sale.intVrnoA,
                        dtVrDate = sale.dtVrDate,
                        varRemarks = sale.varRemarks,
                        intTransporterId = sale.intTransporterId,
                        varVrType = sale.varVrType,
                        dcDiscount = sale.dcDiscount,
                        dcDiscountAmount = sale.dcDiscountAmount,
                        dcExpense = sale.dcExpense,
                        dcAdditionalCharges = sale.dcAdditionalCharges,
                        dcNetAmount = sale.dcNetAmount,
                        dcTotalAmount = sale.dcTotalAmount,
                        dtCreationDate = DateTime.Now,
                        intCreatedBy = sale.intCreatedBy
                    };

                    db.tblStockMains.Add(stockMain);
                    db.SaveChanges();

                    foreach (var detail in sale.StockDetails)
                    {
                        tblStockDetail stockDetail = new tblStockDetail
                        {
                            intStid = stockMain.intStid,
                            intItemId = detail.intItemId,
                            intWarehouseId = detail.intWarehouseId,
                            intQuantity = detail.intQuantity,
                            dcRate = detail.dcRate,
                            dcAmount = detail.dcAmount,
                            dcDisc = detail.dcDisc,
                            dcDiscAmount = detail.dcDiscAmount,
                            dcExclTaxAmount = detail.dcExclTaxAmount,
                            dcTax = detail.dcTax,
                            dcTaxAmount = detail.dcTaxAmount,
                            dcInclTaxAmount = detail.dcInclTaxAmount,
                            varType = detail.varType,
                            dcPurRate = detail.dcPurRate,
                            dtCreationDate = DateTime.Now,
                            intCreatedBy = detail.intCreatedBy
                        };

                        db.tblStockDetails.Add(stockDetail);
                    }

                    db.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Sale added successfully" });
                }
                catch (Exception ex)
                {
                    return Request.CreateResponse(HttpStatusCode.InternalServerError, new { status = 500, message = "Internal Server Error", error = ex.Message });
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { status = 400, message = "Invalid sale data" });
            }
        }

        [HttpPut, Route("updateSale")]
        public HttpResponseMessage UpdateSale([FromBody] StockMain sale)
        {
            try
            {
                tblStockMain stockMain = db.tblStockMains.Find(sale.intStid);
                if (stockMain == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { status = 404, message = "Sale not found" });
                }

                stockMain.intCompanyId = sale.intCompanyId;
                stockMain.intPartyId = sale.intPartyId;
                stockMain.intVrno = sale.intVrno;
                stockMain.intVrnoA = sale.intVrnoA;
                stockMain.dtVrDate = sale.dtVrDate;
                stockMain.varRemarks = sale.varRemarks;
                stockMain.intTransporterId = sale.intTransporterId;
                stockMain.varVrType = sale.varVrType;
                stockMain.dcDiscount = sale.dcDiscount;
                stockMain.dcDiscountAmount = sale.dcDiscountAmount;
                stockMain.dcExpense = sale.dcExpense;
                stockMain.dcAdditionalCharges = sale.dcAdditionalCharges;
                stockMain.dcNetAmount = sale.dcNetAmount;
                stockMain.dtUpdationDate = DateTime.Now;
                stockMain.intUpdatedBy = sale.intUpdatedBy;
                stockMain.dcTotalAmount = sale.dcTotalAmount;

                db.Entry(stockMain).State = System.Data.Entity.EntityState.Modified;

                var existingDetails = db.tblStockDetails.Where(d => d.intStid == sale.intStid).ToList();
                db.tblStockDetails.RemoveRange(existingDetails);

                foreach (var detail in sale.StockDetails)
                {
                    tblStockDetail stockDetail = new tblStockDetail
                    {
                        intStid = stockMain.intStid,
                        intItemId = detail.intItemId,
                        intWarehouseId = detail.intWarehouseId,
                        intQuantity = detail.intQuantity,
                        dcRate = detail.dcRate,
                        dcAmount = detail.dcAmount,
                        dcDisc = detail.dcDisc,
                        dcDiscAmount = detail.dcDiscAmount,
                        dcExclTaxAmount = detail.dcExclTaxAmount,
                        dcTax = detail.dcTax,
                        dcTaxAmount = detail.dcTaxAmount,
                        dcInclTaxAmount = detail.dcInclTaxAmount,
                        varType = detail.varType,
                        dcPurRate = detail.dcPurRate,
                        dtCreationDate = DateTime.Now,
                        intCreatedBy = detail.intCreatedBy
                    };

                    db.tblStockDetails.Add(stockDetail);
                }

                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Sale updated successfully" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { status = 500, message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpDelete, Route("deleteSale/{id}")]
        public HttpResponseMessage DeleteSale(int id)
        {
            try
            {
                tblStockMain stockMain = db.tblStockMains.Find(id);
                if (stockMain == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { status = 404, message = "Sale not found" });
                }

                var stockDetails = db.tblStockDetails.Where(d => d.intStid == id).ToList();
                db.tblStockDetails.RemoveRange(stockDetails);
                db.tblStockMains.Remove(stockMain);

                db.SaveChanges();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Sale deleted successfully" });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { status = 500, message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet, Route("getSales")]
        public HttpResponseMessage GetSales()
        {
            try
            {
                var sales = db.tblStockMains.Select(s => new StockMain
                {
                    intStid = s.intStid,
                    intCompanyId = s.intCompanyId,
                    intPartyId = s.intPartyId,
                    intVrno = s.intVrno,
                    intVrnoA = s.intVrnoA,
                    dtVrDate = s.dtVrDate,
                    varRemarks = s.varRemarks,
                    intTransporterId = s.intTransporterId,
                    varVrType = s.varVrType,
                    dcDiscount = s.dcDiscount,
                    dcDiscountAmount = s.dcDiscountAmount,
                    dcExpense = s.dcExpense,
                    dcAdditionalCharges = s.dcAdditionalCharges,
                    dcNetAmount = s.dcNetAmount,
                    dtCreationDate = s.dtCreationDate,
                    dtUpdationDate = s.dtUpdationDate,
                    intCreatedBy = s.intCreatedBy,
                    intUpdatedBy = s.intUpdatedBy,
                    dcTotalAmount = s.dcTotalAmount,
                    StockDetails = s.tblStockDetails.Select(d => new StockDetail
                    {
                        intStockDetailId = d.intStockDetailId,
                        intStid = d.intStid,
                        intItemId = d.intItemId,
                        intWarehouseId = d.intWarehouseId,
                        intQuantity = d.intQuantity,
                        dcRate = d.dcRate,
                        dcAmount = d.dcAmount,
                        dcDisc = d.dcDisc,
                        dcDiscAmount = d.dcDiscAmount,
                        dcExclTaxAmount = d.dcExclTaxAmount,
                        dcTax = d.dcTax,
                        dcTaxAmount = d.dcTaxAmount,
                        dcInclTaxAmount = d.dcInclTaxAmount,
                        varType = d.varType,
                        dcPurRate = d.dcPurRate,
                        dtCreationDate = d.dtCreationDate,
                        dtUpdationDate = d.dtUpdationDate,
                        intCreatedBy = d.intCreatedBy,
                        intUpdatedBy = d.intUpdatedBy
                    }).ToList()
                }).ToList();
                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success", sales });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { status = 500, message = "Internal Server Error", error = ex.Message });
            }
        }

        [HttpGet, Route("getSaleById/{id}")]
        public HttpResponseMessage GetSaleById(int id)
        {
            try
            {
                var sale = db.tblStockMains.Where(s => s.intStid == id).Select(s => new StockMain
                {
                    intStid = s.intStid,
                    intCompanyId = s.intCompanyId,
                    intPartyId = s.intPartyId,
                    intVrno = s.intVrno,
                    intVrnoA = s.intVrnoA,
                    dtVrDate = s.dtVrDate,
                    varRemarks = s.varRemarks,
                    intTransporterId = s.intTransporterId,
                    varVrType = s.varVrType,
                    dcDiscount = s.dcDiscount,
                    dcDiscountAmount = s.dcDiscountAmount,
                    dcExpense = s.dcExpense,
                    dcAdditionalCharges = s.dcAdditionalCharges,
                    dcNetAmount = s.dcNetAmount,
                    dtCreationDate = s.dtCreationDate,
                    dtUpdationDate = s.dtUpdationDate,
                    intCreatedBy = s.intCreatedBy,
                    intUpdatedBy = s.intUpdatedBy,
                    dcTotalAmount = s.dcTotalAmount,
                    StockDetails = s.tblStockDetails.Select(d => new StockDetail
                    {
                        intStockDetailId = d.intStockDetailId,
                        intStid = d.intStid,
                        intItemId = d.intItemId,
                        intWarehouseId = d.intWarehouseId,
                        intQuantity = d.intQuantity,
                        dcRate = d.dcRate,
                        dcAmount = d.dcAmount,
                        dcDisc = d.dcDisc,
                        dcDiscAmount = d.dcDiscAmount,
                        dcExclTaxAmount = d.dcExclTaxAmount,
                        dcTax = d.dcTax,
                        dcTaxAmount = d.dcTaxAmount,
                        dcInclTaxAmount = d.dcInclTaxAmount,
                        varType = d.varType,
                        dcPurRate = d.dcPurRate,
                        dtCreationDate = d.dtCreationDate,
                        dtUpdationDate = d.dtUpdationDate,
                        intCreatedBy = d.intCreatedBy,
                        intUpdatedBy = d.intUpdatedBy
                    }).ToList()
                }).FirstOrDefault();

                if (sale == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, new { status = 404, message = "Sale not found" });
                }

                return Request.CreateResponse(HttpStatusCode.OK, new { status = 200, message = "Success", sale });
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.InternalServerError, new { status = 500, message = "Internal Server Error", error = ex.Message });
            }
        }
        #endregion

    }
    public class DateRequest
    {
        public DateTime date { get; set; }
    }
}

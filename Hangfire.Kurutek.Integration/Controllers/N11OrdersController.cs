using Hangfire.Kurutek.Integration.n11service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hangfire.Kurutek.Integration.Controllers
{
    public static class N11OrdersController
    {
        public static void N11Orders()
        {
            KurutekModel db = new KurutekModel();
            String strAppKey = "0e4767b6-67d3-45a9-84d0-5c366f1a308a";
            String strAppSecret = "FFJWra0dgMbBnbHS";
            String strStartDate = DateTime.Now.AddDays(-7).ToString("dd.MM.yyyy");
            String strEndDate = DateTime.Now.AddDays(7).ToString("dd.MM.yyyy");
            String strOrderStatus = "1";
            String strRecipient = "";
            String strBuyerName = "";
            String strOrderNumber = "";
            String strProductSellerCode = "";
            long productIdValue = 0;
            long totalCountValue = 10;
            int currentPageValue = 0;
            int pageCountValue = 1;
            int pageSizeValue = 10;

            Authentication authentication = new Authentication();
            authentication.appKey = strAppKey;
            authentication.appSecret = strAppSecret;

            OrderSearchPeriod orderSearchPeriod = new OrderSearchPeriod();
            orderSearchPeriod.startDate = strStartDate;
            orderSearchPeriod.endDate = strEndDate;

            OrderDataListRequest orderDataListRequest = new OrderDataListRequest();
            orderDataListRequest.productSellerCode = strProductSellerCode;
            orderDataListRequest.recipient = strRecipient;
            orderDataListRequest.period = orderSearchPeriod;
            orderDataListRequest.buyerName = strBuyerName;
            orderDataListRequest.productId = productIdValue;
            orderDataListRequest.orderNumber = strOrderNumber;
            orderDataListRequest.status = strOrderStatus;

            PagingData pagingData = new PagingData();
            pagingData.currentPage = currentPageValue;
            pagingData.pageCount = pageCountValue;
            pagingData.pageSize = pageSizeValue;
            pagingData.totalCount = totalCountValue;

            DetailedOrderListRequest request = new DetailedOrderListRequest();
            request.auth = authentication;
            request.pagingData = pagingData;
            request.searchData = orderDataListRequest;

            OrderServicePortService port = new OrderServicePortService();
            DetailedOrderListResponse response = port.DetailedOrderList(request);
            var data = response.orderList.ToList().OrderByDescending(x => x.id);
            var ordersdata = db.Orders.ToList().Where(x => x.marketplace == 1).OrderByDescending(x => x.id).FirstOrDefault().id;
            var convertlastdata = data.OrderByDescending(x => x.id).FirstOrDefault().id;
            if (convertlastdata != ordersdata)
            {
                foreach (var item in data)
                {
                    OrderDataRequest orderDataRequest = new OrderDataRequest();
                    orderDataRequest.id = item.id;

                    OrderDetailRequest request2 = new OrderDetailRequest();
                    request2.auth = authentication;
                    request2.orderRequest = orderDataRequest;

                    OrderServicePortService port2 = new OrderServicePortService();
                    OrderDetailResponse orderDetailResponse = port.OrderDetail(request2);
                    if (item.id != ordersdata)
                    {
                        Orders o = new Orders();
                        o.id = Convert.ToInt32(item.id);
                        o.marketplace = 1;
                        o.orderDate = Convert.ToDateTime(orderDetailResponse.orderDetail.createDate);
                        o.orderNumber = orderDetailResponse.orderDetail.orderNumber;
                        o.customerEmail = orderDetailResponse.orderDetail.buyer.email;
                        o.tcIdentityNumber = orderDetailResponse.orderDetail.buyer.taxId;
                        o.customerFirstName = orderDetailResponse.orderDetail.buyer.fullName.Split(' ')[0];
                        o.customerLastName = orderDetailResponse.orderDetail.buyer.fullName.Split(' ')[1];
                        o.totalPrice = Convert.ToDouble(item.totalAmount);
                        o.customerId = Convert.ToInt32(orderDetailResponse.orderDetail.buyer.id);
                        o.grossAmount = Convert.ToDouble(item.totalAmount);
                        o.currencyCode = "TRY";
                        db.Orders.Add(o);
                        db.SaveChanges();
                    }
                    else
                    {
                        return;
                    }

                }
            }
        }
    }
}
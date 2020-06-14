using ClosedXML.Excel;
using DeliveryApp.Models.Data;
using DeliveryApp.Models.DTO;
using DeliveryApp.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using PFEGestionConges.Data.Repo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> repoOrder;

        public OrderService(IRepository<Order> repoOrder)
        {
            this.repoOrder = repoOrder;
        }

        public Order AddOrder(Order order)
        {
            repoOrder.Insert(order);
            return order;
        }


        public Order DeleteOrder(int id)
        {
            var order = GetOrderById(id);
            if (order != null)
            {
                repoOrder.Delete(order);
            }
            return order;
        }

        public Order EditOrder(Order order)
        {
            repoOrder.Update(order);
            return order;
        }

        public Order GetClientPendingOrder(int clientId)
        {
            var notDeliveredOrder = repoOrder.TableNoTracking
                .Where(o => o.Status == EnumOrderStatus.Pending && o.IdClient == clientId)
                .FirstOrDefault();

            return notDeliveredOrder;
        }

        public IEnumerable<Order> GetClientTreatedOrders(int clientId)
        {
            var treatedOrders = repoOrder.TableNoTracking
                .Where(o => o.IdClient == clientId && o.Status == EnumOrderStatus.Delivered)
                .ToList();

            return treatedOrders;
        }


        public Order GetOrderById(int id)
        {
            var order = repoOrder.TableNoTracking.Where(o => o.Id == id).FirstOrDefault();
            return order;
        }

        public int GetClientNbDeliveredProducts(int clientId)
        {
            return repoOrder.TableNoTracking
                .Where(o => o.IdClient == clientId && o.Status == EnumOrderStatus.Delivered)
                .Count();
        }

        public IEnumerable<Order> GetClientOrders(int clientId)
        {
            var orders = repoOrder.TableNoTracking
                .Where(o => o.IdClient == clientId)
                .OrderByDescending(o => o.OrderTime)
                .ToList();

            return orders;
        }

        public IEnumerable<Order> GetAllPendingOrders()
        {
            var orders = repoOrder.TableNoTracking
                .Where(o => o.Status == EnumOrderStatus.Pending)
                .OrderByDescending(o => o.OrderTime)
                .ToList();

            return orders;
        }

        public IEnumerable<Order> GetAllDeliveredOrders()
        {
            var deliveredOrders = repoOrder.TableNoTracking
                .Where(o => o.Status == EnumOrderStatus.Delivered)
                .ToList();

            return deliveredOrders;
        }

        public IEnumerable<Order> GetAllProcessingOrders()
        {
            var processingOrders = repoOrder.TableNoTracking
                .Where(o => o.Status == EnumOrderStatus.Processing)
                .ToList();

            return processingOrders;
        }

        public IEnumerable<Order> GetAllInDeliveryOrders()
        {
            var inDeliveryOrders = repoOrder.TableNoTracking
                .Where(o => o.Status == EnumOrderStatus.InDelivery)
                .ToList();

            return inDeliveryOrders;
        }


        public FileModel ExportHistoryFile(IEnumerable<DeliveredOrderDto> deliveredOrders)
        {
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = $"Commandes livrées {DateTime.Now.ToString("dd/MM/yyyy HH:mm")}.xlsx";

            try
            {
                using (var workbook = new XLWorkbook())
                {
                    IXLWorksheet worksheet = workbook.Worksheets.Add("DeliveredOrders");
                    worksheet.ColumnWidth = 20;
                    worksheet.RightToLeft = false;

                    worksheet.Cell(1, 1).Value = "Id";
                    worksheet.Cell(1, 1).Style.Font.Bold = true;

                    worksheet.Cell(1, 2).Value = "Client";
                    worksheet.Cell(1, 2).Style.Font.Bold = true;

                    worksheet.Cell(1, 3).Value = "Heure commande";
                    worksheet.Cell(1, 3).Style.Font.Bold = true;

                    worksheet.Cell(1, 4).Value = "Livreur";
                    worksheet.Cell(1, 4).Style.Font.Bold = true;

                    worksheet.Cell(1, 5).Value = "Heure livraison";
                    worksheet.Cell(1, 5).Style.Font.Bold = true;

                    worksheet.Cell(1, 6).Value = "Prix articles";
                    worksheet.Cell(1, 6).Style.Font.Bold = true;

                    worksheet.Cell(1, 7).Value = "Prix livraison";
                    worksheet.Cell(1, 7).Style.Font.Bold = true;

                    int i = 1;
                    foreach (var order in deliveredOrders)
                    {
                        worksheet.Cell(i + 1, 1).Value = order.DeliveredOrder.Id;
                        worksheet.Cell(i + 1, 2).Value = $"{order.Client.FirstName} {order.Client.LastName}";
                        worksheet.Cell(i + 1, 3).Value = order.DeliveredOrder.OrderTime.ToString("dd/MM/yyyy HH:mm");
                        worksheet.Cell(i + 1, 4).Value = $"{order.DeliveryMan.FirstName} {order.DeliveryMan.LastName}";
                        worksheet.Cell(i + 1, 5).Value = order.DeliveryInfo.RealDeliveryTime.ToString("dd/MM/yyyy HH:mm");
                        worksheet.Cell(i + 1, 6).Value = $"{order.DeliveredOrder.OrderPrice} DT";
                        worksheet.Cell(i + 1, 7).Value = $"{order.DeliveredOrder.DeliveryPrice} DT";

                        i++;
                    }
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        var content = stream.ToArray();
                        return new FileModel { Content = content, ContentType = contentType, FileName = fileName };
                    }
                }

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

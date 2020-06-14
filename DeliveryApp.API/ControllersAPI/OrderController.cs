using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DeliveryApp.API.Models.DTO;
using DeliveryApp.API.Models.DTO.OrdersDTOs;
using DeliveryApp.Extensions;
using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;


namespace DeliveryApp.API.ControllersAPI
{
    [ApiController]
    [Route("delivery-app/orders")]
    public class OrderController : ControllerBase
    {
        private readonly IClientService clientService;
        private readonly IOrderService orderService;
        private readonly IProductOrderService productOrderService;
        private readonly IProductService productService;
        private readonly ICartProductService cartProductService;
        private readonly IDeliveryInfoService deliveryInfoService;
        private readonly IDeliveryManService deliveryManService;
        private readonly IRatingService ratingService;
        private readonly IMapper _mapper;
        private readonly IAdminService adminService;
        private readonly IEmailSenderService emailSenderService;

        public OrderController(IClientService clientService, IOrderService orderService,
            IProductOrderService productOrderService, IProductService productService,
            ICartProductService cartProductService, IDeliveryInfoService deliveryInfoService,
            IDeliveryManService deliveryManService, IRatingService ratingService, IMapper mapper,
            IAdminService adminService, IEmailSenderService emailSenderService)
        {
            this.clientService = clientService;
            this.orderService = orderService;
            this.productOrderService = productOrderService;
            this.productService = productService;
            this.cartProductService = cartProductService;
            this.deliveryInfoService = deliveryInfoService;
            this.deliveryManService = deliveryManService;
            this.ratingService = ratingService;
            _mapper = mapper;
            this.adminService = adminService;
            this.emailSenderService = emailSenderService;
        }

        [EnableCors("AllowAll")]
        [HttpPost("add")]
        public ActionResult<Order> AddNewOrder(OrderForCreationDto orderDto)
        {
            var client = clientService.GetClientById(orderDto.ClientId);
            if (client == null)
            {
                return NotFound();
            }

            //Calculate delivery price
            double deliveryPrice;
            if (orderDto.Distance >= 0 && orderDto.Distance <= 10)
            {
                deliveryPrice = 2;
            }
            else
            {
                if (orderDto.Distance > 10 && orderDto.Distance <= 25)
                {
                    deliveryPrice = 5;
                }
                else
                {
                    deliveryPrice = 10;
                }
            }

            var cartProducts = cartProductService.GetCartProducts(orderDto.ClientId);

            double totalPrice = 0;
            foreach (var prod in cartProducts)
            {
                var product = productService.GetProductById(prod.ProductId);
                if (product != null)
                {
                    //The following code line wont work if the amount can't be converted to int
                    totalPrice += Math.Floor(1000 * (product.Price * Convert.ToInt32(prod.Amount))) / 1000;
                }
            }

            var order = orderService.AddOrder(new Order
            {
                IdClient = client.Id,
                DeliveryPrice = deliveryPrice,
                OrderPrice = totalPrice,
                OrderTime = DateTime.Now,
                Status = EnumOrderStatus.Pending,
                WithBill = orderDto.WithBill
            });

            foreach (var prod in cartProducts)
            {
                //Add each product that was in the cart to the table ProductOrder
                var product = productService.GetProductById(prod.ProductId);
                if (product != null)
                {
                    var orderProduct = productOrderService.AddProduct(new ProductOrder
                    {
                        IdProduct = product.Id,
                        Amount = prod.Amount,
                        IdOrder = order.Id
                    });
                }
            }

            //Empty the client cart
            cartProductService.RemoveAllProducts(client.Id);

            //Send email to admins
            var admin = adminService.GetAdminByEmail("moez.deliverytn@gmail.com");

            if(admin != null)
            {
                var msg = "<span>Bonjour <strong>" + admin.FirstName + " " + admin.LastName + "</strong>,</span>" +
                "<br />" +
                "<p>Une nouvelle commande a été enregistrée.</p>" +
                "<p>Connectez-vous à l'application pour avoir plus de détails en cliquant <a href='https://localhost:44352/Order/PendingOrders'>ici</a></p>";

                emailSenderService.SendEmail(admin.Email, "Nouvelle commande", msg);
            }
            
            return Ok(order);
        }

        [EnableCors("AllowAll")]
        [HttpGet("notDelivered/{clientId}")]
        public ActionResult<Order> GetNotDeliveredOrders(int clientId)
        {
            var client = clientService.GetClientById(clientId);
            if (client == null)
            {
                return NotFound();
            }

            var order = orderService.GetClientPendingOrder(clientId);
            if (order == null)
            {
                return Ok(new { nbOrders = 0 });
            }
            return Ok(new { order = order, nbOrders = 1 });
        }

        [EnableCors("AllowAll")]
        [HttpGet("clients/{clientId}")]
        public ActionResult<IEnumerable<OrderInfosDto>> GetClientTreatedOrders(int clientId)
        {
            var client = clientService.GetClientById(clientId);
            if (client == null)
            {
                return NotFound();
            }

            var orders = orderService.GetClientTreatedOrders(clientId);
            var ordersInfos = new List<OrderInfosDto>();

            foreach (var order in orders)
            {
                var deliveryInfos = deliveryInfoService.GetOrderDeliveryInfo(order.Id);
                var orderDeliveryMan = deliveryManService.GetDeliveryManById(deliveryInfos.IdDeliveryMan);

                // We have to consider the situation where the deliveryMan object is null

                var orderInfo = new OrderInfosDto
                {
                    OrderId = order.Id,
                    OrderTime = order.OrderTime,
                    OrderPrice = order.OrderPrice,
                    OrderStatus = order.Status,
                    DeliveryPrice = order.DeliveryPrice,
                    DeliveryManId = orderDeliveryMan.Id,
                    DeliveryManName = $"{orderDeliveryMan.FirstName} {orderDeliveryMan.LastName}",
                    DeliveryManPicture = orderDeliveryMan.ImageBase64,
                    RealDeliveryTime = deliveryInfos.RealDeliveryTime
                };
                ordersInfos.Add(orderInfo);
            }

            return Ok(ordersInfos);
        }

        // -------------------------------------- Edited 29/04/2020 16:37 ------------------------------------

        [EnableCors("AllowAll")]
        [HttpGet("all/clients/{clientId}")]
        public ActionResult<IEnumerable<OrderInfosDto>> GetClientOrders(int clientId)
        {

            var client = clientService.GetClientById(clientId);
            if (client == null)
            {
                return NotFound();
            }

            var orders = orderService.GetClientOrders(clientId);
            var ordersInfos = new List<OrderInfosDto>();

            foreach (var order in orders)
            {
                OrderInfosDto orderInfo;
                if (order.Status == EnumOrderStatus.Pending)
                {
                    orderInfo = new OrderInfosDto
                    {
                        OrderId = order.Id,
                        OrderTime = order.OrderTime,
                        OrderPrice = order.OrderPrice,
                        OrderStatus = order.Status,
                        DeliveryPrice = order.DeliveryPrice
                    };
                }
                else
                {
                    var deliveryInfos = deliveryInfoService.GetOrderDeliveryInfo(order.Id);
                    var orderDeliveryMan = deliveryManService.GetDeliveryManById(deliveryInfos.IdDeliveryMan);

                    orderInfo = new OrderInfosDto
                    {
                        OrderId = order.Id,
                        OrderTime = order.OrderTime,
                        OrderPrice = order.OrderPrice,
                        OrderStatus = order.Status,
                        EstimatedDeliveryTime = deliveryInfos.EstimatedDeliveryTime,
                        DeliveryPrice = order.DeliveryPrice,
                        DeliveryManId = orderDeliveryMan.Id,
                        DeliveryManName = $"{orderDeliveryMan.FirstName} {orderDeliveryMan.LastName}",
                        DeliveryManPicture = orderDeliveryMan.ImageBase64,
                        RealDeliveryTime = deliveryInfos.RealDeliveryTime
                    };
                }
                ordersInfos.Add(orderInfo);

            }
            return Ok(ordersInfos);
        }


        //-----------------------------------------------------------------------------------------------------

        [EnableCors("AllowAll")]
        [HttpGet("{orderId}")]
        public ActionResult<OrderDetailsDto> GetOrderDetails(int orderId)
        {
            var order = orderService.GetOrderById(orderId);
            if (order == null)
            {
                return NotFound();
            }

            var orderProducts = productOrderService.GetOrderProducts(order);
            var products = new List<ProductForCheckout>();
            foreach (var prod in orderProducts)
            {
                var product = productService.GetProductById(prod.IdProduct);

                products.Add(new ProductForCheckout
                {
                    Id = product.Id,
                    Amount = prod.Amount,
                    ImageBase64 = product.ProductImages.FirstOrDefault().ImageBase64,
                    Name = product.Name
                });
            }

            OrderDetailsDto orderDetails;

            if (order.Status == EnumOrderStatus.Pending)
            {
                orderDetails = new OrderDetailsDto
                {
                    OrderId = order.Id,
                    OrderTime = order.OrderTime,
                    OrderPrice = order.OrderPrice,
                    OrderStatus = order.Status,
                    Products = products,
                    DeliveryPrice = order.DeliveryPrice
                };
            }
            else
            {
                var deliveryInfos = deliveryInfoService.GetOrderDeliveryInfo(order.Id);
                var orderDeliveryMan = deliveryManService.GetDeliveryManById(deliveryInfos.IdDeliveryMan);

                var deliveryManClientRating = ratingService.GetClientRatingForDeliveryMan(order.IdClient, orderDeliveryMan.Id);
                var rating = 0;
                if (deliveryManClientRating != null)
                {
                    rating = deliveryManClientRating.Rate;
                }

                orderDetails = new OrderDetailsDto
                {
                    OrderId = order.Id,
                    OrderTime = order.OrderTime,
                    OrderPrice = order.OrderPrice,
                    OrderStatus = order.Status,
                    Products = products,
                    EstimatedDeliveryTime = deliveryInfos.EstimatedDeliveryTime,
                    DeliveryPrice = order.DeliveryPrice,
                    DeliveryManId = orderDeliveryMan.Id,
                    DeliveryManName = $"{orderDeliveryMan.FirstName} {orderDeliveryMan.LastName}",
                    DeliveryManPicture = orderDeliveryMan.ImageBase64,
                    DeliveryManClientRating = rating,
                    RealDeliveryTime = deliveryInfos.RealDeliveryTime
                };
            }
            return Ok(orderDetails);

        }

        [EnableCors("AllowAll")]
        [HttpGet("deliveryMan/{idDeliveryMan}")]
        public ActionResult<IEnumerable<OrdersHistoryForDeliveryManDto>> GetOrdersHistory(int idDeliveryMan)
        {
            var deliveryMan = deliveryManService.GetDeliveryManById(idDeliveryMan);
            if (deliveryMan == null)
            {
                return NotFound();
            }

            var ordersInfos = deliveryInfoService.GetDeliveryManOrderHistory(idDeliveryMan);
            var list = new List<OrdersHistoryForDeliveryManDto>();

            foreach (var info in ordersInfos)
            {
                var order = orderService.GetOrderById(info.IdOrder);

                if (order.Status == EnumOrderStatus.Delivered)
                {
                    var orderClient = clientService.GetClientById(order.IdClient);

                    list.Add(new OrdersHistoryForDeliveryManDto
                    {
                        OrderId = order.Id,
                        OrderTime = order.OrderTime,
                        RealDeliveryTime = info.RealDeliveryTime,
                        EstimatedDeliveryTime = info.EstimatedDeliveryTime,
                        OrderPrice = order.OrderPrice,
                        DeliveryPrice = order.DeliveryPrice,
                        ClientId = orderClient.Id,
                        ClientName = $"{orderClient.FirstName} {orderClient.LastName}",
                        ClientPicture = orderClient.ImageBase64
                    });
                }
            }

            return Ok(list);
        }


        [EnableCors("AllowAll")]
        [HttpGet("history/{orderId}")]
        public ActionResult<HistoryOrderDetailsForDeliveryManDto> GetHistoryOrderDetails(int orderId)
        {
            var order = orderService.GetOrderById(orderId);
            if (order == null)
            {
                return NotFound();
            }

            var orderProducts = productOrderService.GetOrderProducts(order);
            var products = new List<ProductForCheckout>();
            foreach (var prod in orderProducts)
            {
                var product = productService.GetProductById(prod.IdProduct);

                products.Add(new ProductForCheckout
                {
                    Id = product.Id,
                    Amount = prod.Amount,
                    ImageBase64 = product.ProductImages.FirstOrDefault().ImageBase64,
                    Name = product.Name
                });
            }


            var deliveryInfos = deliveryInfoService.GetOrderDeliveryInfo(order.Id);
            var client = clientService.GetClientById(order.IdClient);


            var orderDetails = new HistoryOrderDetailsForDeliveryManDto
            {
                OrderId = order.Id,
                OrderTime = order.OrderTime,
                OrderPrice = order.OrderPrice,
                Products = products,
                EstimatedDeliveryTime = deliveryInfos.EstimatedDeliveryTime,
                DeliveryPrice = order.DeliveryPrice,
                ClientId = client.Id,
                ClientName = $"{client.FirstName} {client.LastName}",
                ClientPicture = client.ImageBase64,
                RealDeliveryTime = deliveryInfos.RealDeliveryTime
            };

            return Ok(orderDetails);
        }

        [EnableCors("AllowAll")]
        [HttpGet("details/{orderId}")]
        public ActionResult<HistoryOrderDetailsForDeliveryManDto> GetPendingOrderDetails(int orderId)
        {
            var order = orderService.GetOrderById(orderId);
            if (order == null)
            {
                return NotFound();
            }

            var orderProducts = productOrderService.GetOrderProducts(order);
            var products = new List<ProductForProcessingOrderDetailsDto>();
            foreach (var prod in orderProducts)
            {
                var product = productService.GetProductById(prod.IdProduct);

                products.Add(new ProductForProcessingOrderDetailsDto
                {
                    Id = product.Id,
                    Amount = prod.Amount,
                    ImageBase64 = product.ProductImages.FirstOrDefault().ImageBase64,
                    Name = product.Name,
                    NotBought = prod.NotBought
                });
            }


            var client = clientService.GetClientById(order.IdClient);
            var orderDetails = new PendingOrderDetailsDto
            {
                OrderId = order.Id,
                OrderTime = order.OrderTime,
                OrderPrice = order.OrderPrice,
                Products = products,
                DeliveryPrice = order.DeliveryPrice,
                Client = _mapper.Map<ClientForPendingOrdersDto>(client)
            };

            return Ok(orderDetails);
        }

        [EnableCors("AllowAll")]
        [HttpGet("pending")]
        public ActionResult<IEnumerable<PendingOrdersForDeliveryManDto>> GetPendingOrders()
        {
            var pendingOrders = orderService.GetAllPendingOrders();

            var ordersToReturn = new List<PendingOrdersForDeliveryManDto>();
            foreach (var order in pendingOrders)
            {
                var client = _mapper.Map<ClientForPendingOrdersDto>(clientService.GetClientById(order.IdClient));
                ordersToReturn.Add(new PendingOrdersForDeliveryManDto
                {
                    Client = client,
                    OrderId = order.Id,
                    OrderTime = order.OrderTime
                });
            }

            return Ok(ordersToReturn);
        }

        [EnableCors("AllowAll")]
        [HttpGet("processing/deliveryMan/{idDeliveryMan}")]
        public ActionResult<IEnumerable<ProcessingOrderForDeliveryManDto>> GetDeliveryManProcessingOrders(int idDeliveryMan)
        {
            var deliveryMan = deliveryManService.GetDeliveryManById(idDeliveryMan);
            if (deliveryMan == null)
            {
                return NotFound();
            }

            var ordersInfos = deliveryInfoService.GetDeliveryManOrderHistory(idDeliveryMan);
            var processingOrders = new List<ProcessingOrderForDeliveryManDto>();
            foreach (var info in ordersInfos)
            {
                var order = orderService.GetOrderById(info.IdOrder);


                if (order.Status == EnumOrderStatus.Processing || order.Status == EnumOrderStatus.InDelivery)
                {
                    var client = _mapper.Map<ClientForPendingOrdersDto>(clientService.GetClientById(order.IdClient));

                    string statusString;
                    if (order.Status == EnumOrderStatus.Processing)
                    {
                        statusString = "En cours de traitement";
                    }
                    else
                    {
                        statusString = "En cours de livraison";
                    }

                    processingOrders.Add(new ProcessingOrderForDeliveryManDto
                    {
                        Id = order.Id,
                        Status = statusString,
                        OrderTime = order.OrderTime,
                        EstimatedDeliveryTime = info.EstimatedDeliveryTime,
                        Client = client
                    });
                }
            }
            return Ok(processingOrders);
        }

        [EnableCors("AllowAll")]
        [HttpGet("processing/details/{idOrder}", Name = "ProcessingOrderDetails")]
        public ActionResult<ProcessingOrderDetailsDto> GetProcessingOrderDetails(int idOrder)
        {
            var order = orderService.GetOrderById(idOrder);
            if (order == null)
            {
                return NotFound();
            }

            var orderProducts = productOrderService.GetOrderProducts(order);
            var products = new List<ProductForProcessingOrderDetailsDto>();
            foreach (var prod in orderProducts)
            {
                var product = productService.GetProductById(prod.IdProduct);

                products.Add(new ProductForProcessingOrderDetailsDto
                {
                    Id = product.Id,
                    Amount = prod.Amount,
                    ImageBase64 = product.ProductImages.FirstOrDefault().ImageBase64,
                    Name = product.Name,
                    NotBought = prod.NotBought
                });
            }

            string statusString;
            if (order.Status == EnumOrderStatus.Processing)
            {
                statusString = "En cours de traitement";
            }
            else
            {
                statusString = "En cours de livraison";
            }

            var orderDetails = new ProcessingOrderDetailsDto
            {
                Id = idOrder,
                StatusString = statusString,
                Status = order.Status,
                OrderPrice = order.OrderPrice,
                Products = products,
                Client = _mapper.Map<ClientForPendingOrdersDto>(clientService.GetClientById(order.IdClient))
            };

            return Ok(orderDetails);
        }



        [EnableCors("AllowAll")]
        [HttpGet("inDelivery/deliveryMan/{idDeliveryMan}")]
        public ActionResult<IEnumerable<ProcessingOrderForDeliveryManDto>> GetDeliveryManInDeliveryOrders(int idDeliveryMan)
        {
            var deliveryMan = deliveryManService.GetDeliveryManById(idDeliveryMan);
            if (deliveryMan == null)
            {
                return NotFound();
            }

            var ordersInfos = deliveryInfoService.GetDeliveryManOrderHistory(idDeliveryMan);
            var inDeliveryOrders = new List<ProcessingOrderForDeliveryManDto>();
            foreach (var info in ordersInfos)
            {
                var order = orderService.GetOrderById(info.IdOrder);


                if (order.Status == EnumOrderStatus.InDelivery)
                {
                    var client = _mapper.Map<ClientForPendingOrdersDto>(clientService.GetClientById(order.IdClient));

                    string statusString = statusString = "En cours de livraison";


                    inDeliveryOrders.Add(new ProcessingOrderForDeliveryManDto
                    {
                        Id = order.Id,
                        Status = statusString,
                        OrderTime = order.OrderTime,
                        EstimatedDeliveryTime = info.EstimatedDeliveryTime,
                        Client = client
                    });
                }
            }
            return Ok(inDeliveryOrders);
        }

        [EnableCors("AllowAll")]
        [HttpPost("completeDelivery")]
        public ActionResult<OrdersHistoryForDeliveryManDto> CompleteDelivery([FromBody] OrderToCompleteDeliveryDto orderToUpdate)
        {
            var order = orderService.GetOrderById(orderToUpdate.IdOrder);
            if (order == null)
            {
                return NotFound();
            }

            var deliveryInfos = deliveryInfoService.GetOrderDeliveryInfo(orderToUpdate.IdOrder);

            order.Status = EnumOrderStatus.Delivered;
            deliveryInfos.RealDeliveryTime = DateTime.Now;

            //Upload signature Image
            //Signatures
            ImageModel uploadedImage = FileUploader.Base64ToImage(orderToUpdate.SignatureImageBase64String, "Signatures");

            deliveryInfos.SignatureImageBase64 = uploadedImage.ImageBytes;
            deliveryInfos.SignatureImagePath = uploadedImage.Path;

            deliveryInfoService.EditDeliveryInfo(deliveryInfos);

            orderService.EditOrder(order);

            var client = clientService.GetClientById(order.IdClient);

            var orderToReturn = new OrdersHistoryForDeliveryManDto
            {
                OrderId = order.Id,
                OrderTime = order.OrderTime,
                ClientId = order.IdClient,
                ClientName = $"{client.FirstName} {client.LastName}",
                ClientPicture = client.ImageBase64,
                OrderPrice = order.OrderPrice,
                DeliveryPrice = order.DeliveryPrice,
                EstimatedDeliveryTime = deliveryInfos.EstimatedDeliveryTime,
                RealDeliveryTime = deliveryInfos.RealDeliveryTime
            };

            return Ok(orderToReturn);
        }

        [EnableCors("AllowAll")]
        [HttpPost("deliverOrder")]
        public ActionResult<ProcessingOrderDetailsDto> DeliverOrder([FromBody] OrderToUpdateStatusDto orderToUpdate)
        {
            var order = orderService.GetOrderById(orderToUpdate.IdOrder);
            if (order == null)
            {
                return NotFound();
            }

            double totalPrice = order.OrderPrice;
            /*
             * If there are missing products we have to delete them from orderProducts table 
             * and update the orderPrice
            */
            if (orderToUpdate.MissingProducts.Length != 0)
            {
                totalPrice = 0;
                foreach (int id in orderToUpdate.MissingProducts)
                {
                    var orderProduct = productOrderService.GetOrderProduct(orderToUpdate.IdOrder, id);
                    orderProduct.NotBought = true;
                    productOrderService.EditOrderProduct(orderProduct);
                }
            }

            var products = productOrderService.GetOrderProducts(orderService.GetOrderById(orderToUpdate.IdOrder));

            var productsToReturn = new List<ProductForProcessingOrderDetailsDto>();


            foreach (var prod in products)
            {
                var product = productService.GetProductById(prod.IdProduct);
                var notBought = true;

                if (!prod.NotBought)
                {
                    //The following code line wont work if the amount can't be converted to int
                    totalPrice += Math.Floor(1000 * (product.Price * Convert.ToInt32(prod.Amount))) / 1000;
                    notBought = false;
                }

                productsToReturn.Add(new ProductForProcessingOrderDetailsDto
                {
                    Id = product.Id,
                    Amount = prod.Amount,
                    ImageBase64 = product.ProductImages.FirstOrDefault().ImageBase64,
                    Name = product.Name,
                    NotBought = notBought
                });

            }

            //var deliveryInfos = deliveryInfoService.GetOrderDeliveryInfo(orderToUpdate.IdOrder);

            order.Status = EnumOrderStatus.InDelivery;
            order.OrderPrice = totalPrice;

            orderService.EditOrder(order);

            var statusString = "En cours de livraison";


            var client = clientService.GetClientById(order.IdClient);

            var orderToReturn = new ProcessingOrderDetailsDto
            {
                Id = order.Id,
                OrderPrice = totalPrice,
                Products = productsToReturn,
                Status = EnumOrderStatus.InDelivery,
                StatusString = statusString,
                Client = _mapper.Map<ClientForPendingOrdersDto>(client)
            };

            return Ok(orderToReturn);
        }

        [EnableCors("AllowAll")]
        [HttpPost("acceptOrder")]
        public ActionResult<DeliveryInfo> AcceptOrderDelivery([FromBody] OrderToAcceptDto orderToAccept)
        {

            var order = orderService.GetOrderById(orderToAccept.OrderId);
            if (order == null)
            {
                return NotFound();
            }

            var deliveryMan = deliveryManService.GetDeliveryManById(orderToAccept.DeliveryManId);
            if (deliveryMan == null)
            {
                return NotFound();
            }

            var deliveryInfo = deliveryInfoService.AddDeliveryInfo(new DeliveryInfo
            {
                IdOrder = orderToAccept.OrderId,
                IdDeliveryMan = orderToAccept.DeliveryManId,
                AcceptingOrderTime = DateTime.Now,
                /*  EstimatedDeliveryTime = AcceptingOrderTime
                        + 20 minutes(approximatly sufficient time for buying articles)
                        + time to reach the destination
                */
                EstimatedDeliveryTime = DateTime.Now.AddMinutes(20 + orderToAccept.DurationToDestination)
            });

            order.Status = EnumOrderStatus.Processing;
            orderService.EditOrder(order);

            return Ok(deliveryInfo);
        }

        [EnableCors("AllowAll")]
        [HttpPost("buyProduct")]
        public ActionResult<ProcessingOrderDetailsDto> BuyProduct([FromBody] BoughtProductDto boughtProduct)
        {
            var orderProduct = productOrderService.GetOrderProduct(boughtProduct.IdOrder, boughtProduct.IdProduct);
            if (orderProduct == null)
            {
                return NotFound();
            }

            orderProduct.Amount = boughtProduct.BoughtAmount.ToString();
            orderProduct.NotBought = false;
            productOrderService.EditOrderProduct(orderProduct);


            var order = orderService.GetOrderById(boughtProduct.IdOrder);
            var products = productOrderService.GetOrderProducts(order);

            var productsToReturn = new List<ProductForProcessingOrderDetailsDto>();


            double totalPrice = 0;
            foreach (var prod in products)
            {
                var product = productService.GetProductById(prod.IdProduct);
                var notBought = true;

                if (!prod.NotBought)
                {
                    //The following code line wont work if the amount can't be converted to int
                    totalPrice += Math.Floor(1000 * (product.Price * Convert.ToInt32(prod.Amount))) / 1000;
                    notBought = false;
                }

                productsToReturn.Add(new ProductForProcessingOrderDetailsDto
                {
                    Id = product.Id,
                    Amount = prod.Amount,
                    ImageBase64 = product.ProductImages.FirstOrDefault().ImageBase64,
                    Name = product.Name,
                    NotBought = notBought
                });

            }

            order.OrderPrice = totalPrice;
            orderService.EditOrder(order);

            var statusString = "En cours de livraison";


            var client = clientService.GetClientById(order.IdClient);

            var orderToReturn = new ProcessingOrderDetailsDto
            {
                Id = order.Id,
                OrderPrice = totalPrice,
                Products = productsToReturn,
                Status = EnumOrderStatus.InDelivery,
                StatusString = statusString,
                Client = _mapper.Map<ClientForPendingOrdersDto>(client)
            };

            return Ok(orderToReturn);
        }


        [EnableCors("AllowAll")]
        [HttpPost("abortBuyingProduct")]
        public ActionResult<ProcessingOrderDetailsDto> AbortBuyingProduct([FromBody] ProductToAbortBuyingDto boughtProduct)
        {
            var orderProduct = productOrderService.GetOrderProduct(boughtProduct.IdOrder, boughtProduct.IdProduct);
            if (orderProduct == null)
            {
                return NotFound();
            }

            orderProduct.NotBought = true;
            productOrderService.EditOrderProduct(orderProduct);


            var order = orderService.GetOrderById(boughtProduct.IdOrder);
            var products = productOrderService.GetOrderProducts(order);

            var productsToReturn = new List<ProductForProcessingOrderDetailsDto>();


            double totalPrice = 0;
            foreach (var prod in products)
            {
                var product = productService.GetProductById(prod.IdProduct);
                var notBought = true;

                if (!prod.NotBought)
                {
                    //The following code line wont work if the amount can't be converted to int
                    totalPrice += Math.Floor(1000 * (product.Price * Convert.ToInt32(prod.Amount))) / 1000;
                    notBought = false;
                }

                productsToReturn.Add(new ProductForProcessingOrderDetailsDto
                {
                    Id = product.Id,
                    Amount = prod.Amount,
                    ImageBase64 = product.ProductImages.FirstOrDefault().ImageBase64,
                    Name = product.Name,
                    NotBought = notBought
                });

            }

            order.OrderPrice = totalPrice;
            orderService.EditOrder(order);

            var statusString = "En cours de livraison";


            var client = clientService.GetClientById(order.IdClient);

            var orderToReturn = new ProcessingOrderDetailsDto
            {
                Id = order.Id,
                OrderPrice = totalPrice,
                Products = productsToReturn,
                Status = EnumOrderStatus.InDelivery,
                StatusString = statusString,
                Client = _mapper.Map<ClientForPendingOrdersDto>(client)
            };

            return Ok(orderToReturn);
        }

        [EnableCors("AllowAll")]
        [HttpPost("cancelOrder")]
        public ActionResult<int> CancelPendingOrder([FromBody] OrderToCancel orderToCancel)
        {
            var order = orderService.GetOrderById(orderToCancel.OrderId);
            if (order == null)
            {
                return NotFound();
            }

            var orderProducts = productOrderService.GetOrderProducts(order);
            foreach (var product in orderProducts)
            {
                productOrderService.DeleteProduct(orderToCancel.OrderId, product.IdProduct);
            }

            orderService.DeleteOrder(orderToCancel.OrderId);

            return Ok(new { OrderId = orderToCancel.OrderId });
        }
    }
}

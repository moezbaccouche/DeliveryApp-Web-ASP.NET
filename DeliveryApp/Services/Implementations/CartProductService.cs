using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using PFEGestionConges.Data.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Implementations
{
    public class CartProductService : ICartProductService
    {
        private readonly IRepository<CartProduct> repoCartProduct;

        public CartProductService(IRepository<CartProduct> repoCartProduct)
        {
            this.repoCartProduct = repoCartProduct;
        }

        public CartProduct AddProduct(CartProduct newCartProduct)
        {
            if(newCartProduct != null)
            {
                repoCartProduct.Insert(newCartProduct);
            }
            return newCartProduct;
        }

        public CartProduct EditProduct(CartProduct newCartProduct)
        {
            if(newCartProduct != null)
            {
                repoCartProduct.Update(newCartProduct);
            }
            return newCartProduct;
        }

        public IEnumerable<CartProduct> GetCartProducts(int userId)
        {
            var cartProducts = (from cp in repoCartProduct.TableNoTracking
                                where cp.ClientId == userId
                                orderby cp.Amount
                                select cp)
                                .ToList();
            return cartProducts;
        }

        public CartProduct GetProduct(int clientId, int productId)
        {
            var product = (from cp in repoCartProduct.TableNoTracking
                           where cp.ClientId == clientId && cp.ProductId == productId
                           select cp)
                           .FirstOrDefault();

            /*  if product != null 
             *  this will return the object product, which means that the user cart does already have the added product
             *  else it will return null
             */

            return product;
        }

        public IEnumerable<CartProduct> RemoveAllProducts(int userId)
        {
            var cartProducts = (from cp in repoCartProduct.TableNoTracking
                               where cp.ClientId == userId
                               select cp)
                                .ToList();

            foreach(var prod in cartProducts)
            {
                repoCartProduct.Delete(prod);
            }
            return cartProducts;
        }

        public CartProduct RemoveProduct(CartProduct cartProduct)
        {
            if(cartProduct != null)
            {
                repoCartProduct.Delete(cartProduct);
            }
            return cartProduct;
        }
    }
}

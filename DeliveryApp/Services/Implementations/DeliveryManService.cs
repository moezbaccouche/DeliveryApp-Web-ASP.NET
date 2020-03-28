﻿using DeliveryApp.Models.Data;
using DeliveryApp.Services.Contracts;
using Microsoft.EntityFrameworkCore;
using PFEGestionConges.Data.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DeliveryApp.Services.Implementations
{

    public class DeliveryManService : IDeliveryManService
    {
        private readonly IRepository<DeliveryMan> repoDeliveryMan;

        public DeliveryManService(IRepository<DeliveryMan> repoDeliveryMan)
        {
            this.repoDeliveryMan = repoDeliveryMan;
        }

        public DeliveryMan AddDeliveryMan(DeliveryMan newDeliveryMan)
        {
            repoDeliveryMan.Insert(newDeliveryMan);
            return newDeliveryMan;
        }

        public DeliveryMan DeleteDeliveryMan(int id)
        {
            var deliveryMan = repoDeliveryMan.TableNoTracking.Where(d => d.Id == id).FirstOrDefault();
            repoDeliveryMan.Delete(deliveryMan);
            return deliveryMan;
        }

        public DeliveryMan EditDeliveryMan(DeliveryMan editedDeliveryMan)
        {
            repoDeliveryMan.Update(editedDeliveryMan);
            return editedDeliveryMan;
        }

        public IEnumerable<DeliveryMan> GetAllAvailableDeliveryMen()
        {
            var availableDeliveryMen = repoDeliveryMan.TableNoTracking.Where(d => d.IsAvailable == true).ToList();
            return availableDeliveryMen;
        }

        public IEnumerable<DeliveryMan> GetAllDeliveryMen()
        {
            var allDeliveryMen = repoDeliveryMan.TableNoTracking
                .Where(d => d.IsValidated == true)
                .Include(d => d.Location)
                .ToList();
            return allDeliveryMen;
        }

        public DeliveryMan GetDeliveryManById(int id)
        {
            var deliveryMan = repoDeliveryMan.TableNoTracking
                .Where(d => d.Id == id)
                .Include(d => d.Location)
                .FirstOrDefault();
            return deliveryMan;
        }

        public IEnumerable<DeliveryMan> GetNotValidatedDeliveryMen()
        {
            var allDeliveryMen = repoDeliveryMan.TableNoTracking
                .Where(d => d.IsValidated == false)
                .Include(d => d.Location)
                .ToList();
            return allDeliveryMen;
        }

        public DeliveryMan ValidateDeliveryMan(DeliveryMan deliveryMan)
        {
            deliveryMan.IsValidated = true;
            repoDeliveryMan.Update(deliveryMan);
            return deliveryMan;
        }
    }
}
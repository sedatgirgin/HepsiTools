﻿using HepsiTools.Business.Abstract;
using HepsiTools.Entities;
using HepsiTools.GenericRepositories.Concrate;

namespace HepsiTools.Business.Concrate
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
    }
}
﻿using Database.Abstract;

namespace DigestsSubDomain.Data.Entities
{
    public class EfCompany : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
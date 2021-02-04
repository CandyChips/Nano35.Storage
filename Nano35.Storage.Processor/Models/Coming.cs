using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class ComingDetail :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        
        //Data
        public double Price { get; set; }
        public int Count { get; set; }
        
        //Forgein keys
        public Guid ToWarehouseId { get; set; }
        public WarehouseByItemOnStorage ToWarehouse { get; set; }
        
        public Guid ComingId { get; set; }
        public Coming Coming { get; set; }
    }
    public class Coming :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        
        //Data
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public Guid ToUnitId { get; set; }
        
        //Forgein keys
    }
}
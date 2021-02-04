using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class CancelationDetail :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        
        //Data
        public int Count { get; set; }
        
        //Forgein keys
        public Guid FromWarehouseId { get; set; }
        public WarehouseByItemOnStorage FromWarehouse { get; set; }
        
        public Guid CancelationId { get; set; }
        public Cancelation Cancelation { get; set; }
    }
    public class Cancelation :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        
        //Data
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public Guid FromUnitId { get; set; }
        
        //Forgein keys
    }
}
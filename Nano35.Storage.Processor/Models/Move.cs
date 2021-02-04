using System;
using Microsoft.EntityFrameworkCore;
using Nano35.Contracts;

namespace Nano35.Storage.Processor.Models
{
    public class MoveDetail :
        ICastable
    {
        // Primary key
        public Guid Id { get; set; }
        
        //Data
        public int Count { get; set; }
        
        //Forgein keys
        public Guid ToWarehouseId { get; set; }
        public WarehouseByItemOnStorage ToWarehouse { get; set; }
        
        public Guid FromWarehouseId { get; set; }
        public WarehouseByItemOnStorage FromWarehouse { get; set; }
        
        public Guid MoveId { get; set; }
        public Move Move { get; set; }
    }
    public class Move :
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
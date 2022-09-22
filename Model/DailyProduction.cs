using System;

namespace DailyProduction.Model
{

    public enum BikeModel {
        undefined = 0,
        IBv1 = 1,
        evIB100 = 2,
        evIB200 = 3
    }

    /// <summary>
    /// An container entity for a single daily production of a bike model.
    /// </summary>
    public class DailyProductionDTO
    {
        
        public DateTime Date { get; set; }

        public BikeModel Model { get; set; }
        public int ItemsProduced { get; set; }

    }
}
﻿namespace WebApi.Ecommerce.Domain.Abstracts
{
    public abstract class BootstrapTable
    {
        // Constructor
        protected BootstrapTable()
        {
            Limit = 10;
            Offset = 0;
        }

        // Properties
        public int Limit { get; set; }

        public int Offset { get; set; }

        public string Sort { get; set; }

        public string Order { get; set; }

        public string Search { get; set; }
    }
}
﻿namespace BookingSystem.Domin.Specification
{
    public class BaseSpecificationPram
    {
        public int PageIndex { get; set; } = 1;
        private const int _maxPageSize = 50;
        private int _pageSize = 20;
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                _pageSize = value > _maxPageSize ? _maxPageSize : value;
            }
        }
    }
}

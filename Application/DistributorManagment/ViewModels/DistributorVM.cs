﻿using Domain.SharedKernel.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DistributorManagment.ViewModels
{
    public class DistributorVM
    {
        public string Id { get; set; }
        public string Name { get; private set; }
        public Address Address { get; set; }
    }
}
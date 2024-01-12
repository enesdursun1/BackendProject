﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Dtos.Responses.Product;

public class GetListProductResponse
{

    public int Id { get; set; }
    public int CategoryId { get; set; }   
    public string CategoryName { get; set; }
    public string Name { get; set; }
    public decimal UnitPrice { get; set; }
    public int Stock { get; set; }

}

